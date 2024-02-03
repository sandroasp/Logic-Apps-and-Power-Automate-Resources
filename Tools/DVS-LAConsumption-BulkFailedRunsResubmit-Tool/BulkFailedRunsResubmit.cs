using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Azure.Management.Logic;
using Microsoft.Azure.Management.Logic.Models;
using Microsoft.Azure.Services.AppAuthentication;

namespace DVS_LogicAppBulkFailedRunsResubmit_POC
{
    public partial class BulkFailedRunsResubmit : Form
    {
        private List<LogicAppRun> failedRuns;

        public BulkFailedRunsResubmit()
        {
            InitializeComponent();
            InitializeListView();

        }

        private void InitializeListView()
        {
            listView1.View = View.Details;
            listView1.Columns.Add("Failed Runs", -2, HorizontalAlignment.Left);
            listView1.Columns.Add("Start Time", -2, HorizontalAlignment.Left);
        }


        private void aboutToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            using (AboutForm aboutForm = new AboutForm())
            {
                aboutForm.StartPosition = FormStartPosition.CenterParent;
                aboutForm.ShowDialog(this);
            }
        }

        private async void retrieveFailedRunsButton_Click(object sender, EventArgs e)
        {
            try
            {
                progressLabel.Text = "Searching in progress";

                string subscription = subscriptionRichTextBox.Text;
                string resourceGroup = resourceGroupRichTextBox.Text;
                string logicAppName = logicAppRichTextBox.Text;

                DateTime? startDate = dateTimePickerEarliest.Checked ? dateTimePickerEarliest.Value : (DateTime?)null;
                DateTime? endDate = dateTimePickerLatest.Checked ? dateTimePickerLatest.Value : (DateTime?)null;

                string accessToken = await GetAccessTokenAsync();

                failedRuns = await GetFailedLogicAppRunsAsync(subscription, resourceGroup, logicAppName, accessToken, startDate, endDate);

                if (failedRuns.Any())
                {
                    DisplayRunsInListView(failedRuns);
                    runCounter.Text = $"Runs Found: {failedRuns.Count}";
                }
                else
                {
                    MessageBox.Show("No failed runs found within the specified date range.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                progressLabel.Text = "Search completed";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }





        private async Task<List<LogicAppRun>> GetFailedLogicAppRunsAsync(string subscriptionId, string resourceGroupName, string logicAppName, string accessToken, DateTime? startDate, DateTime? endDate)
        {
            var failedRunsList = new List<LogicAppRun>();
            int batchSize = 250; // Adjust as needed
            string continuationToken = null;

            do
            {
                HttpResponseMessage response = await GetPageAsync(subscriptionId, resourceGroupName, logicAppName, accessToken, startDate, endDate, continuationToken, batchSize);

                try
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();

                        var rootObject = Newtonsoft.Json.JsonConvert.DeserializeObject<RootObject>(responseBody);

                        if (rootObject != null && rootObject.value != null)
                        {
                            var runsInPage = rootObject.value
                                .Where(run => (!startDate.HasValue || run.properties.startTime >= startDate) &&
                                              (!endDate.HasValue || run.properties.startTime <= endDate))
                                .Select(run => new LogicAppRun
                                {
                                    Name = run.name,
                                    Status = run.properties.status,
                                    StartTime = run.properties.startTime,
                                });

                            failedRunsList.AddRange(runsInPage);
                        }


                    }
                    else
                    {
                        string responseContent = await response.Content.ReadAsStringAsync();
                        LogMessage($"Failed to retrieve data. Status code: {response.StatusCode}. Response content: {responseContent}");
                        throw new Exception($"Failed to retrieve data. Status code: {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    LogMessage($"Failed to retrieve data. Status code: {response.StatusCode}. Response content: {responseContent}");
                }

            } while (!string.IsNullOrEmpty(continuationToken));

            return failedRunsList;
        }




        private async Task<HttpResponseMessage> GetPageAsync(string subscriptionId, string resourceGroupName, string logicAppName, string accessToken, DateTime? startDate, DateTime? endDate, string continuationToken, int batchSize)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

                string apiUrl;

                if (string.IsNullOrEmpty(continuationToken))
                {
                    apiUrl = $"https://management.azure.com/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.Logic/workflows/{logicAppName}/runs?api-version=2017-07-01&$filter=status eq 'Failed'&$top={batchSize}";
                }
                else
                {
                    apiUrl = $"https://management.azure.com/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.Logic/workflows/{logicAppName}/runs?api-version=2017-07-01&$filter=status eq 'Failed'&$top={batchSize}&$skiptoken={continuationToken}";
                }

                return await client.GetAsync(apiUrl);
            }
        }


        private async Task<string> GetAccessTokenAsync()
        {
            try
            {
                string resource = "https://management.azure.com/";

                var azureServiceTokenProvider = new AzureServiceTokenProvider();
                string accessToken = await azureServiceTokenProvider.GetAccessTokenAsync(resource);

                return accessToken;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to get access token: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        private void DisplayRunsInListView(List<LogicAppRun> failedRuns)
        {
            listView1.Items.Clear();

            foreach (var failedRun in failedRuns)
            {
                var formattedStartTime = GetFormattedStartTime(failedRun.StartTime);

                var listViewItem = new ListViewItem(new string[] { failedRun.Name, formattedStartTime });
                listView1.Items.Add(listViewItem);
            }
        }

        private string GetFormattedStartTime(DateTime? startTime)
        {
            return startTime.HasValue ? startTime.Value.ToString("yyyy-MM-dd HH:mm:ss tt") : "N/A";
        }

        private DateTime? ParseDate(string dateString)
        {
            DateTime parsedDate;
            return DateTime.TryParse(dateString, out parsedDate) ? parsedDate : (DateTime?)null;
        }



        private void LogMessage(string message)
        {
            Invoke(new Action(() => logRichTextBox.AppendText($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}\n")));
        }

        private async void resubmitButton_Click(object sender, EventArgs e)
        {
            try
            {
                progressLabel.Text = "Resubmit in progress";
                int successfullyResubmittedCount = 0;

                // Assuming HttpClient is already setup and authenticated
                HttpClient client = new HttpClient();
                string accessToken = await GetAccessTokenAsync();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

                foreach (var failedRun in failedRuns)
                {
                    try
                    {
                        // Step 1: Retrieve the Logic App definition to get trigger names
                        string logicAppDefinitionUrl = $"https://management.azure.com/subscriptions/{subscriptionRichTextBox.Text}/resourcegroups/{resourceGroupRichTextBox.Text}/providers/Microsoft.Logic/workflows/{logicAppRichTextBox.Text}?api-version=2019-05-01";
                        var definitionResponse = await client.GetAsync(logicAppDefinitionUrl);
                        definitionResponse.EnsureSuccessStatusCode();
                        string definitionContent = await definitionResponse.Content.ReadAsStringAsync();

                        // Step 2: Parse the Logic App definition to find a trigger name
                        string triggerName = ExtractTriggerNameFromDefinition(definitionContent);
                        if (string.IsNullOrEmpty(triggerName))
                        {
                            LogMessage($"No trigger found for Logic App: {logicAppRichTextBox.Text}");
                            continue;
                        }

                        // Step 3: Use the trigger name to construct the resubmit URL
                        string apiUrl = $"https://management.azure.com/subscriptions/{subscriptionRichTextBox.Text}/resourcegroups/{resourceGroupRichTextBox.Text}/providers/Microsoft.Logic/workflows/{logicAppRichTextBox.Text}/triggers/{triggerName}/histories/{failedRun.Name}/resubmit?api-version=2016-10-01";

                        // Resubmit the failed run
                        await LogicAppResubmitter.ResubmitLogicAppRunAsync(apiUrl, accessToken);

                        LogMessage($"Resubmitted run with ID: {failedRun.Name} at {failedRun.StartTime}");
                        successfullyResubmittedCount++;
                        runCounter.Text = $"Runs Left Resubmit: {failedRuns.Count - successfullyResubmittedCount}";
                    }
                    catch (Exception ex)
                    {
                        LogMessage($"Failed to resubmit run with ID: {failedRun.Name}. Error: {ex.Message}");
                    }
                }

                MessageBox.Show("Resubmission completed successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                progressLabel.Text = "Resubmit completed";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string ExtractTriggerNameFromDefinition(string definitionContent)
        {
            dynamic definition = Newtonsoft.Json.JsonConvert.DeserializeObject(definitionContent);
            foreach (var trigger in definition.properties.definition.triggers)
            {
                // This example picks the first trigger found. Adjust based on your logic.
                return (string)trigger.Name;
            }
            return null; // or appropriate handling if no trigger is found
        }








        private async void searchButton_Click(object sender, EventArgs e)
        {
            // Placeholder for future functionality
        }

        public class LogicAppRun
        {
            public string Name { get; set; }
            public string Status { get; set; }
            public DateTime? StartTime { get; set; }
        }

        public class RootObject
        {
            public List<Value> value { get; set; }
        }

        public class Value
        {
            public string name { get; set; }
            public Properties properties { get; set; }
        }

        public class Properties
        {
            public DateTime startTime { get; set; }
            public string status { get; set; }
        }


        public class LogicAppResubmitter
        {
            public static async Task ResubmitLogicAppRunAsync(string apiUrl, string accessToken)
            {
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                        HttpResponseMessage response = await client.PostAsync(apiUrl, new StringContent(""));

                        if (response.IsSuccessStatusCode)
                        {
                        }
                        else
                        {

                            string responseContent = await response.Content.ReadAsStringAsync();
                        }
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }
    }
}