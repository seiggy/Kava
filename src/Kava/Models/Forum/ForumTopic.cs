using System;
using System.Collections.Generic;

namespace Kava.Models.Forum
{
	public class ForumTopic
	{
		public Guid ForumTopicID { get; private set; }
		public ApplicationUser OriginatingUser { get; set; }
		public string TopicTitle { get; set; }
		public List<ForumPost> ChildPosts { get; set; }
		public bool TopicLocked { get; set; }
		public DateTimeOffset TopicCreatedDate { get; set; }
		public DateTimeOffset LastPostDate { get; set; }
	}
}