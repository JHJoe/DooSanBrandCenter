using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace BrandCenter.Models
{
    [Table("tblGroupUser")]
    public partial class tblGroupUser
    {
        [Key]
        public short GroupUserId { get; set; }

        public short UserId { get; set; }

        public short GroupId { get; set; }

    }
}