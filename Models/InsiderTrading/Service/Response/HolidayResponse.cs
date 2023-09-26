using ProcsDLL.Models.InsiderTrading.Model;
using System.Collections.Generic;

namespace ProcsDLL.Models.InsiderTrading.Service.Response
{
    public class HolidayResponse : BaseResponse
    {
        private Holiday _Holiday;
        private List<Holiday> lstHoliday;
        public Holiday Holiday
        {
            set
            {
                _Holiday = value;
            }
            get
            {
                return _Holiday;
            }
        }
        public List<Holiday> HolidayList
        {
            set
            {
                lstHoliday = value;
            }
            get
            {
                return lstHoliday;
            }
        }
        public void AddObject(Holiday o)
        {
            if (lstHoliday == null)
            {
                lstHoliday = new List<Holiday>();
            }
            lstHoliday.Add(o);
        }

    }
}