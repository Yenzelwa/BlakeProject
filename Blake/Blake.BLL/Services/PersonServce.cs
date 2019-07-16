using AutoMapper;
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
   public class PersonServce:IPerson
    {
        private readonly UnitOfWork.UnitOfWork _unitOfWork;

        public PersonServce()
        {
            _unitOfWork = new UnitOfWork.UnitOfWork();
        }

        public long CreatePerson(PersonDetail _personModel)
        {
            using (var scope = new TransactionScope())
            {
                var _person = new Person
                {
                    
                        name = _personModel.PersonName,
                        surname = _personModel.PersonSurname,

                    id_number = _personModel.IdNumber
                    
                };
                _unitOfWork.PersonDetailRepository.Insert(_person);
                _unitOfWork.Save();
                scope.Complete();
                return _person.code;
            }
        }

        public bool updatePerson(int _id, PersonDetail _personModel)
        {
            var success = false;
            if (_personModel != null)
            {
                using (var scope = new TransactionScope())
                {
                    var _person = _unitOfWork.PersonRepository.GetPersonById(_id);
                    if (_person != null)
                    {
                        var person = new Person
                        {
                            code =_person[0].code,
                            name = _personModel.PersonName,
                            surname = _personModel.PersonSurname,

                            id_number = _personModel.IdNumber

                        };
                        _person[0].code = _personModel.PersonId;
                        _unitOfWork.PersonDetailRepository.Update(person);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }

        public bool deletPerson(int _id)
        {
            var success = false;
            if (_id > 0)
            {
                using (var scope = new TransactionScope())
                {
                    var _event = _unitOfWork.PersonRepository.GetByID(_id);
                    if (_event != null)
                    {

                        _unitOfWork.PersonDetailRepository.Delete(_event);
                        _unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }


        public PersonDetail GetById(int personId)
        {
            var _person = _unitOfWork.PersonDetailRepository.GetPersonById(personId);

            if ( _person.Count > 0 && _person != null)
            {
                var person = new PersonDetail
                {
                    PersonId = _person[0].code,
                    PersonName = _person[0].name,
                    PersonSurname = _person[0].surname,
                    IdNumber = _person[0].id_number
                };
                person.Accounts = new List<PersonAccount>();
                foreach (var item in _person)
                {


                    var account = new PersonAccount
                    {
                        AccountId = item.AccCode,
                        AccountNumber = item.account_number,
                        Balance = item.outstanding_balance,
                        FK_PersonId = item.code,

                    };
                    person.Accounts.Add(account);

                }
                person.DisableAccountDelete = _person[0].account_number == null ? false : true;

                return person;
            }
            return null;
        }
        public List<PersonDetail> GetAll(int pageSize, int pageNumber , string search)
        {
            var people = _unitOfWork.PersonRepository.GetAll(pageSize, pageNumber, search).ToList();
            if (people.Any())
            {

                //Mapper.CreateMap<GetAllPersons_Result, Person>();
                //var eventdetailsModel = Mapper.Map<List<GetAllPersons_Result>, List<Person>>(people);
                //return eventdetailsModel;
                var peopleResponse = new List<PersonDetail>();
                foreach (var item in people)
                {
                    var person = new PersonDetail
                    {
                        PersonId = item.code,
                        PersonName = item.name,
                        PersonSurname = item.surname,
                        TotalPages = item.TotalPages,

                       
                    };
                    

                    peopleResponse.Add(person);
                }

                return peopleResponse;
                        
            }
            return null;
        }

        public PersonDetail SearchPerson(string search)
        {
            var _person = _unitOfWork.PersonDetailRepository.SearchPerson(search);

            if (_person != null)
            {
                var _personAccount = _unitOfWork.PersonDetailRepository.GetByID(_person.code);
                var person = new PersonDetail
                {
                    PersonId = _person.code,
                    PersonName = _person.name,
                    PersonSurname = _person.surname,
                    DisableAccountDelete =  false 
                };
                person.Accounts = new List<PersonAccount>();

                var account = new PersonAccount
                {
                    //AccountId = _person.AccCode,
                    //AccountNumber = _person.account_number,
                    //Balance = _person.outstanding_balance,
                    //FK_PersonId = _person.code
                };
                person.Accounts.Add(account);

                return person;
            }
            return null;
        }
    }
}

