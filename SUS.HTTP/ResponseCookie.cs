using System.Text;

namespace SUS.HTTP
{
    public class ResponseCookie : Cookie
    {
        public ResponseCookie(string name, string value) 
            : base(name, value)
        {
            this.Path = "/";
        }

        public int MaxAge { get; set; }

        public bool HttpOnly { get; set; }

        public string Path { get; set; }

        public override string ToString()
        {
            StringBuilder cookieSb = new StringBuilder();
            cookieSb.Append($"{this.Name}={this.Value}; Path={this.Path}");
            if (this.MaxAge != 0)
            {
                cookieSb.Append($"; Max-Age={this.MaxAge}");
            }

            if (this.HttpOnly)
            {
                cookieSb.Append("; HttpOnly");
            }

            return cookieSb.ToString();
        }
    }
}