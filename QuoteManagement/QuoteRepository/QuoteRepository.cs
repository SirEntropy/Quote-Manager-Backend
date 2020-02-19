using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QuoteModels;

namespace QuoteRepository
{
    public class QuoteRepository:GenericRepository<Quote>, IQuoteRepository
    {
        public QuoteRepository(QuoteManagementEntities context) : base(context)
        {
            Context = context;
        }

        public QuoteManagementEntities Context { get; set; }
        



    }
}