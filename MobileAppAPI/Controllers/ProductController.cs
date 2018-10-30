using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MobileAppAPI.Models.Admin;
using MobileAppAPI.Models;
using System.Web.Helpers;
using System.Web.Mvc;
using MobileAppAPI.Models.Product;
using static MobileAppAPI.Models.Product.ProductDetails;
using System.Web.Http;
using System.Net;
using System.Net.Http;

namespace MobileAppAPI.Controllers
{
    [System.Web.Http.Authorize]
    public class ProductController : ApiController
    {
        ProductDetails produtDetail = new ProductDetails();
        Status status = new Models.Status();
        //asdsas
        public IHttpActionResult GetProductList()
        {
          try
            {
               List<Product> produtResult = produtDetail.GetProductList();

                if (produtResult.Count>0)
                {
                    status.code = 200;
                    status.message = "success";
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, new { status = status, result = produtResult }));
                }
                else
                {
                    status.code = 201;
                    status.message = "Product Not Available?";
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound, new { status = status}));
                }
            }
            catch (Exception ex)
            {
                status.code = 500;
                status.message = ex.Message.ToString();
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, new { status = status }));
            }

        }
        public IHttpActionResult GetProductCategory(int productid)
        {
            try
            {
                List<ProductCategory> produtCategory = produtDetail.GetProductCategory(productid);

                if (produtCategory.Count > 0)
                {
                    status.code = 200;
                    status.message = "success";
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, new { status = status, result = produtCategory }));
                }
                else
                {
                    status.code = 201;
                    status.message = "Product Category Not Available?";
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound, new { status = status }));
                }
            }
            catch (Exception ex)
            {
                status.code = 500;
                status.message = ex.Message.ToString();
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, new { status = status }));
            }

        }

        [System.Web.Http.HttpPost]
        public IHttpActionResult PostOrderItems([FromBody] Order order)
        {
            try
            {
                //List<ProductCategory> produtCategory = produtDetail.GetProductCategory(productid);

                if (Convert.ToInt16(order.UserId) > 0)
                {
                    status.code = 200;
                    status.message = "success";
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, new { status = status, result = order }));
                }
                else
                {
                    status.code = 201;
                    status.message = "Order Not Save?";
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound, new { status = status }));
                }
            }
            catch (Exception ex)
            {
                status.code = 500;
                status.message = ex.Message.ToString();
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, new { status = status }));
            }

        }
        public IHttpActionResult GetSubscription()
        {
            try
            {
                List<ProductCategory> produtCategory = produtDetail.GetSubscription();

                if (produtCategory.Count > 0)
                {
                    status.code = 200;
                    status.message = "success";
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, new { status = status, result = produtCategory }));
                }
                else
                {
                    status.code = 201;
                    status.message = "Subscription Not Available?";
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound, new { status = status }));
                }
            }
            catch (Exception ex)
            {
                status.code = 500;
                status.message = ex.Message.ToString();
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, new { status = status }));
            }

        }

        [System.Web.Http.HttpPost]
        public IHttpActionResult SaveSubsActivated([FromBody] SubscriptionActivated subActivated)
        {
            try
            {
                int AffectedRow = produtDetail.SaveSubsActivated(subActivated);

                if (AffectedRow > 0) 
                {
                    status.code = 200;
                    status.message = "success";
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, new { status = status, result = subActivated }));
                }
                else
                {
                    status.code = 201;
                    status.message = "Subscription Not Saved?";
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound, new { status = status }));
                }
            }
            catch (Exception ex)
            {
                status.code = 500;
                status.message = ex.Message.ToString();
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, new { status = status }));
            }

        }
        public IHttpActionResult GetUserSubscription(int userId)
        {
            try
            {
                List<SubscriptionActivated> lstsubActivated = produtDetail.GetUserSubscription(userId);

                if (lstsubActivated.Count > 0)
                {
                    status.code = 200;
                    status.message = "success";
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, new { status = status, result = lstsubActivated }));
                }
                else
                {
                    status.code = 201;
                    status.message = "Subscription Not Available?";
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound, new { status = status }));
                }
            }
            catch (Exception ex)
            {
                status.code = 500;
                status.message = ex.Message.ToString();
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, new { status = status }));
            }

        }
    }
}