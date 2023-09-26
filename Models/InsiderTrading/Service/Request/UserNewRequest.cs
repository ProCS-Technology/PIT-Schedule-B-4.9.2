using ProcsDLL.Models.InsiderTrading.Model;
using ProcsDLL.Models.InsiderTrading.Repository;
using ProcsDLL.Models.InsiderTrading.Service.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProcsDLL.Models.InsiderTrading.Service.Request
{
    public class UserNewRequest
    {

        private UserNewHeader _UserNewHdr;
        public UserNewRequest()
        {
            _UserNewHdr = new UserNewHeader();
        }

        public UserNewRequest(UserNewHeader UserNewHdr)
        {
            _UserNewHdr = new UserNewHeader();
            _UserNewHdr = UserNewHdr;
        }

        //public UserNewResponse SaveUserNewHdr()
        //{
        //    try
        //    {
        //        UserNewRepository oRepository = new UserNewRepository();
        //        return oRepository.AddUserNewHdr(_UserNewHdr);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(Convert.ToString(ex.Message));
        //    }
        //}
        public UserNewResponse SaveUserNew()
        {
            try
            {
                UserNewRepository oRepository = new UserNewRepository();
                return oRepository.SaveUserNew(_UserNewHdr);
            }
            catch (Exception ex)
            {
                throw new Exception(Convert.ToString(ex.Message));
            }
        }

        //public bool ValidateUserNewAsOfDate()
        //{
        //    try
        //    {
        //        UserNewRepository oRepository = new UserNewRepository();
        //        return oRepository.ValidateUserNewAsOfDate(_UserNewHdr);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(Convert.ToString(ex.Message));
        //    }
        //}

        public UserNewResponse GetUserNewList()
        {
            try
            {
                UserNewRepository oRepository = new UserNewRepository();
                return oRepository.GetUserNewList(_UserNewHdr);
            }
            catch (Exception ex)
            {
                throw new Exception(Convert.ToString(ex.Message));
            }
        }
        //public BenposResponse GetAllEsopHdr()
        //{
        //    try
        //    {
        //        BenposRepository oRepository = new BenposRepository();
        //        return oRepository.GetAllEsopHdr(_UserNewHdr);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(Convert.ToString(ex.Message));
        //    }
        //}
        //public BenposResponse UpdateEsopAmount()
        //{
        //    try
        //    {
        //        BenposRepository oRepository = new BenposRepository();
        //        return oRepository.UpdateEsopAmount(_UserNewHdr);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(Convert.ToString(ex.Message));
        //    }
        //}
        //public BenposResponse GetEsopListByUser()
        //{
        //    try
        //    {
        //        BenposRepository oRepository = new BenposRepository();
        //        return oRepository.GetEsopListByUser(_UserNewHdr);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(Convert.ToString(ex.Message));
        //    }
        //}
        //public BenposResponse UpdateBenposDetail()
        //{
        //    try
        //    {
        //        BenposRepository oRepository = new BenposRepository();
        //        return oRepository.UpdateBenposDetail(_UserNewHdr);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(Convert.ToString(ex.Message));
        //    }
        //}

        //public UserNewMappingResponse GetUserNewFieldMapping()
        //{
        //    try
        //    {
        //        UserNewRepository oRepository = new UserNewRepository();
        //        return oRepository.GetUserNewFieldMapping(_UserNewHdr);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(Convert.ToString(ex.Message));
        //    }
        //}


    }
}

