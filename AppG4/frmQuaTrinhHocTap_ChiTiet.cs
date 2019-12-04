using AppG4.Model;
using AppG4.Service;
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
    public partial class frmQuaTrinhHocTap_ChiTiet : Form
    {
        HistoryLearning history;
        public frmQuaTrinhHocTap_ChiTiet(HistoryLearning history = null)
        {
            InitializeComponent();
            this.history = history;
            if (history != null)
            {
                //Chỉnh sửa
                this.Text = "Chỉnh sửa quá trình học tập";
                numTuNam.Value = history.YearFrom;
                numDenNam.Value = history.YearEnd;
                txtHocTai.Text = history.SchoolName;
            }
            else
            {
                //Thêm mới
                this.Text = "Thêm mới quá trình học tập";
            }
        }

        private void frmQuaTrinhHocTap_ChiTiet_Load(object sender, EventArgs e)
        {

        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            if (history != null)
            {
                //Chỉnh sửa
                history.YearFrom = (int) numTuNam.Value;
                history.YearEnd = (int)numDenNam.Value;
                history.SchoolName = txtHocTai.Text;
                HistoryLearningService.Update(Utils.HistoryPathFile, history);
            }
            else
            {
                //Thêm mới
                history = new HistoryLearning();
                history.ID = Guid.NewGuid().ToString();
                history.YearFrom = (int)numTuNam.Value;
                history.YearEnd = (int)numDenNam.Value;
                history.SchoolName = txtHocTai.Text;
                HistoryLearningService.Add(Utils.HistoryPathFile, history);
            }
        }
    }
}
