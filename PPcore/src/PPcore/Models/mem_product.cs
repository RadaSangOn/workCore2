﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PPcore.Models
{
    public partial class mem_product
    {
        public string member_code { get; set; }
        public string product_code { get; set; }
        public decimal? grow_area { get; set; }
        [Display(Name = "ลำดับที่")]
        public int rec_no { get; set; }
        public string x_status { get; set; }
        public string x_note { get; set; }
        public string x_log { get; set; }
        public Guid id { get; set; }
        public byte[] rowversion { get; set; }
    }
}
