using Microsoft.AspNetCore.Mvc;

namespace MonezyAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : Controller
    {
        [HttpGet]
        public object Get([FromServices]IConfiguration configuration)
        {
            return new
            {
                Local = Environment.MachineName,
                ConnString = configuration.GetSection("ConnectionStrings")["ExpensesContext"],
                DbUsername = configuration.GetSection("User")["DbUsername"],
                DbPassword = configuration.GetSection("User")["DbPassword"]
            };
        }
    }
}
