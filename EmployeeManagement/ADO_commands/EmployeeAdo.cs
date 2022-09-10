using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Web.Mvc;
using EmployeeManagement.Models;
using System.Text;
using System.Reflection;

namespace EmployeeManagement.ADO_commands
{
    public class EmployeeAdo
    {
        public string cs = @"Data Source = IND-600\SQLEXPRESS ; Initial Catalog = EmployeeManagement ; Integrated Security= True";
            

        public DataTable EmployeeSearch(string search)
        {
            if(search== null)
            {
                search = "";
            }
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cs))
                {
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
                    SqlCommand cmd = new SqlCommand("spEmployeeSearch", conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@empname", search);
                    sqlDataAdapter.SelectCommand = cmd;
                    sqlDataAdapter.Fill(dt);


                }
                return (dt);
            }
            catch (Exception)
            {
                throw new Exception();
            }


        }


        public void Addemployee(EmployeeDetails addemployee)
        {
            try {
                using (SqlConnection conn = new SqlConnection(cs))
                {
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("spAddEmployee", conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Name", addemployee.EmpName);
                    cmd.Parameters.AddWithValue("@Gender", addemployee.gender);
                    cmd.Parameters.AddWithValue("@Email", addemployee.Email);
                    cmd.Parameters.AddWithValue("@Phone", addemployee.PhoneNumber);
                    cmd.Parameters.AddWithValue("@Jobrole", addemployee.Jobrole);
                    cmd.Parameters.AddWithValue("@Teamid", addemployee.TeamId);
                    cmd.Parameters.AddWithValue("@Doj", addemployee.DateofJoining);
                    sqlDataAdapter.InsertCommand = cmd;
                    sqlDataAdapter.InsertCommand.ExecuteNonQuery();
                }
                return;
            }
            catch (Exception)
            {
                throw new Exception();
            }
            
        }

        public DataTable EmployeeView(int id) 
        {
            try {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cs))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("spEmployeeView", conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);
                    adapter.SelectCommand = cmd;
                    adapter.Fill(dt);
                }
                return (dt);
            }
            catch (Exception)
            {
                throw new Exception();
            }



        }

        public EmployeeDetails Editemployee(DataTable datatable)
        {
            try {
                EmployeeDetails empdetails = new EmployeeDetails();
                empdetails.EmpId = Convert.ToInt32(datatable.Rows[0][0].ToString());
                empdetails.EmpName = (datatable.Rows[0][1].ToString());
                empdetails.gender = (datatable.Rows[0][2].ToString());
                empdetails.Email = (datatable.Rows[0][3].ToString());
                empdetails.PhoneNumber = (datatable.Rows[0][4].ToString());
                empdetails.Jobrole = (datatable.Rows[0][5].ToString());
                empdetails.TeamId = Convert.ToInt32(datatable.Rows[0][8].ToString());


                return (empdetails);
            }
            catch (Exception)
            {
                throw new Exception();
            }
           
        }

        public void UpdateEmployee(EmployeeDetails emp)
        {
            try {
                using (SqlConnection conn = new SqlConnection(cs))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    SqlCommand cmd = new SqlCommand("spEmployeeUpdate", conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", emp.EmpId);
                    cmd.Parameters.AddWithValue("@Name", emp.EmpName);
                    cmd.Parameters.AddWithValue("@Gender", emp.gender);
                    cmd.Parameters.AddWithValue("@Email", emp.Email);
                    cmd.Parameters.AddWithValue("@Phone", emp.PhoneNumber);
                    cmd.Parameters.AddWithValue("@Jobrole", emp.Jobrole);
                    cmd.Parameters.AddWithValue("@TeamId", emp.TeamId);

                    adapter.UpdateCommand = cmd;
                    conn.Open();
                    adapter.UpdateCommand.ExecuteNonQuery();


                }
                return;
            }
            catch (Exception)
            {
                throw new Exception();
            }
            

        }
         public void  DeleteEmployee(int id)
        {
            try {
                using (SqlConnection conn = new SqlConnection(cs))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    SqlCommand cmd = new SqlCommand("spEmployeeDelete", conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);
                    adapter.DeleteCommand = cmd;
                    conn.Open();
                    adapter.DeleteCommand.ExecuteNonQuery();


                }
            }
            catch (Exception)
            {
                throw new Exception();
            }
            
        }

        public Boolean checkemailavailability(string email)
        {
            
            try {
                //Boolean result;
                using (SqlConnection conn = new SqlConnection(cs))
                {

                    
                    SqlCommand cmd = new SqlCommand("dbo.checkemailavailability", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@email", email);
                    SqlParameter returnValue = cmd.Parameters.Add("@RETURN_VALUE", SqlDbType.Int);
                    returnValue.Direction = ParameterDirection.ReturnValue;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    var avail = Convert.ToBoolean(returnValue.Value);
                    return (avail);
                }
                
            }
            catch (Exception )
            {
                throw new Exception();
            }

        }

        public DataTable GetJobRole(int teamid)
        {
            DataTable tbljobs = new DataTable();
            using (SqlConnection conn = new SqlConnection(cs))
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                SqlCommand cmd = new SqlCommand("select  jobname from jobrole where teamid like '%' + @teamid + '%' ", conn);
                cmd.Parameters.AddWithValue("@teamid", teamid.ToString());
                adapter.SelectCommand = cmd;
                adapter.Fill(tbljobs);

            }
            return (tbljobs);

        }

        public string DataTableToJSONWithStringBuilder(DataTable table)
        {
            var JSONString = new StringBuilder();
            //JSONString.Append("{");
            //JSONString.Append("\""+"Jobrole" + "\":");
            if (table.Rows.Count > 0)
            {
                JSONString.Append("[");
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    JSONString.Append("{");
                    for (int j = 0; j < table.Columns.Count; j++)
                    {
                        if (j < table.Columns.Count - 1)
                        {
                            JSONString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + table.Rows[i][j].ToString() + "\",");
                        }
                        else if (j == table.Columns.Count - 1)
                        {
                            JSONString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + table.Rows[i][j].ToString() + "\"");
                        }
                    }
                    if (i == table.Rows.Count - 1)
                    {
                        JSONString.Append("}");
                    }
                    else
                    {
                        JSONString.Append("},");
                    }
                }
                JSONString.Append("]");
                //JSONString.Append('}');
            }
            return JSONString.ToString();
        }

        public List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }
        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    else
                        continue;
                }
            }
            return obj;
        }
    }
   
}