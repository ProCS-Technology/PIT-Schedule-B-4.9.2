using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Repository;
using ProcsDLL.Models.InsiderTrading.Service.Response;

namespace ProcsDLL.Models.InsiderTrading.Service.Request
{
    public class LocationRequest
    {
        private Location _location;

        public LocationRequest()
        {

        }

        public LocationRequest(Location location)
        {
            _location = new Location();
            _location = location;
        }

        public LocationResponse GetLocationList()
        {
            LocationRepository oRepository = new LocationRepository();
            return oRepository.GetLocationList(_location);
        }

        public LocationResponse SaveLocation()
        {
            LocationRepository oRepository = new LocationRepository();
            if (_location.locationId == 0)
            {
                return oRepository.SaveLocation(_location);
            }
            else
            {
                return oRepository.UpdateLocation(_location);

            }


        }

        public LocationResponse EditLocation()
        {
            LocationRepository oRepository = new LocationRepository();
            return oRepository.EditLocation(_location);
        }

        public LocationResponse DeleteLocation()
        {
            LocationRepository oRepository = new LocationRepository();
            return oRepository.DeleteLocation(_location);
        }
    }
}