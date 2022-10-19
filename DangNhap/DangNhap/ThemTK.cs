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

namespace DangNhap
{
    public partial class ThemTK : Form
    {

        public ThemTK()
        {
            InitializeComponent();
        }
        private void TrangThaiBanDau()
        {
            txtTenDN.Text = "";
            txtMK.Text = "";
            txtHoTen.Text = "";
            txtSDT.Text = "";
            txtDiaChi.Text = "";
            txtEmail.Text = "";
            rdbNam.Checked = true;


        }
        private bool KiemTraKH(string TENDANGNHAP)
        {
            SqlConnection conn = DataProvider.KetNoiDB();
            string cautryvan = "Select * from TAIKHOAN where TENDANGNHAP = '" + TENDANGNHAP + "'";
            SqlCommand com = new SqlCommand(cautryvan, conn);
            SqlDataReader dr = com.ExecuteReader();
            bool Result = dr.HasRows;
            dr.Close();
            conn.Close();
            return Result;

        }

        private void ThemTK_Load(object sender, EventArgs e)
        {
            TrangThaiBanDau();
            LayDuLieuDuaVaoComboBox(ref cmbMaCV, "select MACV from CHUCVU");
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (KiemTraKH(txtTenDN.Text.ToString()))
            {
                MessageBox.Show("Tên đăng nhập Ðã Tồn tại !", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string phai = "";
            if (rdbNam.Checked == true)
            {
                phai = rdbNam.Text;
            }
            else
            {
                phai = rdbNu.Text;
            }
            SqlConnection conn = DataProvider.KetNoiDB();
            string cautruyvan = string.Format("insert into TAIKHOAN ([TENDANGNHAP],[MATKHAU],[HOTEN],[NGAYSINH],[PHAI],[SDT],[DIACHI],[EMAIL],[MACV]) values ('{0}','{1}',N'{2}','{3}',N'{4}','{5}',N'{6}','{7}','{8}')", txtTenDN.Text.ToString(),
              txtMK.Text.ToString(), txtHoTen.Text.ToString(), dtpNgaySinh.Value.ToString("yyyy-MM-dd"), phai, txtSDT.Text.ToString(), txtDiaChi.Text.ToString(), txtEmail.Text.ToString(), cmbMaCV.SelectedItem.ToString());
            SqlCommand com = new SqlCommand(cautruyvan, conn);
            int NumOfRows = com.ExecuteNonQuery();
            if (NumOfRows > 0)
            {
                MessageBox.Show("Thêm tài khoản thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Thêm thất bại !", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            conn.Close();
            TrangThaiBanDau();
        }
        private void LayDuLieuDuaVaoComboBox(ref ComboBox cmb, string cautruyvan)
        {
            SqlConnection sqlConnection = DataProvider.KetNoiDB();
            SqlCommand sqlCommand = new SqlCommand(cautruyvan, sqlConnection);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                cmb.Items.Add(reader[0]);
            }
            reader.Close();
        }

        private void ThemTK_FormClosing(object sender, FormClosingEventArgs e)
        {
            QLTK ql = (QLTK)this.Owner;
            ql.Show();
        }
    }
}
