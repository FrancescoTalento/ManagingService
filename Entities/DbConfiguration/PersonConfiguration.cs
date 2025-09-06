using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Entities.DbConfiguration
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("Persons");

            string data = System.IO.File.ReadAllText("persons.json");
            List<Person> list = JsonSerializer.Deserialize<List<Person>>(data,new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
            
            
            builder.HasData(list);
        }
    }
}
