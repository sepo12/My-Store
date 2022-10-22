using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace MyStore.Pages.Clients
{
    public class ClientsModel : PageModel
    {
        public ClientInfo clientInfo = new ClientInfo();
        public String errorMessage="";
        public String successMessage="";
        public void OnGet()
        {
        }
        public void OnPost()
        {

            clientInfo.name = Request.Form["name"];
            clientInfo.email = Request.Form["phone"];
            clientInfo.phone = Request.Form["email"];
            clientInfo.address = Request.Form["address"];

            if(clientInfo.name.Length==0 || clientInfo.phone.Length==0 || clientInfo.address.Length==0 || clientInfo.email.Length==0)
            {
                errorMessage = "All fields required";
                return;
            }

            //save data to database
            try
            {
                String connectionString= "Data Source=localhost;Initial Catalog=Mystore;Integrated Security=True";
                using(SqlConnection connection=new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO clients " +
                        "(name,phone,email,address) VALUES" +
                        "(@name,@phone,@email,@address);";
                               
                    using (SqlCommand command = new SqlCommand (sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", clientInfo.name);
                        command.Parameters.AddWithValue("@phone", clientInfo.email);
                        command.Parameters.AddWithValue("@email", clientInfo.phone);
                        command.Parameters.AddWithValue("@address", clientInfo.address);

                        command.ExecuteNonQuery();
                    }

                }
            }
            catch(Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }


            clientInfo.name = ""; clientInfo.email = ""; clientInfo.address = ""; clientInfo.phone = "";
            successMessage = "New client Added Succesfully";
            Response.Redirect("/Clients/Index1");


        }
    }
}
