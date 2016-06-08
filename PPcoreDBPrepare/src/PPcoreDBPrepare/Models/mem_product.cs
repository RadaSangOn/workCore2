using System;
using System.Collections.Generic;

namespace PPcoreDBPrepare.Models
{
    public partial class mem_product
    {
        public string member_code { get; set; }
        public string product_code { get; set; }
        public decimal? grow_area { get; set; }
        public string x_status { get; set; }
        public string x_note { get; set; }
        public string x_log { get; set; }
        public byte[] id { get; set; }
        public byte[] rowversion { get; set; }

        public virtual member member_codeNavigation { get; set; }
        public virtual product product_codeNavigation { get; set; }
    }
}
