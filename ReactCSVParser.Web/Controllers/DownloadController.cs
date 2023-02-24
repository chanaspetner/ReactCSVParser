using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ReactCSVParser.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactCSVParser.Web.Controllers
{
    public class DownloadController : Controller
    {
        private string _connectionString;
        public DownloadController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }
        public IActionResult GenerateCsv(string stringAmount)
        {
            var amount = int.Parse(stringAmount);
            var repo = new PeopleRepository(_connectionString);
            var csv = repo.GetCsv(amount);
            byte[] bytes = Encoding.UTF8.GetBytes(csv);
            return File(bytes, "text/csv", "people.csv");
        }
    }
}
