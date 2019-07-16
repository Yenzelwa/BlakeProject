using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blake.BO.Person
{
  public  class AccountTransaction
    {
        public int TransactionId { get; set; }
        public DateTime? TransDate { get; set; }
        public DateTime CapturedDate { get; set; }
        public decimal? Amount { get; set; }

        public string Description { get; set; }
        public int FK_AccountId { get; set; }
    }
}
