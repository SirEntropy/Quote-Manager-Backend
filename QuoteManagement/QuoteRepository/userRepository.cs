using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QuoteModels;

namespace QuoteRepository
{
    public class userRepository:GenericRepository<user>, IuserRepository
    {
        public QuoteManagementEntities Context { get; set; }

        public userRepository(QuoteManagementEntities context) : base(context)
        {
            Context = context;
        }

        public bool CheckCredentials(string username, string password)
        {
            return Context.Set<user>().Any(m => m.USERNAME == username && m.USERPASSWORD == password);
        }

        public bool CheckUserExists(string username)
        {
            return Context.Set<user>().Any(m => m.USERNAME == username);
        }
    }
}