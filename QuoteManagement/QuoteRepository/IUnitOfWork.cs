using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuoteRepository
{
    public interface IUnitOfWork:IDisposable
    {
        IQuoteRepository Quotes { get; }
        IuserRepository Users { get; }

        void Complete();
    }
}
