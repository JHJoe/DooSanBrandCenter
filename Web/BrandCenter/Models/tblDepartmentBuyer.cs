//------------------------------------------------------------------------------
// <auto-generated>
//     이 코드는 템플릿에서 생성되었습니다.
//
//     이 파일을 수동으로 변경하면 응용 프로그램에서 예기치 않은 동작이 발생할 수 있습니다.
//     이 파일을 수동으로 변경하면 코드가 다시 생성될 때 변경 내용을 덮어씁니다.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DooSan.BrandCenter.BrandCenterDBConext
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblDepartmentBuyer
    {
        [System.ComponentModel.DataAnnotations.Key]
        public string BUYERNO { get; set; }
        public string DEPARTMENT_CODE { get; set; }
        public string BUY_TYPE { get; set; }
        public string TYPE { get; set; }
        public string CHARGE { get; set; }
        public string BUYERNAME { get; set; }
        public string JOB { get; set; }
        public string POSITION { get; set; }
        public string TEL { get; set; }
        public string MOBILE { get; set; }
        public string BIGO { get; set; }
        public string INPUT_USER { get; set; }
        public Nullable<System.DateTime> INPUT_DATE { get; set; }
        public string UPDATE_USER { get; set; }
        public Nullable<System.DateTime> UPDATE_DATE { get; set; }
    }
}
