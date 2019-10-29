using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolHouse
{
    public class StudentService
    {
        string _connectionstring;
        List<Student2> listOfUsers = new List<Student2>();
       public  StudentService()
        {
            _connectionstring = @"Data Source=WINDOWS-QA8E80O\SQLEXPRESS;Initial Catalog=SchoolHouse;Integrated Security=True;Pooling=False";
        }
        

        public List<Student2> getStudentlist()
        {

            List<Student2> slist = new List<Student2>();
            SqlConnection con = new SqlConnection(_connectionstring);
            SqlDataAdapter adpt = new SqlDataAdapter("select * from students", con);
            DataSet ds = new DataSet();
            adpt.Fill(ds);
            string hid = string.Empty;
            string hdesc = string.Empty;

            foreach(DataRow dr in ds.Tables[0].Rows)
            {
                Student2 student2 = new Student2();
                student2.studentId = Convert.ToInt32(dr["studentId"].ToString());
                student2.grade = dr["grade"].ToString();
                student2.lastName = dr["lastName"].ToString();
                student2.firstName = dr["firstName"].ToString();
                hid = dr["HourId"].ToString();
                hdesc = dr["HourDescription"].ToString();
                student2.HourId = hid.Split(',');
                student2.HourDescription = hdesc.Split(',');

                slist.Add(student2);

            }


            //var path = @"C:\List\Students1.json";
            //string theJson = System.IO.File.ReadAllText(path);
            //List<Student2> newList = JsonConvert.DeserializeObject<List<Student2>>(theJson);
            //    listOfUsers.AddRange(newList);
            return slist;
        }

        public void AddStudents(int sID, string fname, string lname, string sgrade, string[] hid, string[] hdesc)
        {

            SqlConnection con = new SqlConnection(_connectionstring);
            string hourid = string.Empty;
            foreach(string str in hid)
            {
                hourid += str + ",";
            }

            string hourdesc = string.Empty;
            foreach (string str in hdesc)
            {
                hourdesc += str + ",";
            }


            string qry = "insert into students(studentId,firstName,lastName,grade,HourId,HourDescription)values("+ sID + ",'"+ fname + "','"+lname+"','"+ sgrade + "','"+ hourid + "','"+ hourdesc + "')";
            con.Open();
            SqlCommand cmd = new SqlCommand(qry, con);
            cmd.ExecuteNonQuery();
            con.Close();




            listOfUsers.Add(new Student2() { studentId = sID, firstName = fname, lastName = lname, grade = sgrade, HourId = hid, HourDescription = hdesc });
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(listOfUsers, Formatting.Indented);
        }

        
    }
}
