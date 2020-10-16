using System.Collections.Generic;
using System;
using System.Text;

namespace SUS.HTTP
{
    public class HttpResponse
    {

        public HttpResponse(HttpStatusCode statusCode)
        {
            this.StatusCode = statusCode;
            this.Headers = new List<Header>();
            this.Cookies = new List<Cookie>();
        }
        public HttpResponse(string contentType, byte[] body, HttpStatusCode statusCode = HttpStatusCode.Ok)
        {
            if (body == null)
            {
                throw new ArgumentNullException(nameof(body));
            }

            this.StatusCode = statusCode;
            this.Body = body;
            this.Headers = new List<Header>
            {
                { new Header("Content-Type", contentType) },
                { new Header("Content-Length", body.Length.ToString()) },
            };
            this.Cookies = new List<Cookie>();
           
        }

        public HttpStatusCode StatusCode { get; set; }

        public ICollection<Header> Headers { get; set; }

        public ICollection<Cookie> Cookies { get; set; }

        public byte[] Body { get; set; }

        //// return response Headers
        //public override string ToString()
        //{

        //    StringBuilder responseSb = new StringBuilder();

        //    responseSb.Append($"HTTP/1.1 {(int)this.StatusCode} {this.StatusCode}" + HttpConstants.NewLine);

        //    foreach (var header in this.Headers)
        //    {
        //        responseSb.Append(header.ToString() + HttpConstants.NewLine);
        //    }

        //    foreach (var cookie in this.ResponseCookies)
        //    {
        //        responseSb.Append($"Set-Cookie: {cookie.ToString()}" + HttpConstants.NewLine);   
        //    }
        //    responseSb.Append(HttpConstants.NewLine);
        //    return responseSb.ToString();
        //}
    }
}