using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf10_1.Entities
{
    public class EFContext : DbContext
    {
        public EFContext()
            : base("DefaultConnection")
        {

        }
        public DbSet<DbUser> Users { get; set; }

    }
}
