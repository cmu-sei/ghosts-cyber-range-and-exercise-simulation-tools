using System;
using System.Collections.Generic;
using System.Net;
using System.Windows.Forms;
using System.IO;
using System.IO.Compression;
using beamApp.Properties;

namespace beamApp
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
            lblCurrentState.Text = "";
            lblCurrentState.ForeColor = System.Drawing.Color.White;
            lblCurrentDelay.Text = "";
            lblStatus.Text = "";
            lblFileCount.Text = "";
            totalFiles = 0;
            totalBeacons = 0;

            lstMain.Columns.Add("Details", 625);
            lstMain.Columns.Add("Timestamp", 250);
            lstMain.View = View.Details;

            txtDriveMap.Text = Properties.Settings.Default.DriveMap;
            txtFileNumber.Text = Properties.Settings.Default.HeaderFileNumber;
            txtUserAgent.Text = Properties.Settings.Default.HeaderUserAgent;
            txtByteData.Text = Properties.Settings.Default.HeaderByteData;
            txtChunkNumber.Text = Properties.Settings.Default.HeaderDataChunk;
            txtUrl.Text = Properties.Settings.Default.Url;
            nudDataSize.Value = Properties.Settings.Default.DataSize;
            nudDelayMins.Value = Properties.Settings.Default.SendDelay;
            nudDelayVariance.Value = Properties.Settings.Default.DelayVariance;
            nudDataSizeVariance.Value = Properties.Settings.Default.DataSizeVariance;
            nudSecondDelayMins.Value = Properties.Settings.Default.AltSendDelay;
            ckbRandomBytes.Checked = Properties.Settings.Default.UseRandomBytes;
            ckbFileNumber.Checked = Properties.Settings.Default.HeaderFileNumberEnabled;
            ckbChunkNumber.Checked = Properties.Settings.Default.HeaderDataChunkEnabled;
            ckbSecondDelay.Checked = Properties.Settings.Default.AltDelayEnabled;
            ckbEnableDataVariance.Checked = Properties.Settings.Default.DataVarianceEnabled;


            toolTipMain.SetToolTip(ckbEnableDataVariance, "Note:  When enabled, data size can exceed 2048 bytes");
            toolTipMain.SetToolTip(nudDataSizeVariance, "Note:  When enabled, data size can exceed 2048 bytes");
            toolTipMain.SetToolTip(nudDataSize, "Max size 2048 bytes");

            toolTipMain.SetToolTip(nudDelayVariance, "% of the delay that adds a variance to the time that data is sent");

            toolTipMain.SetToolTip(nudSecondDelayMins, "When enabled, the delay alternates between the Send Delay and this value");
            toolTipMain.SetToolTip(ckbSecondDelay, "When enabled, the delay alternates between the Send Delay and this value");

            toolTipMain.SetToolTip(nudDelayMins, "Time in minutes to wait between when data is sent");

            toolTipMain.SetToolTip(txtUserAgent, "AGENT key text that is added to GET header");
            toolTipMain.SetToolTip(txtFileNumber, "Key name added to header representing the file number");
            toolTipMain.SetToolTip(txtChunkNumber, "Key name added to header representing the chunk number of a file when a file must be divided up");
            toolTipMain.SetToolTip(txtByteData, "Key name added to header representing the encoded/compressed byte data of a chunk of a file");

            toolTipMain.SetToolTip(txtDriveMap, "Path for files to be sent");
            toolTipMain.SetToolTip(txtUrl, "URL to sent GET to  -  Must have http or https in the address or error will occur");

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
                lstMain.Items.Clear();

                currentFile = "";
                currentDirectory = "";
                directories = new Queue<string>();
                currentDirectoryFiles = new Queue<string>();
                totalFiles = 0;
                totalBeacons = 0;
                currentFileByteStart = 0;
                currentChunkNum = 0;

                setControlsEnabled(false);

                ShowOutput("Starting Beacon Process");
                directories.Enqueue(txtDriveMap.Text);
                timerMain.Enabled = true;
                btnStart.Text = "Stop";
                btnPause.Enabled = true;
                btnPause.Text = "Pause";
                btnSendNow.Visible = true;
            }
            else
            {
                SetNextBeacon();
                timerMain.Enabled = false;

                setControlsEnabled(true);

                ShowOutput("Beacon Stopped Manually");
                btnStart.Text = "Start";
                btnPause.Enabled = false;
                btnPause.Text = "Pause";
                lblCurrentState.Text = "Stopped";
                lblCurrentState.ForeColor = System.Drawing.Color.Red;
                lblCurrentDelay.Text = "";
                lblStatus.Text = "";
                btnSendNow.Visible = false;
            }

        }

        /// <summary>
        /// Disables or enables controls that should not be changed when the app is running
        /// </summary>
        /// <param name="enabled"></param>
        private void setControlsEnabled(bool enabled)
        {
            txtUrl.Enabled = enabled;
            if (ckbFileNumber.Checked)
                txtFileNumber.Enabled = enabled;
            txtUserAgent.Enabled = enabled;
            txtByteData.Enabled = enabled;
            if (ckbChunkNumber.Checked)
                txtChunkNumber.Enabled = enabled;
            if (!ckbRandomBytes.Checked)
                txtDriveMap.Enabled = enabled;
            btnBrowse.Enabled = enabled;
            ckbRandomBytes.Enabled = enabled;
            ckbFileNumber.Enabled = enabled;
            ckbChunkNumber.Enabled = enabled;

            if (enabled)
            {
                pbLighthouse.Image = Resources.lighthouse_large;
            }
            else
            {
                pbLighthouse.Image = Resources.lighthouse;
            }
            pbLighthouse.Refresh();
            pbLighthouse.Visible = true;

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
                    delayMs = (int)Math.Round(nudDelayMins.Value * MILLISECONDS_PER_MIN, 0);
                else
                    delayMs = (int)Math.Round(nudSecondDelayMins.Value * MILLISECONDS_PER_MIN, 0);

            }
            else
            {
                delayMs = (int)Math.Round(nudDelayMins.Value * MILLISECONDS_PER_MIN, 0);
            }

            Random random = new Random();
            int varianceMax = (int)((nudDelayVariance.Value / 100) * delayMs);
            int randVariance = random.Next(-1 * varianceMax, varianceMax);

            currentDelay = delayMs + randVariance;

            nextBeaconTime = DateTime.Now.AddMilliseconds(currentDelay);
        }


        /// <summary>
        /// The main timer event that handles updating the UI and handles the delay between sending beacons
        /// </summary>
        /// <param name="sender">Object triggering event</param>
        /// <param name="e">Event parameters</param>
        private void timerMain_Tick(object sender, EventArgs e)
        {
            TimeSpan nextBeacon = new TimeSpan(nextBeaconTime.Ticks - DateTime.Now.Ticks);
            lblCurrentDelay.Text = "Current Delay:  " + (currentDelay / 1000).ToString() + " sec";
            var nextBeaconSeconds = Math.Round(nextBeacon.TotalSeconds, 0);
            if (nextBeaconSeconds < 0)
                nextBeaconSeconds = 0;
            lblStatus.Text = "Next Beacon:   " + Math.Round(nextBeaconSeconds, 0).ToString() + " sec";

            if (nextBeacon.TotalSeconds <= 0)
            {
                
                // Check to see if beacon is complete
                if (directories.Count == 0 && currentDirectoryFiles.Count == 0 && currentFile == "")
                {
                    SetNextBeacon();
                    timerMain.Enabled = false;
                    ShowOutput("DONE!");
                    btnStart.Text = "Start";
                    isRunning = false;
                    btnPause.Enabled = false;
                    btnPause.Text = "Pause";
                    lblCurrentState.Text = "Stopped";
                    lblCurrentState.ForeColor = System.Drawing.Color.Red;
                    setControlsEnabled(true);
                    return;
                }

                if (!ckbRandomBytes.Checked && currentFile == "")
                {
                    lblCurrentState.Text = "Searching for files";
                    lblCurrentState.ForeColor = System.Drawing.Color.White;
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
                        lblCurrentState.Text = "Sending Data";
                        lblCurrentState.ForeColor = System.Drawing.Color.Yellow;
                        Application.DoEvents();
                        byte[] bytes;
                        int bytesToRead;
                        long bytesRemaining;

                        var byteCount = Convert.ToInt32(nudDataSize.Value);
                        if (ckbEnableDataVariance.Checked)
                        {
                            var dataVariance = Convert.ToInt32(nudDataSizeVariance.Value);
                            Random random = new Random();
                            int varianceMax = Convert.ToInt32((nudDataSizeVariance.Value / 100) * nudDataSize.Value);
                            int randVariance = random.Next(-1 * varianceMax, varianceMax);
                            byteCount += randVariance;
                        }

                        if (ckbRandomBytes.Checked)
                        {
                            Random random = new Random();
                            bytesToRead = byteCount;
                            bytesRemaining = bytesToRead;
                            bytes = new byte[bytesToRead];
                            random.NextBytes(bytes);
                            currentChunkNum = 1;
                        }
                        else
                        {
                            FileInfo fi = new FileInfo(currentFile);
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
                        lblCurrentState.Text = "Running!!!";
                        lblCurrentState.ForeColor = System.Drawing.Color.Green;

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

                    lblFileCount.Text = "Beacons Sent:  " + totalBeacons.ToString();
                    SetNextBeacon();
                }

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
            if (!timerMain.Enabled)
            {
                ShowOutput("Restarted");
                btnPause.Text = "Pause";
                nextBeaconTime = DateTime.Now;
            }
            else
            {
                ShowOutput("Paused");
                btnPause.Text = "Resume";
                this.lblCurrentState.Text = "Paused";
                this.lblCurrentState.ForeColor = System.Drawing.Color.Yellow;
            }

            timerMain.Enabled = !timerMain.Enabled;

        }

        /// <summary>
        /// Skips the delay and sends the next beacon now
        /// </summary>
        /// <param name="sender">Object triggering event</param>
        /// <param name="e">Event parameters</param>
        private void btnSendNow_Click(object sender, EventArgs e)
        {
            nextBeaconTime = DateTime.Now;
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
            Properties.Settings.Default.DelayVariance = Convert.ToInt32(nudDelayVariance.Value);
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
        /// <param name="sender">Object triggering event</param>
        /// <param name="e">Event parameters</param>
        private void ckbFileNumber_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.HeaderFileNumberEnabled = ckbFileNumber.Checked;
            Properties.Settings.Default.Save();
            txtFileNumber.Enabled = ckbFileNumber.Checked;
        }

        /// <summary>
        /// Enable the naming of the Chunk Number in the HTML header using the text given by the user
        /// </summary>
        /// <param name="sender">Object triggering event</param>
        /// <param name="e">Event parameters</param>
        private void ckbChunkNumber_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.HeaderDataChunkEnabled = ckbFileNumber.Checked;
            Properties.Settings.Default.Save();
            txtChunkNumber.Enabled = ckbFileNumber.Checked;
        }

        /// <summary>
        /// The send delay in minutes
        /// </summary>
        /// <param name="sender">Object triggering event</param>
        /// <param name="e">Event parameters</param>
        private void nudSecondDelayMins_ValueChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.AltSendDelay = nudSecondDelayMins.Value;
            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// Change in the delay saved to config file for alternate delay
        /// </summary>
        /// <param name="sender">Object triggering event</param>
        /// <param name="e">Event parameters</param>
        private void ckbSecondDelay_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.AltDelayEnabled = ckbSecondDelay.Checked;
            Properties.Settings.Default.Save();
            nudSecondDelayMins.Enabled = ckbSecondDelay.Checked;
        }

        /// <summary>
        /// Change in the value for the sending of random data check box
        /// </summary>
        /// <param name="sender">Object triggering event</param>
        /// <param name="e">Event parameters</param>
        private void ckbRandomBytes_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.UseRandomBytes = ckbRandomBytes.Checked;
            Properties.Settings.Default.Save();
            btnBrowse.Enabled = !ckbRandomBytes.Checked;
            txtDriveMap.Enabled = !ckbRandomBytes.Checked;
        }

        /// <summary>
        ///  Change in the value for adding data size variance
        /// </summary>
        /// <param name="sender">Object triggering event</param>
        /// <param name="e">Event parameters</param>
        private void ckbEnableDataVariance_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.DataVarianceEnabled = ckbEnableDataVariance.Checked;
            Properties.Settings.Default.Save();
            nudDataSizeVariance.Enabled = ckbEnableDataVariance.Checked;
        }

        /// <summary>
        /// Called when the form is closed
        /// </summary>
        /// <param name="sender">Object triggering event</param>
        /// <param name="e">Event parameters</param>
        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isRunning)
            {
                // Display a MsgBox asking the user if they really want to close the application.
                if (MessageBox.Show("Do you want to close this application?", "BEAM",
                    MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    // Cancel the Closing event from closing the form.
                    e.Cancel = true;
                }
            }
        }
    }


}
