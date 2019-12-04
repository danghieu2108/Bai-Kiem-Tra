using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppG4
{
    public partial class frmTinhToanCoBan : Form
    {
        public frmTinhToanCoBan()
        {
            InitializeComponent();
        }

        private void BtnCong_Click(object sender, EventArgs e)
        {
            try
            {
                errorProvider.Clear();
                float temp = 0;
                if (!float.TryParse(txtA.Text, out temp))
                {
                    //MessageBox.Show("Vui lòng nhập lại giá trị a.");
                    errorProvider.SetError(txtA, "Vui lòng nhập lại giá trị a");
                    return;
                }
                var a = float.Parse(txtA.Text);

                if (!float.TryParse(txtB.Text, out temp))
                {
                    //MessageBox.Show("Vui lòng nhập lại giá trị b.");
                    errorProvider.SetError(txtB, "Vui lòng nhập lại giá trị b");
                    return;
                }
                var b = float.Parse(txtB.Text);

                var c = a + b;
                var ketQua = string.Format("Kết quả của {0} + {1} là {2}", a, b, c);
                MessageBox.Show(ketQua);
            }
            catch(Exception ex)
            {
                var erString = string.Format("Lỗi rồi. Chi tiết: {0}. {1}", ex.Message, ex.StackTrace);
                MessageBox.Show(erString);
            }
        }
    }
}
