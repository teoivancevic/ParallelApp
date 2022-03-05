using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ParallelApp.Shared.Models
{
    public partial class Message
    {
        public Message()
        {
            Messagetags = new HashSet<Messagetag>();
            Userlikes = new HashSet<Userlike>();
        }

        public int Id { get; set; }
        public string Subject { get; set; } = null!;
        public string Content { get; set; } = null!;
        public int SchoolId { get; set; }
        public int SenderUserId { get; set; }
        public int Likes { get; set; }
        public DateTime Created { get; set; }

        public virtual School School { get; set; } = null!;
        public virtual User SenderUser { get; set; } = null!;
        public virtual ICollection<Messagetag> Messagetags { get; set; }
        public virtual ICollection<Userlike> Userlikes { get; set; }

        public List<Tag> Tags { get; set; }
        //public User senderUser { get; set; }

        public string GetFormattedTime()
        {
            const int SECOND = 1;
            const int MINUTE = 60 * SECOND;
            const int HOUR = 60 * MINUTE;
            const int DAY = 24 * HOUR;
            const int MONTH = 30 * DAY;

            var ts = new TimeSpan(DateTime.Now.Ticks - Created.Ticks);
            double delta = Math.Abs(ts.TotalSeconds);

            if (delta < 1 * MINUTE)
            {
                return ts.Seconds == 1 ? "1s" : ts.Seconds + "s";
            }
                

            if (delta < 2 * MINUTE)
            {
                return "1m";
            }
                

            if (delta < 45 * MINUTE)
            {
                return ts.Minutes + "m";
            }
                

            if (delta < 90 * MINUTE)
            {
                return "1h";
            }
                

            if (delta < 24 * HOUR)
            {
                return ts.Hours + "h";
            }
                

            if (delta < 48 * HOUR)
            {
                return "1d";
            }
                

            if (delta < 30 * DAY)
            {
                return ts.Days + "d";
            }
            if (delta < 12 * MONTH)
            {
                int months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
                return months <= 1 ? "one month ago" : months + " months ago";
            }
            else
            {
                int years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
                return years <= 1 ? "one year ago" : years + " years ago";
            }

            
        }

        public string MakeLink()
        {
            string text = Regex.Replace(Content,
                @"((http|ftp|https):\/\/[\w\-_]+(\.[\w\-_]+)+([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?)",
                "<a target='_blank' href='$1'>$1</a>");
            return text;
        }
    }
}
