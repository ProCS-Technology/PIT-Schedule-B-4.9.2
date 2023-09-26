using ProcsDLL.Models.InsiderTrading.Model;
using System.Collections.Generic;

namespace ProcsDLL.Models.InsiderTrading.Service.Response
{
    public class ModelCodeConductResponse : BaseResponse
    {
        private ModelCodeConduct _modelCodeConduct;
        private List<ModelCodeConduct> lstModelCodeConduct;
        public ModelCodeConduct ModelCodeConduct
        {
            set
            {
                _modelCodeConduct = value;
            }
            get
            {
                return _modelCodeConduct;
            }
        }
        public List<ModelCodeConduct> ModelCodeConductList
        {
            set
            {
                lstModelCodeConduct = value;
            }
            get
            {
                return lstModelCodeConduct;
            }
        }
        public void AddObject(ModelCodeConduct o)
        {
            if (lstModelCodeConduct == null)
            {
                lstModelCodeConduct = new List<ModelCodeConduct>();
            }
            lstModelCodeConduct.Add(o);
        }
    }
}