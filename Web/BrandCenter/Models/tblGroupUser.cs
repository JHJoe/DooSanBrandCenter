using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using FluentValidation.Attributes;
using System;

namespace BrandCenter.Models
{
 //   [Validator(typeof(Validators.GroupUserValidator))]
    [Table("tblGroupUser")]
    public partial class tblGroupUser
    {
        //key는 반드시 null허용으로 
        [Key]
        public short? GroupUserId { get; set; }

        public string UserId { get; set; }

        //[Range(1, 100)]

        public short? GroupId { get; set; }

    }

 
}