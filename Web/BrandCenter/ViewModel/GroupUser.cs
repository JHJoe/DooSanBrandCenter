﻿using BrandCenter.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using FluentValidation.Attributes;
using System;

namespace BrandCenter.ViewModels
{
    //일단 fluent는 쓰지 않는걸로
    //[Validator(typeof(Validators.GroupUserValidator))]

    public class GroupUser 
    {
        public short? GROUPID { get; set; }
        //[errorStringValidate]
        public string NAME { get; set; }
        [Range(1, 1000)]
        public short USERID { get; set; }
        public short? GroupUserId { get; set; }

        public string JobId { get; set; }

    }

    //커스텀 validate         //이건 서버사이드에서 post 이후 내려주는 에러이다.   //join 한 어트리뷰트를 타고 온다.
    public class errorStringValidate : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (Convert.ToString(value) == "error")
            {
                string Message = "error 라고 치면 안되죠";
                return new ValidationResult(Message);
            }
            else
                return ValidationResult.Success;
        }


    }

}