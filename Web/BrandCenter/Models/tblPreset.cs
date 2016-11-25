using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrandCenter.Models
{
    public class tblPreset
    {
        public int Preset_Idx { get; set; }
        public string TypeCode { get; set; }
        public string SizeCode { get; set; }
        public string CaseText { get; set; }
        public string PositionValue { get; set; }
        public Nullable<int> DefaultHeight { get; set; }
        public Nullable<int> DefaultWidth { get; set; }
        public Nullable<int> MaxHeight { get; set; }
        public Nullable<int> MaxWidth { get; set; }
        public Nullable<int> ImageFileIdx { get; set; }
        public Nullable<bool> IsHeaderExist { get; set; }
        public Nullable<int> HeadLineCount { get; set; }
        public Nullable<bool> IsFooterExist { get; set; }
        public string INPUT_USER { get; set; }
        public Nullable<System.DateTime> INPUT_DATE { get; set; }
        public string UPDATE_USER { get; set; }
        public Nullable<System.DateTime> UPDATE_DATE { get; set; }
        public Nullable<int> PresetText_Idx { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblPresetTextBox> tblPresetTextBox { get; set; }
    }
}