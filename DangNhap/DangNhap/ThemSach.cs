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
    public partial class ThemSach : Form
    {
        public ThemSach()
        {
            InitializeComponent();
        }

        private void TrangThaiBanDau()
        {
            txtMaSach.Text = "";
            txtTenSach.Text = "";
            txtSoLuong.Text = "";
            txtMoTa.Text = "";
            txtTrangThai.Text = "";
        }

        private bool KiemTraSachTonTai(string MaSach)
        {
            SqlConnection conn = DataProvider.KetNoiDB();
            string cautryvan = "Select * from SACH where MASACH = '" + MaSach + "'";
            SqlCommand com = new SqlCommand(cautryvan, conn);
            SqlDataReader dr = com.ExecuteReader();
            bool Result = dr.HasRows;
            dr.Close();
            conn.Close();
            return Result;

        }

        private void btnHoanThanh_Click(object sender, EventArgs e)
        {
            if (KiemTraSachTonTai(txtTenSach.Text.ToString()))
            {
                MessageBox.Show("Tên đăng nhập Ðã Tồn tại !", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            SqlConnection conn = DataProvider.KetNoiDB();
            string cautruyvan = string.Format("insert into SACH ([MASACH],[TENSACH],[LOAISACH],[MATG],[SOLUONG],[MOTA],[TRANGTHAI]) values ('{0}',N'{1}','{2}','{3}','{4}',N'{5}', '{6}')", txtMaSach.Text.ToString(), txtTenSach.Text.ToString(), cmbLoaiSach.SelectedItem.ToString(),cmbTacGia.SelectedItem.ToString(), int.Parse(txtSoLuong.Text.ToString()), txtMoTa.Text.ToString(), int.Parse(txtTrangThai.Text.ToString()));
            SqlCommand com = new SqlCommand(cautruyvan, conn);
            int NumOfRows = com.ExecuteNonQuery();
            if (NumOfRows > 0)
            {
                MessageBox.Show("Thêm sách thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Thêm thất bại !", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            conn.Close();
            TrangThaiBanDau();
        }

        private void ThemSach_Load(object sender, EventArgs e)
        {
            TrangThaiBanDau();
            LayDuLieuDuaVaoComboBox(ref cmbLoaiSach, "select MALOAISACH from LOAISACH");
            LayDuLieuDuaVaoComboBox(ref cmbTacGia, "select MATG from TACGIA");
            //LayDuLieuDuaVaoComboBoxMaTG(ref cmbTacGia, "select MATG from TACGIA");
        }

        private void LayDuLieuDuaVaoComboBox(ref ComboBox cmb, string CauTruyVan)
        {
            SqlConnection sqlConnection = DataProvider.KetNoiDB();
            SqlCommand sqlCommand = new SqlCommand(CauTruyVan, sqlConnection);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                cmb.Items.Add(reader[0]);
            }
            reader.Close();
        }

        private void ThemSach_FormClosing(object sender, FormClosingEventArgs e)
        {
            QLTK ql = (QLTK)this.Owner;
            ql.Show();
        }

        //private void LayDuLieuDuaVaoComboBoxMaTG(ref ComboBox cmb, string CauTruyVan)
        //{
        //    SqlConnection sqlConnection = DataProvider.KetNoiDB();
        //    SqlCommand sqlCommand = new SqlCommand(CauTruyVan, sqlConnection);
        //    SqlDataReader reader = sqlCommand.ExecuteReader();
        //    while (reader.Read())
        //    {
        //        cmb.Items.Add(reader[0]);
        //    }
        //    reader.Close();
        //}
    }
}
