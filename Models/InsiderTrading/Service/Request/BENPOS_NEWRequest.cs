using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Repository;
using ProcsDLL.Models.InsiderTrading.Service.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace ProcsDLL.Models.InsiderTrading.Service.Request
{
    public class BENPOS_NEWRequest
    {

        private BenposHeader _benposHdr;
        public BENPOS_NEWRequest()
        {
            _benposHdr = new BenposHeader();
        }

        public BENPOS_NEWRequest(BenposHeader benposHdr)
        {
            _benposHdr = new BenposHeader();
            _benposHdr = benposHdr;
        }

        public BENPOS_NEWResponse SaveBenposHdr()
        {
            try
            {
                BENPOS_NEWRepository oRepository = new BENPOS_NEWRepository();
                return oRepository.AddBenposHdr(_benposHdr);
            }
            catch (Exception ex)
            {
                throw new Exception(Convert.ToString(ex.Message));
            }
        }

        public bool ValidateBenposAsOfDate()
        {
            try
            {
                BENPOS_NEWRepository oRepository = new BENPOS_NEWRepository();
                return oRepository.ValidateBenposAsOfDate(_benposHdr);
            }
            catch (Exception ex)
            {
                throw new Exception(Convert.ToString(ex.Message));
            }
        }

        public BENPOS_NEWResponse GetAllBenposHdr()
        {
            try
            {
                BENPOS_NEWRepository oRepository = new BENPOS_NEWRepository();
                return oRepository.GetAllBenposHdr(_benposHdr);
            }
            catch (Exception ex)
            {
                throw new Exception(Convert.ToString(ex.Message));
            }
        }

        public BenposResponse UpdateBenposDetail()
        {
            try
            {
                BenposRepository oRepository = new BenposRepository();
                return oRepository.UpdateBenposDetail(_benposHdr);
            }
            catch (Exception ex)
            {
                throw new Exception(Convert.ToString(ex.Message));
            }
        }

        public BenposMappingResponse GetBenposFieldMapping()
        {
            try
            {
                BenposRepository oRepository = new BenposRepository();
                return oRepository.GetBenposFieldMapping(_benposHdr);
            }
            catch (Exception ex)
            {
                throw new Exception(Convert.ToString(ex.Message));
            }
        }
    }
}