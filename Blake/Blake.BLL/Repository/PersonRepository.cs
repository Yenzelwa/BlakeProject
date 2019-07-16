using Blake.DLL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blake.BLL.GenericRepository
{
    
   public class PersonRepository<TEntity> where TEntity : class
    {
        internal BlakeEntities Context;
        internal DbSet<TEntity> DbSet;
        public PersonRepository(BlakeEntities context)
        {
            this.Context = context;
            this.DbSet = context.Set<TEntity>();

        }
        public List<GetPersons_Result> GetAll(int pagenumber , int pageSize)
        {
            return Context.GetPersons(pagenumber,pageSize,"").ToList();
        }
        public GetPersonById_Result GetPersonById(int personId)
        {
            return Context.GetPersonById(personId).FirstOrDefault();
        }
    }
}
