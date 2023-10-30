using Microsoft.AspNetCore.Mvc;
using DataGeneratorApi.Models;
using DataGeneratorApi.Data;

namespace DataGeneratorApi.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class DataGeneratorController : ControllerBase {
        private readonly ApiContext _context;

        public DataGeneratorController(ApiContext context) {
               _context = context;
        }
        [HttpGet]
        public IActionResult GetSQLScript(string tableName) {
            var result = _context.Tables
        }
        [HttpGet]
        [Route("GenerateSqlScript")]
        public ActionResult<string> GenerateSqlScript(string tableName) {
            // Hier sollten Sie Logik zum Generieren des SQL-Skripts für die angegebene Tabelle implementieren
            // Verwenden Sie die Informationen aus Ihrem Datenbankkontext (_context) und dem Tabellennamen
            string sqlScript = GenerateSqlScriptForTable(tableName);
            return sqlScript;
        }
    }
}
