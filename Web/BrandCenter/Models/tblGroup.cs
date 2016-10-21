namespace BrandCenter.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tblGroup")]
    public partial class tblGroup
    {
        public short Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(150, ErrorMessage = "150글자보다 작아야합니다.")]
        [Column("Desc")]
        [Display(Name = "Description")]
        public string Descript { get; set; }
    }
}
