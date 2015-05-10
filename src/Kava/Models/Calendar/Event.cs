using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kava.Models.Calendar
{
    public class Event
    {
        public Guid EventID { get; set; }
        public string EventName { get; set; }
        public string EventDescription { get; set; }
        public ApplicationUser CreatedByUser { get; set; }
        public DateTimeOffset StartDateTime { get; set; }
        public DateTimeOffset EndDateTime { get; set; }
        public bool Repeats { get; set; }
        public RepeatInterval RepeatInterval { get; set; }
        public List<ApplicationUser> AttendingUsers { get; set; }
        public int MaxSlots { get; set; }
    }
}
