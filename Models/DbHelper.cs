using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ESCMS.Models;

namespace ESCMS.Models
{
    public class DbHelper
    {
        string connectionString = "Integrated Security=true;Persist Security Info=False;Initial Catalog=ESCMS2;Data Source=DESKTOP-GOPM6K4\\SQLEXPRESS";

        //To View all employees details  
        public IEnumerable<ProductModel> GetAllProducts()
        {
            List<ProductModel> lstproduct = new List<ProductModel>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("GetProducts", con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    ProductModel product = new ProductModel();

                    product.ProductId = Convert.ToInt32(rdr["ProductId"]);
                    product.ProductName = rdr["ProductName"].ToString();
                   

                    lstproduct.Add(product);
                }
                con.Close();
            }
            return lstproduct;
        }

        //To Add new Product record  
        public void AddProduct(ProductModel productModel)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("InsertProduct", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProductId", productModel.ProductId);

                cmd.Parameters.AddWithValue("@ProductName", productModel.ProductName);
               

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        //To Update the records of a particluar product
        public void UpdateEmployee(ProductModel productModel)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("UpdateProduct", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ProductId", productModel.ProductId);
                cmd.Parameters.AddWithValue("@ProductName", productModel.ProductName);
               

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        //Get the details of a particular product
        public ProductModel GetEmployeeData(int? id)
        {
            ProductModel employee = new ProductModel();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT * FROM tblEmployee WHERE EmployeeID= " + id;
                SqlCommand cmd = new SqlCommand(sqlQuery, con);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    employee.ProductId = Convert.ToInt32(rdr["ProductId"]);
                    employee.ProductName = rdr["ProductName"].ToString();
                   
                }
            }
            return employee;
        }

        //To Delete the record on a particular Product
        public void DeleteEmployee(int? ProductId)
        {

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("DeleteProduct", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ProductId", ProductId);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        internal void UpdateProduct(ProductModel productmodel)
        {
            throw new NotImplementedException();
        }

        internal ProductModel GetProductData(int? productId)
        {
            throw new NotImplementedException();
        }
    }
}