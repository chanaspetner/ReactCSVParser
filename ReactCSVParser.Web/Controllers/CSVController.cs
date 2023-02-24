using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReactCSVParser.Data;
using Microsoft.Extensions.Configuration;
using System.Text;
using ReactCSVParser.Web.Model;

namespace ReactCSVParser.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CSVController : ControllerBase
    {
        private string _connectionString;
        public CSVController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }

        [HttpPost]
        [Route("upload")]
        public void Upload(UploadViewModel vm)
        {
            int index = vm.Base64Csv.IndexOf(",") + 1;
            string base64 = vm.Base64Csv.Substring(index);
            byte[] csvBytes = Convert.FromBase64String(base64);
            var repo = new PeopleRepository(_connectionString);
            repo.GetFromCsvBytes(csvBytes);
            System.IO.File.WriteAllBytes($"uploads/{vm.Name}", csvBytes);
        }

        [HttpGet]
        [Route("getpeople")]
        public List<Person> GetPeople()
        {
            var repo = new PeopleRepository(_connectionString);
            return repo.GetPeople();
        }

        [HttpPost]
        [Route("deleteall")]
        public void DeleteAll()
        {
            var repo = new PeopleRepository(_connectionString);
            repo.DeleteAll();
        }



    }
}
