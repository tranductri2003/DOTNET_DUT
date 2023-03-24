using QLSV_MVC.Controller;
using QLSV_MVC.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLSV_MVC
{
    public partial class DetailForm : Form
    {
        public delegate void MyDel(int ID_lop, string txt);
        public MyDel d { get; set; }
        public string MSSV { get; set; }
        public DetailForm(string mssv)
        {
            MSSV = mssv;
            InitializeComponent();
            SetCBB();
            GUI();

        }
        public void SetCBB()
        {
            QLSV_Controller controller = new QLSV_Controller();
            txtLopsh.Items.AddRange(controller.SetCBB1().ToArray());
        }
        public void GUI()
        {
            if (MSSV != "")
            {
                QLSV_Controller controller = new QLSV_Controller();
                SV sv = new SV();
                sv = controller.GetSVByMSSV(MSSV);
                txtMSSV.Text = sv.MSSV.ToString();
                txtLopsh.Text = controller.GetClassName(sv.MALOP).ToString();
                txtDate.Text = sv.NGAYSINH.ToString();
                txtDtb.Text = sv.DIEMTRUNGBINH.ToString();
                if (sv.GIOITINH)
                {
                    nam.Checked = true;
                }
                else
                {
                    nu.Checked = true;
                }
                anh.Checked = sv.ANH;
                hocba.Checked = sv.HOCBA;
                cccd.Checked = sv.CCCD;
                txtMSSV.Enabled = false;
            }
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ok_Click(object sender, EventArgs e)
        {
            try
            {
                SV sv = new SV();
                QLSV_Controller controller = new QLSV_Controller();
                sv.MSSV = txtMSSV.Text.ToString();
                sv.MALOP = controller.GetClassID(txtLopsh.Text.ToString());
                sv.NGAYSINH = Convert.ToDateTime(txtDate.Text.ToString());
                if (nam.Checked)
                {
                    sv.GIOITINH = true;
                }
                else { sv.GIOITINH = false; }
                sv.DIEMTRUNGBINH = Convert.ToDouble(txtDtb.Text.ToString());
                sv.ANH = Convert.ToBoolean(anh.Checked.ToString());
                sv.HOCBA = Convert.ToBoolean(hocba.Checked.ToString());
                sv.CCCD = Convert.ToBoolean(cccd.Checked.ToString());
                if (MSSV != "")
                {
                    controller.Update(sv);
                }
                else {  controller.Add(sv); }
                d(0, "");
                this.Close();
            }
            catch
            {
                MessageBox.Show("Không thành công");
            }

        }

    }
}
