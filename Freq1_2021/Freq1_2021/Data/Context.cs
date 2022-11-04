using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Freq1_2021.Models;

namespace Db_al73254.Data
{
    public class Context : DbContext
    {
        public Context (DbContextOptions<Context> options)
            : base(options)
        {
        }

        public DbSet<Freq1_2021.Models.Contact> Contact { get; set; } = default!;
    }
}
