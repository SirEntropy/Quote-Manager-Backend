using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QuoteModels;

namespace QuoteRepository
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly QuoteManagementEntities _context;
        public IQuoteRepository Quotes { get; set; }
        public IuserRepository Users { get; set; }

        public UnitOfWork(QuoteManagementEntities context)
        {
            _context = context;
            Quotes=new QuoteRepository(_context);
            Users=new userRepository(_context);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void Complete()
        { 
            _context.SaveChanges();
        }
    }
}