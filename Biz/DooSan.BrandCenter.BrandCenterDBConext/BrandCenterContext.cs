using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Data.Entity.ModelConfiguration.Conventions;
using DooSan.BrandCenter.BrandCenterDBConext;

namespace BrandCenter.DAL
{
    public class BrandCenterContext : DbContext
    {
        //public SchoolContext() : base("BrandCenterContext")
        //{
        //}
        public virtual DbSet<tblGroup> tblGroup { get; set; }
        //상속받은넘
        //public DbSet<tblGroup> Groups { get; set; }
        public virtual DbSet<tblGroupUser> tblGroupUser { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
           // modelBuilder.Entity<Group>().ToTable("tblGroup");
            //modelBuilder.Entity<Group>().MapToStoredProcedures();
        }
    }
}