using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Company.Function
{
    public static class fillInterviewDetails
    {
        [FunctionName("fillInterviewDetails")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            List<string> sqlResult = new List<string>();
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
                    var query = $"select InterviewId,InterviewName,FORMAT (InterviewDate, 'dd/MM/yyyy ') as InterviewDate ,a.SkillId,SkillName from InterviewDetails a inner join Skills b on a.SkillId=b.SkillId where InterviewDate >getdate() and a.ServiceLineId ='{serviceLineId}'";
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
                        Console.WriteLine(String.Format("{0},{1},{2},{3},{4}", reader[0],reader[1],reader[2],reader[3],reader[4]));
                        sqlResult.Add(String.Format("{0},{1},{2},{3},{4}", reader[0],reader[1],reader[2],reader[3],reader[4]));
                    }
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
