using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TestRail_Integration.Utils
{
    public static class TestRailManager
    {
        // Configuration (Update these values)
        public static string TestRunId = "100007";
        public static string TestRailUserName = "a.bakar@a1qa.com";
        public static string TestRailPassword = Environment.GetEnvironmentVariable("TR_PASS");
        public static string TestRailEngineUrl = "https://tr.a1qa.com";
        public static int TestCasePassStatus = 1;
        public static int TestCaseFailStatus = 5;

        private static readonly HttpClient httpClient = new HttpClient();

        static TestRailManager()
        {
            var authToken = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{TestRailUserName}:{TestRailPassword}"));
            httpClient.DefaultRequestHeaders.Authorization = 
                new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", authToken);
        }

        public static async Task AddResultsForTestCase(string testCaseId, int status, string resultText)
        {
            try
            {
                // Validate numeric IDs
                if (!int.TryParse(testCaseId, out int caseId) || 
                    !int.TryParse(TestRunId, out int runId))
                {
                    throw new ArgumentException("Invalid Case ID or Run ID");
                }

                // API endpoint
                var endpoint = $"{TestRailEngineUrl}/index.php?/api/v2/add_result_for_case/{runId}/{caseId}";
                
                // Request data
                var data = new 
                { 
                    status_id = status, 
                    comment = $"Test Result: {resultText}" 
                };

                // Send request
                var jsonContent = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(endpoint, jsonContent);
                
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"TestRail API Error: {errorContent}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to send results to TestRail: {ex.Message}");
                throw;
            }
        }
    }
}