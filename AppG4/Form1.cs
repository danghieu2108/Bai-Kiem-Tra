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

namespace AppG4
{
    public partial class Form1 : Form
    {
        //Ngành học: 1-Văn; 2-Vật Lý; 3-CNTT
        public static int NganhHoc;
        //Để insert
        public static int idSinhVienMax;
        QL_NhanvienEntities db = new QL_NhanvienEntities();
        SinhVien sv;
        SinhVien svCurrent;
        Boolean kt;
        int idNhomMonHoc;
        public Form1()
        {
            InitializeComponent();
        }

        private void ToolStripLabel1_Click(object sender, EventArgs e)
        {

        }


        private void Form1_Load(object sender, EventArgs e)
        {
            a FillData(db.SinhViens.ToArray()[0].HoTen);
            var dsSV = db.SinhViens.ToArray();
            idSinhVienMax = dsSV.Length;
            for (int i = 0; i < idSinhVienMax; i++) {
                checkedListBox1.Items.Add(dsSV[i].HoTen);
            };
        }

        private void ToolStripDropDownButton1_Click(object sender, EventArgs e)
        {

        }

        //Kiểm tra tính đúng
        private void TextBox5_TextChanged(object sender, EventArgs e)
        {
            if (idNhomMonHoc == 2 && txtHatNhan.Text != "")
            {
                try
                {
                    txtDiemTB.Text = ((float)(int.Parse(txtCoHoc.Text.ToString()) + int.Parse(txtQuangHoc.Text.ToString()) + int.Parse(txtDien.Text.ToString()) + int.Parse(txtHatNhan.Text.ToString())) / 4).ToString();
                }
                catch (Exception)
                {
                    if (!kt)
                    {
                        MessageBox.Show("Nhập điểm sai!");
                        txtHatNhan.Text = "";
                    }

                }
            }
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }
        
        //Điền dữ liệu vào form
        private void FillData(String HoTen)
        {
            //kt để biết fill lần đầu kiểm tra tính đúng dữ liệu
            try
            {
                db = new QL_NhanvienEntities();
                kt = true;
                var a = db.SinhViens.First(n => n.HoTen == HoTen);
                var diem = db.QuaTrinhHocTaps.Where(n => n.idSinhVien == a.idSinhVien).ToArray();
                var monhoc = db.MonHocs.ToArray();
                var qtht = db.QuaTrinhHocTaps.ToArray();


                var idVanCD = monhoc.First(n => n.TenMonHoc == "Văn học CĐ").idMonHoc;
                var idVanHD = monhoc.First(n => n.TenMonHoc == "Văn học HĐ").idMonHoc;
                var idCoHoc = monhoc.First(n => n.TenMonHoc == "Cơ học").idMonHoc;
                var idQuangHoc = monhoc.First(n => n.TenMonHoc == "Quang học").idMonHoc;
                var idDien = monhoc.First(n => n.TenMonHoc == "Điện").idMonHoc;
                var idVLHN = monhoc.First(n => n.TenMonHoc == "VL hạt nhân").idMonHoc;
                var idPascal = monhoc.First(n => n.TenMonHoc == "Pascal").idMonHoc;
                var idCsharp = monhoc.First(n => n.TenMonHoc == "C#").idMonHoc;
                var idSQL = monhoc.First(n => n.TenMonHoc == "SQL").idMonHoc;

                //Reset 
                txtName.Text = "";
                gioiTinh.Checked = false;
                txtVanHocCD.Text = "";
                txtVanHocHD.Text = "";
                txtCoHoc.Text = "";
                txtQuangHoc.Text = "";
                txtDien.Text = "";
                txtHatNhan.Text = "";
                txtPascal.Text = "";
                txtCsharp.Text = "";
                txtSQL.Text = "";
                //Reset

                txtName.Text = a.HoTen;
                sv = a;
                DateTime d;
                DateTime.TryParse(a.NgaySinh.ToString(), out d);

                ngaySinh.Value = d;
                if (a.GioiTinh == true)
                    gioiTinh.Checked = true;
                else
                    gioiTinh.Checked = false;
                idNhomMonHoc = monhoc.First(n => n.idMonHoc == qtht.First(m => m.idSinhVien == a.idSinhVien).idMonHoc).idNhomMonHoc;
                if (idNhomMonHoc == 1)
                {
                    txtVanHocCD.Text = diem.First(n => n.idMonHoc == idVanCD).Diem.ToString();
                    txtVanHocHD.Text = diem.First(n => n.idMonHoc == idVanHD).Diem.ToString();
                    float dtb = (float)(diem.First(n => n.idMonHoc == idVanCD).Diem + diem.First(n => n.idMonHoc == idVanHD).Diem) / 2;
                    txtDiemTB.Text = dtb.ToString();
                }
                if (idNhomMonHoc == 2)
                {
                    txtCoHoc.Text = diem.First(n => n.idMonHoc == idCoHoc).Diem.ToString();
                    txtQuangHoc.Text = diem.First(n => n.idMonHoc == idQuangHoc).Diem.ToString();
                    txtDien.Text = diem.First(n => n.idMonHoc == idDien).Diem.ToString();
                    txtHatNhan.Text = diem.First(n => n.idMonHoc == idVLHN).Diem.ToString();
                    float dtb = (float)(diem.First(n => n.idMonHoc == idCoHoc).Diem + diem.First(n => n.idMonHoc == idQuangHoc).Diem + diem.First(n => n.idMonHoc == idDien).Diem + diem.First(n => n.idMonHoc == idVLHN).Diem) / 4;
                    txtDiemTB.Text = dtb.ToString();
                }
                if (idNhomMonHoc == 3)
                {
                    txtPascal.Text = diem.First(n => n.idMonHoc == idPascal).Diem.ToString();
                    txtCsharp.Text = diem.First(n => n.idMonHoc == idCsharp).Diem.ToString();
                    txtSQL.Text = diem.First(n => n.idMonHoc == idSQL).Diem.ToString();
                    float dtb = (float)(diem.First(n => n.idMonHoc == idPascal).Diem + diem.First(n => n.idMonHoc == idCsharp).Diem + diem.First(n => n.idMonHoc == idSQL).Diem) / 3;
                    txtDiemTB.Text = dtb.ToString();
                }
                kt = false;
            }
            catch (Exception)
            {
                MessageBox.Show("Sinh viên này chưa đăng ký học bất kỳ môn nào!");
            }
        }

        private void CheckedListBox1_Click(object sender, EventArgs e)
        {

        }


        //Sự kiện click vào CheckedListBox
        private void CheckedListBox1_Click_1(object sender, EventArgs e)
        {
            svCurrent = db.SinhViens.First(n => n.HoTen == checkedListBox1.SelectedItem.ToString());
            FillData(checkedListBox1.SelectedItem.ToString());
            var dsSV = db.SinhViens.ToArray();
            checkedListBox1.Items.Clear();
            idSinhVienMax = dsSV.Length;
            for (int i = 0; i < idSinhVienMax; i++)
            {
                checkedListBox1.Items.Add(dsSV[i].HoTen);
            };
        }

        private void TxtVanHocCD_TextChanged(object sender, EventArgs e)
        {
            if (idNhomMonHoc == 1 && txtVanHocCD.Text != "")
            {
                try
                {
                    txtDiemTB.Text = ((float)(int.Parse(txtVanHocCD.Text.ToString()) + int.Parse(txtVanHocHD.Text.ToString())) / 2).ToString();
                }
                catch (Exception)
                {
                    if (!kt)
                    {
                        MessageBox.Show("Nhập điểm sai!");
                        txtVanHocCD.Text = "";
                        txtDiemTB.Text = "";
                    }
                }
            }
        }

        private void TxtVanHocHD_TextChanged(object sender, EventArgs e)
        {
            if (idNhomMonHoc == 1 && txtVanHocHD.Text != "")
            {
                try
                {
                    txtDiemTB.Text = ((float)(int.Parse(txtVanHocCD.Text.ToString()) + int.Parse(txtVanHocHD.Text.ToString())) / 2).ToString();
                }
                catch (Exception)
                {
                    if (!kt)
                    {
                        MessageBox.Show("Nhập điểm sai!");
                        txtVanHocHD.Text = ""; txtDiemTB.Text = "";
                    }

                }
            }
        }

        private void TxtCoHoc_TextChanged(object sender, EventArgs e)
        {
            if (idNhomMonHoc == 2 && txtCoHoc.Text != "")
            {
                try
                {
                    txtDiemTB.Text = ((float)(int.Parse(txtCoHoc.Text.ToString()) + int.Parse(txtQuangHoc.Text.ToString()) + int.Parse(txtDien.Text.ToString()) + int.Parse(txtHatNhan.Text.ToString())) / 4).ToString();
                }
                catch (Exception)
                {
                    if (!kt)
                    {
                        MessageBox.Show("Nhập điểm sai!");
                        txtCoHoc.Text = ""; txtDiemTB.Text = "";
                    }

                }
            }
        }

        private void TxtQuangHoc_TextChanged(object sender, EventArgs e)
        {
            if (idNhomMonHoc == 2 && txtQuangHoc.Text != "")
            {
                try
                {
                    txtDiemTB.Text = ((float)(int.Parse(txtCoHoc.Text.ToString()) + int.Parse(txtQuangHoc.Text.ToString()) + int.Parse(txtDien.Text.ToString()) + int.Parse(txtHatNhan.Text.ToString())) / 4).ToString();
                }
                catch (Exception)
                {
                    if (!kt)
                    {
                        MessageBox.Show("Nhập điểm sai!");
                        txtQuangHoc.Text = ""; txtDiemTB.Text = "";
                    }

                }
            }
        }

        private void TxtDien_TextChanged(object sender, EventArgs e)
        {
            if (idNhomMonHoc == 2 && txtDien.Text != "")
            {
                try
                {
                    txtDiemTB.Text = ((float)(int.Parse(txtCoHoc.Text.ToString()) + int.Parse(txtQuangHoc.Text.ToString()) + int.Parse(txtDien.Text.ToString()) + int.Parse(txtHatNhan.Text.ToString())) / 4).ToString();
                }
                catch (Exception)
                {
                    if (!kt)
                    {
                        MessageBox.Show("Nhập điểm sai!");
                        txtDien.Text = ""; txtDiemTB.Text = "";
                    }

                }
            }
        }

        private void TxtPascal_TextChanged(object sender, EventArgs e)
        {
            if (idNhomMonHoc == 3 && txtPascal.Text != "")
            {
                try
                {
                    txtDiemTB.Text = ((float)(int.Parse(txtPascal.Text.ToString()) + int.Parse(txtCsharp.Text.ToString()) + int.Parse(txtSQL.Text.ToString())) / 3).ToString();
                }
                catch (Exception)
                {
                    if (!kt)
                    {
                        MessageBox.Show("Nhập điểm sai!");
                        txtPascal.Text = ""; txtDiemTB.Text = "";
                    }

                }
            }
        }

        private void TxtCsharp_TextChanged(object sender, EventArgs e)
        {
            if (idNhomMonHoc == 3 && txtCsharp.Text != "")
            {
                try
                {
                    txtDiemTB.Text = ((float)(int.Parse(txtPascal.Text.ToString()) + int.Parse(txtCsharp.Text.ToString()) + int.Parse(txtSQL.Text.ToString())) / 3).ToString();
                }
                catch (Exception)
                {
                    if (!kt)
                    {
                        MessageBox.Show("Nhập điểm sai!");
                        txtCsharp.Text = ""; txtDiemTB.Text = "";
                    }

                }
            }
        }

        private void TxtSQL_TextChanged(object sender, EventArgs e)
        {
            if (idNhomMonHoc == 3 && txtSQL.Text != "")
            {
                try
                {
                    txtDiemTB.Text = ((float)(int.Parse(txtPascal.Text.ToString()) + int.Parse(txtCsharp.Text.ToString()) + int.Parse(txtSQL.Text.ToString())) / 3).ToString();
                }
                catch (Exception)
                {
                    if (!kt)
                    {
                        MessageBox.Show("Nhập điểm sai!");
                        txtSQL.Text = ""; txtDiemTB.Text = "";
                    }

                }
            }
        }

        private void LưuToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }


        //Hàm thực hiện lưu điểm vào cơ sở dữ liệu
        private void SaveOne(int IdSV, int diem, int idMonHoc)
        {
            SqlConnection conn = DBUtils.GetDBConnection();
            conn.Open();
            try
            {
                //string sql = "Update Employee set Salary = @salary where Emp_Id = @empId";
                string sql = "Update QuaTrinhHocTap set Diem = @diem where idSinhVien = @idSV and idMonHoc = @idMonHoc";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = sql;

                // Thêm và sét đặt giá trị cho tham số.
                cmd.Parameters.Add("@idSV", SqlDbType.Int).Value = IdSV;
                cmd.Parameters.Add("@diem", SqlDbType.Int).Value = diem;
                cmd.Parameters.Add("@idMonHoc", SqlDbType.Int).Value = idMonHoc;

                // Thực thi Command (Dùng cho delete, insert, update).
                int rowCount = cmd.ExecuteNonQuery();
                Console.WriteLine("Row Count affected = " + rowCount);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
                Console.WriteLine(e.StackTrace);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                conn = null;
            }
        }

        private void ToolStripButton9_Click(object sender, EventArgs e)
        {
            try
            {
                if (idNhomMonHoc == 1)
                {
                    SaveOne(svCurrent.idSinhVien, int.Parse(txtVanHocCD.Text.ToString()), 5);
                    SaveOne(svCurrent.idSinhVien, int.Parse(txtVanHocHD.Text.ToString()), 6);
                    if (txtCoHoc.Text != "" || txtQuangHoc.Text != "" || txtDien.Text != "" || txtHatNhan.Text != "" || txtPascal.Text != "" || txtCsharp.Text != "" || txtSQL.Text != "")
                        MessageBox.Show("Đã nhập thừa điểm ở bộ môn sinh viên không theo học!");
                }
                if (idNhomMonHoc == 2)
                {
                    SaveOne(svCurrent.idSinhVien, int.Parse(txtCoHoc.Text.ToString()), 1);
                    SaveOne(svCurrent.idSinhVien, int.Parse(txtQuangHoc.Text.ToString()), 2);
                    SaveOne(svCurrent.idSinhVien, int.Parse(txtDien.Text.ToString()), 3);
                    SaveOne(svCurrent.idSinhVien, int.Parse(txtHatNhan.Text.ToString()), 4);
                    if (txtVanHocCD.Text != "" || txtVanHocHD.Text != "" || txtPascal.Text != "" || txtCsharp.Text != "" || txtSQL.Text != "")
                        MessageBox.Show("Đã nhập thừa điểm ở bộ môn sinh viên không theo học!");
                }
                if (idNhomMonHoc == 3)
                {
                    SaveOne(svCurrent.idSinhVien, int.Parse(txtPascal.Text.ToString()), 7);
                    SaveOne(svCurrent.idSinhVien, int.Parse(txtCsharp.Text.ToString()), 8);
                    SaveOne(svCurrent.idSinhVien, int.Parse(txtSQL.Text.ToString()), 9);
                    if (txtCoHoc.Text != "" || txtQuangHoc.Text != "" || txtDien.Text != "" || txtHatNhan.Text != "" || txtVanHocCD.Text != "" || txtVanHocHD.Text != "")
                        MessageBox.Show("Đã nhập thừa điểm ở bộ môn sinh viên không theo học!");
                }

                MessageBox.Show("Cập nhật thành công!");
                FillData(svCurrent.HoTen);
            }
            catch (Exception)
            {
                MessageBox.Show("Cập nhật thất bại!");
            }
        }


        //Hàm xoá sinh viên, vì sinh viên tồn tại trong 3 bảng nên phải xoá 3 lần
        private void DeleteSV(int maSV)
        {
            SqlConnection conn = DBUtils.GetDBConnection();
            conn.Open();
            try
            {

                string sql = "Delete from SinhVien where idSinhVien = @idSV ";

                SqlCommand cmd = new SqlCommand();

                cmd.Connection = conn;
                cmd.CommandText = sql;

                cmd.Parameters.Add("@idSV", SqlDbType.Int).Value = maSV;

                // Thực thi Command (Dùng cho delete, insert, update).
                int rowCount = cmd.ExecuteNonQuery();

                Console.WriteLine("Row Count affected = " + rowCount);
                MessageBox.Show("Xoá sinh viên thành công");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
                Console.WriteLine(e.StackTrace);
                MessageBox.Show("Xoá sinh viên thất bại");
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                conn = null;
            }

            Console.Read();
        }


        //Xoá sinh viên lần 2
        private void DeleteQTHT(int maSV)
        {
            SqlConnection conn = DBUtils.GetDBConnection();
            conn.Open();
            try
            {

                string sql = "Delete from QuaTrinhHocTap where idSinhVien = @idSV ";

                SqlCommand cmd = new SqlCommand();

                cmd.Connection = conn;
                cmd.CommandText = sql;

                cmd.Parameters.Add("@idSV", SqlDbType.Int).Value = maSV;

                // Thực thi Command (Dùng cho delete, insert, update).
                int rowCount = cmd.ExecuteNonQuery();

                Console.WriteLine("Row Count affected = " + rowCount);
                MessageBox.Show("Xoá sinh viên thành công (QTHT)");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
                Console.WriteLine(e.StackTrace);
                MessageBox.Show("Xoá sinh viên thất bại (QTHT)");
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                conn = null;
            }

            Console.Read();
        }


        //Xoá sinh viên lần 3
        private void DeleteQuanLyKhoa(int maSV)
        {
            SqlConnection conn = DBUtils.GetDBConnection();
            conn.Open();
            try
            {

                string sql = "Delete from QuaTrinhHocTap where idSinhVien = @idSV ";

                SqlCommand cmd = new SqlCommand();

                cmd.Connection = conn;
                cmd.CommandText = sql;

                cmd.Parameters.Add("@idSV", SqlDbType.Int).Value = maSV;

                // Thực thi Command (Dùng cho delete, insert, update).
                int rowCount = cmd.ExecuteNonQuery();

                Console.WriteLine("Row Count affected = " + rowCount);
                MessageBox.Show("Xoá sinh viên thành công (QLK)");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
                Console.WriteLine(e.StackTrace);
                MessageBox.Show("Xoá sinh viên thất bại (QLK)");
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                conn = null;
            }

            Console.Read();
        }

        private void SinhViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NganhHoc = 1;
            new FrmThemSV().ShowDialog();
        }

        private void SVVậtLýToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NganhHoc = 2;
            new FrmThemSV().ShowDialog();
        }

        private void SVCNTTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NganhHoc = 3;
            new FrmThemSV().ShowDialog();
        }


        //Click vào nút xoá
        private void ToolStripButton8_Click(object sender, EventArgs e)
        {
            DeleteSV(svCurrent.idSinhVien);
            DeleteQTHT(svCurrent.idSinhVien);
            DeleteQuanLyKhoa(svCurrent.idSinhVien);
            var dsSV = db.SinhViens.ToArray();
            checkedListBox1.Items.Clear();
            idSinhVienMax = dsSV.Length;
            for (int i = 0; i < idSinhVienMax; i++)
            {
                checkedListBox1.Items.Add(dsSV[i].HoTen);
            };
        }


        //Thực hiện reset dữ liệu để thân thiện hơn với người dùng
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            var dsSV = db.SinhViens.ToArray();
            checkedListBox1.Items.Clear();
            idSinhVienMax = dsSV.Length;
            for (int i = 0; i < idSinhVienMax; i++)
            {
                checkedListBox1.Items.Add(dsSV[i].HoTen);
            };
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
