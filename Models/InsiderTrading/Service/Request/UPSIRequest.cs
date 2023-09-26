using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Repository;
using ProcsDLL.Models.InsiderTrading.Service.Response;

namespace ProcsDLL.Models.InsiderTrading.Service.Request
{
    public class UPSIRequest
    {
        private UPSI _upsi;

        private User _user;

        public UPSIRequest()
        {

        }

        public UPSIRequest(UPSI upsi)
        {
            _upsi = new UPSI();
            _upsi = upsi;

        }

        public UPSIRequest(User user)
        {
            _user = new User();
            _user = user;

        }

        public UPSIResponse SaveUPSI()
        {
            //_group.Validate();
            //if (_group.GetRules().Count == 0)
            //{
            UPSIRepository URepository = new UPSIRepository();
            if (_upsi.upsi_id == 0)
            {
                return URepository.Addupsi(_upsi);
            }
            else
            {
                return URepository.Updateupsi(_upsi);
            }
            //}
            return null; // already removed
        }

        public UPSIResponse list_user()
        {

            UPSIRepository URepository = new UPSIRepository();

            return URepository.list_user(_upsi);


        }

        public UPSIResponse list_upsi()
        {

            UPSIRepository URepository = new UPSIRepository();

            return URepository.list_upsi(_upsi);


        }

        public UPSIResponse edit_upsi()
        {

            UPSIRepository URepository = new UPSIRepository();

            return URepository.edit_upsi(_upsi);


        }

        public UPSIResponse delete_upsi()
        {

            UPSIRepository URepository = new UPSIRepository();

            return URepository.delete_upsi(_upsi);


        }
        public UPSIResponse HistoryUPSIGroup()
        {

            UPSIRepository URepository = new UPSIRepository();

            return URepository.HistoryUPSIGroup(_upsi);


        }

        public UPSIResponse send_mail()
        {

            UPSIRepository URepository = new UPSIRepository();

            return URepository.send_email(_upsi);
        }

        public UPSIResponse SaveUPSIGroupRemarks()
        {
            UPSIRepository URepository = new UPSIRepository();
            return URepository.SaveUPSIGroupRemarks(_upsi);
        }

        public UserResponse GetUsersForUPSI()
        {
            UPSIRepository URepository = new UPSIRepository();
            return URepository.GetUsersForUPSI(_user);
        }
    }
}