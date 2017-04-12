using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading;
using System.Threading.Tasks;
using LibraryTradeMarket;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
namespace TradeMarket.Views.Mart
{
    public class MartController : Controller
    {
        // GET: Mart
        public ActionResult Index()
        {
            return View();
        }

        // GET: Product
        public ActionResult Product(string productType)
        { 
            return View();
        }

        // GET: Cart
        public ActionResult Cart()
        {            
            return View();
        }

        public async Task<List<ProductViewModel>> getProductByType(string productType)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://220.130.10.50:6868");
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("productType", productType),
                //new KeyValuePair<string, string>("password", Utility.getSecretCode(password))
            });
            var result = await client.PostAsync("/api/WebApi/getProductByType", content);
            string resultJson = await result.Content.ReadAsStringAsync();

            List<ProductViewModel> response = (List<ProductViewModel>)JsonConvert.DeserializeObject(resultJson, typeof(List<ProductViewModel>));

            return response;

        }

        

        //public string tempOrderID { get; set; }
        //public string productCustomizeID { get; set; }
        //public string productName { get; set; }
        //public string quantity { get; set; }
        //public string price { get; set; }
        //public string unitName { get; set; }

        public async Task<ApiResponseData> addCartAndCount(string tempOrderID,
            string productCustomizeID,string productName,string quantity,string price,
            string unitName)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://220.130.10.50:6868");
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("tempOrderID", tempOrderID),
                new KeyValuePair<string, string>("productCustomizeID", productCustomizeID),
                new KeyValuePair<string, string>("productName", productName),
                new KeyValuePair<string, string>("quantity", quantity),
                new KeyValuePair<string, string>("price", price),
                new KeyValuePair<string, string>("unitName", unitName)
            });
            var result = await client.PostAsync("/api/WebApi/addCart", content);
            string resultJson = await result.Content.ReadAsStringAsync();

            ApiResponseData response = (ApiResponseData)JsonConvert.DeserializeObject(resultJson, typeof(ApiResponseData));

            return response;

        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> addPriceCart(string tempOrderID,
            string productCustomizeID, string productName, string quantity, string price,
            string unitName, string returnUrl = "/Home/Login")
        {

            Business business = new Business();

            MemberViewModel member = new MemberViewModel();

            ApiResponseData response = new ApiResponseData();

            response = await addCartAndCount(tempOrderID, productCustomizeID,productName,quantity,price,unitName);


            Session["cartCount"] = response.Result;
            return View();

            //if (response.Result == "1")
            //{
            //    Session["cartCount"] = response.Result;                                
            //    return RedirectToAction("Index", "Mart");
            //}
            //else
            //{
                
            //    return View();

            //}


        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> addCart(
            string product_customize_id, string product_name, string quantity,
            string product_unit_name, string returnUrl = "/Home/Login")
        {

            Business business = new Business();

            //MemberViewModel member = new MemberViewModel();

            ApiResponseData response = new ApiResponseData();


            string tempOrderID = "";

            HttpCookie cookieTempID = Request.Cookies["tempID"];

            //Console.Write(loadCookie.Values["tempID"]);

            if (cookieTempID == null)
            {
                string tempID = Utility.getUniqueID();

                HttpCookie saveCookieTempID = new HttpCookie("tempID");
                saveCookieTempID.Value = tempID;
                Response.Cookies.Add(saveCookieTempID);
                tempOrderID = tempID;
            }
            else
            {
                if (cookieTempID.Value != null)
                {
                    tempOrderID = cookieTempID.Value;
                }
            }



            response = await addCartAndCount(tempOrderID, product_customize_id, product_name, quantity, "0", product_unit_name);

            HttpCookie cookieCartCount = Request.Cookies["cartCount"];

            HttpCookie saveCookieCartCount = new HttpCookie("cartCount");
            saveCookieCartCount.Value = response.Result;
            Response.Cookies.Add(saveCookieCartCount);

            //if (cookieCartCount == null)
            //{
                
            //}
            //else
            //{

            //}

            //Session["cartCount"] = response.Result;



            return Redirect(returnUrl);

            //if (response.Result == "1")
            //{
            //    Session["cartCount"] = response.Result;                                
            //    return RedirectToAction("Index", "Mart");
            //}
            //else
            //{

            //    return View();

            //}


        }

    }
}