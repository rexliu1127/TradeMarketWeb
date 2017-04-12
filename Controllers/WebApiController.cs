using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LibraryTradeMarket;
using Newtonsoft.Json;
namespace TradeMarket.Controllers
{
    public class WebApiController : ApiController
    {
        //[HttpPost]
        //public HttpResponseMessage isLoginValidate(string account, string password)
        //{
        //    string result = "";
        //    Business business = new Business();

        //    try
        //    {

        //        if (business.isLogin(account, password).Account != "")
        //        {

        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        business.addErrorLog("WebApi", "isLoginValidate", ex.Message);
        //        //Utility.ErrorMessageToLogFile(ex);
        //        //throw;
        //    }



        //    return new HttpResponseMessage()
        //    {
        //        Content = new StringContent(result)
        //    };

        //}

        [HttpGet]
        //[EnableCors(origins: "*", headers: "*", methods: "*")]
        public HttpResponseMessage getProductType()
        {
            string result = "";
            Business business = new Business();
            try
            {

                List<ProductTypeViewModel> list = new List<ProductTypeViewModel>();

                list = business.getProductType();

                result = JsonConvert.SerializeObject(list, Formatting.Indented);

            }
            catch (Exception ex)
            {
                business.addErrorLog("WebApi", "getProductType", ex.Message);
                //Utility.ErrorMessageToLogFile(ex);
                //throw;
            }



            return new HttpResponseMessage()
            {
                Content = new StringContent(result)
            };

        }

        [HttpGet]
        //[EnableCors(origins: "*", headers: "*", methods: "*")]
        public HttpResponseMessage getProductByType(string productType)
        {
            string result = "";
            Business business = new Business();
            try
            {

                List<ProductViewModel> list = new List<ProductViewModel>();

                list = business.getProductByType(productType);

                result = JsonConvert.SerializeObject(list, Formatting.Indented);

            }
            catch (Exception ex)
            {
                business.addErrorLog("WebApi", "getProductByType", ex.Message);
                //Utility.ErrorMessageToLogFile(ex);
                //throw;
            }



            return new HttpResponseMessage()
            {
                Content = new StringContent(result)
            };

        }

        [HttpGet]
        //[EnableCors(origins: "*", headers: "*", methods: "*")]
        public HttpResponseMessage getProductByKeyword(string keyword)
        {
            string result = "";
            Business business = new Business();
            try
            {

                List<ProductViewModel> list = new List<ProductViewModel>();

                list = business.getProductByKeyword(keyword);

                result = JsonConvert.SerializeObject(list, Formatting.Indented);

            }
            catch (Exception ex)
            {
                business.addErrorLog("WebApi", "getProductByType", ex.Message);
                //Utility.ErrorMessageToLogFile(ex);
                //throw;
            }



            return new HttpResponseMessage()
            {
                Content = new StringContent(result)
            };

        }
    }
}
