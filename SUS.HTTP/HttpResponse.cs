using System.Collections.Generic;
using System;
using System.Text;

namespace SUS.HTTP
{
    public class HttpResponse
    {
        public HttpResponse(string contentType, byte[] body, HttpStatusCode sc = HttpStatusCode.Ok)
        {
            if (body == null)
            {
                throw new ArgumentNullException(nameof(body));
            }

            this.StatusCode = sc;
            this.Body = body;
            this.Headers = new List<Header>
            {
                { new Header("Content-Type", contentType) },
                { new Header("Content-Length", body.Length.ToString()) },
            };
            this.ResponseCookies = new List<ResponseCookie>();
           
        }

        public HttpStatusCode StatusCode { get; set; }

        public ICollection<Header> Headers { get; set; }

        public ICollection<ResponseCookie> ResponseCookies { get; set; }

        public byte[] Body { get; set; }

        // return response Headers
        public override string ToString()
        {

            StringBuilder responseSb = new StringBuilder();

            responseSb.Append($"HTTP/1.1 {(int)this.StatusCode} {this.StatusCode}" + HttpConstants.NewLine);

            foreach (var header in this.Headers)
            {
                responseSb.Append(header.ToString() + HttpConstants.NewLine);
            }

            foreach (var cookie in this.ResponseCookies)
            {
                responseSb.Append($"Set-Cookie: {cookie.ToString()}" + HttpConstants.NewLine);   
            }
            responseSb.Append(HttpConstants.NewLine);
            return responseSb.ToString();
        }
    }
}