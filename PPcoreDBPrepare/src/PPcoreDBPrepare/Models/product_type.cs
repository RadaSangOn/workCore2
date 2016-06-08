using System;
using System.Collections.Generic;

namespace PPcoreDBPrepare.Models
{
    public partial class product_type
    {
        public product_type()
        {
            product = new HashSet<product>();
        }

        public string product_group_code { get; set; }
        public string product_type_code { get; set; }
        public string product_type_desc { get; set; }
        public string x_status { get; set; }
        public string x_note { get; set; }
        public string x_log { get; set; }
        public byte[] id { get; set; }
        public byte[] rowversion { get; set; }

        public virtual ICollection<product> product { get; set; }
        public virtual product_group product_group_codeNavigation { get; set; }
    }
}
