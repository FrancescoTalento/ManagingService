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
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.ToTable("Countries");

            string data = System.IO.File.ReadAllText("countries.json");
            List<Country> list = JsonSerializer.Deserialize<List<Country>>(data, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            })!;
            
            builder.HasData(list);
        }
    }
}
