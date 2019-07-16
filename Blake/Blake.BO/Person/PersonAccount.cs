using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blake.BO.Person
{
    public class PersonAccount
    {
        public int? AccountId { get; set; }

        public string AccountNumber { get; set; }

        public decimal? Balance { get; set; }
        public int FK_PersonId { get; set; }
        public bool CanCloseAccount { get; set; }
        public List<AccountTransaction> Transactions { get; set; }
    }
}
