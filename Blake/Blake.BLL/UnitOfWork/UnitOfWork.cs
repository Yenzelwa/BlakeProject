
using Blake.BLL.GenericRepository;
using Blake.BO.Person;
using Blake.DLL;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blake.BLL.UnitOfWork
{
    public class UnitOfWork : IDisposable
    {
        private BlakeEntities _context = null;
        private GenericRepository<Person> _personRep;
        private GenericRepository<Person> _personDetailRep;
        private GenericRepository<Account> _personAccountRep;
        private GenericRepository<Transaction> _accountTransactionRep;

        public UnitOfWork()
        {
            _context = new BlakeEntities();
        }

        // Get/Set Property for event GenericRepositorysitory.

        public GenericRepository<Person> PersonRepository
        {
            get
            {
                if (this._personRep == null)
                    this._personRep = new GenericRepository<Person>(_context);
                return _personRep;
            }
        }

        public GenericRepository<Person> PersonDetailRepository
        {
            get
            {
                if (this._personDetailRep == null)
                    this._personDetailRep = new GenericRepository<Person>(_context);
                return _personDetailRep;
            }
        }

        public GenericRepository<Account> PersonAccountRepository
        {
            get
            {
                if (this._personAccountRep == null)
                    this._personAccountRep = new GenericRepository<Account>(_context);
                return _personAccountRep;
            }
        }

        public GenericRepository<Transaction> AccountTransactionRepRepository
        {
            get
            {
                if (this._accountTransactionRep == null)
                    this._accountTransactionRep = new GenericRepository<Transaction>(_context);
                return _accountTransactionRep;
            }
        }

        /// Save method.
        /// </summary>
        public void Save()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {

                var outputLines = new List<string>();
                foreach (var eve in e.EntityValidationErrors)
                {
                    outputLines.Add(string.Format("{0}: Entity of type \"{1}\" in state \"{2}\" has the following validation errors:", DateTime.Now, eve.Entry.Entity.GetType().Name, eve.Entry.State));
                    foreach (var ve in eve.ValidationErrors)
                    {
                        outputLines.Add(string.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage));
                    }
                }
                System.IO.File.AppendAllLines(@"C:\errors.txt", outputLines);

                throw e;
            }

        }

        /// Protected Virtual Dispose method
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    Debug.WriteLine("UnitOfWork is being disposed");
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        /// <summary>
        /// Dispose method
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }



        public bool disposed { get; set; }
    }
}
