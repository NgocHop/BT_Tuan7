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
    public partial class QLTK : Form
    {
        DataSet ds;
        SqlDataAdapter daTK, daSach, daLoaiSach;
        DataView dvTK, dvSach;
        BindingSource bsSach, bsTK, bsLoai;
        SqlConnection conn = DataProvider.KetNoiDB();
        string strNhan;
        private void QLTK_Load(object sender, EventArgs e)
        {
            DanhSachTK();
            DanhSachSach();
            //this.dgvDanhSachTK.DefaultCellStyle.ForeColor = Color.Black;
            //this.dgvDanhSachSach.DefaultCellStyle.ForeColor = Color.Black;


            conn.Close();
        }

        public QLTK(string nhangiatri)
            : this()
        {
            strNhan = nhangiatri;
            lblUser.Text = strNhan;
        }

        public QLTK()
        {
            InitializeComponent();
        }

        private void btnThemTK_Click(object sender, EventArgs e)
        {
            ThemTK ThemTK = new ThemTK();
            this.Hide();
            ThemTK.Show(this);
        }





        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (bsTK.Current == null)
            {
                MessageBox.Show("xoa that bai!", "xoa");
                return;
            }

            //int i = bsTK.Position;
            SqlConnection conn = DataProvider.KetNoiDB();
            string CauTruyVan = ("delete TAIKHOAN where TENDANGNHAP ='" + lblUser.Text + "'");
            SqlCommand sqlCommand = new SqlCommand(CauTruyVan, conn);
            //SqlDataReader sqlDataReader = sqlCommand.ExecuteNonQuery();
            int NumOfRows = sqlCommand.ExecuteNonQuery();
            if (NumOfRows > 0)
            {
                MessageBox.Show("Thêm tài khoản thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Thêm thất bại !", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            bsTK.RemoveCurrent();
            conn.Close();
        }

        private void DanhSachTK()
        {
            //daTK = new SqlDataAdapter("SELECT TENDANGNHAP, HOTEN as 'Họ và tên', NGAYSINH as 'Ngày sinh', PHAI as 'Giới tính', SDT as'Số điện thoại', DIACHI as 'Địa chỉ', EMAIL as 'Email', MACV as 'Mã chức vụ' from TAIKHOAN", conn);
            //ds = new DataSet();
            //daTK.Fill(ds, "TAIKHOAN");
            //dvTK = ds.Tables["TAIKHOAN"].DefaultView;

            //bsTK = new BindingSource();
            //bsTK.DataSource = dvTK;
            //dgvDanhSachTK.DataSource = dvTK;

            daTK = new SqlDataAdapter("SELECT TENDANGNHAP, HOTEN, NGAYSINH, PHAI, SDT, DIACHI, EMAIL, MACV from TAIKHOAN", conn);
            ds = new DataSet();
            daTK.Fill(ds, "TAIKHOAN");
            //bsTK = new BindingSource(ds, "TAIKHOAN");
            dvTK = new DataView(ds.Tables["TAIKHOAN"]);
            dgvDanhSachTK.DataSource = dvTK;

            txtUser.DataBindings.Add(new Binding("Text", dvTK, "TENDANGNHAP"));
            txtHoTen.DataBindings.Add(new Binding("Text", dvTK, "HOTEN"));
            txtPhai.DataBindings.Add(new Binding("Text", dvTK, "PHAI"));
            txtSDT.DataBindings.Add(new Binding("Text", dvTK, "SDT"));
            txtDiaChi.DataBindings.Add(new Binding("Text", dvTK, "DIACHI"));
            txtEmail.DataBindings.Add(new Binding("Text", dvTK, "EMAIL"));
            conn.Close();

        }
        private void txtTimTK_KeyUp(object sender, KeyEventArgs e)
        {

            if (txtTimTK.Text.ToString() == "")
            {
                dvTK.RowFilter = "";
                return;
            }
            dvTK.RowFilter = string.Format("TENDANGNHAP LIKE '%{0}%'", txtTimTK.Text.ToString());
        }

        private void DanhSachSach()
        {
            daSach = new SqlDataAdapter("SELECT MASACH,TENSACH,LOAISACH,SOLUONG,MOTA from SACH", conn);
            daLoaiSach = new SqlDataAdapter("SELECT * from LOAISACH", conn);
            daSach.Fill(ds, "SACH");
            dvSach = new DataView(ds.Tables["SACH"]);
            //daLoaiSach.Fill(ds, "LOAISACH");
            //bsSach = new BindingSource(ds, "SACH");
            //bsLoai = new BindingSource(ds, "LOAISACH");
            dgvDanhSachSach.DataSource = dvSach;
            txtMaSach.DataBindings.Add(new Binding("Text", dvSach, "MASACH"));
            txtTenSach.DataBindings.Add(new Binding("Text", dvSach, "TENSACH"));
            txtSoLuong.DataBindings.Add(new Binding("Text", dvSach, "SOLUONG"));
            txtLoaiSach.DataBindings.Add(new Binding("Text", dvSach, "LOAISACH"));
            txtMoTa.DataBindings.Add(new Binding("Text", dvSach, "MOTA"));
            conn.Close();
        }

        private void QLTK_FormClosing(object sender, FormClosingEventArgs e)
        {
            DangNhap dn = (DangNhap)this.Owner;
            dn.Show();
        }

        private void txtTimSach_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtTimSach.Text.ToString() == "")
            {
                dvSach.RowFilter = "";
                return;
            }
            dvSach.RowFilter = string.Format("TENSACH LIKE '%{0}%'", txtTimSach.Text.ToString());
        }

        private void btnThemSach_Click(object sender, EventArgs e)
        {
            ThemSach ThemSach = new ThemSach();
            this.Hide();
            ThemSach.Show(this);
        }
    }
}
