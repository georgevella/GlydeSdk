using System.Collections.Generic;
using System.Threading.Tasks;
using Glyde.Web.Api.Controllers;
using GlydeSdk.Sample.CompanionApi.Resource;
using GlydeSdk.Sample.CompanionApi.Resource.V1;

namespace GlydeSdk.Sample.CompanionApi.Controller
{
    public class CompanionApiController : ApiController<Companion, string>
    {
        public override async Task<IEnumerable<Companion>> GetAll()
        {
            return await Task.Run<IEnumerable<Companion>>(() => new List<Companion>
            {
                new Companion("Susan Foreman"),
                new Companion("Barbara Wright"),
                new Companion("Ian Chesterton"),
                new Companion("Vicki"),
                new Companion("Steven Taylor"),
                new Companion("Katarina"),
                new Companion("Sara Kingdom"),
                new Companion("Dodo Chaplet"),
                new Companion("Polly"),
                new Companion("Ben Jackson"),
                new Companion("Jamie McCrimmon"),
                new Companion("Victoria Waterfield"),
                new Companion("Zoe Heriot"),
                new Companion("Liz Shaw"),
                new Companion("Jo Grant"),
                new Companion("Sarah Jane Smith"),
                new Companion("Harry Sullivan"),
                new Companion("Leela"),
                new Companion("K9"),
                new Companion("Romana I"),
                new Companion("Romana II"),
                new Companion("Adric"),
                new Companion("Tegan Jovanka"),
                new Companion("Nyssa of Traken"),
                new Companion("Vislor Turlough"),
                new Companion("Kamelion"),
                new Companion("Perpugilliam 'Peri' Brown"),
                new Companion("Melanie Bush"),
                new Companion("Ace"),
                new Companion("Grace Holloway"),
                new Companion("Rose Tyler"),
                new Companion("Adam Mitchell"),
                new Companion("Jack Harkness"),
                new Companion("Mickey Smith"),
                new Companion("Donna Noble"),
                new Companion("Martha Jones"),
                new Companion("Astrid Peth"),
                new Companion("Wilfred Mott"),
                new Companion("Amy Pond"),
                new Companion("Rory Williams"),
                new Companion("River Song"),
                new Companion("Craig Owens"),
                new Companion("John Riddell"),
                new Companion("Queen Nefertiti of Egypt"),
                new Companion("Clara Oswald"),
                new Companion("Paternoster Gang"),
            });
        }
    }
}