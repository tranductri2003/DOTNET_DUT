using QLSV_MVC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSV_MVC.Controller
{
    internal class QLSV_Controller
    {
        public List<CBBItems> SetCBB()
        {
            List<CBBItems> list = new List<CBBItems>();
            QLSV_Model model = new QLSV_Model();
            list = model.GetCBB();
            return list;
        }
        public List<CBBItems> SetCBB1()
        {
            List<CBBItems> list = new List<CBBItems>();
            QLSV_Model model = new QLSV_Model();
            list = model.GetCBB1();
            return list;
        }
        public List<SV> filterchange(int ID_LopSH,string txt)
        {
            List<SV> result = new List<SV>();
            QLSV_Model model=new QLSV_Model();
            result = model.GetSVBySearch(ID_LopSH, txt);
            return result;
        }
        public SV GetSVByMSSV(string MSSV)
        {
            SV result = new SV();
            QLSV_Model model= new QLSV_Model();
            result = model.GetSVByMSSV(MSSV);
            return result;
        }
        public string GetClassName(int ID_Lop)
        {
            QLSV_Model model = new QLSV_Model();
            return model.GetClassName(ID_Lop);
        }
        public int GetClassID(string txt)
        {
            QLSV_Model model = new QLSV_Model();
            return model.GetClassID(txt);
        }
        public void Add(SV s)
        {
            QLSV_Model model = new QLSV_Model();
            model.Add(s);
        }
        public void Update(SV s)
        {
            QLSV_Model model = new QLSV_Model();
            model.Update(s);
        }
        public void Delete(List<string> li)
        {
            QLSV_Model model = new QLSV_Model();
            foreach (string i in li)
            {
                model.Delete(i);
            }
        }
        public List<SV> Sort(List<string> li,string txt)
        {
            QLSV_Model model = new QLSV_Model();
            return model.Sort(li,txt);
        }
    }
}
