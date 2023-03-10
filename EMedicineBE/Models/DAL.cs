using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace EMedicineBE.Models
{
    public class DAL
    {
        public Response register(Users users, SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("sp_register", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FirstName", users.FirstName);
            cmd.Parameters.AddWithValue("@LastName", users.LastName);
            cmd.Parameters.AddWithValue("@Password", users.Password);
            cmd.Parameters.AddWithValue("@Email", users.Email);
            cmd.Parameters.AddWithValue("@Fund", 0);
            cmd.Parameters.AddWithValue("@Type", "Users");
            cmd.Parameters.AddWithValue("@Status", "Pending");

            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();

            if(i> 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "User Registered Succefully";
            }else
            {
                response.StatusCode = 100;
                response.StatusMessage = "User Registered Failed";
            }


            return response;
        }

        public Response login(Users users, SqlConnection connection)
        {
            SqlDataAdapter da = new SqlDataAdapter("sp_login", connection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@Email", users.Email);
            da.SelectCommand.Parameters.AddWithValue("@Password", users.Password);
            DataTable dt = new DataTable();
            da.Fill(dt);
            Response response = new Response();
            if(dt.Rows.Count > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "User is Valid";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "User is Invalid";
            }

            return response;
        }
    }


}
