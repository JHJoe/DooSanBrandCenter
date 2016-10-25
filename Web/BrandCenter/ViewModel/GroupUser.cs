using BrandCenter.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace BrandCenter.ViewModels
{

    public class GroupUser 
    {
        public short GROUPID { get; set; }
        public string NAME { get; set; }
        public short USERID { get; set; }
        public short GroupUserId { get; set; }

    }
}