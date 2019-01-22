using BOL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class LoginBuissnessLayer
    {

        public Customer customers(Login cust)
        {
                string ConnectionString = ConfigurationManager.ConnectionStrings["CustomerContext"].ConnectionString;
            //List<Customer> customers = new List<Customer>();
            Customer person = new Customer();
            bool flag = false;
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    SqlCommand command = new SqlCommand("sptbl_LoginCheck", con);
                    command.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    SqlParameter paramEmail = new SqlParameter();
                    paramEmail.ParameterName = "@CustEmail";
                    paramEmail.Value = cust.Email;
                    command.Parameters.Add(paramEmail);

                    SqlParameter paramPassword = new SqlParameter();
                    paramPassword.ParameterName = "@CustPassword";
                    paramPassword.Value = cust.password;
                    command.Parameters.Add(paramPassword);

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        
                       flag = true;
                        person.CustomerID = Convert.ToInt32(reader["CustomerID"].ToString());
                        person.FirstName = reader["FirstName"].ToString();
                        person.LastName = reader["LastName"].ToString();
                        person.Email = reader["CustEmail"].ToString();
                        person.Address = reader["CustAddress"].ToString();
                         person.IsValidUser = Convert.ToInt32(reader["IsValidUser"].ToString());
                         person.RoleId = Convert.ToInt32(reader["RoleId"].ToString());
                  }
                }

            if (flag)
            {
                return person;
            }
            else {
                return null;
            }
           
        }
    }
}
