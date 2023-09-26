using System;
using System.Collections.Generic;
namespace ProcsDLL.Models.InsiderTrading.Model
{
    public class BaseEntity
    {
        private List<String> _brokenRules = new List<String>();
        public String MODULE_DATABASE { set; get; }
        public String ADMIN_DATABASE { set; get; }
        public String LoggedInUser { set; get; }
        public Int32 CompanyId { set; get; }
        public virtual void Validate() { }
        public void ClearRules()
        {
            _brokenRules.Clear();
        }
        public void AddRule(String rule)
        {
            _brokenRules.Add(rule);
        }
        public List<String> GetRules()
        {
            return _brokenRules;
        }
        public Boolean ValidateInput()
        {
            Type objType = this.GetType();
            foreach (System.Reflection.PropertyInfo propertyInfo in objType.GetProperties())
            {

                if (!propertyInfo.PropertyType.IsGenericType)
                {
                    if (!propertyInfo.PropertyType.FullName.StartsWith("ProcsDLL"))
                    {
                        if (propertyInfo.PropertyType.Name.ToUpper() == "STRING")
                        {
                            string sVal = (string)propertyInfo.GetValue(this, null);
                            if (sVal != null)
                            {
                                if (
                                    sVal.ToUpper().StartsWith("+") || sVal.ToUpper().StartsWith("=")
                                    || sVal.ToUpper().StartsWith("-") || sVal.ToUpper().StartsWith("@")
                                    || sVal.ToUpper().Contains("<SCRIPT") || sVal.ToUpper().Contains("SUM(")
                                    || sVal.ToUpper().Contains("<APPLET>") || sVal.ToUpper().Contains("<BODY")
                                    || sVal.ToUpper().Contains("<EMBED") || sVal.ToUpper().Contains("<FRAME")
                                    || sVal.ToUpper().Contains("<FRAMESET") || sVal.ToUpper().Contains("<HTML")
                                    || sVal.ToUpper().Contains("<IFRAME") || sVal.ToUpper().Contains("<IMG")
                                    || sVal.ToUpper().Contains("<STYLE") || sVal.ToUpper().Contains("<LAYER")
                                    || sVal.ToUpper().Contains("<LINK") || sVal.ToUpper().Contains("<ILAYER")
                                    || sVal.ToUpper().Contains("<META") || sVal.ToUpper().Contains("<OBJECT")
                                    || sVal.ToUpper().StartsWith("<") || sVal.ToUpper().Contains("FTP://")
                                    || sVal.ToUpper().Contains("FILE:///") || sVal.ToUpper().Contains("GOPHER://")
                                    || sVal.ToUpper().Contains("HTTP://")
                                )
                                {
                                    return false;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (propertyInfo.GetValue(this, null) != null)
                        {
                            dynamic o = propertyInfo.GetValue(this, null);
                            if (!o.ValidateInput())
                            {
                                return false;
                            }
                        }
                    }
                }
                else
                {
                    string s2x = propertyInfo.PropertyType.FullName.ToUpper();
                    if (!s2x.Contains("SYSTEM.DATETIME"))
                    {
                        if (propertyInfo.GetValue(this, null) != null)
                        {
                            dynamic oSelf = this;
                            int cnt = propertyInfo.GetValue(oSelf, null).Count;
                            if (cnt > 0)
                            {
                                for (int cntNum = 0; cntNum < cnt; cntNum++)
                                {
                                    dynamic o = propertyInfo.GetValue(oSelf, null)[cntNum];
                                    string s1 = o.GetType().FullName;
                                    if (s1.StartsWith("ProcsDLL"))
                                    {
                                        if (!o.ValidateInput())
                                        {
                                            return false;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return true;
        }
    }
}