using System;
using System.Collections.Generic;

namespace Kava.Models.Blog
{
	public class BlogPost
	{
		public Guid BlogPostID { get; private set; }
		public string Title { get; set; }
		public string ShortDescription { get; set; }
		public string Content { get; set; }
		public string Meta { get; set; }
		public string UrlSlug { get; set; }
		public bool Published { get; set; }
		public DateTimeOffset PostedDateTime { get; set; }
		public DateTimeOffset ModifiedDateTime { get; set; }
		public BlogCategory Category { get; set; }
		public List<BlogTag> Tags { get; set; }
	}
}