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
    
    public partial class Users
    {
        public int rowid { get; set; }
        public string mname { get; set; }
        public string password { get; set; }
        public string user_email { get; set; }
        public string phone { get; set; }
        public string role_no { get; set; }
        public Nullable<System.DateTime> birthday { get; set; }
        public string remark { get; set; }
        public string varify_code { get; set; }
        public Nullable<int> isvarify { get; set; }
    }
}
