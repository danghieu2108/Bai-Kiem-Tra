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
    public partial class FrmThemSV : Form
    {
        public FrmThemSV()
        {
            InitializeComponent();
        }

        //Sự kiện click vào nút thêm
        private void Button1_Click(object sender, EventArgs e)
        {
            int idSV = Form1.idSinhVienMax + 1;
            InsertSV(txtTenSV.Text.ToString(), checkBox1.Checked, dateTimePicker1.Value, idSV);
            InsertQuanLyKhoa(Form1.NganhHoc, idSV);
            this.Close();
        }

        //Hàm insert thực hiện thêm sinh viên, vì sinh viên mới đăng ký nên chỉ cần chèn vào 2 bảng: bảng Quản lý khoa và bảng Sinh viên
        public void InsertSV(String tenSV, Boolean gioiTinh, DateTime ngaySinh, int idSV)
        {
            SqlConnection connection = DBUtils.GetDBConnection();
            connection.Open();
            try
            {
                // Câu lệnh Insert.
                string sql = "insert into SinhVien (idSinhVien, HoTen, GioiTinh, NgaySinh) "
                                                 + " values (@idSV,@HoTen,@GioiTinh, @NgaySinh) ";

                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = sql;

                // Tạo một đối tượng Parameter.
                SqlParameter gradeParam = new SqlParameter("@idSV", SqlDbType.Int);
                gradeParam.Value = idSV;
                cmd.Parameters.Add(gradeParam);

                // Thêm tham số @highSalary (Viết ngắn hơn).
                SqlParameter highSalaryParam = cmd.Parameters.Add("@HoTen", SqlDbType.NVarChar);
                highSalaryParam.Value = tenSV;

                // Thêm tham số @lowSalary (Viết ngắn hơn nữa).
                cmd.Parameters.Add("@GioiTinh", SqlDbType.Bit).Value = gioiTinh;

                cmd.Parameters.Add("@NgaySinh", SqlDbType.DateTime).Value = ngaySinh;

                // Thực thi Command (Dùng cho delete, insert, update).
                int rowCount = cmd.ExecuteNonQuery();

                Console.WriteLine("Row Count affected = " + rowCount);
                MessageBox.Show("Thêm sinh viên thành công");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
                Console.WriteLine(e.StackTrace);
                MessageBox.Show("Thêm sinh viên thất bại");
            }
            finally
            {
                connection.Close();
                connection.Dispose();
                connection = null;
            }

            Console.Read();
        }

        //Chèn sinh viên lần 2
        public void InsertQuanLyKhoa(int idNhomMonHoc, int idSV)
        {
            SqlConnection connection = DBUtils.GetDBConnection();
            connection.Open();
            try
            {
                // Câu lệnh Insert.
                string sql = "insert into QuanLyKhoa (idNhomMonHoc, idSinhVien) " + " values (@idNhomMonHoc,@idSV)";

                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = sql;

                // Tạo một đối tượng Parameter.
                SqlParameter gradeParam = new SqlParameter("@idSV", SqlDbType.Int);
                gradeParam.Value = idSV;
                cmd.Parameters.Add(gradeParam);

                cmd.Parameters.Add("@idNhomMonHoc", SqlDbType.Int).Value = idNhomMonHoc;

                // Thực thi Command (Dùng cho delete, insert, update).
                int rowCount = cmd.ExecuteNonQuery();

                Console.WriteLine("Row Count affected = " + rowCount);
                MessageBox.Show("Thêm sinh viên vào khoa thành công");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
                Console.WriteLine(e.StackTrace);
                MessageBox.Show("Thêm sinh viên vào khoa thất bại");
            }
            finally
            {
                connection.Close();
                connection.Dispose();
                connection = null;
            }

            Console.Read();
        }
    }
}
