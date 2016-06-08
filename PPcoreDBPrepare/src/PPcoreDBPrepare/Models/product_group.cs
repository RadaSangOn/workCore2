using System;
using System.Collections.Generic;

namespace PPcoreDBPrepare.Models
{
    public partial class product_group
    {
        public product_group()
        {
            product_type = new HashSet<product_type>();
        }

        public string product_group_code { get; set; }
        public string product_group_desc { get; set; }
        public string x_status { get; set; }
        public string x_note { get; set; }
        public string x_log { get; set; }
        public byte[] id { get; set; }
        public byte[] rowversion { get; set; }

        public virtual ICollection<product_type> product_type { get; set; }
    }
}
