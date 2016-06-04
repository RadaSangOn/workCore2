using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PPcore.Models
{
    public partial class album
    {
        public string album_code { get; set; }

        [Display(Name = "ชื่ออัลบั้ม")]
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        public string album_name { get; set; }

        [Display(Name = "คำอธิบาย")]
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        public string album_desc { get; set; }

        [Display(Name = "สร้างโดย")]
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        public string created_by { get; set; }

        [Display(Name = "วันที่")]
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        public DateTime created_date { get; set; }

        public string x_status { get; set; }
        public string x_note { get; set; }
        public string x_log { get; set; }
        public Guid id { get; set; }
        public byte[] rowversion { get; set; }
    }
}
