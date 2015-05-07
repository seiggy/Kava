using System;
using System.Collections.Generic;

namespace Kava.Models.Blog
{
	public class BlogTag
	{
		public Guid BlogTagID { get; set; }
		public string Name { get; set; }
		public string UrlSlug { get; set; }
		public string Description { get; set; }
		public List<BlogPost> Posts { get; set; }
	}
}