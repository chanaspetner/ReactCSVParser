using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactCSVParser.Data
{
    public class PeopleRepository
    {
        private readonly string _connectionString;

        public PeopleRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public string GetCsv(int amount)
        {
            var builder = new StringBuilder();
            var stringWriter = new StringWriter(builder);
            using var csv = new CsvWriter(stringWriter, CultureInfo.InvariantCulture);
            var people = GetPeople(amount);
            csv.WriteRecords(people);
            return builder.ToString();
        }

        public List<Person> GetPeople(int amount)
        {
            List<Person> result = new();
            for (int i = 1; i <= amount; i++)
            {
                result.Add(new Person
                {
                    FirstName = Faker.Name.First(),
                    LastName = Faker.Name.Last(),
                    Age = Faker.RandomNumber.Next(20, 100),
                    Address = Faker.Address.UsState(),
                    Email = Faker.Internet.Email()

                }) ;
            }

            return result;
        }

        public void GetFromCsvBytes(byte[] csvBytes)
        {
            using var memoryStream = new MemoryStream(csvBytes);
            var streamReader = new StreamReader(memoryStream);
            using var reader = new CsvReader(streamReader, CultureInfo.InvariantCulture);
            AddPeople(reader.GetRecords<Person>().ToList());
        }

        public void AddPeople(List<Person> people)
        {
            using var ctx = new PeopleDataContext(_connectionString);
            ctx.People.AddRange(people);
            ctx.SaveChanges();
        }

        public List<Person> GetPeople()
        {
            using var ctx = new PeopleDataContext(_connectionString);
            return ctx.People.ToList();
        }

        public void DeleteAll()
        {
            using var ctx = new PeopleDataContext(_connectionString);
            var people = ctx.People;
            ctx.People.RemoveRange(people);
            ctx.SaveChanges();
        }

    }
}
