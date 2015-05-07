using System;
using System.Collections.Generic;

namespace Kava.Models.Forum
{
	public class ForumCategory
	{
		public Guid ForumCategoryID { get; private set; }
		public string CategoryName { get; set; }
		public string CategoryDescription { get; set; }
		public List<ForumPost> CategoryPosts { get; set; }
	}
}