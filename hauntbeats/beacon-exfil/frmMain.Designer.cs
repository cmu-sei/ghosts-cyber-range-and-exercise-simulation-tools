namespace beacon_exfil
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panelMain = new System.Windows.Forms.Panel();
            this.label15 = new System.Windows.Forms.Label();
            this.panelParams = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.nudVariance = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.nudDataSize = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.nudDelayMins = new System.Windows.Forms.NumericUpDown();
            this.label14 = new System.Windows.Forms.Label();
            this.panelAddress = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDriveMap = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.label13 = new System.Windows.Forms.Label();
            this.txtUserAgent = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtByteData = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtChunkNumber = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtFileNumber = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.lstMain = new System.Windows.Forms.ListView();
            this.btnStart = new System.Windows.Forms.Button();
            this.lblCurrentDelay = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblCurrentState = new System.Windows.Forms.Label();
            this.timerMain = new System.Windows.Forms.Timer(this.components);
            this.lblFileCount = new System.Windows.Forms.Label();
            this.btnPause = new System.Windows.Forms.Button();
            this.ckbFileNumber = new System.Windows.Forms.CheckBox();
            this.ckbChunkNumber = new System.Windows.Forms.CheckBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.nudSecondDelayMins = new System.Windows.Forms.NumericUpDown();
            this.ckbSecondDelay = new System.Windows.Forms.CheckBox();
            this.ckbRandomBytes = new System.Windows.Forms.CheckBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.panelMain.SuspendLayout();
            this.panelParams.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudVariance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDataSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDelayMins)).BeginInit();
            this.panelAddress.SuspendLayout();
            this.panelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSecondDelayMins)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Location = new System.Drawing.Point(2, 610);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(53, 52);
            this.panel1.TabIndex = 0;
            // 
            // panelMain
            // 
            this.panelMain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelMain.BackColor = System.Drawing.SystemColors.Control;
            this.panelMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelMain.Controls.Add(this.label15);
            this.panelMain.Controls.Add(this.panelParams);
            this.panelMain.Controls.Add(this.label14);
            this.panelMain.Controls.Add(this.panelAddress);
            this.panelMain.Controls.Add(this.panelHeader);
            this.panelMain.Controls.Add(this.label9);
            this.panelMain.Location = new System.Drawing.Point(12, 12);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(910, 207);
            this.panelMain.TabIndex = 1;
            this.panelMain.Tag = "Setup";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(3, 144);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(78, 13);
            this.label15.TabIndex = 18;
            this.label15.Text = "Parameters :";
            // 
            // panelParams
            // 
            this.panelParams.BackColor = System.Drawing.SystemColors.Control;
            this.panelParams.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelParams.Controls.Add(this.ckbSecondDelay);
            this.panelParams.Controls.Add(this.label16);
            this.panelParams.Controls.Add(this.label17);
            this.panelParams.Controls.Add(this.nudSecondDelayMins);
            this.panelParams.Controls.Add(this.label4);
            this.panelParams.Controls.Add(this.label5);
            this.panelParams.Controls.Add(this.nudVariance);
            this.panelParams.Controls.Add(this.label6);
            this.panelParams.Controls.Add(this.nudDataSize);
            this.panelParams.Controls.Add(this.label3);
            this.panelParams.Controls.Add(this.label8);
            this.panelParams.Controls.Add(this.label7);
            this.panelParams.Controls.Add(this.nudDelayMins);
            this.panelParams.Location = new System.Drawing.Point(3, 158);
            this.panelParams.Name = "panelParams";
            this.panelParams.Size = new System.Drawing.Size(902, 44);
            this.panelParams.TabIndex = 16;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(97, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "minutes";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(447, 2);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(156, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Delay Variance (0% - 75%)";
            // 
            // nudVariance
            // 
            this.nudVariance.Location = new System.Drawing.Point(450, 18);
            this.nudVariance.Maximum = new decimal(new int[] {
            75,
            0,
            0,
            0});
            this.nudVariance.Name = "nudVariance";
            this.nudVariance.Size = new System.Drawing.Size(45, 20);
            this.nudVariance.TabIndex = 7;
            this.nudVariance.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudVariance.ValueChanged += new System.EventHandler(this.nudVariance_ValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(501, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(15, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "%";
            // 
            // nudDataSize
            // 
            this.nudDataSize.Location = new System.Drawing.Point(663, 18);
            this.nudDataSize.Maximum = new decimal(new int[] {
            2048,
            0,
            0,
            0});
            this.nudDataSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudDataSize.Name = "nudDataSize";
            this.nudDataSize.Size = new System.Drawing.Size(54, 20);
            this.nudDataSize.TabIndex = 10;
            this.nudDataSize.Value = new decimal(new int[] {
            2048,
            0,
            0,
            0});
            this.nudDataSize.ValueChanged += new System.EventHandler(this.nudDataSize_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(18, 2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Send Delay:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(723, 22);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(32, 13);
            this.label8.TabIndex = 12;
            this.label8.Text = "bytes";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(660, 2);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(241, 13);
            this.label7.TabIndex = 11;
            this.label7.Text = "Data Size to Read or Generate (1 - 2048)";
            // 
            // nudDelayMins
            // 
            this.nudDelayMins.DecimalPlaces = 2;
            this.nudDelayMins.Location = new System.Drawing.Point(21, 18);
            this.nudDelayMins.Maximum = new decimal(new int[] {
            10080,
            0,
            0,
            0});
            this.nudDelayMins.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.nudDelayMins.Name = "nudDelayMins";
            this.nudDelayMins.Size = new System.Drawing.Size(70, 20);
            this.nudDelayMins.TabIndex = 2;
            this.nudDelayMins.Value = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.nudDelayMins.ValueChanged += new System.EventHandler(this.nudDelayMins_ValueChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(433, 3);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(217, 13);
            this.label14.TabIndex = 17;
            this.label14.Text = "Data Source and Destination Values:";
            // 
            // panelAddress
            // 
            this.panelAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelAddress.Controls.Add(this.btnBrowse);
            this.panelAddress.Controls.Add(this.ckbRandomBytes);
            this.panelAddress.Controls.Add(this.label1);
            this.panelAddress.Controls.Add(this.txtDriveMap);
            this.panelAddress.Controls.Add(this.label2);
            this.panelAddress.Controls.Add(this.txtUrl);
            this.panelAddress.Location = new System.Drawing.Point(436, 16);
            this.panelAddress.Name = "panelAddress";
            this.panelAddress.Size = new System.Drawing.Size(469, 123);
            this.panelAddress.TabIndex = 16;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(20, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Files Location";
            // 
            // txtDriveMap
            // 
            this.txtDriveMap.Location = new System.Drawing.Point(23, 33);
            this.txtDriveMap.Name = "txtDriveMap";
            this.txtDriveMap.Size = new System.Drawing.Size(360, 20);
            this.txtDriveMap.TabIndex = 0;
            this.txtDriveMap.Text = "\\\\nec-file\\reports";
            this.txtDriveMap.TextChanged += new System.EventHandler(this.txtDriveMap_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(20, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(207, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Valid Web Address to Exfil Data to:";
            // 
            // txtUrl
            // 
            this.txtUrl.Location = new System.Drawing.Point(23, 79);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(414, 20);
            this.txtUrl.TabIndex = 1;
            this.txtUrl.Text = "http://itsalongwaytothetop.com";
            this.txtUrl.TextChanged += new System.EventHandler(this.txtUrl_TextChanged);
            // 
            // panelHeader
            // 
            this.panelHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelHeader.Controls.Add(this.ckbChunkNumber);
            this.panelHeader.Controls.Add(this.ckbFileNumber);
            this.panelHeader.Controls.Add(this.label13);
            this.panelHeader.Controls.Add(this.txtUserAgent);
            this.panelHeader.Controls.Add(this.label12);
            this.panelHeader.Controls.Add(this.txtByteData);
            this.panelHeader.Controls.Add(this.label11);
            this.panelHeader.Controls.Add(this.txtChunkNumber);
            this.panelHeader.Controls.Add(this.label10);
            this.panelHeader.Controls.Add(this.txtFileNumber);
            this.panelHeader.Location = new System.Drawing.Point(3, 16);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(418, 123);
            this.panelHeader.TabIndex = 15;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(3, 3);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(105, 13);
            this.label13.TabIndex = 21;
            this.label13.Text = "User Agent Value";
            // 
            // txtUserAgent
            // 
            this.txtUserAgent.Location = new System.Drawing.Point(3, 18);
            this.txtUserAgent.Name = "txtUserAgent";
            this.txtUserAgent.Size = new System.Drawing.Size(392, 20);
            this.txtUserAgent.TabIndex = 20;
            this.txtUserAgent.Text = "Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) ";
            this.txtUserAgent.TextChanged += new System.EventHandler(this.txtUserAgent_TextChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(120, 100);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(63, 13);
            this.label12.TabIndex = 19;
            this.label12.Text = "Byte Data";
            // 
            // txtByteData
            // 
            this.txtByteData.Location = new System.Drawing.Point(187, 96);
            this.txtByteData.Name = "txtByteData";
            this.txtByteData.Size = new System.Drawing.Size(100, 20);
            this.txtByteData.TabIndex = 18;
            this.txtByteData.Text = "BUFAUTH1";
            this.txtByteData.TextChanged += new System.EventHandler(this.txtByteData_TextChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(52, 74);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(132, 13);
            this.label11.TabIndex = 17;
            this.label11.Text = "Data Package Number";
            // 
            // txtChunkNumber
            // 
            this.txtChunkNumber.Location = new System.Drawing.Point(187, 70);
            this.txtChunkNumber.Name = "txtChunkNumber";
            this.txtChunkNumber.Size = new System.Drawing.Size(100, 20);
            this.txtChunkNumber.TabIndex = 16;
            this.txtChunkNumber.Text = "VAXAUTH5";
            this.txtChunkNumber.TextChanged += new System.EventHandler(this.txtChunkNumber_TextChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(110, 48);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(73, 13);
            this.label10.TabIndex = 15;
            this.label10.Text = "File Number";
            // 
            // txtFileNumber
            // 
            this.txtFileNumber.Location = new System.Drawing.Point(187, 44);
            this.txtFileNumber.Name = "txtFileNumber";
            this.txtFileNumber.Size = new System.Drawing.Size(100, 20);
            this.txtFileNumber.TabIndex = 14;
            this.txtFileNumber.Text = "TTXAUTH8";
            this.txtFileNumber.TextChanged += new System.EventHandler(this.txtFileNumber_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(3, 2);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(286, 13);
            this.label9.TabIndex = 13;
            this.label9.Text = "Values for GET header that embeds exfilled data:";
            // 
            // lstMain
            // 
            this.lstMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstMain.HideSelection = false;
            this.lstMain.Location = new System.Drawing.Point(12, 225);
            this.lstMain.Name = "lstMain";
            this.lstMain.Size = new System.Drawing.Size(910, 379);
            this.lstMain.TabIndex = 13;
            this.lstMain.UseCompatibleStateImageBehavior = false;
            this.lstMain.View = System.Windows.Forms.View.Details;
            // 
            // btnStart
            // 
            this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStart.Location = new System.Drawing.Point(815, 610);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(107, 42);
            this.btnStart.TabIndex = 14;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // lblCurrentDelay
            // 
            this.lblCurrentDelay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblCurrentDelay.AutoSize = true;
            this.lblCurrentDelay.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentDelay.Location = new System.Drawing.Point(332, 610);
            this.lblCurrentDelay.Name = "lblCurrentDelay";
            this.lblCurrentDelay.Size = new System.Drawing.Size(102, 16);
            this.lblCurrentDelay.TabIndex = 15;
            this.lblCurrentDelay.Text = "Current Delay";
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(332, 636);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(102, 16);
            this.lblStatus.TabIndex = 16;
            this.lblStatus.Text = "Current Delay";
            // 
            // lblCurrentState
            // 
            this.lblCurrentState.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblCurrentState.AutoSize = true;
            this.lblCurrentState.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentState.Location = new System.Drawing.Point(127, 636);
            this.lblCurrentState.Name = "lblCurrentState";
            this.lblCurrentState.Size = new System.Drawing.Size(102, 16);
            this.lblCurrentState.TabIndex = 17;
            this.lblCurrentState.Text = "Current Delay";
            // 
            // timerMain
            // 
            this.timerMain.Interval = 500;
            this.timerMain.Tick += new System.EventHandler(this.timerMain_Tick);
            // 
            // lblFileCount
            // 
            this.lblFileCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblFileCount.AutoSize = true;
            this.lblFileCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFileCount.Location = new System.Drawing.Point(127, 610);
            this.lblFileCount.Name = "lblFileCount";
            this.lblFileCount.Size = new System.Drawing.Size(102, 16);
            this.lblFileCount.TabIndex = 18;
            this.lblFileCount.Text = "Current Delay";
            // 
            // btnPause
            // 
            this.btnPause.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPause.Enabled = false;
            this.btnPause.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPause.Location = new System.Drawing.Point(733, 622);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(76, 30);
            this.btnPause.TabIndex = 19;
            this.btnPause.Text = "Pause";
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // ckbFileNumber
            // 
            this.ckbFileNumber.AutoSize = true;
            this.ckbFileNumber.Checked = true;
            this.ckbFileNumber.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbFileNumber.Location = new System.Drawing.Point(293, 48);
            this.ckbFileNumber.Name = "ckbFileNumber";
            this.ckbFileNumber.Size = new System.Drawing.Size(68, 17);
            this.ckbFileNumber.TabIndex = 22;
            this.ckbFileNumber.Text = "Enabled ";
            this.ckbFileNumber.UseVisualStyleBackColor = true;
            this.ckbFileNumber.CheckedChanged += new System.EventHandler(this.ckbFileNumber_CheckedChanged);
            // 
            // ckbChunkNumber
            // 
            this.ckbChunkNumber.AutoSize = true;
            this.ckbChunkNumber.Checked = true;
            this.ckbChunkNumber.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbChunkNumber.Location = new System.Drawing.Point(293, 74);
            this.ckbChunkNumber.Name = "ckbChunkNumber";
            this.ckbChunkNumber.Size = new System.Drawing.Size(68, 17);
            this.ckbChunkNumber.TabIndex = 23;
            this.ckbChunkNumber.Text = "Enabled ";
            this.ckbChunkNumber.UseVisualStyleBackColor = true;
            this.ckbChunkNumber.CheckedChanged += new System.EventHandler(this.ckbChunkNumber_CheckedChanged);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(269, 23);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(43, 13);
            this.label16.TabIndex = 15;
            this.label16.Text = "minutes";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(190, 3);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(167, 13);
            this.label17.TabIndex = 14;
            this.label17.Text = "Alternating Delay (Optional):";
            // 
            // nudSecondDelayMins
            // 
            this.nudSecondDelayMins.DecimalPlaces = 2;
            this.nudSecondDelayMins.Enabled = false;
            this.nudSecondDelayMins.Location = new System.Drawing.Point(193, 19);
            this.nudSecondDelayMins.Maximum = new decimal(new int[] {
            10080,
            0,
            0,
            0});
            this.nudSecondDelayMins.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.nudSecondDelayMins.Name = "nudSecondDelayMins";
            this.nudSecondDelayMins.Size = new System.Drawing.Size(70, 20);
            this.nudSecondDelayMins.TabIndex = 13;
            this.nudSecondDelayMins.Value = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.nudSecondDelayMins.ValueChanged += new System.EventHandler(this.nudSecondDelayMins_ValueChanged);
            // 
            // ckbSecondDelay
            // 
            this.ckbSecondDelay.AutoSize = true;
            this.ckbSecondDelay.Location = new System.Drawing.Point(324, 21);
            this.ckbSecondDelay.Name = "ckbSecondDelay";
            this.ckbSecondDelay.Size = new System.Drawing.Size(68, 17);
            this.ckbSecondDelay.TabIndex = 24;
            this.ckbSecondDelay.Text = "Enabled ";
            this.ckbSecondDelay.UseVisualStyleBackColor = true;
            this.ckbSecondDelay.CheckedChanged += new System.EventHandler(this.ckbSecondDelay_CheckedChanged);
            // 
            // ckbRandomBytes
            // 
            this.ckbRandomBytes.AutoSize = true;
            this.ckbRandomBytes.Location = new System.Drawing.Point(244, 10);
            this.ckbRandomBytes.Name = "ckbRandomBytes";
            this.ckbRandomBytes.Size = new System.Drawing.Size(139, 17);
            this.ckbRandomBytes.TabIndex = 24;
            this.ckbRandomBytes.Text = "Generate Random Data";
            this.ckbRandomBytes.UseVisualStyleBackColor = true;
            this.ckbRandomBytes.CheckedChanged += new System.EventHandler(this.ckbRandomBytes_CheckedChanged);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(389, 33);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 20);
            this.btnBrowse.TabIndex = 25;
            this.btnBrowse.Text = "Choose";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(934, 661);
            this.Controls.Add(this.btnPause);
            this.Controls.Add(this.lblFileCount);
            this.Controls.Add(this.lblCurrentState);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblCurrentDelay);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.lstMain);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(950, 700);
            this.Name = "frmMain";
            this.Text = "BEAM - Beacon Exfil Generator for AI and ML 1.0";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.panelParams.ResumeLayout(false);
            this.panelParams.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudVariance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDataSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDelayMins)).EndInit();
            this.panelAddress.ResumeLayout(false);
            this.panelAddress.PerformLayout();
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSecondDelayMins)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nudDelayMins;
        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.TextBox txtDriveMap;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown nudDataSize;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown nudVariance;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Panel panelAddress;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtUserAgent;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtByteData;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtChunkNumber;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtFileNumber;
        private System.Windows.Forms.ListView lstMain;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label lblCurrentDelay;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Panel panelParams;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblCurrentState;
        private System.Windows.Forms.Timer timerMain;
        private System.Windows.Forms.Label lblFileCount;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.CheckBox ckbChunkNumber;
        private System.Windows.Forms.CheckBox ckbFileNumber;
        private System.Windows.Forms.CheckBox ckbSecondDelay;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.NumericUpDown nudSecondDelayMins;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.CheckBox ckbRandomBytes;
    }
}

