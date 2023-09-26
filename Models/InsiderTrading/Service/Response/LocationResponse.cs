using ProcsDLL.Models.InsiderTrading.Model;
using System.Collections.Generic;

namespace ProcsDLL.Models.InsiderTrading.Service.Response
{
    public class LocationResponse : BaseResponse
    {
        private Location _location;
        private List<Location> lstLocation;
        public Location Location
        {
            set
            {
                _location = value;
            }
            get
            {
                return _location;
            }
        }
        public List<Location> LocationList
        {
            set
            {
                lstLocation = value;
            }
            get
            {
                return lstLocation;
            }
        }
        public void AddObject(Location o)
        {
            if (lstLocation == null)
            {
                lstLocation = new List<Location>();
            }
            lstLocation.Add(o);
        }
    }
}