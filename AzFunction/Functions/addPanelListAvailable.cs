using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;


namespace Company.Function
{
    public static class addPanelListAvailable
    {
        [FunctionName("addPanelListAvailable")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            string responseMessage = "This HTTP triggered function executed successfully";
            string connectionString = "Server=tcp:uc1.database.windows.net,1433;Initial Catalog=RoasterDb;Persist Security Info=False;User ID=adminuser;Password=Admin@123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            // Using the connection string to open a connection
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Opening a connection
                    connection.Open();

                    // Defining the  form details
                    var InterviewId = req.Query["InterviewId"];                    
                    var PanelListId = req.Query["PanelListId"];
					var Availability = req.Query["Availability"];
                    var Slot_A = req.Query["Slot_A"];                    
                    var Slot_B = req.Query["Slot_B"];
					var Slot_C = req.Query["Slot_C"];
                    var Slot_D = req.Query["Slot_D"];
					var Remarks = req.Query["Remarks"];


                    // Prepare the SQL Query
                    var query = $"INSERT INTO PanelList_Availability (PanelListId,InterviewId,Availability,Slot_A,Slot_B,Slot_C,Slot_D,Remarks) VALUES('{PanelListId}', '{InterviewId}', '{Availability}', '{Slot_A}','{Slot_B}', '{Slot_C}', '{Slot_D}', '{Remarks}')";

                    // Prepare the SQL command and execute query
                    SqlCommand command = new SqlCommand(query, connection);

                    // Open the connection, execute and close connection
                    if (command.Connection.State == System.Data.ConnectionState.Open)
                    {
                        command.Connection.Close();
                    }
                    command.Connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                log.LogError(e.ToString());
            }
            return new OkObjectResult(responseMessage);
        }
    }
}
