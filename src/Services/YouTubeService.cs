using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using TheTopPlays.Models;

namespace TheTopPlays.Services
{
    /// <summary>
    /// Allows application to search and retrieve video information from YouTube.com
    /// https://developers.google.com/api-client-library/dotnet/get_started
    /// </summary>
    public class YouTubeServices
    {
        public List<Video> GetMatchingVideos(string search)
        {
            search += string.Format("top {0} plays", search);

            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = "YOUR_API_KEY",
                ApplicationName = "YOUR_APPLICATION_NAME"
            });

            var searchListRequest = youtubeService.Search.List("snippet");
            searchListRequest.Q = search; 
            searchListRequest.Order = SearchResource.ListRequest.OrderEnum.Relevance;
            searchListRequest.MaxResults = 10;

            // Call the search.list method to retrieve results matching the specified query term.
            var searchListResponse = searchListRequest.Execute();

            var videos = new List<Video>();
            
            // Add each result to the appropriate list, and then display the lists of
            // matching videos, channels, and playlists.
            foreach (var searchResult in searchListResponse.Items)
            {
                if (searchResult.Id.Kind == "youtube#video")
                {
                    var newVideo = new Video
                    {
                        Id = searchResult.Id.VideoId,
                        ChannelId = searchResult.Id.ChannelId?? "no channel id",
                        Title = searchResult.Snippet.Title,
                        Description = searchResult.Snippet.Description?? "no description",
                        Thumbnail = searchResult.Snippet.Thumbnails.Maxres == null? 
                                    searchResult.Snippet.Thumbnails.High.Url : 
                                    searchResult.Snippet.Thumbnails.Maxres.Url,
                        PublishedDate = searchResult.Snippet.PublishedAt,
                        Search = search,
                        Url = string.Format("http://www.youtube.com/embed/{0}", searchResult.Id.VideoId)
                    };

                    videos.Add(newVideo);
                }        
            }

            return videos;
        }
    }
}
