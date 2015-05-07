using System;

namespace Kava.Models.Forum
{
	public class ForumPost
	{
		public Guid ForumPostID { get; private set; }
		public ApplicationUser User { get; set; }
		public string PostContent { get; set; }
		public DateTimeOffset PostCreatedDateTime { get; private set; }
		public DateTimeOffset PostUpdatedDateTime { get; set; }
	}
}