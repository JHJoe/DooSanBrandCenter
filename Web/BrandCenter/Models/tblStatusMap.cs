using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BrandCenter.Models
{
    public partial class tblStatusGroupMapp
    {
        [Column(Order = 0), System.ComponentModel.DataAnnotations.Key]
        public string StatusGroupCode { get; set; }
        [Column(Order = 1), System.ComponentModel.DataAnnotations.Key]
        public string StatusCode { get; set; }
    }
}