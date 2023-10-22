namespace DWinFormsThreadException
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
            bGetQotd = new Button();
            tbQotD = new TextBox();
            SuspendLayout();
            // 
            // bGetQotd
            // 
            bGetQotd.Location = new Point(32, 36);
            bGetQotd.Name = "bGetQotd";
            bGetQotd.Size = new Size(315, 29);
            bGetQotd.TabIndex = 0;
            bGetQotd.Text = "Get Quote of the Day";
            bGetQotd.UseVisualStyleBackColor = true;
            bGetQotd.Click += bGetQotd_Click;
            // 
            // tbQotD
            // 
            tbQotD.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point);
            tbQotD.Location = new Point(32, 100);
            tbQotD.Multiline = true;
            tbQotD.Name = "tbQotD";
            tbQotD.ReadOnly = true;
            tbQotD.Size = new Size(729, 304);
            tbQotD.TabIndex = 1;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(tbQotD);
            Controls.Add(bGetQotd);
            Name = "Form1";
            Text = "WinForms and Thread Exceptions";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button bGetQotd;
        private TextBox tbQotD;
    }
}