using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Repository;
using ProcsDLL.Models.InsiderTrading.Service.Response;
using System;
namespace ProcsDLL.Models.InsiderTrading.Service.Request
{
    public class BenposRequest
    {
        private BenposHeader _benposHdr;
        public BenposRequest()
        {
            _benposHdr = new BenposHeader();
        }

        public BenposRequest(BenposHeader benposHdr)
        {
            _benposHdr = new BenposHeader();
            _benposHdr = benposHdr;
        }

        public BenposResponse SaveBenposHdr()
        {
            try
            {
                BenposRepository oRepository = new BenposRepository();
                return oRepository.AddBenposHdr(_benposHdr);
            }
            catch (Exception ex)
            {
                throw new Exception(Convert.ToString(ex.Message));
            }
        }
        public BenposResponse SaveEsop()
        {
            try
            {
                BenposRepository oRepository = new BenposRepository();
                return oRepository.SaveEsop(_benposHdr);
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
                BenposRepository oRepository = new BenposRepository();
                return oRepository.ValidateBenposAsOfDate(_benposHdr);
            }
            catch (Exception ex)
            {
                throw new Exception(Convert.ToString(ex.Message));
            }
        }

        public BenposResponse GetAllBenposHdr()
        {
            try
            {
                BenposRepository oRepository = new BenposRepository();
                return oRepository.GetAllBenposHdr(_benposHdr);
            }
            catch (Exception ex)
            {
                throw new Exception(Convert.ToString(ex.Message));
            }
        }
        public BenposResponse GetAllEsopHdr()
        {
            try
            {
                BenposRepository oRepository = new BenposRepository();
                return oRepository.GetAllEsopHdr(_benposHdr);
            }
            catch (Exception ex)
            {
                throw new Exception(Convert.ToString(ex.Message));
            }
        }
        public BenposResponse UpdateEsopAmount()
        {
            try
            {
                BenposRepository oRepository = new BenposRepository();
                return oRepository.UpdateEsopAmount(_benposHdr);
            }
            catch (Exception ex)
            {
                throw new Exception(Convert.ToString(ex.Message));
            }
        }
        public BenposResponse GetEsopListByUser()
        {
            try
            {
                BenposRepository oRepository = new BenposRepository();
                return oRepository.GetEsopListByUser(_benposHdr);
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
        public BenposResponse GetCorporateListById()
        {
            BenposRepository oRepository = new BenposRepository();
            return oRepository.GetCorporateListById(_benposHdr);
        }
        public BenposResponse DeleteBenposDetail()
        {
            try
            {
                BenposRepository oRepository = new BenposRepository();
                return oRepository.DeleteBenposDetail(_benposHdr);
            }
            catch (Exception ex)
            {
                BenposResponse oBenpos = new BenposResponse();
                oBenpos.StatusFl = false;
                oBenpos.Msg = "Processing failed, because of system error !";
                return oBenpos;
            }
        }
    }
}