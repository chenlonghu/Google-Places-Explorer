using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PlaceExplorer.Client
{
    public class GooglePlacesClient : IGooglePlacesClient
    {
        private HttpClient _client;
        private string _query;
        private string _apiKey;

        public string Request { get; }

        public GooglePlacesClient(string apiKey, string latitude, string longitude, string radius, string type, string name = null, string language = null)
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("https://maps.googleapis.com");

            _query = QueryGenerator.NearbySearchQuery(apiKey, latitude, longitude, radius, type, name, language);
            _apiKey = apiKey;

            Request = _client.BaseAddress.ToString() + _query;
        }

        // Get 20 detailed places with next page token
        // Generate 21 requests per call, Google allows 1000 requests per day
        public async Task<KeyValuePair<string, List<PlaceDetail>>> GetPlaceDetailList(string nextPageToken)
        {
            var response = await _client.GetAsync(QueryGenerator.NextPageQuery(_query, nextPageToken));

            if (!response.IsSuccessStatusCode)
                throw new Exception(response.StatusCode.ToString() + response.ReasonPhrase);

            var placeResponse = JsonConvert.DeserializeObject<PlaceResponse>(await response.Content.ReadAsStringAsync());

            // Get 20 place details
            List<PlaceDetail> placeDetailList = new List<PlaceDetail>();

            foreach (Place place in placeResponse.Results)
            {
                var r = await _client.GetAsync(QueryGenerator.DetailSearchQuery(_apiKey, place.Place_id));

                if (!r.IsSuccessStatusCode)
                    throw new Exception(r.StatusCode.ToString() + r.ReasonPhrase);

                var placeDetail = JsonConvert.DeserializeObject<PlaceDetailResponse>(await r.Content.ReadAsStringAsync()).Result;

                placeDetailList.Add(placeDetail); 
            }

            return new KeyValuePair<string, List<PlaceDetail>>(placeResponse.Next_page_token, placeDetailList);
        }
    }
}
