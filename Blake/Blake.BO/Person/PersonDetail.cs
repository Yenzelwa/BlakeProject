using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blake.BO.Person
{
    public class PersonDetail
    {
        public int AccountId { get; set; }
        public int PersonId { get; set; }
        public string PersonName { get; set; }
        public string PersonSurname { get; set; }
        public string IdNumber { get; set; }
       public bool  DisableAccountDelete { get; set; }
        public int? TotalPages { get; set; }
        public List<PersonAccount> Accounts { get; set; }

    }
}
