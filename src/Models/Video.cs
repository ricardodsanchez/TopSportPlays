using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheTopPlays.Models
{
    public class Video
    {
        public string Id { get; set; }
        public string ChannelId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Thumbnail { get; set; }
        public DateTime? PublishedDate { get; set; }
        public string Url { get; set; }

        public string Search { get; set; }
        public string Comments { get; set; }
    }
}