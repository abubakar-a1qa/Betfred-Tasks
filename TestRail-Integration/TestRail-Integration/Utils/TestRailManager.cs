using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json.Linq;
using TestRail_Integration.Gurock.TestRail;

namespace TestRail_Integration.Utils
{
    public static class TestRailManager
    {
        public static string TestRunId = "100007";
        public static string TestRailUserName = "a.bakar@a1qa.com";
        public static string TestRailPassword = Environment.GetEnvironmentVariable("TR_PASS");
        public static string TestRailEngineUrl = "https://tr.a1qa.com";
        public static int TestCasePassStatus = 1;
        public static int TestCaseFailStatus = 5;

        private static APIClient GetClient()
        {
            return new APIClient(TestRailEngineUrl)
            {
                User = TestRailUserName,
                Password = TestRailPassword
            };
        }

        public static void AddResultsForTestCase(
            string testCaseId,
            int status,
            string resultText,
            string screenshotPath)
        {
            var client = GetClient();
            
            try
            {
                // Send test result
                var result = (JObject)client.SendPost(
                    $"add_result_for_case/{TestRunId}/{testCaseId}",
                    new Dictionary<string, object>
                    {
                        {"status_id", status},
                        {"comment", $"Test execution completed. {resultText}"}
                    }
                );

                // Extract the result ID from the response
                var resultId = result["id"].ToString();

                // Attach screenshot if exists
                if (!string.IsNullOrEmpty(screenshotPath) && File.Exists(screenshotPath))
                {
                    client.SendPost(
                        $"add_attachment_to_result/{resultId}",
                        screenshotPath
                    );
                }
            }
            catch (APIException ex)
            {
                Console.WriteLine($"TestRail API Error: {ex.Message}");
                throw;
            }
        }
    }
}