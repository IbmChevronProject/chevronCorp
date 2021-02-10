using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
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
    public static class getInterviewInformationForCSV
    {
        [FunctionName("getInterviewInformationForCSV")]
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

                    // Defining serviceLineID to get Interview details from the corresponding ServiceLine
                    var serviceLineId = req.Query["serviceLineId"];

                    // Prepare the SQL Query
                    var query = $"select InterviewName,InterviewDate,Firstname +' ' +LastName as PanelListName,UserName,Slot_A,Slot_B,Slot_C,Slot_D from [dbo].[InterviewDetails] a inner join PanelList_Availability b  on a.InterviewId=b.InterviewId left outer join Userdetails c on b.panellistId=c.userid  and Availability='true' where InterviewDate between '02/09/2021' and '03/14/2021'";
                    // Prepare the SQL command and execute query
                    SqlCommand command = new SqlCommand(query, connection);

                    // Open the connection, execute and close connection
                    if (command.Connection.State == System.Data.ConnectionState.Open)
                    {
                        command.Connection.Close();
                    }
                    command.Connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    var results = new List<Dictionary<string, object>>();
                    var cols = new List<string>();
                    for (var i = 0; i < reader.FieldCount; i++)
                    {
                        var colName = reader.GetName(i);
                        var camelCaseName = Char.ToLowerInvariant(colName[0]) + colName.Substring(1);
                        cols.Add(camelCaseName);
                    }

                    while (reader.Read())
                        results.Add(SerializeRow(cols, reader));

                    string jsonResult = JsonConvert.SerializeObject(results, Formatting.Indented);
                    jsonResult = jsonResult.Replace(@"\", "");
                    return new OkObjectResult(jsonResult);
                }
            }
            catch (Exception e)
            {
                log.LogError(e.ToString());
            }
            return new OkObjectResult(responseMessage);
        }

        private static Dictionary<string, object> SerializeRow(IEnumerable<string> cols, SqlDataReader reader)
        {
            var result = new Dictionary<string, object>();
            foreach (var col in cols)
                result.Add(col, reader[col]);
            return result;
        }
    }
}
