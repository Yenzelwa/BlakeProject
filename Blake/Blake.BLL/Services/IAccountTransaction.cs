using Blake.BO.Person;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blake.BLL.Services
{
   public interface IAccountTransaction
    {
        long CreateAccountTransaction(AccountTransaction _AccTransModel);
        bool updatetAccountTransaction(int _id, AccountTransaction _AccTransModel);
        bool deleteAccountTransaction(int _id);

        AccountTransaction GetAccountTransactionById(int accountId);
    }
}
