using Blake.BO.Person;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blake.BLL.Services
{
   public interface IPerson
    {
        long CreatePerson(PersonDetail _personModel);
        bool updatePerson(int _id, PersonDetail _personModel);
        bool deletPerson(int _id);
        List<PersonDetail> GetAll(int pageSize , int pageNumber, string search);
        PersonDetail GetById(int personId);
        PersonDetail SearchPerson(string search);
    }
}
