using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfService1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        public List<SearchResultCustomized> SearchYoutubeVideo(string keyword)
        {

            //Construyendo el servicio de Youtube
            YouTubeService youtube = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = "AIzaSyA-HQVqE6Smy-oBBk9RPrYx7jL1VwYMXTI"
            });

            SearchResource.ListRequest listRequest = youtube.Search.List("snippet");
            listRequest.Q = keyword;
            listRequest.MaxResults = 6;


            SearchListResponse searchResponse = listRequest.Execute();
            IList<SearchResult> searchResults = searchResponse.Items;
            List<SearchResultCustomized> searchResultCustomizeds = new List<SearchResultCustomized>();
            
            foreach (var item in searchResults.ToList())
            {
                SearchResultCustomized searchResultCustomized = new SearchResultCustomized();
                searchResultCustomized.VideoId = item.Id.VideoId;
                searchResultCustomized.Title = item.Snippet.Title;
                searchResultCustomized.ImageURL = item.Snippet.Thumbnails.Default__.Url;
                searchResultCustomizeds.Add(searchResultCustomized);
            }
            return searchResultCustomizeds;
        }
    }
}
