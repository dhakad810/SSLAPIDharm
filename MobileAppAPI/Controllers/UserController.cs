using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using MobileAppAPI.Models.Admin;
using MobileAppAPI.Models;
using System.Web.Helpers;

namespace MobileAppAPI.Controllers
{
    public class UserController : ApiController
    {
        UserLogin userLogin = new UserLogin();
        Status status = new Models.Status();

        [HttpPost]
        [ActionName("NewUser")]
        public HttpResponseMessage NewUser([FromBody] UserRegistration user)
        {
            try
            {
                int i = 0;
                commonEncryption commonEncrpt = new commonEncryption();
                UserRegistration URD = new UserRegistration();
                URD.UserMobileNo = user.UserMobileNo;
                URD.UserAddress = user.UserAddress;
                URD.UserName = user.UserName;
                URD.UserPassWord = commonEncrpt.Encrypt(user.UserPassWord);
                URD.EmailId = user.EmailId;
                URD.DeviceID = user.DeviceID;
                URD.ModelNo = user.ModelNo;
                i = userLogin.SaveUserDetail(URD);

                if (i > 0)
                {
                    status.code = 200;
                    status.message = "Success";
                    return Request.CreateResponse(HttpStatusCode.OK, new { status = status, result = URD });

                }
                else
                {
                    status.code = 201;
                    status.message = "Mobile No Already Exists";
                    return Request.CreateResponse(HttpStatusCode.OK, new { status = status, result = URD });
                }
            }
            catch (Exception ex)
            {
                // Log exception code goes here 
                status.code = 500;
                status.message = ex.Message.ToString();
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { status = status });
            }
        }

        [HttpGet]
        [ActionName("LoginUser")]
        public HttpResponseMessage LoiginUser([FromUri] LoginUser user)
        {
            try
            {
                commonEncryption commonEncrpt = new commonEncryption();
                LoginUser LoginU = new LoginUser();
                LoginU.UserMobileNo = user.UserMobileNo;
                LoginU.Password = commonEncrpt.Encrypt(user.Password);

                UserRegistration userRegistration = userLogin.CheckLoginDetail(LoginU);

                if (userRegistration.UserName != null)
                {
                    status.code = 200;
                    status.message = "Success";
                    return Request.CreateResponse(HttpStatusCode.OK, new { status = status, result = userRegistration });
                }
                else
                {
                    status.code = 201;
                    status.message = "Invalid UserName and Password";
                    return Request.CreateResponse(HttpStatusCode.OK, new { status = status, result = userRegistration });
                }

            }
            catch (Exception ex)
            {
                // Log exception code goes here  
                status.code = 500;
                status.message = ex.Message.ToString();
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { status = status });
            }
        }

        [HttpGet]
        [ActionName("GetUserDetail")]
        public IHttpActionResult GetUserDetail(string mobileno)
        {
            List<UserRegistration> userResult = new List<UserRegistration>();
                userResult = userLogin.GetUserDetail(mobileno);
            try
            {
               

                //if (userResult.UserName != null)
                //{
                //    //return Ok(lstResult1);

                //    status.code = 200;
                //    status.message = "success";
                   
                //    return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, new {userResult }));
                //}
                //else
                //{
                //    status.code = 201;
                //    status.message = "Details Not Available?";
                //    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound, new { status = status, result = userResult }));
                //}
            }
            catch (Exception ex)
            {
                status.code = 500;
                status.message = ex.Message.ToString();
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, new { status = status }));
            }
           return Ok(userResult);
        }

        [HttpGet]
        [ActionName("GetAllUserDetail")]
        public IHttpActionResult GetAllUserDetail()
        {
            List<UserRegistration> userResult = new List<UserRegistration>();
            userResult = userLogin.GetAllUserDetail();
            try
            {
            }
            catch (Exception ex)
            {
                status.code = 500;
                status.message = ex.Message.ToString();
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, new { status = status }));
            }
            return Ok(userResult);
        }
    }
}


  