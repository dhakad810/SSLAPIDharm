using MobileAppAPI.Models.Admin;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MobileAppAPI.Models.Product
{
    public class Product
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
    }
    public class ProductCategory
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Quantity { get; set; }
        public string QuantityType { get; set; }
        public string Price { get; set; }
        public List<MonthlySubscription> MonthSubscription { get; set; }
    }
    public class MonthlySubscription
    {
        public string Month { get; set; }
        public string SubscriptionPrice { get; set; }
    }
    public class SubscriptionActivated
    {
        public int? UserId { get; set; }
        public int? ProductId { get; set; }
        public string ProductName { get; set; }
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int? Quantity { get; set; }
        public int? Month { get; set; }
        public float? TotalSubscriptionPrice { get; set; }
        public string PaymentType { get; set; }
    }
    public class Order
    {
        public string UserId { get; set; }
        public string OrderAddress { get; set; }
        public int? OrderItem { get; set; }
        public string OrderStatus { get; set; }
        public float? TotalOrderPrice { get; set; }
        public string PaymentType { get; set; }
        public List<OrderItems> OrderItems { get; set; }
    }
    public class OrderItems
    {
        public string ItemName { get; set; }
        public int? ItemQuantity { get; set; }
        public string TotalItemPrice { get; set; }
        public int? UserRating { get; set; }
    }
    public class ProductDetails : DBConnection
    {
        public List<Product> GetProductList()
        {
            List<Product> lstProduct = new List<Product>();

            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(strConString))
            {
                SqlCommand cmd = new SqlCommand("ProcGetInsertProductDetails", con);
                cmd.Parameters.Add(new SqlParameter("@Action", "GetProduct"));
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    Product product = new Product();
                    product.ProductId = dt.Rows[i]["ProductId"].ToString();
                    product.ProductName = dt.Rows[i]["ProductName"].ToString();
                    lstProduct.Add(product);
                }
            }
            return lstProduct;
        }
        public List<ProductCategory> GetProductCategory(int productid)
        {
            List<ProductCategory> lstProductCat = new List<ProductCategory>();

            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(strConString))
            {
                SqlCommand cmd = new SqlCommand("ProcGetInsertProductDetails", con);
                cmd.Parameters.Add(new SqlParameter("@Action", "GetProductCategory"));
                cmd.Parameters.Add(new SqlParameter("@ProductId", productid));
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    ProductCategory productCategory = new ProductCategory();
                    productCategory.ProductId = dt.Rows[i]["ProductId"].ToString();
                    productCategory.ProductName = dt.Rows[i]["ProductName"].ToString();
                    productCategory.CategoryId = dt.Rows[i]["CategoryId"].ToString();
                    productCategory.CategoryName = dt.Rows[i]["CategoryName"].ToString();
                    productCategory.Quantity = dt.Rows[i]["Quantity"].ToString();
                    productCategory.QuantityType = dt.Rows[i]["QuantityType"].ToString();
                    productCategory.Price = dt.Rows[i]["Price"].ToString();
                    lstProductCat.Add(productCategory);
                }
            }
            return lstProductCat;
        }

        public List<ProductCategory> GetSubscription()
        {
            List<ProductCategory> lstProductCat = new List<ProductCategory>();

            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(strConString))
            {
                SqlCommand cmd = new SqlCommand("ProcGetInsertProductDetails", con);
                cmd.Parameters.Add(new SqlParameter("@Action", "GetSuscription"));
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                {
                    int productid = 0, categoryid = 0;
                    ProductCategory productCategory = new ProductCategory();
                    productCategory.ProductId = ds.Tables[0].Rows[i]["ProductId"].ToString();
                    productCategory.ProductName = ds.Tables[0].Rows[i]["ProductName"].ToString();
                    productCategory.CategoryId = ds.Tables[0].Rows[i]["CategoryId"].ToString();
                    productCategory.CategoryName = ds.Tables[0].Rows[i]["CategoryName"].ToString();
                    productCategory.Quantity = ds.Tables[0].Rows[i]["Quantity"].ToString();
                    productCategory.QuantityType = ds.Tables[0].Rows[i]["QuantityType"].ToString();
                    productCategory.Price = ds.Tables[0].Rows[i]["Price"].ToString();

                    productid = Convert.ToInt16(ds.Tables[0].Rows[i]["ProductId"].ToString());
                    categoryid = Convert.ToInt16(ds.Tables[0].Rows[i]["CategoryId"].ToString());

                    if (productid != 0)
                    {
                        List<MonthlySubscription> lstSuscription = new List<MonthlySubscription>();
                        if (ds.Tables[1].Rows.Count > 0)
                        {
                            DataRow[] result = ds.Tables[1].Select("ProductId = " + productid + " AND CategoryId = " + categoryid);
                            //ds.Tables[1].DefaultView.RowFilter = "ProductId = " + productid + " AND CategoryId = " + categoryid;
                            //DataTable dt = (ds.Tables[1].DefaultView).ToTable();

                            foreach (DataRow row in result)
                            {
                                MonthlySubscription monthlySuscription = new MonthlySubscription();
                                monthlySuscription.Month = row["Month"].ToString();
                                monthlySuscription.SubscriptionPrice = row["SubscriptionPrice"].ToString();
                                lstSuscription.Add(monthlySuscription);
                                productCategory.MonthSubscription = lstSuscription;
                            }

                        }
                    }
                    lstProductCat.Add(productCategory);
                }

            }
            return lstProductCat;
        }

     public int SaveSubsActivated(SubscriptionActivated subActivated)
        {
            int RowAffected = 0;
            using (SqlConnection con = new SqlConnection(strConString))
            {
                SqlCommand cmd = new SqlCommand("ProcGetInsertProductDetails", con);
                cmd.Parameters.Add(new SqlParameter("@Action", "SubscriptionActivated"));
                cmd.Parameters.Add(new SqlParameter("@UserId", subActivated.UserId));
                cmd.Parameters.Add(new SqlParameter("@ProductId", subActivated.ProductId));
                cmd.Parameters.Add(new SqlParameter("@CategoryId ", subActivated.CategoryId));
                cmd.Parameters.Add(new SqlParameter("@Quantity", subActivated.Quantity));
                cmd.Parameters.Add(new SqlParameter("@Month", subActivated.Month));
                cmd.Parameters.Add(new SqlParameter("@TotalSubscriptionPrice", subActivated.TotalSubscriptionPrice));
                cmd.Parameters.Add(new SqlParameter("@PaymentType", subActivated.PaymentType));
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                RowAffected = cmd.ExecuteNonQuery();
                cmd.Dispose();
                con.Close();
            }
            return RowAffected;
        }
     public List<SubscriptionActivated> GetUserSubscription(int useId)
        {
            List<SubscriptionActivated> lstsubActivated = new List<SubscriptionActivated>();

            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(strConString))
            {
                SqlCommand cmd = new SqlCommand("ProcGetInsertProductDetails", con);
                cmd.Parameters.Add(new SqlParameter("@Action", "GetUserSubscription"));
                cmd.Parameters.Add(new SqlParameter("@UserId", useId));
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    SubscriptionActivated SubActivated = new SubscriptionActivated();
                    SubActivated.UserId = Convert.ToInt16(dt.Rows[i]["UserId"].ToString());
                    SubActivated.ProductId = Convert.ToInt16(dt.Rows[i]["ProductId"].ToString());
                    SubActivated.ProductName = dt.Rows[i]["ProductName"].ToString();
                    SubActivated.CategoryId = Convert.ToInt16(dt.Rows[i]["CategoryId"].ToString());
                    SubActivated.CategoryName = dt.Rows[i]["CategoryName"].ToString();
                    SubActivated.TotalSubscriptionPrice = float.Parse(dt.Rows[i]["TotalSubscriptionPrice"].ToString());
                    SubActivated.Quantity = Convert.ToInt16(dt.Rows[i]["Quantity"].ToString());
                    SubActivated.Month = Convert.ToInt16(dt.Rows[i]["Month"].ToString());
                    SubActivated.PaymentType = dt.Rows[i]["PaymentType"].ToString();
                    lstsubActivated.Add(SubActivated);
                }
            }
            return lstsubActivated;
        }
    }
}