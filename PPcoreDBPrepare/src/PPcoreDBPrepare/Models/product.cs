using System;
using System.Collections.Generic;

namespace PPcoreDBPrepare.Models
{
    public partial class product
    {
        public product()
        {
            mem_product = new HashSet<mem_product>();
        }

        public string product_code { get; set; }
        public string product_type_code { get; set; }
        public string product_group_code { get; set; }
        public string product_desc { get; set; }
        public string x_status { get; set; }
        public string x_note { get; set; }
        public string x_log { get; set; }
        public byte[] id { get; set; }
        public byte[] rowversion { get; set; }

        public virtual ICollection<mem_product> mem_product { get; set; }
        public virtual product_type product_ { get; set; }
    }
}
