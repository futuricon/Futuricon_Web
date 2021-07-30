using Futuricon.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web;

namespace Futuricon.Domain.Entities.Portfolio
{
    public class Portfolio : IDomainEntity
    {
        public Portfolio()
        {
            Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }
        public string Slug { get; set; }
        public string CoverImage { get; set; }
        public string Title { get; set; }
        public DateTime Published { get; set; }
        public string ProjectLink { get; set; }
        public string Client { get; set; }
        public string GitHubLink { get; set; }
        public string Content { get; set; }
        public virtual IList<Tag> Tags { get; set; } = new List<Tag>();
        public static string GetShortContent(string articleContent, int length)
        {
            var content = HttpUtility.HtmlDecode(articleContent);
            content = Regex.Replace(content, @"<(.|\n)*?>", "");
            if (content.Length > 500)
            {
                content = content.Substring(0, length).Trim() + "...";
            }

            return content;
        }
    }
}
