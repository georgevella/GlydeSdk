using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Glyde.Web.Api.Exceptions;
using Glyde.Web.Api.Resources;
using Newtonsoft.Json;

namespace Glyde.Web.Api.Client
{
    public class ApiClient<TResource, TResourceId> : IApiClient<TResource>, IApiClient<TResource, TResourceId>
        where TResource : Resource<TResourceId>
    {
        private readonly HttpClient _client;
        private readonly Uri _resourceUri;

        public async Task<IEnumerable<TResource>> GetAll()
        {
            var response = await _client.GetAsync(_resourceUri);
            var stream = await response.Content.ReadAsStreamAsync();

            using (var reader = new JsonTextReader(new StreamReader(stream)))
            {
                return JsonSerializer.Create().Deserialize<IEnumerable<TResource>>(reader);
            }
        }

        public async Task<TResource> Create(TResource resource)
        {
            if (resource == null) throw new ArgumentNullException(nameof(resource));

            using (var memoryStream = new MemoryStream())
            using (var writer = new JsonTextWriter(new StreamWriter(memoryStream)))
            {
                JsonSerializer.Create().Serialize(writer, resource);
                writer.Flush();

                memoryStream.Position = 0;

                var content = new StreamContent(memoryStream);
                content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                var request = new HttpRequestMessage(HttpMethod.Post, _resourceUri)
                {
                    Content = content,
                };
                request.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));
                var response = await _client.SendAsync(request);

                if (response.StatusCode == HttpStatusCode.Created)
                {
                    var stream = await response.Content.ReadAsStreamAsync();

                    using (var reader = new JsonTextReader(new StreamReader(stream)))
                    {
                        return JsonSerializer.Create().Deserialize<TResource>(reader);
                    }
                }

                if (response.StatusCode == HttpStatusCode.Conflict)
                    throw new ResourceExistsException(resource.Id);

                throw new InvalidOperationException();
            }
        }

        public async Task<bool> Update(TResourceId id, TResource resource)
        {
            var updateUri = BuildUriForResourceWithId(id);

            using (var memoryStream = new MemoryStream())
            using (var writer = new JsonTextWriter(new StreamWriter(memoryStream)))
            {
                JsonSerializer.Create().Serialize(writer, resource);

                writer.Flush();
                memoryStream.Position = 0;

                var content = new StreamContent(memoryStream);
                content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                var request = new HttpRequestMessage(HttpMethod.Put, updateUri)
                {
                    Content = content,
                };
                request.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json"));

                var response = await _client.SendAsync(request);

                switch (response.StatusCode)
                {
                    case HttpStatusCode.NoContent:
                    case HttpStatusCode.NotModified:
                        return response.StatusCode == HttpStatusCode.NoContent;
                    default:
                        throw new InvalidOperationException($"Update call returned [{response.StatusCode}]");
                }
            }
        }

        public async Task<TResource> Get(TResourceId id)
        {
            var u = BuildUriForResourceWithId(id);

            var response = await _client.GetAsync(u);

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    var stream = await response.Content.ReadAsStreamAsync();

                    using (var reader = new JsonTextReader(new StreamReader(stream)))
                    {
                        return JsonSerializer.Create().Deserialize<TResource>(reader);
                    }
                case HttpStatusCode.NotFound:
                    throw new ResourceDoesNotExistException(id);

                case HttpStatusCode.MethodNotAllowed:
                    throw new UnsupportedOperationException(id);
                default:
                    throw new InvalidOperationException($"An unknown response code obtained from the get call [{response.StatusCode}]");
            }

        }

        private Uri BuildUriForResourceWithId(TResourceId id)
        {
            var baseUri = _resourceUri.ToString();
            var idPart = baseUri.EndsWith("/") ? $"{id}" : $"/{id}";
            var u = new Uri(baseUri + idPart, UriKind.Relative);
            return u;
        }

        public async Task Delete(TResourceId id)
        {
            throw new NotImplementedException();
        }

        public ApiClient(HttpClient client, Uri resourceUri)
        {
            _client = client;
            _resourceUri = resourceUri;
        }

        async Task<bool> IApiClient<TResource>.Update(object id, TResource resource)
        {
            return await Update((TResourceId)id, resource);
        }

        async Task<TResource> IApiClient<TResource>.Get(object id)
        {
            return await Get((TResourceId)id);
        }

        async Task IApiClient<TResource>.Delete(object id)
        {
            await Delete((TResourceId)id);
        }
    }

}
