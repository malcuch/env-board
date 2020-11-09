using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace EnvBoard.Web.Pages
{
    using System.IO;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Model;
    using Newtonsoft.Json;

    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly Data _data;

        public State State { get; private set; }



        public IndexModel(ILogger<IndexModel> logger, IWebHostEnvironment env)
        {
            _logger = logger;
            _data = new Data(Path.Combine(env.ContentRootPath, "App_Data", "state.json"));
            State = _data.LoadState();
        }

        public void OnGet()
        {
        }

        public  async Task<ActionResult> OnPatch()
        {
            using var reader = new StreamReader(Request.Body);

            var environment = JsonConvert.DeserializeObject<Environment>(await reader.ReadToEndAsync());
            State.UpdateEnvironment(environment);
            _data.SaveState(State);

            return new EmptyResult();
        }
    }
}
