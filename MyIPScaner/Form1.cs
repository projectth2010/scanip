using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

using MySql.Data.MySqlClient;
using MongoDB.Driver;
using MongoDB.Bson;

namespace MyIPScaner
{
    public partial class Form1 : Form
    {
        private List<int> additionalPorts = new List<int>();
        private string selectedIPAddress; // Store selected IP address from lstResults

        public Form1()
        {
            InitializeComponent();
            InitializeContextMenu(); // Call the method to set up context menu
        }

        //private void lstResults_MouseClick(object sender, MouseEventArgs e)
        //{
        //    if (e.Button == MouseButtons.Right)
        //    {
        //        int index = lstResults.IndexFromPoint(e.Location);
        //        if (index != -1)
        //        {
        //            lstResults.SelectedIndex = index;
        //            selectedIPAddress = lstResults.SelectedItem.ToString().Split("-")[0].Trim();
        //            contextMenuStrip1.Show(lstResults, e.Location);
        //        }
        //    }
        //}

        private void InitializeContextMenu()
        {
            contextMenuStrip1 = new ContextMenuStrip();
            ToolStripMenuItem testMySQLItem = new ToolStripMenuItem("Test MySQL Connection");
            ToolStripMenuItem testMongoDBItem = new ToolStripMenuItem("Test MongoDB Connection");
            ToolStripMenuItem openHTTPItem = new ToolStripMenuItem("Open HTTP in Browser");
            ToolStripMenuItem openHTTPSItem = new ToolStripMenuItem("Open HTTPS in Browser");

            testMySQLItem.Click += TestMySQLConnectionMenuItem_Click;
            testMongoDBItem.Click += TestMongoDBConnectionMenuItem_Click;
            openHTTPItem.Click += OpenHTTPMenuItem_Click;
            openHTTPSItem.Click += OpenHTTPSMenuItem_Click;

            contextMenuStrip1.Items.Add(testMySQLItem);
            //contextMenuStrip1.Items.Add(testMongoDBItem);
            contextMenuStrip1.Items.Add(openHTTPItem);
            contextMenuStrip1.Items.Add(openHTTPSItem);
        }

        private void TestMySQLConnectionMenuItem_Click(object sender, EventArgs e)
        {
            // Prompt user for MySQL credentials and test the connection
            using (MySqlConnectionForm mysqlForm = new MySqlConnectionForm())
            {
                if (mysqlForm.ShowDialog() == DialogResult.OK)
                {
                    TestMySQLConnection(selectedIPAddress, 3306, mysqlForm.Username, mysqlForm.Password);
                }
            }
        }

        private void TestMongoDBConnectionMenuItem_Click(object sender, EventArgs e)
        {
            using (MongoDbConnection mongoDb = new MongoDbConnection())
            {
                if (mongoDb.ShowDialog() == DialogResult.OK)
                {
                    TestMongoDBConnection(selectedIPAddress, 27017, mongoDb.ConnectionStr);
                }
            }
            // Test MongoDB connection on the default port
            //TestMongoDBConnection(selectedIPAddress, 27017);
        }

        private void OpenHTTPMenuItem_Click(object sender, EventArgs e)
        {
            // Open HTTP in the browser
            TestHttpOrHttps($"{selectedIPAddress}", 80);
        }

        private void OpenHTTPSMenuItem_Click(object sender, EventArgs e)
        {
            // Open HTTPS in the browser
            TestHttpOrHttps($"{selectedIPAddress}", 443);

        }

        private void TestMySQLConnection(string ipAddress, int port, string username, string password)
        {
            string connectionString = $"Server={ipAddress};Port={port};Uid={username};Pwd={password};";
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    MessageBox.Show($"{ipAddress} - MySQL Connection Test Successful on Port {port}");
                    //UpdateUI($"{ipAddress} - MySQL Connection Test Successful on Port {port}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ipAddress} - MySQL Connection Test Failed on Port {port}\n" +ex.Message);
                //UpdateUI($"{ipAddress} - MySQL Connection Test Failed on Port {port}");
            }
        }

        private void TestMongoDBConnection(string ipAddress, int port, string connectionString)
        {
            // string connectionString = $"mongodb://{ipAddress}:{port}";
            try
            {
                MongoClientSettings settings = MongoClientSettings.FromConnectionString(connectionString);
                MongoClient client = new MongoClient(settings);

                IMongoDatabase database = client.GetDatabase("admin");
                var pingCommand = new BsonDocument("ping", 1);
                database.RunCommand<BsonDocument>(pingCommand);

                MessageBox.Show($"{ipAddress} - MongoDB Connection Test Successful on Port {port}");
                //UpdateUI($"{ipAddress} - MongoDB Connection Test Successful on Port {port}");
            }
            catch (Exception)
            {
                MessageBox.Show($"{ipAddress} - MongoDB Connection Test Failed on Port {port}");
                //UpdateUI($"{ipAddress} - MongoDB Connection Test Failed on Port {port}");
            }
        }




        private void TestHttpOrHttps(string ipAddress, int port)
        {
            string scheme = (port == 80) ? "http" : "https";
            string url = $"{scheme}://{ipAddress}";

            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = "curl";
                startInfo.Arguments = $"-s -o NUL --head --fail {url}";
                startInfo.UseShellExecute = false;
                startInfo.CreateNoWindow = true;
                startInfo.RedirectStandardOutput = true;
                startInfo.RedirectStandardError = true;

                using (Process process = Process.Start(startInfo))
                {
                    process.WaitForExit();
                    int exitCode = process.ExitCode;
                    if (exitCode == 0)
                    {
                        MessageBox.Show($"{ipAddress} - {scheme.ToUpper()} Connection Test Successful on Port {port}");
                    }
                    else
                    {
                        MessageBox.Show($"{ipAddress} - {scheme.ToUpper()} Connection Test Failed on Port {port}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error testing {scheme.ToUpper()} connection: {ex.Message}");
            }
        }

        private async void btnScan_Click(object sender, EventArgs e)
        {
            lstResults.Items.Clear();

            string startIp = txtStartIp.Text.Trim();

            if (!IsValidIP(startIp))
            {
                MessageBox.Show("Invalid IP address.");
                return;
            }

            lblStatus.Text = "Scanning IP addresses...";

            await Task.Run(() => ScanIPRange(startIp));

            lblStatus.Text = "Scan completed.";
        }

        private void ScanIPRange(string startIp)
        {
            string[] octets = startIp.Split('.');
            if (octets.Length != 4)
            {
                MessageBox.Show("Invalid IP address.");
                return;
            }

            string baseIp = $"{octets[0]}.{octets[1]}.{octets[2]}.";
            int startOctet = int.Parse(octets[3]);

            List<Task> tasks = new List<Task>();

            for (int i = startOctet; i <= 255; i++)
            {
                string currentIp = $"{baseIp}{i}";
                try
                {
                    using (Ping ping = new Ping())
                    {
                        PingReply reply = ping.Send(currentIp);
                        if (reply.Status == IPStatus.Success)
                        {
                            UpdateUI($"{currentIp} - Online");
                            // Perform port scanning here if needed
                        }
                        else
                        {
                            //UpdateUI($"{currentIp} - Offline");
                        }
                    }
                }
                catch (Exception ex)
                {
                    UpdateUI($"{currentIp} - Error: {ex.Message}");
                }
            }
        }



        private bool IsValidIP(string ip)
        {
            IPAddress address;
            return IPAddress.TryParse(ip, out address);
        }



        private void ScanPorts(string ipAddress)
        {
            int[] portsToScan = { 80, 443, 8080, 3000, 3306, 27017 }; // Add or customize ports as needed
            List<int> openPorts = new List<int>();
            bool isOnline = false;

            try
            {
                using (Ping ping = new Ping())
                {
                    PingReply reply = ping.Send(ipAddress);
                    isOnline = reply.Status == IPStatus.Success;
                }
            }
            catch (Exception)
            {
                // Error while pinging, consider the host as offline
            }

            if (isOnline)
            {
                foreach (int port in portsToScan)
                {
                    try
                    {
                        using (TcpClient client = new TcpClient())
                        {
                            client.Connect(ipAddress, port);
                            openPorts.Add(port);
                        }
                    }
                    catch (Exception)
                    {
                        // Port is closed
                    }
                }

                if (openPorts.Count > 0)
                {
                    UpdateUI($"{ipAddress} - Online, Open Ports: {string.Join(", ", openPorts)}");
                }
                else
                {
                    UpdateUI($"{ipAddress} - Online, No Open Ports");
                }
            }
            else
            {
                //UpdateUI($"{ipAddress} - Offline");
            }
        }

        private void UpdateUI(string message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(UpdateUI), message);
            }
            else
            {
                lstResults.Items.Add(message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void lstResults_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int index = lstResults.IndexFromPoint(e.Location);
                if (index != -1)
                {
                    lstResults.SelectedIndex = index;
                    //selectedIPAddress = lstResults.SelectedIndex.ToString();
                    selectedIPAddress = lstResults.SelectedItem.ToString().Split("-")[0].Trim();
                    contextMenuStrip1.Show(lstResults, e.Location);
                }
            }
        }

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }
    }
}
