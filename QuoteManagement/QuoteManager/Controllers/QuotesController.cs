using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using QuoteManagement.Dtos;
using QuoteModels;
using QuoteRepository;


namespace QuoteManagement.Controllers
{
    [Authorize]
    public class QuotesController : ApiController
    {
        private readonly QuoteManagementEntities _context;
        private readonly IUnitOfWork _unitOfWork;

        public QuotesController()
        {
            _context = new QuoteManagementEntities();
            _unitOfWork=new UnitOfWork(_context);

        }

        

        //GET /api/Quote
        [HttpGet]
        public List<Quote> GetQuotes()
        {
            return _unitOfWork.Quotes.GetAll();
        }

        //GET /api/Quote/1
        [HttpGet]
        public Quote GetQuote(int id)
        {
            return _unitOfWork.Quotes.GetById(id);
        }


        //POST /api/Quote
        [HttpPost]
        public QuoteDto CreateQuote(QuoteDto quote)
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            var mapper = new Mapper(config);

            var dest = mapper.Map<QuoteDto, Quote>(quote);
            _unitOfWork.Quotes.Add(dest);
            _unitOfWork.Complete();

            quote.QuoteID = dest.QuoteID;

            return quote;
        }


        //PUT /api/Quote/1
        [HttpPut]
        public void UpdateQuote(int id, QuoteDto quote)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var quotefromDb = _unitOfWork.Quotes.GetById(id);

            if (quotefromDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            var mapper = new Mapper(config);

            mapper.Map(quote,quotefromDb);

            _unitOfWork.Complete();

        }


        //DELETE /api/Quote/1
        [HttpDelete]
        public void DeleteQuote(int id)
        {
            var quotefromDb = _unitOfWork.Quotes.GetById(id);

            if (quotefromDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            _unitOfWork.Quotes.Remove(quotefromDb);
            _unitOfWork.Complete();
        }
    }
}
