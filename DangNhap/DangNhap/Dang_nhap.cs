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
    public partial class DangNhap : Form
    {
        public DangNhap()
        {
            InitializeComponent();
        }
        public void TrangThaiBanDau()
        {
            txtTaiKhoan.Text = "";
            txtMatKhau.Text = "";

        }

        private void DangNhap_Load(object sender, EventArgs e)
        {

        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            //kiểm tra nhập tài khoản mật khẩu
            if (txtTaiKhoan.Text.Trim() == "")
            {
                MessageBox.Show("Bạn chưa nhập tài khoản !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (txtMatKhau.Text.Trim() == "")
            {
                MessageBox.Show("Bạn chưa nhập mật khẩu !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //kết nói sql
            SqlConnection conn = DataProvider.KetNoiDB();
            string CauTruyVan = ("SELECT*FROM TAIKHOAN WHERE TENDANGNHAP ='" + txtTaiKhoan.Text.ToString() + "' AND MATKHAU = '" + txtMatKhau.Text.ToString() + "' AND MACV = '002'");
            SqlCommand com = new SqlCommand(CauTruyVan, conn);
            SqlDataReader dr = com.ExecuteReader();
            if(dr.HasRows)
            {
                Muon_sach ms = new Muon_sach(txtTaiKhoan.Text.ToString());
                this.Hide();
                TrangThaiBanDau();
                ms.Show(this);
                
            }
            else
            {
                dr.Close();
                CauTruyVan = ("SELECT*FROM TAIKHOAN WHERE TENDANGNHAP ='" + txtTaiKhoan.Text.ToString() + "' AND MATKHAU = '" + txtMatKhau.Text.ToString() + "' AND MACV = '003'");
                com = new SqlCommand(CauTruyVan, conn);
                dr = com.ExecuteReader();
                if (dr.HasRows)
                {
                    QLTK ql = new QLTK(txtTaiKhoan.Text.ToString());
                    this.Hide();
                    TrangThaiBanDau();
                    ql.Show(this);
                    
                }
                else
                {
                    MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng hoặc tài khoản không tồn tại!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    TrangThaiBanDau();
                }
                
            }
            //DangNhap dn = new DangNhap();
            //dn.Show();
            conn.Close();
        }
    }
}
