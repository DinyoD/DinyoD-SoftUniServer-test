using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace SUS.HTTP
{
    public class HttpRequest
    {

        public HttpRequest(string request)
        {
            this.Headers = new List<Header>();
            this.Cookies = new List<Cookie>();
            this.FormData = new Dictionary<string, string>();

            var lines = request.Split(new string[] { HttpConstants.NewLine }, StringSplitOptions.None);
            var firstLine = lines[0];
            var firstLineParts = firstLine.Split(' ');

            this.Method = (HttpMethod)Enum.Parse(typeof(HttpMethod), firstLineParts[0], true);
            this.Path = firstLineParts[1];

            int lineIndex = 1;
            bool isHeaderLine = true;

            StringBuilder bodySb = new StringBuilder();

            while(lineIndex < lines.Length)
            {
                var line = lines[lineIndex];
                lineIndex++;

                if (string.IsNullOrEmpty(line))
                {
                    isHeaderLine = false;
                    //continue;
                }

                if (isHeaderLine)
                {
                    this.Headers.Add(new Header(line));
                }
                else
                {
                    bodySb.AppendLine(line);
                }
            }

            this.Body = bodySb.ToString().TrimEnd().TrimStart();

            if (this.Method == HttpMethod.Post)
            {
                var parameters = this.Body.Split(new char[] { '&' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var param in parameters)
                {
                    var paramParts = param.Split('=');
                    var name = paramParts[0];
                    var value = WebUtility.UrlDecode(paramParts[1]);

                    this.FormData.Add(name, value);
                }
            }

            if (this.Headers.Any(x=>x.Name == HttpConstants.REquestCookieHeader))
            {
                var cookiesAsString = this.Headers.FirstOrDefault(x => x.Name == HttpConstants.REquestCookieHeader).Value;

                var cookies = cookiesAsString.Split(new string[] { "; " }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var cookieAsString in cookies)
                {
                    this.Cookies.Add(new Cookie(cookieAsString));
                }
            }
        }

        public string Path { get; set; }

        public HttpMethod Method { get; set; }

        public ICollection<Header> Headers { get; set; }

        public ICollection<Cookie> Cookies { get; set; }

        public Dictionary<string, string> FormData { get; set; }

        public string Body { get; set; }
    }
}