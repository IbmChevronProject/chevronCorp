using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Company.Function
{
    public static class getSLEmail
    {
        [FunctionName("getSLEmail")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {

            {
                string sqlResult;
                string responseMessage = "This HTTP triggered function executed successfully";
                string connectionString = "Server=tcp:uc1.database.windows.net,1433;Initial Catalog=RoasterDb;Persist Security Info=False;User ID=adminuser;Password=Admin@123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
                // Using the connection string to open a connection
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        // Opening a connection
                        connection.Open();

                        // Defining ServiceLineId to get Smail
                        var serviceLineId = req.Query["serviceLineId"];

                        // Prepare the SQL Query
                        var query = $"SELECT [SlEmail] FROM [ServiceLine] WHERE [ServiceLineId] = '{serviceLineId}'";
                        // Prepare the SQL command and execute query
                        SqlCommand command = new SqlCommand(query, connection);

                        // Open the connection, execute and close connection
                        if (command.Connection.State == System.Data.ConnectionState.Open)
                        {
                            command.Connection.Close();
                        }
                        command.Connection.Open();
                        string result = command.ExecuteScalar().ToString();
                        sqlResult = String.Format("{0}", result);
                        return new OkObjectResult(sqlResult);
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
}
