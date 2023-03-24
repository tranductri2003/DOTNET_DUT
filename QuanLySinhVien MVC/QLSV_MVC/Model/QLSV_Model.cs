using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSV_MVC.Model
{
    internal class QLSV_Model
    {
        public List<CBBItems> GetCBB()
        {
            List<CBBItems> list=new List<CBBItems>();
            list.Add(new CBBItems { Value = 0, Text = "All" });
            
            foreach(LSH i in GetAllLSH()) 
            {
                list.Add(new CBBItems { Value = i.ID, Text = i.LOPSH });
            }
            return list;
        }
        public List<CBBItems> GetCBB1()
        {
            List<CBBItems> list = new List<CBBItems>();
            

            foreach (LSH i in GetAllLSH())
            {
                list.Add(new CBBItems { Value = i.ID, Text = i.LOPSH });
            }
            return list;
        }
        public List<LSH> GetAllLSH()
        {
            List<LSH> data = new List<LSH>();
            string query = "select * from LSH";
            foreach (DataRow i in DBHelper.Instance.GetRecords(query).Rows)
            {
                data.Add(GetLSHByDataRow(i));
            }
            return data;
        }
        public LSH GetLSHByDataRow(DataRow i)
        {
            return new LSH
            {
                ID = Convert.ToInt32(i["ID"].ToString()),
                LOPSH = i["LopSH"].ToString(),

            };
        }
        public List<SV> GetSVBySearch(int ID_Lop,string txt)
        {
            List<SV> data = new List<SV>();
            string query = "select * from SV ";
            if (ID_Lop == 0)
            {
                if(txt != "") 
                {
                    query += "where MSSV like '%" + txt + "%' ";
                }
            }
            else
            {
                if (txt != "")
                {
                    query += "where MALOP = "+ID_Lop+" and MSSV like '%" + txt + "%' ";
                }
                else
                {
                    query += "where MALOP = " + ID_Lop;
                }
            }
            foreach(DataRow i in DBHelper.Instance.GetRecords(query).Rows)
            {
                data.Add (GetSVByDataRow(i));
            }
            return data;
        }
        public SV GetSVByDataRow(DataRow i)
        {
            return new SV
            {
                MSSV = i["MSSV"].ToString(),
                MALOP = Convert.ToInt32(i["MALOP"].ToString()),
                NGAYSINH = Convert.ToDateTime(i["NGAYSINH"].ToString()),
                GIOITINH = Convert.ToBoolean(i["GIOITINH"].ToString()),
                DIEMTRUNGBINH = Convert.ToDouble(i["DIEMTRUNGBINH"].ToString()),
                ANH = Convert.ToBoolean(i["ANH"].ToString()),
                HOCBA = Convert.ToBoolean(i["HOCBA"].ToString()),
                CCCD = Convert.ToBoolean(i["CCCD"].ToString()),
            };
        }
        
        public SV GetSVByMSSV(string mssv)
        {

            string query = "Select * from SV where MSSV='" + mssv + "'";

            SV data = new SV();
            data = (GetSVByDataRow(DBHelper.Instance.GetRecords(query).Rows[0]));
            return data;
        }
        public string GetClassName(int id)
        {
            string data;
            string query = "Select * from LSH where ID=" + id;
            data = GetLSHByDataRow(DBHelper.Instance.GetRecords(query).Rows[0]).LOPSH;
            return data;
        }
        public int GetClassID(string txt)
        {
            int data;
            string query = "Select * from LSH where LopSH='" + txt + "'";
            data = Convert.ToInt32(GetLSHByDataRow(DBHelper.Instance.GetRecords(query).Rows[0]).ID);
            return data;
        }
        public void Add(SV s)
        {
            SqlParameter MSSV = new SqlParameter("@MSSV", s.MSSV);
            SqlParameter MALOP = new SqlParameter("@MALOP", s.MALOP);
            SqlParameter NGAYSINH = new SqlParameter("@NGAYSINH", s.NGAYSINH);
            SqlParameter GIOITINH = new SqlParameter("@GIOITINH", s.GIOITINH);
            SqlParameter DIEMTRUNGBINH = new SqlParameter("@DIEMTRUNGBINH", s.DIEMTRUNGBINH);
            SqlParameter ANH = new SqlParameter("@ANH", s.ANH);
            SqlParameter HOCBA = new SqlParameter("@HOCBA", s.HOCBA);
            SqlParameter CCCD = new SqlParameter("@CCCD", s.CCCD);
            List<SqlParameter> Parameters = new List<SqlParameter>();
            Parameters.Add(MSSV);
            Parameters.Add(MALOP);
            Parameters.Add(NGAYSINH);
            Parameters.Add(GIOITINH);
            Parameters.Add(DIEMTRUNGBINH);
            Parameters.Add(ANH);
            Parameters.Add(HOCBA);
            Parameters.Add(CCCD);
            string query = string.Format("Insert into SV Values ({0},{1},{2},{3},{4},{5},{6},{7})"
                , "@MSSV", "@MALOP", "@NGAYSINH", "@GIOITINH", "@DIEMTRUNGBINH", "@ANH", "@HOCBA", "@CCCD");
            DBHelper.Instance.ExecuteDBs(query, Parameters);
        }
        public void Update(SV s)
        {
            SqlParameter MSSV = new SqlParameter("@MSSV", s.MSSV);
            SqlParameter MALOP = new SqlParameter("@MALOP", s.MALOP);
            SqlParameter NGAYSINH = new SqlParameter("@NGAYSINH", s.NGAYSINH);
            SqlParameter GIOITINH = new SqlParameter("@GIOITINH", s.GIOITINH);
            SqlParameter DIEMTRUNGBINH = new SqlParameter("@DIEMTRUNGBINH", s.DIEMTRUNGBINH);
            SqlParameter ANH = new SqlParameter("@ANH", s.ANH);
            SqlParameter HOCBA = new SqlParameter("@HOCBA", s.HOCBA);
            SqlParameter CCCD = new SqlParameter("@CCCD", s.CCCD);
            List<SqlParameter> Parameters = new List<SqlParameter>();
            Parameters.Add(MSSV);
            Parameters.Add(MALOP);
            Parameters.Add(NGAYSINH);
            Parameters.Add(GIOITINH);
            Parameters.Add(DIEMTRUNGBINH);
            Parameters.Add(ANH);
            Parameters.Add(HOCBA);
            Parameters.Add(CCCD);
            string query = string.Format("Update SV set  MALOP={1},NGAYSINH={2}, GIOITINH={3}, DIEMTRUNGBINH={4}, ANH={5}, HOCBA={6}, CCCD={7} " +
                "WHERE MSSV={0}", "@MSSV", "@MALOP", "@NGAYSINH", "@GIOITINH", "@DIEMTRUNGBINH", "@ANH", "@HOCBA", "@CCCD");

            DBHelper.Instance.ExecuteDBs(query, Parameters);
        }
        public void Delete(string s)
        {
            string query = "Delete from SV where MSSV ='" + s + "'";
            DBHelper.Instance.ExecuteDBs(query);
        }
        public List<SV> Sort(List<string> li, string txt)
        {
            List<SV> result = new List<SV>();
            foreach (string i in li)
            {
                result.Add(GetSVByMSSV(i));
            }
            if ("MSSV".CompareTo(txt) == 0)
            {
                result= result.OrderBy(p => p.MSSV)
                    .Select(p => p).ToList();
            }
            else if ("MALOP".CompareTo(txt) == 0)
            {
                result = result.OrderBy(p => p.MALOP)
                    .Select(p => p).ToList();
            }
            else if ("NGAYSINH".CompareTo(txt) == 0)
            {
                result = result.OrderBy(p => p.NGAYSINH).Select(p => p).ToList();
            }
            else if ("DIEMTRUNGBINH".CompareTo(txt) == 0)
            {
                result = result.OrderByDescending(p => p.DIEMTRUNGBINH)
                    .Select(p => p).ToList();
            }
            return result;
        }
    }
}
