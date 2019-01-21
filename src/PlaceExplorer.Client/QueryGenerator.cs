namespace PlaceExplorer.Client
{
    public static class QueryGenerator
    {
        // Supported languages : https://developers.google.com/maps/faq#using-google-maps-apis
        public static string NearbySearchQuery(string apiKey, string latitude, string longitude, string radius, string type, string name = null, string language = null)
            => "/maps/api/place/nearbysearch/json" +
            "?location=" + latitude + "," + longitude +
            "&radius=" + radius +
            "&type=" + type +
            "&name=" + name +
            "&language=" + language +
            "&key=" + apiKey;

        public static string NextPageQuery(string query, string nextpageToken)
            => query + "&pagetoken=" + nextpageToken;

        public static string DetailSearchQuery(string apiKey, string placeId, string language = null)
            => "/maps/api/place/details/json" +
            "?placeid=" + placeId +
            "&key=" + apiKey +
            "&language=" + language; 
    }
}
