namespace DangNhap
{
    partial class RePort
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
           

            this.SuspendLayout();
            // 
            // rpvReport
            // 
            this.rpvReport.Location = new System.Drawing.Point(0, -3);
            this.rpvReport.Name = "rpvReport";
            this.rpvReport.Size = new System.Drawing.Size(920, 546);
            this.rpvReport.TabIndex = 0;
            this.rpvReport.Load += new System.EventHandler(this.rpvReport_Load);
            // 
            // RePort
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(917, 564);
            this.Controls.Add(this.rpvReport);
            this.Name = "RePort";
            this.Text = "RePort";
            this.Load += new System.EventHandler(this.RePort_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer rpvReport;
    }
}