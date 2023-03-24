using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace RestAPIClient.Exceptions
{
    public class RestApiRequestException : Exception
    {
        public RestApiRequestException(HttpStatusCode httpStatusCode, string exMsg) : base(exMsg)
        {

        }
        public HttpStatusCode StatusCode { get; set; }
    }
}
