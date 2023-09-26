using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Repository;
using ProcsDLL.Models.InsiderTrading.Service.Response;

namespace ProcsDLL.Models.InsiderTrading.Service.Request
{
    public class ModelCodeConductRequest
    {
        private ModelCodeConduct _modelCodeConduct;

        public ModelCodeConductRequest()
        {

        }

        public ModelCodeConductRequest(ModelCodeConduct modelCodeConduct)
        {
            _modelCodeConduct = new ModelCodeConduct();
            _modelCodeConduct = modelCodeConduct;

        }

        public ModelCodeConductResponse SaveModelCodeConduct()
        {
            ModelCodeConductRepository oRepository = new ModelCodeConductRepository();
            if (_modelCodeConduct.MODEL_ID == 0)
            {
                return oRepository.AddModelCodeConduct(_modelCodeConduct);
            }
            else
            {
                return oRepository.UpdateModelCodeConduct(_modelCodeConduct);
            }
        }

        public ModelCodeConductResponse GetModelCodeConductList()
        {
            ModelCodeConductRepository oRepository = new ModelCodeConductRepository();
            return oRepository.GetModelCodeConductList(_modelCodeConduct);
        }
    }
}