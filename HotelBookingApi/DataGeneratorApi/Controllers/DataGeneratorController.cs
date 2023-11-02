using Microsoft.AspNetCore.Mvc;
using DataGeneratorApi.Models;
using DataGeneratorApi.Data;
using DataGeneratorApi;


namespace DataGeneratorApi.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class DataGeneratorController : ControllerBase {

        public DataGeneratorController() {
        }

        [HttpGet]
        public IActionResult Get(string tableName) {
            DatabaseTableToSQLScriptConverter d = new DatabaseTableToSQLScriptConverter(@$"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\M8LN\Documents\ABSCHLUSSPROJEKT\bwater\HotelBookingApi\DataGeneratorApi\ProductionDb.mdf;Integrated Security=True");
            var result = d.CopyTableSchemeToSQLScript(tableName);
            return Ok(result);  
        }
    }
}
