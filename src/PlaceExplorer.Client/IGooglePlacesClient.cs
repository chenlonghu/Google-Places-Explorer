using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlaceExplorer.Client
{
    public interface IGooglePlacesClient
    {
        Task<KeyValuePair<string,List<PlaceDetail>>> GetPlaceDetailList(string nextPageToken);
    }
}
