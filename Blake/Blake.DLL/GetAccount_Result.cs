//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Blake.DLL
{
    using System;
    
    public partial class GetAccount_Result
    {
        public int code { get; set; }
        public string account_number { get; set; }
        public Nullable<int> tranCode { get; set; }
        public decimal outstanding_balance { get; set; }
        public Nullable<decimal> amount { get; set; }
        public Nullable<System.DateTime> transaction_date { get; set; }
        public string description { get; set; }
        public int person_code { get; set; }
    }
}
