using Blake.BO.Person;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blake.BLL.Services
{
   public interface IPersonAccount
    {
        long CreateAccount(PersonAccount _personAccModel);
        bool updateAccount(int _id, PersonAccount _personAccModel);
        bool deleteAccount(int _id);
       PersonAccount GetPersonAccountById(int accountId);
    }
}
