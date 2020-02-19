using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;

namespace QuoteManager.Filters
{
    public class QuoteExceptionFilter:ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            if (context.Exception is NotImplementedException)
            {
                context.Response=new HttpResponseMessage(HttpStatusCode.NotImplemented)
                {
                    Content = new StringContent("The information asked is not found, contact your provider."),
                    ReasonPhrase = "Not Found"

                };
            }



        }
    }
}