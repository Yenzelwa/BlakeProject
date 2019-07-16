using Blake.BLL.Services;
using Blake.BO.Person;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blake.Controllers
{
    public class PersonController : Controller
    {
        private IPerson _personService;
        private IPersonAccount _personAccount;
        private IAccountTransaction _accountTransaction;

        public PersonController() {
          _personService = new PersonServce();
          _personAccount = new PersonAccountService();
            _accountTransaction = new AccountTransactionService();
    } 

        // GET: Person
        public ActionResult Index()
        {
            return View();
        } 
        public JsonResult GetPersonList(int currentPage, int pageSize , string filterLetter)
        {
            try
            {
                filterLetter = String.IsNullOrEmpty(filterLetter) ? null : filterLetter;
                var _people = _personService.GetAll(pageSize ,currentPage, filterLetter);
                List<PersonDetail> _personResponse = new List<PersonDetail>();
                if (_people != null)
                {
                    var _peopleResponse = _people as List<PersonDetail> ?? _people.ToList();
                    if (_peopleResponse.Any())
                    {
                         _personResponse = _peopleResponse.ToList();
                    }
                }
                return Json(_personResponse, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
 
        public JsonResult SavePerson(PersonDetail person)
        {
            try
            {
                var _searchPerson = _personService.GetById(person.PersonId);
              if(_searchPerson != null)
                {
                    _personService.updatePerson(person.PersonId, person);
                    return Json(new { person, statusMsg = "Save Successful..." }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var _personExists = _personService.SearchPerson(person.IdNumber);
                    if(_personExists == null) {

                        var _newPerson = _personService.CreatePerson(person);
                        if (_newPerson > 1)
                            return Json(new { person, statusMsg = "Save Successful..." }, JsonRequestBehavior.AllowGet);
                    } else {
                            return Json(new { person, statusMsg = "ID Number Exists" }, JsonRequestBehavior.DenyGet);
                    }
                   
                }
                return Json(new { person, statusMsg = "Error Occured" }, JsonRequestBehavior.DenyGet);
            }
            catch (Exception ex)
            {
                return Json(new { statusMsg = "Error Occured" }, JsonRequestBehavior.DenyGet);
            }
        }
        public JsonResult GetPersonById(int personId)
        {
            try
            {
                var _person = _personService.GetById(personId);
                var _personResponse = new PersonDetail();
                if (_person != null)
                {
                    _personResponse = _person as PersonDetail ?? _person;
                    ;
                }
                return Json(_personResponse, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult DeletePerson(int personId)
        {
            try
            {
                var _person = _personService.GetById(personId);
                if (_person != null)
                {
                    _personService.deletPerson(_person.PersonId);
                    return Json(new { _person, statusMsg = "Save Successful..." }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { _person, statusMsg = "Error Occured" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { ex.Message, statusMsg = "Error Occured" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Account(int? accId, int? personId)
        {
            ViewBag.AccNumber = accId ?? 0;
            ViewBag.PersonId = personId ?? 0;
            return View();
        }
        public JsonResult AccountDetail(int accId, int personId)
        {

            try
            {
                if (accId > 0 || personId > 0)
                    return Json(new { redirectUrl = @Url.Action("Account", new { accId = accId, personId = personId }) });
                else
                {
                    return Json(new { redirectUrl = @Url.Action("Account") });
                }
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult SaveAccount(PersonAccount personAccount)
        {
            try
            {
                var _searchAccount = _personAccount.GetPersonAccountById(personAccount.AccountId ??0 );
                if (_searchAccount != null)
                {
                    _personAccount.updateAccount(personAccount.AccountId ?? 0, personAccount);
                    return Json( new { personAccount, statusMsg = "Save Successful..." }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var _accountExists = _personService.SearchPerson(personAccount.AccountNumber);
                    if (_accountExists == null)
                    {

                        var _newPerson = _personAccount.CreateAccount(personAccount);
                        if (_newPerson > 1)
                            return Json(new { personAccount, statusMsg = "Save Successful..." }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { personAccount, statusMsg = "Account Number Exists" }, JsonRequestBehavior.DenyGet);
                    }
                   
                }
              
                return Json(new { personAccount, statusMsg = "Error Occured" }, JsonRequestBehavior.DenyGet);
            }
            catch (Exception ex)
            {
                return Json(new { personAccount, statusMsg = "Error Occured" }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetPersonAccounts(int personId)
        {
            try
            {
                var _account = _personAccount.GetPersonAccountById(personId);
                PersonAccount _personAccountResponse = new PersonAccount();
                if (_account != null)
                {
                    var _accountResponse = _account as PersonAccount ?? _personAccountResponse;
                    if (_accountResponse != null)
                    {
                        _personAccountResponse = _accountResponse;
                    }
                }
                return Json(_personAccountResponse, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult DeleteAccount(int accId)
        {
            try
            {
                var _account = _personAccount.GetPersonAccountById(accId);
                if (_account != null)
                {
                    _personAccount.deleteAccount(accId);
                    return Json(new { _account, statusMsg = "success" }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { _account, statusMsg = "error" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { ex.Message, statusMsg = "error" }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult AccountTransactionDetail(int? transactionId, int? accId)
        {

            try
            {
                if (accId > 0 || transactionId > 0)
                    return Json(new { redirectUrl = @Url.Action("AccountTransaction", new { transactionId = transactionId, accId = accId }) });
                else
                {
                    return Json(new { redirectUrl = @Url.Action("AccountTransaction") });
                }
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult AccountTransaction(int? transactionId, int? accId)
        {
            ViewBag.TransId = transactionId ?? 0;
            ViewBag.AccountId = accId ?? 0;
            return View();
        }
        public JsonResult SaveAccountTransaction(AccountTransaction accTransaction)
        {
            try
            {
                if(accTransaction.Amount == 0)
                {
                    return Json(new { accTransaction, statusMsg = "Amount can't be zero" }, JsonRequestBehavior.AllowGet);
                }
                var _searchAccount = _accountTransaction.GetAccountTransactionById(accTransaction.TransactionId);
                if (_searchAccount != null)
                {
                    _accountTransaction.updatetAccountTransaction(accTransaction.TransactionId , accTransaction);
                    return Json( new { accTransaction, statusMsg = "Save Successful..." }, JsonRequestBehavior.AllowGet);
                }
                else
                {

                    var _newPerson = _accountTransaction.CreateAccountTransaction(accTransaction);
                    if (_newPerson > 1)
                        return Json(new { accTransaction, statusMsg = "Save Successful..." }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { accTransaction, statusMsg = "Error Occured" }, JsonRequestBehavior.DenyGet);
            }
            catch (Exception ex)
            {
                return Json(new { accTransaction, statusMsg = "Error Occured" }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetAccountTransaction(int transId)
        {
            try
            {
                var _trans = _accountTransaction.GetAccountTransactionById(transId);
                AccountTransaction _personTransctionResponse = new AccountTransaction();
                if (_trans != null)
                {
                    var _accountResponse = _trans as AccountTransaction ?? _personTransctionResponse;
                    if (_accountResponse != null)
                    {
                        _personTransctionResponse = _accountResponse;
                    }
                }
                return Json(_personTransctionResponse, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult DeleteAccountTransaction(int transId)
        {
            try
            {
                var _account = _accountTransaction.GetAccountTransactionById(transId);

                if (_account != null)
                {
                    _accountTransaction.deleteAccountTransaction(transId);
                }
                return Json(new { _account, statusMsg = "success" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { ex.Message, statusMsg = "error" }, JsonRequestBehavior.AllowGet);
            }
        }
    }

}