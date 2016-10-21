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

        [StringLength(150, ErrorMessage = "150���ں��� �۾ƾ��մϴ�.")]
        [Column("Desc")]
        [Display(Name = "Description")]
        public string Descript { get; set; }
    }
}
