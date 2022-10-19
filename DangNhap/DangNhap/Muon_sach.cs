using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
//using Microsoft.Office.Interop.Excel;
//using app = Microsoft.Office.Interop.Excel._Application;

namespace DangNhap
{
    public partial class Muon_sach : Form
    {
        SqlDataAdapter daMuonSach,daUser, daPhieuMuonSach;
        DataSet ds;
        DataView dvMuon;
        DataTable dtdanhsach;
        BindingSource bsdanhsach;
        SqlConnection conn = DataProvider.KetNoiDB();

        
        public Muon_sach()
        {
            InitializeComponent();

        }

        private void Muon_sach_Load(object sender, EventArgs e)
        {
            //btncong.Enabled = btntru.Enabled = btnxoa.Enabled = btnmuon.Enabled = false;
            nudSoLuongMuon.Value = 1;
            ds = new DataSet();
            daMuonSach = new SqlDataAdapter("SELECT * FROM SACH", conn);
            daMuonSach.Fill(ds, "SACH");
            dvMuon = new DataView(ds.Tables["SACH"]);
            dgvSach.DataSource = dvMuon;
            txtMaSach.DataBindings.Add("Text", dvMuon, "MASACH");
            txtTenSach.DataBindings.Add("Text", dvMuon, "TENSACH");
            txtSoluongTon.DataBindings.Add("Text", dvMuon, "SOLUONG");

            dtdanhsach = new DataTable();
            dtdanhsach.Columns.Add("MASACH");
            dtdanhsach.Columns.Add("TENSACH");
            dtdanhsach.Columns.Add("SOLUONGTON");
            dtdanhsach.Columns.Add("SOLUONGMUON");

            bsdanhsach = new BindingSource();
            bsdanhsach.DataSource = dtdanhsach;
            dgvDSSach.DataSource = bsdanhsach;

            daPhieuMuonSach = new SqlDataAdapter("SELECT * FROM PHIEUMUONSACH", conn);
            daPhieuMuonSach.Fill(ds, "PHIEUMUONSACH");
            SqlCommand ThemPMS = new SqlCommand("INSERT INTO PHIEUMUONSACH VALUES(@ngay,@TenUser, @hoten, @masach,@TenSach, @soluong)", conn);
            //ThemPMS.Parameters.Add("@maPMS", SqlDbType.NChar, 30, "MAPHIEUMUONSACH");
            ThemPMS.Parameters.Add("@ngay", SqlDbType.DateTime, 8, "NGAYDAT");
            ThemPMS.Parameters.Add("@TenUser", SqlDbType.NChar, 30, "TENUSER");
            ThemPMS.Parameters.Add("@hoten", SqlDbType.NChar, 30, "HOTEN");
            ThemPMS.Parameters.Add("@masach", SqlDbType.NChar, 30, "MASACH");
            ThemPMS.Parameters.Add("@TenSach", SqlDbType.NChar, 30, "TENSACH");
            ThemPMS.Parameters.Add("@soluong", SqlDbType.Int, 4, "SOLUONG");
            daPhieuMuonSach.InsertCommand = ThemPMS;

            User();
        }

        private void txtTim_KeyUp(object sender, KeyEventArgs e)
        {
            if(txtTim.Text.ToString()=="")
            {
                dvMuon.RowFilter = "";
                return;
            }
            dvMuon.RowFilter = string.Format("TENSACH LIKE '%{0}%'", txtTim.Text.ToString());
        }
        string strNhan;
        public Muon_sach(string nhangiatri) : this()
        {
            strNhan = nhangiatri;
            lblUser.Text = strNhan;
        }
        private void User()
        {
            daUser = new SqlDataAdapter("SELECT * FROM TAIKHOAN WHERE TENDANGNHAP = '" + lblUser.Text + "'", conn);
            daUser.Fill(ds, "TAIKHOAN");
            if (ds.Tables["TAIKHOAN"].Rows.Count==1)
            {
                txtMaTK.Text = ds.Tables["TAIKHOAN"].Rows[0]["MATK"].ToString();
                txtName.Text = ds.Tables["TAIKHOAN"].Rows[0]["HOTEN"].ToString();
                txtSDT.Text = ds.Tables["TAIKHOAN"].Rows[0]["SDT"].ToString();
                txtEmail.Text = ds.Tables["TAIKHOAN"].Rows[0]["EMAIL"].ToString();
                txtDiaChi.Text = ds.Tables["TAIKHOAN"].Rows[0]["DIACHI"].ToString();
            }
        }

        private void Muon_sach_FormClosing(object sender, FormClosingEventArgs e)
        {
            DangNhap dn = (DangNhap)this.Owner;
            dn.Show();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btncong.Enabled = btntru.Enabled = btnxoa.Enabled = btnmuon.Enabled = true;
            
            int SLTon, SLMuon;
            SLTon = int.Parse(txtSoluongTon.Text.ToString());
            SLMuon = int.Parse(nudSoLuongMuon.Value.ToString());
            if (SLMuon <= 0 || SLMuon > SLTon)
            {
                MessageBox.Show("Số lượng mượn phải lớn hơn 0 và nhỏ hơn hoặc bằng số lượng tồn!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                int masach = int.Parse(txtMaSach.Text);
                int i = 0;
                for(; i < dtdanhsach.Rows.Count;i++ )
                {
                    if (int.Parse(dtdanhsach.Rows[i]["MASACH"].ToString()) == masach)
                    {
                        int SoLuong = int.Parse(dtdanhsach.Rows[i]["SOLUONGMUON"].ToString()) + (int)nudSoLuongMuon.Value;
                        if (SoLuong > int.Parse(dtdanhsach.Rows[i]["SOLUONGTON"].ToString()))
                            break;
                        else
                        {
                            dtdanhsach.Rows[i]["SOLUONGMUON"] = SoLuong;

                            break;
                        }
                    }
                }
                if (i == dtdanhsach.Rows.Count)
                {
                    DataRow row = dtdanhsach.NewRow();
                    row["MASACH"] = txtMaSach.Text;
                    row["TENSACH"] = txtTenSach.Text;
                    row["SOLUONGTON"] = int.Parse(txtSoluongTon.Text);
                    row["SOLUONGMUON"] = (int)nudSoLuongMuon.Value;
                    dtdanhsach.Rows.Add(row);
                }
            }
        }

        private void btncong_Click(object sender, EventArgs e)
        {
            if(bsdanhsach.Current == null)
            {

                MessageBox.Show("Chưa chọn sách", "Xóa");
                return;
            }
            int i = bsdanhsach.Position;
            int SoLuong = int.Parse(dtdanhsach.Rows[i]["SOLUONGMUON"].ToString()) + 1;
            if (SoLuong > int.Parse(dtdanhsach.Rows[i]["SOLUONGTON"].ToString()))
            {
                MessageBox.Show("Số lượng mượn phải lớn hơn 0 và nhỏ hơn hoặc bằng số lượng tồn!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            else
            {
                dtdanhsach.Rows[i]["SOLUONGMUON"] = SoLuong;

                
            }
        }

        private void btntru_Click(object sender, EventArgs e)
        {
            if (bsdanhsach.Current == null)
            {

                MessageBox.Show("Chưa chọn sách", "Xóa");
                return;
            }
            int i = bsdanhsach.Position;
            int SoLuong = int.Parse(dtdanhsach.Rows[i]["SOLUONGMUON"].ToString()) - 1;
            if (SoLuong > int.Parse(dtdanhsach.Rows[i]["SOLUONGTON"].ToString()) || SoLuong <= 0)
            {
                MessageBox.Show("Số lượng mượn phải lớn hơn 0 và nhỏ hơn hoặc bằng số lượng tồn!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            else
            {
                dtdanhsach.Rows[i]["SOLUONGMUON"] = SoLuong;


            }
        }

        private void btnxoa_Click(object sender, EventArgs e)
        {
            if (bsdanhsach.Current == null)
            {

                MessageBox.Show("Chưa chọn sách", "Xóa");
                return;
            }
            bsdanhsach.RemoveCurrent();
        }

        private void btnmuon_Click(object sender, EventArgs e)
        {
            if (dtdanhsach.Rows.Count == 0)
            {
                MessageBox.Show("Chưa chon sách!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                DataRow PhieuMuonSach = ds.Tables["PHIEUMUONSACH"].NewRow();
                //PhieuMuonSach["MAPHIEUMUONSACH"] = "101";
                PhieuMuonSach["NGAYDAT"] = DateTime.Now;
                PhieuMuonSach["TENUSER"] = lblUser.Text;
                PhieuMuonSach["HOTEN"] = txtName.Text;
                PhieuMuonSach["MASACH"] = txtMaSach.Text;
                PhieuMuonSach["TENSACH"] = txtTenSach.Text;
                foreach (DataRow row in dtdanhsach.Rows)
                {
                    PhieuMuonSach["SOLUONG"] = row["SOLUONGMUON"];
                }
                ds.Tables["PHIEUMUONSACH"].Rows.Add(PhieuMuonSach);
                daPhieuMuonSach.Update(ds, "PHIEUMUONSACH");
                MessageBox.Show("Mượn thành công", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                RePort rp = new RePort(ds.Tables["PHIEUMUONSACH"]);
                rp.ShowDialog();
                dtdanhsach.Rows.Clear();
                
            }
        }


        //{
        //    //khai báo thư viện hỗ trợ Microsoft.Office.Interop.Excel
        //    Microsoft.Office.Interop.Excel.Application excel;
        //    Microsoft.Office.Interop.Excel.Workbook workbook;
        //    Microsoft.Office.Interop.Excel.Worksheet worksheet;
        //    try
        //    {
        //        //Tạo đối tượng COM.
        //        excel = new Microsoft.Office.Interop.Excel.Application();
        //        excel.Visible = false;
        //        excel.DisplayAlerts = false;
        //        //tạo mới một Workbooks bằng phương thức add()
        //        workbook = excel.Workbooks.Add(Type.Missing);
        //        worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Sheets["Sheet1"];
        //        //đặt tên cho sheet
        //        worksheet.Name = "Quản lý học sinh";

        //        // export header trong DataGridView
        //        for (int i = 0; i < dataGridView1.ColumnCount; i++)
        //        {
        //            worksheet.Cells[1, i + 1] = dataGridView1.Columns[i].HeaderText;
        //        }
        //        // export nội dung trong DataGridView
        //        for (int i = 0; i < dataGridView1.RowCount; i++)
        //        {
        //            for (int j = 0; j < dataGridView1.ColumnCount; j++)
        //            {
        //                worksheet.Cells[i + 2, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString();
        //            }
        //        }
        //        // sử dụng phương thức SaveAs() để lưu workbook với filename
        //        workbook.SaveAs(fileName);
        //        //đóng workbook
        //        workbook.Close();
        //        excel.Quit();
        //        MessageBox.Show("Xuất dữ liệu ra Excel thành công!");
        //    }nally
        //    {
        //        workbook = null;
        //        worksheet = null;
        //    }
        //}

      
    }
}
