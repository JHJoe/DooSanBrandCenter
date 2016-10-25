using System;
using System.Collections.Generic;
using System.Linq;
using BrandCenter.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace BrandCenter.DAL
{
    public class BrandCenterContext : DbContext
    {
        //public SchoolContext() : base("BrandCenterContext")
        //{
        //}
        public DbSet<tblGroup> tblGroup { get; set; }
        //상속받은넘
        //public DbSet<tblGroup> Groups { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
           // modelBuilder.Entity<Group>().ToTable("tblGroup");
            //modelBuilder.Entity<Group>().MapToStoredProcedures();
        }
    }
}