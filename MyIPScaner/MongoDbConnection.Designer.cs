namespace MyIPScaner
{
    partial class MongoDbConnection
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
            btnCancel = new Button();
            btnTest = new Button();
            textBox1 = new TextBox();
            SuspendLayout();
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(180, 100);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 23);
            btnCancel.TabIndex = 0;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnTest
            // 
            btnTest.Location = new Point(12, 100);
            btnTest.Name = "btnTest";
            btnTest.Size = new Size(75, 24);
            btnTest.TabIndex = 1;
            btnTest.Text = "Test";
            btnTest.UseVisualStyleBackColor = true;
            btnTest.Click += button2_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(10, 15);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(245, 79);
            textBox1.TabIndex = 2;
            textBox1.Text = "mongodb://localhost:27017";
            // 
            // MongoDbConnection
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(268, 140);
            Controls.Add(textBox1);
            Controls.Add(btnTest);
            Controls.Add(btnCancel);
            Name = "MongoDbConnection";
            Text = "MongoDb Connection";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnCancel;
        private Button btnTest;
        private TextBox textBox1;
    }
}