using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrandCenter.Models
{
    public class tblPresetTextBox
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int PresetText_Idx { get; set; }
        public string TextControlType { get; set; }
        public string PositionX { get; set; }
        public string PositionY { get; set; }
        public string IsHorizon { get; set; }
        public string TextAlignment { get; set; }
        public string FontSize { get; set; }
        public Nullable<int> Preset_Idx { get; set; }

        public virtual tblPreset tblPreset { get; set; }
    }
}