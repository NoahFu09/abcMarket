//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace abcMarket.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Carts
    {
        public int rowid { get; set; }
        public string lot_no { get; set; }
        public string user_email { get; set; }
        public string product_no { get; set; }
        public string product_name { get; set; }
        public string product_spec { get; set; }
        public Nullable<int> qty { get; set; }
        public Nullable<int> price { get; set; }
        public Nullable<int> amount { get; set; }
        public Nullable<System.DateTime> create_time { get; set; }
    }
}
