using System;
using System.IO;
using System.Collections;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace Company.Function
{
    public static class getInterviewInformationForCSV
    {
        [FunctionName("getInterviewInformationForCSV")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            ArrayList sqlResult = new ArrayList();
            string jsonResult = null;
            string responseMessage = "This HTTP triggered function executed successfully";
            string connectionString = "Server=tcp:uc1.database.windows.net,1433;Initial Catalog=RoasterDb;Persist Security Info=False;User ID=adminuser;Password=Admin@123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

            // Using the connection string to open a connection
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Opening a connection
                    connection.Open();

                    // Defining serviceLineID to get Interview details from the corresponding ServiceLine
                    var serviceLineId = req.Query["serviceLineId"];

                    // Prepare the SQL Query
                    var query = $"SELECT InterviewName, InterviewDate, Description from [InterviewDetails] FOR JSON AUTO";
                    // Prepare the SQL command and execute query
                    SqlCommand command = new SqlCommand(query, connection);

                    // Open the connection, execute and close connection
                    if (command.Connection.State == System.Data.ConnectionState.Open)
                    {
                        command.Connection.Close();
                    }
                    command.Connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Object[] rows = new Object[reader.FieldCount];

                        // Get the Row with all its column values..
                        reader.GetValues(rows);

                        // Add this Row to ArrayList.
                        sqlResult.Add(rows);

                        jsonResult = JsonSerializer.Serialize(sqlResult);
                    }
                    return new OkObjectResult(jsonResult);
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
