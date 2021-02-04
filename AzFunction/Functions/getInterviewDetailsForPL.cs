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
    public static class getInterviewDetailsForPL
    {
        [FunctionName("getInterviewDetailsForPL")]
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

                    // Defining Parameters
                    var InterviewId = req.Query["InterviewId"];

                    // Prepare the SQL Query
                    var query = $"select InterviewId,InterviewName,FORMAT (FromDate, 'dd/MM/yyyy ') as FromDate ,FORMAT (ToDate, 'dd/MM/yyyy ') as ToDate,a.SkillId,SkillName from InterviewDetails a inner join Skills b on a.SkillId=b.SkillId where Fromdate >getdate() and a.InterviewId ='{InterviewId}'";
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
                        Console.WriteLine(String.Format("{0},{1},{2},{3},{4},{5}", reader[0],reader[1],reader[2],reader[3],reader[4],reader[5]));
                        sqlResult.Add(String.Format("{0},{1},{2},{3},{4},{5}", reader[0],reader[1],reader[2],reader[3],reader[4],reader[5]));
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