using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using BOL;

namespace DAL
{
    public class CustomerBuissnessLayer
    {

        public IEnumerable<Customer> persons
        {
            get
            {
                string ConnectionString = ConfigurationManager.ConnectionStrings["CustomerContext"].ConnectionString;
                List<Customer> persons = new List<Customer>();

                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    SqlCommand command = new SqlCommand("sptbl_GetAllDetails", con);
                    command.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Customer person = new Customer();
                        person.CustomerID = Convert.ToInt32(reader["CustomerID"].ToString());
                        person.FirstName = reader["FirstName"].ToString();
                        person.LastName = reader["LastName"].ToString();
                        person.Password = reader["CustPassword"].ToString();
                        person.ConfirmPassword = (reader["CustPassword"].ToString());
                        person.Address = reader["CustAddress"].ToString();
                        person.Email = reader["CustEmail"].ToString();
                        person.RoleId = Convert.ToInt32(reader["RoleId"].ToString());
                        persons.Add(person);
                    }
                }

                return persons;
            }

        }


        public Customer customers(int id)
        {
            
                string ConnectionString = ConfigurationManager.ConnectionStrings["CustomerContext"].ConnectionString;
                //Customer customers = new Customer();
                Customer person = new Customer();
              using (SqlConnection con = new SqlConnection(ConnectionString))
                  {
                    SqlCommand command = new SqlCommand("sptbl_GetCustomerDetails", con);
                    command.CommandType = CommandType.StoredProcedure;
                    con.Open();

                     SqlParameter paramCustomerID = new SqlParameter();
                      paramCustomerID.ParameterName = "@CustomerID";
                     paramCustomerID.Value = id;
                     command.Parameters.Add(paramCustomerID);

                   SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        
                        person.CustomerID = Convert.ToInt32(reader["CustomerID"].ToString());
                        person.FirstName = reader["FirstName"].ToString();
                        person.LastName = reader["LastName"].ToString();
                        person.Password = reader["CustPassword"].ToString();
                        person.ConfirmPassword = reader["CustPassword"].ToString();
                        person.Email = reader["CustEmail"].ToString();
                        person.Address = reader["CustAddress"].ToString();
                    
                    }
                }
                return person;
            
        }



        public void AddCustomers(Customer customer)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["CustomerContext"].ConnectionString;
            List<Customer> persons = new List<Customer>();

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand("sptbl_pranit_tblRegisterDetailsInsertCustomers", con);
                command.CommandType = CommandType.StoredProcedure;

                SqlParameter paramFirstName = new SqlParameter();
                paramFirstName.ParameterName = "@FirstName";
                paramFirstName.Value = customer.FirstName;
                command.Parameters.Add(paramFirstName);

                SqlParameter paramGender = new SqlParameter();
                paramGender.ParameterName = "@LastName";
                paramGender.Value = customer.LastName;
                command.Parameters.Add(paramGender);

                SqlParameter paramCity = new SqlParameter();
                paramCity.ParameterName = "@CustPassword";
                paramCity.Value = customer.Password;
                command.Parameters.Add(paramCity);

                SqlParameter paramCustEmail = new SqlParameter();
                paramCustEmail.ParameterName = "@CustEmail";
                paramCustEmail.Value = customer.Email;
                command.Parameters.Add(paramCustEmail);

                SqlParameter paramCustAddress = new SqlParameter();
                paramCustAddress.ParameterName = "@CustAddress";
                paramCustAddress.Value = customer.Address;
                command.Parameters.Add(paramCustAddress);

                SqlParameter paramIsValidUser = new SqlParameter();
                paramIsValidUser.ParameterName = "@IsValidUser";
                paramIsValidUser.Value = 1;
                command.Parameters.Add(paramIsValidUser);

                con.Open();
                command.ExecuteNonQuery();
            }
        }

        public void UpdatePerson(Customer customer)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["CustomerContext"].ConnectionString;
            List<Customer> persons = new List<Customer>();

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand("sptbl_pranit_tblRegisterDetailsUpdateCustomers", con);
                command.CommandType = CommandType.StoredProcedure;

                SqlParameter paramFirstName = new SqlParameter();
                paramFirstName.ParameterName = "@FirstName";
                paramFirstName.Value = customer.FirstName;
                command.Parameters.Add(paramFirstName);

                SqlParameter paramLastName = new SqlParameter();
                paramLastName.ParameterName = "@LastName";
                paramLastName.Value = customer.LastName;
                command.Parameters.Add(paramLastName);

                SqlParameter paramPassword = new SqlParameter();
                paramPassword.ParameterName = "@CustPassword";
                paramPassword.Value = customer.Password;
                command.Parameters.Add(paramPassword);

                SqlParameter paramCustEmail = new SqlParameter();
                paramCustEmail.ParameterName = "@CustEmail";
                paramCustEmail.Value = customer.Email;
                command.Parameters.Add(paramCustEmail);

                SqlParameter paramCustAddress = new SqlParameter();
                paramCustAddress.ParameterName = "@CustAddress";
                paramCustAddress.Value = customer.Address;
                command.Parameters.Add(paramCustAddress);

                SqlParameter paramCustomerID = new SqlParameter();
                paramCustomerID.ParameterName = "@CustomerID";
                paramCustomerID.Value = customer.CustomerID;
                command.Parameters.Add(paramCustomerID);

                con.Open();
                command.ExecuteNonQuery();
            }
        
}

        public void DeleteCustomer(int id)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["CustomerContext"].ConnectionString;
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand("sptbl_DeleteCustomer", con);
                command.CommandType = CommandType.StoredProcedure;

                SqlParameter paramId = new SqlParameter();
                paramId.ParameterName = "@CustomerId";
                paramId.Value = id;
                command.Parameters.Add(paramId);

                con.Open();
                command.ExecuteNonQuery();
            }
        }

        public void DeactivateCustomer(int id)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["CustomerContext"].ConnectionString;
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand("sptbl_DeactivateCustomer", con);
                command.CommandType = CommandType.StoredProcedure;

                SqlParameter paramId = new SqlParameter();
                paramId.ParameterName = "@CustomerId";
                paramId.Value = id;
                command.Parameters.Add(paramId);

                con.Open();
                command.ExecuteNonQuery();
            }
        }


    }


}
