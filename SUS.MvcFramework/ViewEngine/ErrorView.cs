﻿using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SUS.MvcFramework.ViewEngine
{
    internal class ErrorView : IView
    {
        private IEnumerable<string> errors;
        private string csharpCode;

        public ErrorView(IEnumerable<string> errors, string csharpCode)
        {
            this.errors = errors;
            this.csharpCode = csharpCode;
        }

        public string ExecuteTemplate(object viewModel, string user)
        {
            var html = new StringBuilder();

            html.AppendLine($"<h1> View compile {this.errors.Count()} errors: </h1><ul>");

            foreach (var error in this.errors)
            {
                html.AppendLine($"<li>{error}</li>");
            }


            html.AppendLine($"</ul><pre>{this.csharpCode}</pre>");

            return html.ToString();
        }
    }
}