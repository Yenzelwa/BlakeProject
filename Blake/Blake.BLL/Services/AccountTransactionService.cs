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
    public class AccountTransactionService : IAccountTransaction
    {
        private readonly UnitOfWork.UnitOfWork _unitOfWork;

        public AccountTransactionService()
        {
            _unitOfWork = new UnitOfWork.UnitOfWork();
        }

        public long CreateAccountTransaction(AccountTransaction _accountModel)
        {
            using (var scope = new TransactionScope())
            {
                var _trans = new Blake.DLL.Transaction
                {
                    amount = _accountModel.Amount ?? 0,
                    description = _accountModel.Description,
                    account_code = _accountModel.FK_AccountId,
                    capture_date = DateTime.Now,
                    transaction_date = _accountModel.TransDate ?? DateTime.Now

                };
                _unitOfWork.AccountTransactionRepRepository.Insert(_trans);
                var account = new Account();
                var _getAccount = _unitOfWork.PersonAccountRepository.GetAccount(_accountModel.FK_AccountId).FirstOrDefault();
                if (_getAccount != null)
                {
                     account = new Account
                    {
                        code = _accountModel.FK_AccountId,
                        outstanding_balance = _getAccount.outstanding_balance + _accountModel.Amount??0,
                        account_number= _getAccount.account_number,
                        person_code= _getAccount.person_code
                    };
                }
                _unitOfWork.PersonAccountRepository.Update(account);
                _unitOfWork.Save();
                scope.Complete();
                return _trans.code;
            }
        }

        public bool updatetAccountTransaction(int _id, AccountTransaction _accountModel)
        {
            var success = false;
            if (_accountModel != null)
            {
                using (var scope = new TransactionScope())
                {
                    var _account = _unitOfWork.AccountTransactionRepRepository.GetTransaction(_id);
                    if (_account != null)
                    {
                        var account = new Blake.DLL.Transaction
                        {
                            code = _accountModel.TransactionId,
                            amount = _accountModel.Amount ?? 0,
                            description = _accountModel.Description,
                            account_code = _accountModel.FK_AccountId,
                            capture_date = DateTime.Now,
                            transaction_date = _accountModel.TransDate ?? DateTime.Now

                        };
                        account.code = _accountModel.TransactionId;
                        _unitOfWork.AccountTransactionRepRepository.Update(account);
                        var _accountUpdate = new Account();
                        var _getAccount = _unitOfWork.PersonAccountRepository.GetAccount(_accountModel.FK_AccountId).FirstOrDefault();
                        if (_getAccount != null)
                        {
                            _accountUpdate = new Account
                            {
                                code = _accountModel.FK_AccountId,
                                outstanding_balance = _getAccount.outstanding_balance + _accountModel.Amount ?? 0
                            };
                        }
                        _unitOfWork.PersonAccountRepository.Update(_accountUpdate);

                    }
                
                }
                
            }
            return success;
        }

            public bool deleteAccountTransaction(int _id)
            {
                var success = false;
                if (_id > 0)
                {
                    using (var scope = new TransactionScope())
                    {
                        var _account = _unitOfWork.AccountTransactionRepRepository.GetTransaction(_id);
                        if (_account != null)
                        {

                            _unitOfWork.AccountTransactionRepRepository.Delete(_id);
                            _unitOfWork.Save();
                            scope.Complete();
                            success = true;
                        }
                    }
                success = true;
            }
                return success;
            }


            public AccountTransaction GetAccountTransactionById(int transId)
            {
                var trans = _unitOfWork.AccountTransactionRepRepository.GetTransaction(transId);
                if (trans != null)
                {

                    //Mapper.CreateMap<GetAllPersons_Result, Person>();
                    //var eventdetailsModel = Mapper.Map<List<GetAllPersons_Result>, List<Person>>(people);
                    //return eventdetailsModel;

                    var _account = new AccountTransaction
                    {
                        TransactionId = trans.code,
                        Amount = trans.amount,
                        Description = trans.description,
                        FK_AccountId = trans.code,
                        CapturedDate = trans.capture_date,
                        TransDate = trans.transaction_date
                    };


                    return _account;

                }
                return null;
            }
        }
    }

   
    

