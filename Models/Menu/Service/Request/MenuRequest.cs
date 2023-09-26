using ProcsDLL.Models.Menu.Repository;
using ProcsDLL.Models.Menu.Service.Response;

namespace ProcsDLL.Models.Menu.Service.Request
{
    public class MenuRequest
    {
        private ProcsDLL.Models.Menu.Modal.Menu _objMenu;
        public MenuRequest(ProcsDLL.Models.Menu.Modal.Menu objMenu)
        {
            _objMenu = objMenu;
        }
        public MenuResponse GetMenu()
        {
            MenuRepository rep = new MenuRepository();
            return rep.GetMenu(_objMenu);
        }
    }
}