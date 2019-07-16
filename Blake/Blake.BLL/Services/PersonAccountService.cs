using Blake.BO.Person;
using Blake.DLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Blake.BLL.Services
{
   public class PersonAccountService : IPersonAccount
    {
        private readonly UnitOfWork.UnitOfWork _unitOfWork;

        public PersonAccountService()
        {
            _unitOfWork = new UnitOfWork.UnitOfWork();
        }

        public long CreateAccount(PersonAccount _accountModel)
        {
            using (var scope = new TransactionScope())
            {
                var _account = new Account
                {

                     account_number = _accountModel.AccountNumber,
                      outstanding_balance  = _accountModel.Balance??0,
                      person_code = _accountModel.FK_PersonId

                };
                _unitOfWork.PersonAccountRepository.Insert(_account);
                _unitOfWork.Save();
                scope.Complete();
                return _account.code;
            }
        }

        public bool updateAccount(int _id, PersonAccount _accountModel)
        {
            var success = false;
            if (_accountModel != null)
            {
                using (var scope = new TransactionScope())
                {
                    var _account = _unitOfWork.PersonAccountRepository.GetAccount(_id).FirstOrDefault();
                    if (_account != null)
                    {
                        var account = new Account
                        {
                            code = _accountModel.AccountId??0,
                            account_number = _accountModel.AccountNumber,
                             person_code =_accountModel.FK_PersonId
                              

                        };
                        account.code = _accountModel.AccountId??0;
                        _unitOfWork.PersonAccountRepository.Update(account);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }

        public bool deleteAccount(int _id)
        {
            var success = false;
            if (_id > 0)
            {
                using (var scope = new TransactionScope())
                {
                    var _account = _unitOfWork.PersonAccountRepository.GetAccount(_id).FirstOrDefault();
                    if (_account != null)
                    {

                        _unitOfWork.PersonAccountRepository.Delete(_id);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }


        public PersonAccount GetPersonAccountById(int accountId)
        {
            var accounts = _unitOfWork.PersonAccountRepository.GetAccount(accountId).ToList();
            if (accounts.Any())
            {

                //Mapper.CreateMap<GetAllPersons_Result, Person>();
                //var eventdetailsModel = Mapper.Map<List<GetAllPersons_Result>, List<Person>>(people);
                //return eventdetailsModel;
                var accountResponse = new PersonAccount();
                
                    var _account = new PersonAccount
                    {
                        AccountId = accounts[0].code,
                        AccountNumber = accounts[0].account_number,
                        Balance = accounts[0].outstanding_balance,
                        FK_PersonId = accounts[0].code,
                        CanCloseAccount = accounts[0].outstanding_balance > 0 || accounts[0].amount > 0 ? true :  false
                    };
                _account.Transactions = new List<AccountTransaction>();
                foreach (var item in accounts)
                {
                   

                    var account = new AccountTransaction
                    {
                        TransactionId = item.tranCode??0,
                        Amount = item.amount,
                        Description = item.description,
                        TransDate = item.transaction_date

                    };
                    _account.Transactions.Add(account);
                   
                }

                return _account;

            }
            return null;
        }
   

    }
}

