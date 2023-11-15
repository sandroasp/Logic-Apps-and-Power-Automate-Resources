using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;

namespace DevScope.LogicAppConsumption.Copilot
{
    public partial class MainFormAddParam : Form
    {
        private ContextMenuStrip contextMenuStrip;
        private ToolStripMenuItem addNewParameterMenuItem;
        private ToolStripMenuItem addNewWorkFlowParameterMenuItem;
        private ToolStripMenuItem addNewLogicAppMenuItem;
        private ToolStripMenuItem addEmptyLogicAppMenuItem;
        private ToolStripMenuItem EditdNode;
        private ToolStripMenuItem editSelectedNode;

        private JObject jsonTemplate;
        private JObject jsonTemplate2;
        private JObject jsonTemplate3;
        private List<string> expandedNodesPaths = new List<string>(); // Declare and initialize the list
        public TreeView TreeView { get { return treeView; } } // Change the access modifier to public
        private ToolStripMenuItem initializeVariableMenuItem;
        private string jsonFilePath;
        private List<int> foundWordPositions = new List<int>();
        private string searchText;
        private int currentSearchIndex = -1; // Initialize it to -1
        public JObject logicAppParametersJson = null; // To store Logic App Parameters JSON
        public JObject logicAppParametersTemplateJson = null; // To store Logic App Parameters Template JSON

        private List<string> expandedParametersNodesPaths = new List<string>(); // Declare and initialize the list for Logic App Parameters TreeView
        private List<string> expandedParametersTemplateNodesPaths = new List<string>(); // Declare and initialize the list for Logic App Parameters Template TreeView

        private List<string> loadedJsonFilePaths = new List<string>();
        private List<string> loadedLogicAppParametersFilePaths = new List<string>();
        private string loadedLogicAppParametersTemplateFilePaths;
        private string previewButtonClicked = "";

        private bool canEditJsonContent = false;

        public TreeView TreeViewLogicAppParameters
        {
            get { return treeViewLogicAppParameters; }
        }

        public TreeView TreeViewLogicAppParametersTemplate
        {
            get { return treeViewLogicAppParametersTemplate; }
        }

        public MainFormAddParam()
        {
            InitializeComponent();

            // Create and configure the context menu strip.
            contextMenuStrip = new ContextMenuStrip();
            addNewParameterMenuItem = new ToolStripMenuItem("Add ARM Parameter");
            addNewParameterMenuItem.Click += AddNewParameterMenuItem_Click;
            contextMenuStrip.Items.Add(addNewParameterMenuItem);

            addNewWorkFlowParameterMenuItem = new ToolStripMenuItem("Add Logic App Parameter");
            addNewWorkFlowParameterMenuItem.Click += AddWorkFlowParameters_Click;
            contextMenuStrip.Items.Add(addNewWorkFlowParameterMenuItem);

            addNewLogicAppMenuItem = new ToolStripMenuItem("Add New Logic App from Template");
            addNewLogicAppMenuItem.Click += AddWorkFlowParameters_Click;
            contextMenuStrip.Items.Add(AddNewLogicApp);

            addEmptyLogicAppMenuItem = new ToolStripMenuItem("Add Empty Logic App Template");
            addEmptyLogicAppMenuItem.Click += AddEmptyLogicApp_Click;
            contextMenuStrip.Items.Add(addEmptyLogicAppMenuItem);

            #region V2
            //EditdNode = new ToolStripMenuItem("Edit");
            //EditdNode.Click += EditNode_Click;
            //contextMenuStrip.Items.Add(EditdNode);

            // ************************V2*****************************************************
            // Create the common EditSelectedNode menu item
            //editSelectedNode = new ToolStripMenuItem("Edit");
            //editSelectedNode.Click += EditNode_Click;

            // Add the EditSelectedNode menu item to the context menu strips of all TreeView controls
            //treeView.ContextMenuStrip = contextMenuStrip;  // Use the existing context menu strip
            //treeView.ContextMenuStrip.Items.Add(editSelectedNode);

            //treeViewLogicAppParameters.ContextMenuStrip = contextMenuStrip; // Same context menu strip
            //treeViewLogicAppParameters.ContextMenuStrip.Items.Add(editSelectedNode);

            //treeViewLogicAppParametersTemplate.ContextMenuStrip = contextMenuStrip; // Same context menu strip
            //treeViewLogicAppParametersTemplate.ContextMenuStrip.Items.Add(editSelectedNode);
            #endregion

            jsonTemplate = new JObject(); // Initialize your JSON template here or load it from a file
            initializeVariableMenuItem = new ToolStripMenuItem("Add Variable");
            initializeVariableMenuItem.Click += AddNewVariable_Click;
            contextMenuStrip.Items.Add(initializeVariableMenuItem);
            richTextBoxPreview.ReadOnly = true;
            // Assign the context menu strip to the treeView.
            treeView.ContextMenuStrip = contextMenuStrip;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public JObject GetJsonTemplate()
        {
            if (jsonTemplate == null)
            {
                jsonTemplate = new JObject(); // Initialize your JSON template here or load it from a file
            }

            return jsonTemplate;
        }

        /// <summary>
        /// 
        /// </summary>
        public void RefreshTreeView()
        {
            // Store the expansion state of top-level nodes
            StoreExpansionState(treeView.Nodes, "");

            treeView.Nodes.Clear(); // Clear the existing TreeView

            // Add the JSON data from the updated template to the TreeView.
            AddJsonToTreeView(treeView.Nodes, jsonTemplate);

            // Restore the expansion state of top-level nodes
            RestoreExpansionState(treeView.Nodes, "");

            richTextBoxPreview.Text = jsonTemplate.ToString(Formatting.Indented);

            // Set the LabelPath text to the file path
            LabelPath.Text = "Path: " + jsonFilePath;
        }

        /// <summary>
        /// 
        /// </summary>
        public void InitializeJsonTemplate()
        {
            // Initialize jsonTemplate here, e.g., with an empty object
            jsonTemplate = new JObject();

            richTextBoxPreview.Text = jsonTemplate.ToString(Formatting.Indented);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="parentPath"></param>
        private void StoreExpansionState(TreeNodeCollection nodes, string parentPath)
        {
            foreach (TreeNode node in nodes)
            {
                if (node.IsExpanded)
                {
                    expandedParametersNodesPaths.Add(parentPath + node.Text);
                }
                StoreExpansionState(node.Nodes, parentPath + node.Text + "/");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="parentPath"></param>
        private void RestoreExpansionState(TreeNodeCollection nodes, string parentPath)
        {
            foreach (TreeNode node in nodes)
            {
                string nodePath = parentPath + node.Text;
                if (expandedParametersNodesPaths.Contains(nodePath))
                {
                    node.Expand();
                }
                RestoreExpansionState(node.Nodes, nodePath + "/");
            }
        }

        public void AddNameAndValueToTreeView(string name, string value)
        {
            // Add the parameter to the treeViewLogicAppParameters.
            treeViewLogicAppParameters.Nodes.Add($"{name}: {value}");

            // Add the parameter to the treeViewLogicAppParametersTemplate.
            treeViewLogicAppParametersTemplate.Nodes.Add($"{name}: {value}");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoadJsonButton_Click(object sender, EventArgs e)
        {
            // Create and configure an OpenFileDialog control.
            canEditJsonContent = true;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JSON Files|*.json";
            openFileDialog.Title = "Select a JSON File";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    jsonFilePath = openFileDialog.FileName; // Store the file path
                    string jsonContent = File.ReadAllText(jsonFilePath);
                    loadedJsonFilePaths.Add(jsonFilePath); // Add the loaded file path to the list

                    // Attempt to parse the JSON data.
                    try
                    {
                        jsonTemplate = JObject.Parse(jsonContent);
                    }
                    catch (JsonReaderException ex)
                    {
                        // Handle the JSON parsing error.
                        MessageBox.Show("Invalid JSON: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return; // Prevent further processing.
                    }

                    // Refresh the TreeView with the new template.
                    RefreshTreeView();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading JSON: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="json"></param>
        private void AddJsonToTreeView(TreeNodeCollection nodes, JToken json)
        {
            if (json is JObject obj)
            {
                foreach (var property in obj.Properties())
                {
                    if (property.Name != "Type") // Check if the property name is not "Type."
                    {
                        TreeNode node = nodes.Add(property.Name);
                        AddJsonToTreeView(node.Nodes, property.Value);
                    }
                }
            }
            else if (json is JArray array)
            {
                for (int i = 0; i < array.Count; i++)
                {
                    TreeNode node = nodes.Add($"[{i}]");
                    AddJsonToTreeView(node.Nodes, array[i]);
                }
            }
            else
            {
                nodes.Add(json.ToString());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void RefreshLogicAppParametersTreeView()
        {
            //// Attempt to parse the JSON data.            
            jsonTemplate2 = logicAppParametersJson;

            StoreExpansionState(treeViewLogicAppParameters.Nodes, "");
            treeViewLogicAppParameters.Nodes.Clear(); // Clear the existing TreeView
            AddJsonToTreeView(treeViewLogicAppParameters.Nodes, jsonTemplate2);
            RestoreExpansionState(treeViewLogicAppParameters.Nodes, "");

        }

        /// <summary>
        /// 
        /// </summary>
        public void RefreshLogicAppParametersTemplateTreeView()
        {
            //// Attempt to parse the JSON data.            
            jsonTemplate3 = logicAppParametersTemplateJson;

            StoreExpansionState(treeViewLogicAppParametersTemplate.Nodes, "");
            treeViewLogicAppParametersTemplate.Nodes.Clear(); // Clear the existing TreeView
            AddJsonToTreeView(treeViewLogicAppParametersTemplate.Nodes, jsonTemplate3);
            RestoreExpansionState(treeViewLogicAppParametersTemplate.Nodes, "");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            // You can add additional logic here if needed.
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainFormAddParam_Load(object sender, EventArgs e)
        {


        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                // Show the context menu at the mouse position.
                contextMenuStrip.Show(treeView, e.Location);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddNewParameterMenuItem_Click(object sender, EventArgs e)
        {
            // Open the AddParameterForm and pass the reference to the main form
            AddParameterForm addParameterForm = new AddParameterForm(this); // 'this' refers to the current MainFormAddParam instance
            DialogResult result = addParameterForm.ShowDialog();

            if (result == DialogResult.OK)
            {
                // Handle the case when the parameter is added successfully, if needed.
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="updatedTemplate"></param>
        public void ReloadTreeViewWithUpdatedTemplate(JObject updatedTemplate)
        {
            treeView.Nodes.Clear(); // Clear the existing TreeView

            // Add the JSON data from the updated template to the TreeView.
            AddJsonToTreeView(treeView.Nodes, updatedTemplate);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextMenuStripParam_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddNewVariable_Click(object sender, EventArgs e)
        {
            // Open the AddVariablesForm and pass the reference to the main form and the JSON template.
            AddVariablesForm addVariablesForm = new AddVariablesForm(this, jsonTemplate);
            DialogResult result = addVariablesForm.ShowDialog();

            if (result == DialogResult.OK)
            {
                // Update the JSON template with the variables and refresh the TreeView.
                jsonTemplate = addVariablesForm.GetUpdatedJsonTemplate();
                RefreshTreeView();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void AddParameterToTreeView(TreeNodeCollection nodes, string name, string value)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="json"></param>
        public void UpdateTreeView(TreeNodeCollection nodes, JToken json)
        {
            if (json is JObject obj)
            {
                nodes.Clear();  // Clear the nodes before adding the updated JSON data.
                foreach (var property in obj.Properties())
                {
                    TreeNode node = nodes.Add(property.Name);
                    AddJsonToTreeView(node.Nodes, property.Value);
                }
            }
            else if (json is JArray array)
            {
                nodes.Clear();  // Clear the nodes before adding the updated JSON data.
                for (int i = 0; i < array.Count; i++)
                {
                    TreeNode node = nodes.Add($"[{i}]");
                    AddJsonToTreeView(node.Nodes, array[i]);
                }
            }
            else
            {
                nodes.Clear();  // Clear the nodes before adding the updated JSON data.
                nodes.Add(json.ToString());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void richTextBoxPreview_TextChanged(object sender, EventArgs e)
        {

        }


        private void LabelPath_Click(object sender, EventArgs e)
        {

        }

        private void SearchTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            searchText = SearchTextBox.Text;

            if (!string.IsNullOrEmpty(searchText))
            {
                // Clear the previous highlights
                richTextBoxPreview.SelectAll();
                richTextBoxPreview.SelectionBackColor = richTextBoxPreview.BackColor;

                foundWordPositions.Clear();

                string textToSearch = richTextBoxPreview.Text;
                int index = 0;

                while (index < textToSearch.Length)
                {
                    index = textToSearch.IndexOf(searchText, index, StringComparison.OrdinalIgnoreCase);

                    if (index == -1)
                        break;

                    foundWordPositions.Add(index);
                    index += searchText.Length;
                }

                // Update the label with the count
                occurrencesLabel.Text = foundWordPositions.Count + " occurrences found";

                if (foundWordPositions.Count == 0)
                {
                    MessageBox.Show("Text not found.", "Search Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void SearchPrevious_Click_1(object sender, EventArgs e)
        {
            if (currentSearchIndex >= 0)
            {
                currentSearchIndex--; // Move to the previous occurrence.
                if (currentSearchIndex >= 0)
                {
                    SelectFoundText(currentSearchIndex);
                }
                else
                {
                    // You've reached the first result, handle as needed.
                }
            }
        }

        private void SearchNext_Click_1(object sender, EventArgs e)
        {
            if (foundWordPositions.Count > 0 && currentSearchIndex < foundWordPositions.Count - 1)
            {
                currentSearchIndex++; // Move to the next occurrence.
                SelectFoundText(currentSearchIndex);
            }
            else
            {
                // You've reached the last result, handle as needed.
            }
        }

        private void SelectFoundText(int index)
        {
            int start = foundWordPositions[index];
            int length = searchText.Length;
            richTextBoxPreview.Select(start, length);
            richTextBoxPreview.ScrollToCaret();
            richTextBoxPreview.Focus();
        }
        private void HighlightScrollBarMarks()
        {
            int lineHeight = richTextBoxPreview.GetPositionFromCharIndex(richTextBoxPreview.GetFirstCharIndexFromLine(1)).Y -
                            richTextBoxPreview.GetPositionFromCharIndex(0).Y;

            // Clear the previous highlights
            richTextBoxPreview.SelectAll();
            richTextBoxPreview.SelectionBackColor = richTextBoxPreview.BackColor;

            if (currentSearchIndex >= 0 && currentSearchIndex < foundWordPositions.Count)
            {
                int position = foundWordPositions[currentSearchIndex];
                int line = richTextBoxPreview.GetLineFromCharIndex(position);
                int offset = position - richTextBoxPreview.GetFirstCharIndexFromLine(line);
                int y = line * lineHeight + (int)(lineHeight / 2.0);
                y += richTextBoxPreview.GetPositionFromCharIndex(offset).Y;

                Rectangle rectangle = new Rectangle(
                    richTextBoxPreview.ClientRectangle.Width - SystemInformation.VerticalScrollBarWidth, y,
                    SystemInformation.VerticalScrollBarWidth, lineHeight);

                using (SolidBrush brush = new SolidBrush(Color.Red))
                {
                    richTextBoxPreview.CreateGraphics().FillRectangle(brush, rectangle);
                }

                // Scroll to the current occurrence
                richTextBoxPreview.Select(position, searchText.Length);
                richTextBoxPreview.ScrollToCaret();
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            HighlightScrollBarMarks();
        }

        private void occurrencesLabel_Click(object sender, EventArgs e)
        {

        }

        private void editButton_Click(object sender, EventArgs e)
        {
            // Set the RichTextBox background color to white
            richTextBoxPreview.BackColor = Color.White;

            // Allow editing by setting ReadOnly to false
            richTextBoxPreview.ReadOnly = false;

            // Optionally, you can set focus to the RichTextBox to start editing immediately
            richTextBoxPreview.Focus();
        }

        private void ResetCanEditFlag()
        {
            canEditJsonContent = false;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            // Set the RichTextBox background color to ButtonFace
            richTextBoxPreview.BackColor = SystemColors.ButtonFace;


            try
            {
                string editedJson = richTextBoxPreview.Text;
                if (previewButtonClicked == "" || previewButtonClicked == "PreviewLA")
                {
                    jsonTemplate = JObject.Parse(editedJson);
                    RefreshTreeView();
                }
                // Check which TreeView is currently selected and update the corresponding JSON data.
                if (previewButtonClicked == "PreviewLAParam")
                {
                    logicAppParametersJson = JObject.Parse(editedJson);
                    RefreshLogicAppParametersTreeView();
                }
                else if (previewButtonClicked == "PreviewLAParamTemplate")
                {
                    logicAppParametersTemplateJson = JObject.Parse(editedJson);
                    RefreshLogicAppParametersTemplateTreeView();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving JSON: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // After saving, set the RichTextBox back to read-only mode.
            richTextBoxPreview.ReadOnly = true;


        }

        private void treeViewLogicAppParameters_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        public void checkBoxAddParameters_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void LoadLogicAppParameters_Click(object sender, EventArgs e)
        {
            // Create and configure an OpenFileDialog control for selecting a JSON file.
            ResetCanEditFlag();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JSON Files|*.json";
            openFileDialog.Title = "Select a JSON File for Logic App Parameters";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string jsonFilePath = openFileDialog.FileName; // Get the selected file path
                    string jsonContent = File.ReadAllText(jsonFilePath);
                    loadedLogicAppParametersFilePaths.Add(jsonFilePath); // Add the loaded file path to the list

                    logicAppParametersJson = JObject.Parse(jsonContent);

                    // Update the respective tree view with the loaded JSON data.
                    treeViewLogicAppParameters.Nodes.Clear();
                    AddJsonToTreeView(treeViewLogicAppParameters.Nodes, logicAppParametersJson);

                    // Optionally, you can highlight scrollbar marks or perform other actions here.
                    HighlightScrollBarMarks();

                    // Update the label with the file path
                    LabelLAParameters.Text = "File Path: " + jsonFilePath;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading Logic App Parameters JSON: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void LoadLogicAppParametersTemplate_Click(object sender, EventArgs e)
        {
            // Create and configure an OpenFileDialog control for selecting a JSON file.
            ResetCanEditFlag();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JSON Files|*.json";
            openFileDialog.Title = "Select a JSON File for Logic App Parameters Template";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string jsonFilePath = openFileDialog.FileName; // Get the selected file path
                    string jsonContent = File.ReadAllText(jsonFilePath);
                    loadedLogicAppParametersTemplateFilePaths = jsonFilePath; // Add the loaded file path to the list

                    logicAppParametersTemplateJson = JObject.Parse(jsonContent);

                    // Update the respective tree view with the loaded JSON data.
                    treeViewLogicAppParametersTemplate.Nodes.Clear();
                    AddJsonToTreeView(treeViewLogicAppParametersTemplate.Nodes, logicAppParametersTemplateJson);

                    // Optionally, you can highlight scrollbar marks or perform other actions here.
                    HighlightScrollBarMarks();

                    // Update the label with the file path
                    LabelLAParametersTemplate.Text = "File Path: " + jsonFilePath;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading Logic App Parameters Template JSON: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void LabelLAParameters_Click(object sender, EventArgs e)
        {

        }

        private void LabelLAParametersTemplate_Click(object sender, EventArgs e)
        {

        }

        private void treeViewLogicAppParametersTemplate_AfterSelect(object sender, TreeViewEventArgs e)
        {
            // Assuming you want to load JSON data for Logic App Parameters Template from a file.
            string jsonFilePath = loadedLogicAppParametersTemplateFilePaths; // Replace with the actual file path.

            try
            {
                string jsonContent = File.ReadAllText(jsonFilePath);
                JObject logicAppParametersTemplateJson = JObject.Parse(jsonContent);

                //  richTextBoxPreview.Text = logicAppParametersTemplateJson.ToString(Formatting.Indented);

                // Optionally, you can highlight scrollbar marks or perform other actions here.
                HighlightScrollBarMarks();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading Logic App Parameters Template JSON: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EnableEditing()
        {
            // Set the RichTextBox background color to white
            richTextBoxPreview.BackColor = Color.White;
            // Allow editing by setting ReadOnly to false
            richTextBoxPreview.ReadOnly = false;
            // Optionally, you can set focus to the RichTextBox to start editing immediately
            richTextBoxPreview.Focus();
        }

        private void LoadJsonToRichTextBox(JObject json)
        {
            if (json != null)
            {
                richTextBoxPreview.Text = json.ToString(Formatting.Indented);
                HighlightScrollBarMarks(); // Optionally, highlight scrollbar marks or perform other actions.
            }
            else
            {
                MessageBox.Show("JSON data has not been loaded.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddNewParameter_Click(object sender, EventArgs e)
        {

        }

        private void AddWorkFlowParameters_Click(object sender, EventArgs e)
        {
            // Open the AddParameterForm and pass the reference to the main form
            AddWorkFlowParameters addWorkFlowParameters = new AddWorkFlowParameters(this);
            DialogResult result = addWorkFlowParameters.ShowDialog();


            if (result == DialogResult.OK)
            {
                // Handle the case when the parameter is added successfully, if needed.
            }
        }

        private void AddNewLogicApp_Click(object sender, EventArgs e)
        {
            // Create and configure an OpenFileDialog control for selecting a JSON or TXT file.
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JSON Files|*.json|Text Files|*.txt";
            openFileDialog.Title = "Select a JSON or TXT File for the new Logic App";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string fileExtension = Path.GetExtension(openFileDialog.FileName);
                    string jsonFilePath = openFileDialog.FileName; // Get the selected file path
                    string jsonContent;

                    // Read the file content based on the file extension.
                    if (fileExtension.Equals(".json", StringComparison.OrdinalIgnoreCase))
                    {
                        jsonContent = File.ReadAllText(jsonFilePath);
                    }
                    else if (fileExtension.Equals(".txt", StringComparison.OrdinalIgnoreCase))
                    {
                        // Handle TXT file reading logic here, e.g., read as plain text.
                        jsonContent = File.ReadAllText(jsonFilePath);
                    }
                    else
                    {
                        MessageBox.Show("Unsupported file type. Please select a JSON or TXT file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Prompt the user for a new Logic App name using NewLogicAppNameForm.
                    using (NewLogicAppNameForm nameForm = new NewLogicAppNameForm(this))
                    {
                        DialogResult result = nameForm.ShowDialog();
                        if (result == DialogResult.OK)
                        {
                            string newLogicAppName = nameForm.AddNewLogicAppName;

                            // Deserialize the JSON content from the file into a JObject.
                            JObject newLogicApp = JObject.Parse(jsonContent);

                            // Ensure the "resources" property exists in the existing JSON data.
                            if (jsonTemplate["resources"] == null)
                            {
                                jsonTemplate["resources"] = new JArray();
                            }

                            // Update the new Logic App's name in the JSON content.
                            newLogicApp["name"] = newLogicAppName;
                            newLogicApp["tags"]["displayName"] = newLogicAppName;
                            newLogicApp["tags"]["logicalResource"] = newLogicAppName;

                            // Add the new Logic App definition to the "resources" array.
                            jsonTemplate["resources"]?.Last?.AddAfterSelf(newLogicApp);

                            // Refresh the TreeView or take other actions as needed.
                            RefreshTreeView();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading Logic App JSON: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void AddNewLogicAppToJson()
        {
            // Ensure the "resources" property exists in the JSON data
            if (jsonTemplate["resources"] == null)
            {
                jsonTemplate["resources"] = new JArray();
            }

            // Create a new Logic App object and add it to the "resources" array
            JObject newLogicApp = new JObject();
            // You can customize the properties of the new Logic App here.
            newLogicApp["name"] = "New Logic App"; // Set a default name, for example.
            jsonTemplate["resources"]?.Last?.AddAfterSelf(newLogicApp);
        }

        private void AddEmptyLogicApp_Click(object sender, EventArgs e)
        {
            // Create a new Logic App definition JSON with a default name.
            string defaultName = "CHANGE NAME"; // Default name
            JObject newLogicApp = new JObject
    {
        { "name", defaultName },
        { "type", "Microsoft.Logic/workflows" },
        { "location", "[parameters('TESTELocation')]" },
        { "apiVersion", "2016-06-01" },
        { "dependsOn", new JArray() },
        { "tags", new JObject
            {
                { "displayName", defaultName }, // Set default name
                { "logicalResource", defaultName } // Set default name
            }
        },
        { "properties", new JObject
            {
                { "definition", new JObject
                    {
                        { "$schema", "https://schema.management.azure.com/providers/Microsoft.Logic/schemas/2016-06-01/workflowdefinition.json#" },
                        { "contentVersion", "1.0.0.0" },
                        { "actions", new JObject() },
                        { "outputs", new JObject() },
                        { "parameters", new JObject() },
                        { "triggers", new JObject() }
                    }
                },
                { "parameters", new JObject() }
            }
        }
    };

            // Add the new Logic App to the JSON template.
            jsonTemplate["resources"]?.Last?.AddAfterSelf(newLogicApp);

            // Refresh the TreeView to display the new Logic App.
            RefreshTreeView();

            // Open the NewEmptyLogicAppNameForm for the user to enter the name.
            NewEmptyLogicAppNameForm newEmptyLogicAppNameForm = new NewEmptyLogicAppNameForm(this);
            DialogResult result = newEmptyLogicAppNameForm.ShowDialog();

            if (result == DialogResult.OK)
            {
                // Retrieve the new Logic App name entered by the user.
                string newLogicAppName = newEmptyLogicAppNameForm.AddNewEmptyLogicAppName;

                // Update the Logic App name, displayName, and logicalResource in the newly added JSON.
                newLogicApp["name"] = newLogicAppName;
                newLogicApp["tags"]["displayName"] = newLogicAppName;
                newLogicApp["tags"]["logicalResource"] = newLogicAppName;

                // Refresh the TreeView to reflect the updated properties.
                RefreshTreeView();
            }
        }

        private void SaveAllButton_Click(object sender, EventArgs e)
        {
            // Create and configure a SaveFileDialog control.
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "JSON Files|*.json";
            saveFileDialog.Title = "Save All JSON Files";

            // Initialize a list of JSON data and file paths.
            List<Tuple<JObject, string>> jsonDataList = new List<Tuple<JObject, string>>
    {
        Tuple.Create(jsonTemplate, jsonFilePath),
        Tuple.Create(logicAppParametersJson, LabelLAParameters.Text.Replace("File Path: ", "")),
        Tuple.Create(logicAppParametersTemplateJson, LabelLAParametersTemplate.Text.Replace("File Path: ", ""))
    };

            foreach (var jsonData in jsonDataList)
            {
                if (jsonData.Item1 != null && !string.IsNullOrWhiteSpace(jsonData.Item2))
                {
                    try
                    {
                        // Serialize the JSON data to a string
                        string jsonContent = jsonData.Item1.ToString(Formatting.Indented);

                        // Write the JSON content to the corresponding file path
                        File.WriteAllText(jsonData.Item2, jsonContent);

                        // Show a success message to the user
                        MessageBox.Show("File saved successfully: " + jsonData.Item2, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error saving JSON file: " + jsonData.Item2 + "\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        ///***********************************************V2***********************************************************************
        private void EditNode_Click(object sender, EventArgs e)
        {
            // Check which TreeView is currently selected.
            if (treeView.Focused)
            {
                EditSelectedNodeText(treeView, jsonTemplate);
            }
            else if (treeViewLogicAppParameters.Focused)
            {
                EditSelectedNodeText(treeViewLogicAppParameters, logicAppParametersJson);
            }
            else if (treeViewLogicAppParametersTemplate.Focused)
            {
                EditSelectedNodeText(treeViewLogicAppParametersTemplate, logicAppParametersTemplateJson);
            }
        }

        // Step 1: EditSelectedNode event handler
        private void EditSelectedNode_Click(object sender, EventArgs e)
        {
            if (treeView.Focused)
            {
                EditSelectedNodeText(treeView, jsonTemplate);
            }
            else if (treeViewLogicAppParameters.Focused)
            {
                EditSelectedNodeText(treeViewLogicAppParameters, logicAppParametersJson);
            }
            else if (treeViewLogicAppParametersTemplate.Focused)
            {
                EditSelectedNodeText(treeViewLogicAppParametersTemplate, logicAppParametersTemplateJson);
            }
        }

        private void EditSelectedNodeText(TreeView treeView, JObject json)
        {
            TreeNode selectedNode = treeView.SelectedNode;

            if (selectedNode != null)
            {
                // Open the EditNodeForm and set the node's text as the initial value.
                using (EditNodeForm editNodeForm = new EditNodeForm(this))
                {
                    editNodeForm.EditedData = selectedNode.Text;

                    // Show the form and handle the user's input.
                    if (editNodeForm.ShowDialog() == DialogResult.OK)
                    {
                        // Get the selected node's key (property name) and value.
                        string nodeText = editNodeForm.EditedData;
                        string propertyName = selectedNode.Text; // The key to replace.
                                                                 //string parentNode = selectedNode.Parent.Text;
                                                                 // Replace the key in the JSON object.
                                                                 //var property = json.SelectToken(parentNode + "['" + propertyName + "']");
                        JProperty property = json.Property(propertyName);
                        if (property != null)
                        {
                            //JProperty propertyTest = new JProperty(propertyName, property);
                            property.Replace(new JProperty(nodeText, property.Value));
                        }

                        // Update the node's text with the edited data.
                        selectedNode.Text = nodeText;

                        // Refresh the RichTextBox with the updated JSON data.
                        LoadJsonToRichTextBox(json);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a node to edit.", "Edit Node", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void UpdateJsonData(JObject json, string nodePath, string editedData)
        {
            // Use Json.NET's `SelectToken` to find the corresponding JSON node and update it.
            JToken jsonNode = json.SelectToken(nodePath);

            if (jsonNode != null)
            {
                if (jsonNode is JValue valueNode)
                {
                    // Update the value of a JValue node.
                    valueNode.Value = editedData;
                }
                else if (jsonNode.Parent is JProperty propertyNode)
                {
                    // Update the value of a property in a JObject.
                    propertyNode.Value = new JValue(editedData);
                }
            }
            else
            {
                MessageBox.Show("Failed to locate the corresponding JSON node.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //private void ReplaceJsonNode(JObject json, string nodePath, string editedData)
        //{
        //    // Locate the corresponding JSON node
        //    JToken jsonNode = json.SelectToken(nodePath);

        //    if (jsonNode != null)
        //    {
        //        // Find the parent container (e.g., a JObject)
        //        var parent = jsonNode.Parent;

        //        if (parent != null)
        //        {
        //            if (jsonNode is JProperty property)
        //            {
        //                // If the JSON node is a property, replace the value
        //                property.Value = new JValue(editedData);
        //            }
        //            else
        //            {
        //                // If the JSON node is not a property, replace the entire node
        //            }
        //        }
        //        else
        //        {
        //            MessageBox.Show("Failed to locate the parent container for the JSON node.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        }
        //    }
        //    else
        //    {
        //        MessageBox.Show("Failed to locate the corresponding JSON node.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}



        // Helper function to get the path of a tree view node
        private string GetNodePath(TreeNode node)
        {
            string nodePath = node.Text;
            while (node.Parent != null)
            {
                node = node.Parent;
                nodePath = node.Text + "." + nodePath;
            }
            return nodePath;
        }




        private void treeView_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (e.Label != null)
            {
                TreeNode editedNode = e.Node;

                // Update the JSON data for the 'treeView' based on the edited node.
                UpdateJsonDataForTreeView(editedNode, e.Label, ref jsonTemplate);

                // You may add other logic specific to 'treeView' if needed.

                // Refresh 'treeView' with the updated JSON data.
                RefreshTreeView();
            }
        }

        private void treeViewLogicAppParameters_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (e.Label != null)
            {
                TreeNode editedNode = e.Node;

                // Update the JSON data for 'treeViewLogicAppParameters' based on the edited node.
                UpdateJsonDataForTreeView(editedNode, e.Label, ref logicAppParametersJson);

                // You may add other logic specific to 'treeViewLogicAppParameters' if needed.

                // Refresh 'treeViewLogicAppParameters' with the updated JSON data.
                RefreshTreeView();
            }
        }

        private void treeViewLogicAppParametersTemplate_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (e.Label != null)
            {
                TreeNode editedNode = e.Node;

                // Update the JSON data for 'treeViewLogicAppParametersTemplate' based on the edited node.
                UpdateJsonDataForTreeView(editedNode, e.Label, ref logicAppParametersTemplateJson);

                // You may add other logic specific to 'treeViewLogicAppParametersTemplate' if needed.

                // Refresh 'treeViewLogicAppParametersTemplate' with the updated JSON data.
                RefreshTreeView();
            }
        }

        private void UpdateJsonDataForTreeView(TreeNode editedNode, string editedLabel, ref JObject jsonData)
        {
            // Identify which JSON data structure corresponds to the edited node and update it.
            if (editedNode.Parent == null)
            {
                // Handle root nodes based on your JSON structure.
                // Update jsonData (e.g., jsonTemplate) based on the edited label.
            }
            else
            {
                // Handle child nodes based on your JSON structure.
                // You may need to traverse the JSON structure to locate the matching node.
            }
        }

        private void UpdateJsonDataFromTreeView(TreeView treeView, JObject jsonData)
        {
            foreach (TreeNode node in treeView.Nodes)
            {
                // Assuming that the node's text follows a specific pattern like "Property: NewValue"
                string nodeText = node.Text;
                string[] parts = nodeText.Split(new[] { ':' }, 2);

                if (parts.Length == 2)
                {
                    string propertyName = parts[0].Trim();
                    string newValue = parts[1].Trim();

                    // Update the JSON object with the new value.
                    if (jsonData[propertyName] != null)
                    {
                        jsonData[propertyName] = newValue;
                    }
                    else
                    {
                        // Handle the case when the property does not exist in the JSON object.
                        MessageBox.Show($"Property '{propertyName}' not found in JSON.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    // Handle the case when the node's text does not follow the expected pattern.
                    MessageBox.Show($"Invalid node text: {nodeText}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void SaveLogicAppJson()
        {
            // Create and configure a SaveFileDialog control.
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "JSON Files|*.json";
            saveFileDialog.Title = "Save Logic App Template";
            saveFileDialog.FileName = "LogicApp.json"; // Default file name

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string jsonFilePath = saveFileDialog.FileName;

                // Serialize the JSON template to a string
                string jsonContent = jsonTemplate.ToString(Formatting.Indented);

                try
                {
                    // Write the JSON content to the selected file
                    File.WriteAllText(jsonFilePath, jsonContent);

                    // Show a success message to the user
                    MessageBox.Show("Logic App template saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saving JSON: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void SaveLogicAppParam()
        {
            // Create and configure a SaveFileDialog control.
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "JSON Files|*.json";
            saveFileDialog.Title = "Save Logic App Template";
            saveFileDialog.FileName = "LogicApp.parameters.json"; // Default file name

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string jsonFilePath = saveFileDialog.FileName;

                // Serialize the JSON template to a string
                string jsonContent = jsonTemplate.ToString(Formatting.Indented);

                try
                {
                    // Write the JSON content to the selected file
                    File.WriteAllText(jsonFilePath, jsonContent);

                    // Show a success message to the user
                    MessageBox.Show("Logic App template saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saving JSON: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void SaveLogicAppParamTemplate()
        {
            // Create and configure a SaveFileDialog control.
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "JSON Files|*.json";
            saveFileDialog.Title = "Save Logic App Template";
            saveFileDialog.FileName = "LogicApp.parameters.template.json"; // Default file name

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string jsonFilePath = saveFileDialog.FileName;

                // Serialize the JSON template to a string
                string jsonContent = jsonTemplate.ToString(Formatting.Indented);

                try
                {
                    // Write the JSON content to the selected file
                    File.WriteAllText(jsonFilePath, jsonContent);

                    // Show a success message to the user
                    MessageBox.Show("Logic App template saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saving JSON: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }



        private void SaveLogicApp_Click(object sender, EventArgs e)
        {
            SaveLogicAppJson();

        }




        private void ButtonSavelLAParameter_Click(object sender, EventArgs e)
        {
            SaveLogicAppParam();

        }

        private void ButtonSaveLAParamTemplate_Click(object sender, EventArgs e)
        {
            SaveLogicAppParamTemplate();

        }

        private void SaveJsonToFile(JObject json)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "JSON Files|*.json";
            saveFileDialog.Title = "Save JSON File";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;

                try
                {
                    System.IO.File.WriteAllText(filePath, json.ToString());
                    MessageBox.Show("JSON data saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while saving the JSON data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ButtonViewLA_Click(object sender, EventArgs e)
        {
            // Update the RichTextBoxPreview with the JSON data from jsonTemplate
            richTextBoxPreview.Text = jsonTemplate.ToString();
            previewButtonClicked = "PreviewLA";
        }

        private void PreviewLAParam_Click(object sender, EventArgs e)
        {
            // Update the RichTextBoxPreview with the JSON data from logicAppParametersJson
            richTextBoxPreview.Text = logicAppParametersJson.ToString();
            previewButtonClicked = "PreviewLAParam";
        }

        private void PreviewLAParamTemplate_Click(object sender, EventArgs e)
        {
            // Update the RichTextBoxPreview with the JSON data from logicAppParametersTemplateJson
            richTextBoxPreview.Text = logicAppParametersTemplateJson.ToString();
            previewButtonClicked = "PreviewLAParamTemplate";
        }

        private void tableLayoutPanel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void buttonEnableDisableLA_Click(object sender, EventArgs e)
        {
            // Get the Logic App names from your JSON data
            List<string> logicAppNames = GetLogicAppNamesFromJson();

            if (logicAppNames.Count == 0)
            {
                MessageBox.Show("No Logic Apps found.");
                return;
            }

            // Create an instance of the EnableDisableLogicAppForm
            using (EnableDisableLogicAppForm enableDisableForm = new EnableDisableLogicAppForm(logicAppNames, jsonTemplate))
            {
                DialogResult result = enableDisableForm.ShowDialog();

                if (result == DialogResult.OK)
                {
                    // Now, you have the updated jsonTemplate with the Logic App state modified.
                    // You can use the updated jsonTemplate as needed.
                    UpdateJsonDataForLogicApp(enableDisableForm.SelectedLogicAppName, enableDisableForm.SelectedState);
                }
            }
            RefreshTreeView();
        }




        private void UpdateJsonDataForLogicApp(string logicAppName, string state)
        {
            // Update your JSON data based on the selected Logic App name and state.
        }

        private List<string> GetLogicAppNamesFromJson()
        {
            List<string> logicAppNames = new List<string>();

            if (jsonTemplate != null && jsonTemplate["resources"] is JArray resourcesArray)
            {
                foreach (var resource in resourcesArray)
                {
                    if (resource["name"] != null)
                    {
                        logicAppNames.Add(resource["name"].ToString());
                    }
                }
            }

            return logicAppNames;
        }

        private void About_Click(object sender, EventArgs e)
        {
            AboutForm aboutForm = new AboutForm();
            aboutForm.ShowDialog();
        }


    }
}