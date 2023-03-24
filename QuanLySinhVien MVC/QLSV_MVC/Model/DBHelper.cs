using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSV_MVC.Model
{
    internal class DBHelper
    {
        private static DBHelper _Instance;
        private SqlConnection _cnn;
        private SqlCommand cmd;
        private DBHelper(string s)
        {
            _cnn = new SqlConnection(s);
        }
        public static DBHelper Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new DBHelper("Data Source=DUCTRITRAN\\SQLEXPRESS01;Initial Catalog=DBSinhVien;Integrated Security=True");
                return _Instance;
            }
            private set { }
        }
        public DataTable GetRecords(string query)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(query, _cnn);
            _cnn.Open();
            da.Fill(dt);
            _cnn.Close();
            return dt;
        }

        public void ExecuteDBs(string query)
        {
            cmd = new SqlCommand(query, _cnn);
            _cnn.Open();
            cmd.ExecuteNonQuery();
            _cnn.Close();
        }

        public void ExecuteDBs(string query, List<SqlParameter> li)
        {
            cmd = new SqlCommand(query, _cnn);
            foreach (SqlParameter p in li)
            {
                cmd.Parameters.Add(p);
            }
            _cnn.Open();
            cmd.ExecuteNonQuery();
            _cnn.Close();
        }
    }
}
