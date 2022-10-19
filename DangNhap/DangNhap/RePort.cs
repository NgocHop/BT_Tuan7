using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DangNhap
{
    public partial class RePort : Form
    {
        DataTable dt;
        public RePort()
        {
            InitializeComponent();
        }
        public RePort(DataTable dt)
        {
            InitializeComponent();
            this.dt = dt;
        }

        private void RePort_Load(object sender, EventArgs e)
        {
            this.rpvReport.LocalReport.ReportEmbeddedResource = "DangNhap.PhieuMS.rdlc";
            this.rpvReport.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("PHIEUMUONSACH", this.dt));
            this.rpvReport.RefreshReport();

        }

        private void rpvReport_Load(object sender, EventArgs e)
        {

        }
    }
}
