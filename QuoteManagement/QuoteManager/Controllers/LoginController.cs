using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using QuoteManager.Filters;
using QuoteModels;
using QuoteRepository;

namespace QuoteManager.Controllers
{
    [AllowAnonymous]
    public class LoginController : ApiController
    {
        private readonly QuoteManagementEntities _context;
        private readonly UnitOfWork _unitOfWork;


        public LoginController()
        {
            _context = new QuoteManagementEntities();
            _unitOfWork = new UnitOfWork(_context);
        }


        [HttpPost]
        public string Login(string username, string password)
        {
            if (_unitOfWork.Users.CheckCredentials(username, password))
            {
                return AuthenticationProcess.GenerateToken(username);
            }

            throw new NotImplementedException();
        }
    }
}