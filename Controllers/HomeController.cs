using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibraryTradeMarket;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TradeMarket.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Data()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Login()
        {
            return View();
        }


        private async void PostLosgin(string account,string password)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://220.130.10.50:6868");
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("account", account),
                new KeyValuePair<string, string>("password", Utility.getSecretCode(password))
            });
            var result = await client.PostAsync("/api/WebApi/validateMember", content);
            string resultContent = await result.Content.ReadAsStringAsync();
            Console.WriteLine(resultContent);

            
            //HttpResponseMessage response = await client.PostAsync("http://220.130.10.50:6868/api/WebApi/isLogin","");
            //response.EnsureSuccessStatusCode();
            //string responseBody = await response.Content.ReadAsStringAsync();
            //ShowResult(JsonConvert.DeserializeObject<List<Product>>(responseBody));
        }

        public async Task<ApiResponseData> getLoginValidate(string account,string password)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://220.130.10.50:6868");
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("account", account),
                new KeyValuePair<string, string>("password", Utility.getSecretCode(password))
            });
            var result = await client.PostAsync("/api/WebApi/validateMember", content);
            string resultJson = await result.Content.ReadAsStringAsync();

            ApiResponseData response = (ApiResponseData)JsonConvert.DeserializeObject(resultJson, typeof(ApiResponseData));

            return response;

        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(string account ,string password, string returnUrl = "/Home/Login")
        {

            Business business = new Business();

            MemberViewModel member = new MemberViewModel();

            ApiResponseData response = new ApiResponseData();

            response = await getLoginValidate(account, password);
            
            //PostLosgin(account, password);


            //HttpClient client = new HttpClient();
            //client.BaseAddress = new Uri("http://220.130.10.50:6868");
            //var content = new FormUrlEncodedContent(new[]
            //{
            //    new KeyValuePair<string, string>("account", account),
            //    new KeyValuePair<string, string>("password", Utility.getSecretCode(password))
            //});
            //var result = await client.PostAsync("/api/WebApi/validateMember", content);
            //string resultJson = await result.Content.ReadAsStringAsync();

            //ClassApiResponseData response = (ClassApiResponseData)JsonConvert.DeserializeObject(resultJson, typeof(ClassApiResponseData));



            //using (WebClient client = new WebClient() { Encoding = Encoding.UTF8 })
            //{
            //    //client.UploadStringCompleted += client_UploadStringCompleted;
            //    client.Headers[HttpRequestHeader.ContentType] = "application/json";

            //    ClassData data = new ClassData() { Param1 = "value1", Param2 = "value2" };

            //    string json = JsonConvert.SerializeObject(data);

            //    client.UploadStringAsync(new Uri(API_HOST + PostNews), json);
            //}

            //using (var client = new HttpClient())
            //{
            //    client.BaseAddress = new Uri("http://220.130.10.50:6868");
            //    client.DefaultRequestHeaders.Accept.Clear();
            //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //    var response = client.GetAsync("api/WebApi/isLogin").Result;
            //    if (response.IsSuccessStatusCode)
            //    {
            //        string responseString = response.Content.ReadAsStringAsync().Result;
            //    }
            //}
            

            if (response.Result=="1")
            {
                Session["account"] = member.Account;
                Session["name"] = member.Name;
                Session["role"] = member.Role;
                //Session["department_id"] = 

                switch (member.Role)
                {
                    //buyer
                    case 1:
                        return RedirectToAction("Index", "Mart");                        
                    case 2:
                        return RedirectToAction("Index", "Mart");
                    default:
                        break;
                }



                //role 3 admin,role 1 seller, role 2 buyer

                
            }
            else
            {
                ModelState.AddModelError("", "帳號或密碼錯誤");
                return View();
                
            }

            //return View();

            //if (!ModelState.IsValid)
            //{
            //    return View(model);
            //}

            // 這不會計算為帳戶鎖定的登入失敗
            // 若要啟用密碼失敗來觸發帳戶鎖定，請變更為 shouldLockout: true
            //var result = await SignInManager.PasswordSignInAsync(model, model.Password, model.RememberMe, shouldLockout: false);




            //BillingEntities db = new BillingEntities();

            //using (var db = new BillingEntities())
            //{
            //    // Query for all blogs with names starting with B 
            //    var admins = from b in db.admins
            //                 where b.account == model.Account && b.password == model.Password
            //                 select b;

            //    var admin = admins
            //        .FirstOrDefault();

            //    if (admin != null)
            //    {
            //        if (admin.account == model.Account && admin.password == model.Password)
            //        {
            //            //ModelState.AddModelError("", "登入成功");
            //            Session["account"] = model.Account;
            //            Session["user_id"] = admin.id;
            //            //Session["department_id"] = 
            //            return RedirectToAction("Index", "BackOffice");
            //        }
            //        else
            //        {
            //            ModelState.AddModelError("", "帳號或密碼錯誤");
            //            return View();
            //        }
            //    }
            //    else
            //    {
            //        ModelState.AddModelError("", "帳號或密碼錯誤");
            //        return View();
            //    }


            //}








        }

    }
}