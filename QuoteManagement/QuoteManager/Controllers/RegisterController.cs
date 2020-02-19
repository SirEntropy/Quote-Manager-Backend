using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using QuoteManager.Dtos;
using AutoMapper;
using QuoteManagement;
using QuoteManager.Filters;
using QuoteModels;
using QuoteRepository;

namespace QuoteManager.Controllers
{
    [AllowAnonymous]
    public class RegisterController : ApiController
    {
        private readonly QuoteManagementEntities _context;
        private readonly UnitOfWork _unitOfWork;


        public RegisterController()
        {
            _context=new QuoteManagementEntities();
            _unitOfWork=new UnitOfWork(_context);
        }


        [HttpPost]
        public string Register(userDto user)
        {
            if (!ModelState.IsValid)
            {
               throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            if (_unitOfWork.Users.CheckUserExists(user.USERNAME))
            {
                return "User Already Exists";
            }

            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            var mapper = new Mapper(config);

            var dest = mapper.Map<userDto, user>(user);

            _unitOfWork.Users.Add(dest);
            _unitOfWork.Complete();
            user.USERID = dest.USERID;

            return AuthenticationProcess.GenerateToken(user.USERNAME);

        }

    }
}
