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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            setCBB();
        }
        public void setCBB()
        {
            QLSV_Controller controller = new QLSV_Controller();
            cbbLop.Items.AddRange(controller.SetCBB().ToArray());
        }
        public void ShowDGV(int ID_Lop, string txt)
        {
            QLSV_Controller controller = new QLSV_Controller();
            data.DataSource = controller.filterchange(ID_Lop, txt);
        }

        private void cbbLop_SelectedIndexChanged(object sender, EventArgs e)
        {
            int ID_Lop = ((CBBItems)cbbLop.SelectedItem).Value;
            ShowDGV(ID_Lop, "");
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            int ID_Lop = ((CBBItems)cbbLop.SelectedItem).Value;
            string txt = txtSearch.Text;
            ShowDGV(ID_Lop, txt);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            DetailForm f = new DetailForm("");
            f.d += new DetailForm.MyDel(ShowDGV);
            f.Show();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (data.SelectedRows.Count == 1)
            {
                string MSSV = data.SelectedRows[0].Cells["MSSV"].Value.ToString();
                DetailForm f = new DetailForm(MSSV);
                f.d += new DetailForm.MyDel(ShowDGV);
                f.Show();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (data.SelectedRows.Count >0)
            {
                List<string> li=new List<string>();
                foreach(DataGridViewRow i in data.SelectedRows)
                {
                    li.Add(i.Cells["MSSV"].Value.ToString());
                }
                QLSV_Controller controller = new QLSV_Controller();
                controller.Delete(li);
                ShowDGV(0, "");
            }
        }

        private void Sort_Click(object sender, EventArgs e)
        {
            string sort=cbbSort.SelectedItem.ToString();
            List<string> list=new List<string>();
            
            QLSV_Controller controller=new QLSV_Controller();
            foreach(DataGridViewRow i in data.Rows)
            {
                list.Add(i.Cells["MSSV"].Value.ToString());
            }
            List<SV> data1=new List<SV>();
            data1 = controller.Sort(list, sort);
            data.DataSource = data1;
            
        }
    }
}
