namespace BrandCenter.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class tblMenu
    {

        [System.ComponentModel.DataAnnotations.Key]
        public string MENUID { get; set; }

        public string MENUNAME { get; set; }
        public string DESC { get; set; }
        public Nullable<int> LEVEL { get; set; }
        [Range(1, 100)]
//        [RegularExpression("^[A-Z]+[a-zA-Z''-'\s]*$")]
        public Nullable<int> SORT { get; set; }
        public Nullable<bool> USE_YN { get; set; }
        public Nullable<bool> DEFAULT_YN { get; set; }
        public Nullable<bool> TEAM_YN { get; set; }
        public string TYPE { get; set; }
        public string PARENT_MENUID { get; set; }
        public string URL { get; set; }
        public string INPUT_USER { get; set; }
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> INPUT_DATE { get; set; }
        public string UPDATE_USER { get; set; }
        //[Range(GetType(DateTime), "1/1/1966", "1/1/2020")]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> UPDATE_DATE { get; set; }
    }


    //일자 커스텀 validation 
    public class ShippeddateValidate : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime CurrentDate = DateTime.Now;
            string Message = string.Empty;

            if (Convert.ToDateTime(value) < CurrentDate)
            {
                Message = "Shipped Date cannot be less than current date";
                return new ValidationResult(Message);
            }
            return ValidationResult.Success;
        }
    }


}
