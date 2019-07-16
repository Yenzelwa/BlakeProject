using AutoMapper;
using Blake.DLL;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace Blake.BO.Mapping
{
   public class PersonProfile : Profile
    {
        public PersonProfile()
        {
            CreateMap<GetPersons_Result, Person.PersonDetail>()
             .ForMember(dest => dest.PersonId, opt => opt.MapFrom(src => src.code))
             .ForMember(dest => dest.PersonName, opt => opt.MapFrom(src => src.name))
             .ForMember(dest => dest.PersonSurname, opt => opt.MapFrom(src => src.surname));

            CreateMap<GetPersonById_Result, Person.PersonDetail>()
           //.ForMember(dest => dest.person, opt => opt.ResolveUsing(sr => new Person.Person
           //{

           //}))
           .ForMember(dest => dest.IdNumber, opt => opt.MapFrom(src => src.id_number)); 
        }
    }
}
