namespace MyIPScaner
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            btnScan = new Button();
            txtEndIp = new TextBox();
            txtStartIp = new TextBox();
            label1 = new Label();
            label2 = new Label();
            chk_80 = new CheckBox();
            chk_443 = new CheckBox();
            chk_8080 = new CheckBox();
            lstResults = new ListBox();
            lblStatus = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            contextMenuStrip1 = new ContextMenuStrip(components);
            SuspendLayout();
            // 
            // btnScan
            // 
            btnScan.Location = new Point(288, 32);
            btnScan.Name = "btnScan";
            btnScan.Size = new Size(75, 23);
            btnScan.TabIndex = 0;
            btnScan.Text = "Scan";
            btnScan.UseVisualStyleBackColor = true;
            btnScan.Click += btnScan_Click;
            // 
            // txtEndIp
            // 
            txtEndIp.Location = new Point(182, 30);
            txtEndIp.Name = "txtEndIp";
            txtEndIp.Size = new Size(100, 23);
            txtEndIp.TabIndex = 1;
            txtEndIp.Text = "192.168.1.255";
            // 
            // txtStartIp
            // 
            txtStartIp.Location = new Point(58, 29);
            txtStartIp.Name = "txtStartIp";
            txtStartIp.Size = new Size(100, 23);
            txtStartIp.TabIndex = 2;
            txtStartIp.Text = "192.168.1.1";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(32, 33);
            label1.Name = "label1";
            label1.Size = new Size(20, 15);
            label1.TabIndex = 3;
            label1.Text = "IP:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(164, 32);
            label2.Name = "label2";
            label2.Size = new Size(12, 15);
            label2.TabIndex = 4;
            label2.Text = "-";
            // 
            // chk_80
            // 
            chk_80.AutoSize = true;
            chk_80.Location = new Point(58, 66);
            chk_80.Name = "chk_80";
            chk_80.Size = new Size(63, 19);
            chk_80.TabIndex = 5;
            chk_80.Text = "port 80";
            chk_80.UseVisualStyleBackColor = true;
            chk_80.Visible = false;
            // 
            // chk_443
            // 
            chk_443.AutoSize = true;
            chk_443.Location = new Point(127, 66);
            chk_443.Name = "chk_443";
            chk_443.Size = new Size(69, 19);
            chk_443.TabIndex = 6;
            chk_443.Text = "port 443";
            chk_443.UseVisualStyleBackColor = true;
            chk_443.Visible = false;
            // 
            // chk_8080
            // 
            chk_8080.AutoSize = true;
            chk_8080.Location = new Point(207, 66);
            chk_8080.Name = "chk_8080";
            chk_8080.Size = new Size(75, 19);
            chk_8080.TabIndex = 7;
            chk_8080.Text = "port 8080";
            chk_8080.UseVisualStyleBackColor = true;
            chk_8080.Visible = false;
            // 
            // lstResults
            // 
            lstResults.FormattingEnabled = true;
            lstResults.ItemHeight = 15;
            lstResults.Location = new Point(38, 100);
            lstResults.Name = "lstResults";
            lstResults.Size = new Size(325, 259);
            lstResults.TabIndex = 8;
            lstResults.MouseDown += lstResults_MouseDown;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(39, 377);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(45, 15);
            lblStatus.TabIndex = 9;
            lblStatus.Text = "Status: ";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(39, 85);
            label3.Name = "label3";
            label3.Size = new Size(17, 15);
            label3.TabIndex = 10;
            label3.Text = "IP";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(103, 85);
            label4.Name = "label4";
            label4.Size = new Size(39, 15);
            label4.TabIndex = 11;
            label4.Text = "Status";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(164, 85);
            label5.Name = "label5";
            label5.Size = new Size(29, 15);
            label5.TabIndex = 12;
            label5.Text = "Port";
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(61, 4);
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(394, 401);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(lblStatus);
            Controls.Add(lstResults);
            Controls.Add(chk_8080);
            Controls.Add(chk_443);
            Controls.Add(chk_80);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtStartIp);
            Controls.Add(txtEndIp);
            Controls.Add(btnScan);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form1";
            Text = "My Ip Scan";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnScan;
        private TextBox txtEndIp;
        private TextBox txtStartIp;
        private Label label1;
        private Label label2;
        private CheckBox chk_80;
        private CheckBox chk_443;
        private CheckBox chk_8080;
        private ListBox lstResults;
        private Label lblStatus;
        private Label label3;
        private Label label4;
        private Label label5;
        private ContextMenuStrip contextMenuStrip1;
    }
}
