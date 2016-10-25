namespace BrandCenter.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;


    [Table("tblGroup")]
    public partial class tblGroup
    {
        [Key]
        public short GroupId { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(150, ErrorMessage = "150글자보다 작아야합니다.")]
        [Column("Desc")]
        [Display(Name = "Description")]
        public string Descript { get; set; }

        [Display(Name = "Test")]
        public string test { get; set; }

        [Display(Name = "Test2")]
        public string test2 { get; set; }


    }
}