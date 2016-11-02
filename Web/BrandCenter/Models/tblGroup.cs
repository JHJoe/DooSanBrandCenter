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
        [Key]
        public short? GroupId { get; set; }

        [Required]
        [GroupValidate, StringLength(10, ErrorMessage = " 10글자보다 작아야합니다.")]
        [errorStringValidate]
        //[Range(1, 100)]
        //[RequiredIfTrue("Married")]
        public string Name { get; set; }

        [errorStringValidate]
        [StringLength(150, ErrorMessage = "150글자보다 작아야합니다.")]
        [Column("Desc")]
        [Display(Name = "Description")]
        public string Descript { get; set; }

        [Display(Name = "Test")]
        public string test { get; set; }

        [Display(Name = "Test2")]
        public string test2 { get; set; }


        //이건 서버사이드에서 post 이후 내려주는 에러이다. 
        public class GroupValidate : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                string Message = string.Empty;

                if (value.ToString() == "error2")
                {
                    Message = "또 에러";
                    return new ValidationResult(Message);
                }
                return ValidationResult.Success;
            }
        }


        //커스텀 validate
        public class errorStringValidate : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                if (Convert.ToString(value) == "error")
                {
                    string Message = "error 라고 치면 안되죠2";
                    return new ValidationResult(Message);
                }
                return ValidationResult.Success;
            }
        }



    }
}