using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Repository;
using ProcsDLL.Models.InsiderTrading.Service.Response;

namespace ProcsDLL.Models.InsiderTrading.Service.Request
{
    public class HolidayRequest
    {
        private Holiday _Holiday_description;
        public HolidayRequest()
        {

        }
        public HolidayRequest(Holiday description)
        {
            _Holiday_description = new Holiday();
            _Holiday_description = description;
        }

        public HolidayResponse GetHolidayList()
        {
            HolidayRepository oRepository = new HolidayRepository();
            return oRepository.GetHolidayList(_Holiday_description);
        }
        public HolidayResponse SaveHoliday()
        {
            HolidayRepository oRepository = new HolidayRepository();
            if (_Holiday_description.ID == 0)
            {
                return oRepository.AddHoliday(_Holiday_description);
            }
            else
            {
                return oRepository.UpdateHoliday(_Holiday_description);
            }
        }
        public HolidayResponse DeleteHoliday()
        {

            HolidayRepository oRepository = new HolidayRepository();
            return oRepository.DeleteHoliday(_Holiday_description);
        }

    }
}