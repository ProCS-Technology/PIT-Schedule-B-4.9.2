using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Repository;
using ProcsDLL.Models.InsiderTrading.Service.Response;


namespace ProcsDLL.Models.InsiderTrading.Service.Request
{
    public class UPSIVendorRequest
    {

        private UPSIVendor _UPSIvendor;

        public UPSIVendorRequest(UPSIVendor upsivendot)
        {
            _UPSIvendor = new UPSIVendor();
            _UPSIvendor = upsivendot;

        }


        public UPSIVendorResponce SaveUPSIVendor()
        {
            UPSIVendorRepository _repovendor = new UPSIVendorRepository();

            if (_UPSIvendor.VendorId == "0")
            {
                return _repovendor.SaveUPSIVendor(_UPSIvendor);
            }
            else
            {
                return _repovendor.UpdateUPSIVendor(_UPSIvendor);
            }

        }


        public UPSIVendorResponce ListUPSIVendor()
        {

            UPSIVendorRepository _repovendor = new UPSIVendorRepository();

            return _repovendor.GetUPSIVendorList(_UPSIvendor);

        }

        public UPSIVendorResponce ListUPSIVendor_ById()
        {

            UPSIVendorRepository _repovendor = new UPSIVendorRepository();

            return _repovendor.ListUPSIVendor_ById(_UPSIvendor);

        }

        public UPSIVendorResponce DeleteUPSIVendor_ById()
        {

            UPSIVendorRepository _repovendor = new UPSIVendorRepository();

            return _repovendor.DeleteUPSIVendor_ById(_UPSIvendor);

        }





    }//main class
}