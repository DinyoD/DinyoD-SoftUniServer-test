using System.Collections.Generic;
using System.Text;

namespace SUS.MvcFramework.ViewEngine
{
    internal class ErrorView : IView
    {
        private IEnumerable<string> errors;
        private string csharpCode;

        public ErrorView(List<string> errors, string csharpCode)
        {
            this.errors = errors;
            this.csharpCode = csharpCode;
        }

        public string ExecuteTemplate(object viewModel)
        {
            var html = new StringBuilder();

            html.AppendLine($"<h1> View compile {} errors: </h1><ul>");

            foreach (var error in this.errors)
            {
                html.AppendLine($"<li>{error}</li>");
            }


            html.AppendLine($"</ul><pre>{this.csharpCode}</pre>");

            return html.ToString();
        }
    }
}