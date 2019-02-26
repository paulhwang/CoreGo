using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Phwang.Models
{
    public class DbContextClass : DbContext
    {
        public DbContextClass() //: base("name=GoDatabase")
        {
        }
        public virtual DbSet<AccountTableClass> Accounts { get; set; }

        protected void onModelCreating(ModelBuilder model_builder_val)
        {
            var account_table = model_builder_val.Entity<AccountTableClass>().ToTable("phwang");

            account_table.Property(c => c.Id)
                .IsRequired()
                .HasColumnType("bigint");

            base.OnModelCreating(model_builder_val);
            

        }
    }
}
