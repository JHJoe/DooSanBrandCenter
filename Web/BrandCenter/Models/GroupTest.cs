namespace BrandCenter.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class GroupTest2 : DbContext
    {
        public GroupTest2()
            : base("name=GroupTest")
        {
        }

        //public virtual DbSet<tblGroup> tblGroup { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<tblGroup>()
            //    .Property(e => e.Name)
            //    .IsUnicode(false);
        }
    }
}
