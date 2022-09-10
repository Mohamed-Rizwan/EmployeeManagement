using EmployeeManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace EmployeeManagement.ADO_commands
{
    public class SalaryAdo
    {
        public string cs = @"Data Source = IND-600\SQLEXPRESS ; Initial Catalog = EmployeeManagement ; Integrated Security= True";

        public void AddSalary(Salary salary)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cs))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    SqlCommand cmd = new SqlCommand("spAddSalary", conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@empid", salary.EmployeeId);
                    cmd.Parameters.AddWithValue("@Salaryamt", salary.SalaryAmount);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                return;
            }
            catch (Exception)
            {
                throw new Exception();
            }
           

        }

        public Salary GetSalarydetailsbyemployee(int id)
        {
            try {
                DataTable dt = new DataTable();
                Salary salary = new Salary();
                using (SqlConnection conn = new SqlConnection(cs))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    SqlCommand command = new SqlCommand("spGetSalaryDetailsByEmployee", conn);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@id", id);
                    adapter.SelectCommand = command;
                    adapter.Fill(dt);

                }
                salary.SalaryId = Convert.ToInt32(dt.Rows[0][0].ToString());
                salary.EmployeeId = Convert.ToInt32(dt.Rows[0][1].ToString());
                salary.SalaryAmount = Convert.ToInt64(dt.Rows[0][2].ToString());
                return salary;
            }
            catch (Exception)
            {
                throw new Exception();
            }
            

        }
        public void incrementsalary(int id, Salary salary)
        {
            try {
                Double hike = (id) / 100d;
                long salar = Convert.ToInt64(salary.SalaryAmount * (hike + 1));
                using (SqlConnection con = new SqlConnection(cs))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    SqlCommand cmd = new SqlCommand("spIncrementSalary", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", salary.EmployeeId);
                    cmd.Parameters.AddWithValue("@amt", salar);
                    cmd.Parameters.AddWithValue("@salaryid", salary.SalaryId);
                    con.Open();
                    cmd.ExecuteNonQuery();


                }
            }
            catch (Exception)
            {
                throw new Exception();
            }

        }

        public DataTable getsalarydetailsbyteam(int id)
        {
            try {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cs))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    SqlCommand cmd = new SqlCommand("Select SalaryID, empid, salaryAmt from salary where empid in (select EmpId from EmployeeDetails where teamid = @id) And status = 'Active'", conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    adapter.SelectCommand = cmd;
                    adapter.Fill(dt);
                }
                return dt;
            }
            catch (Exception)
            {
                throw new Exception();
            }
            
        }

        public DataTable getsalarydetailsforall()
        {
            try {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cs))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    SqlCommand cmd = new SqlCommand("Select SalaryID, empid, salaryAmt from salary where status = 'Active'", conn);
                    adapter.SelectCommand = cmd;
                    adapter.Fill(dt);
                }
                return dt;
            }
            catch (Exception)
            {
                throw new Exception();
            }
            

        }

        public DataTable viewsalarydetailsbyid(int id)
        {
            try {
                DataTable dt = new DataTable();
                using (SqlConnection con = new SqlConnection(cs))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    SqlCommand cmd = new SqlCommand("spViewSalaryDetailsById", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);
                    adapter.SelectCommand = cmd;
                    adapter.Fill(dt);

                }
                return dt;
            }
            catch (Exception)
            {
                throw new Exception();
            }
            

        }
        public DataTable viewsalarybyteam()
        {
            try {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cs))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    SqlCommand command = new SqlCommand("spViewSalaryDetailsByTeam", conn);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    adapter.SelectCommand = command;
                    adapter.Fill(dt);
                }
                return dt;
            }
            catch (Exception)
            {
                throw new Exception();
            }
            
        }

       
        public string generatepayslip(payslip pay)
        {
            try {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cs))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    SqlCommand cmd = new SqlCommand("spGeneratePayslip", conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", pay.salaryid);
                    cmd.Parameters.AddWithValue("@salarydate", pay.Date);
                    cmd.Parameters.Add("@msg", SqlDbType.VarChar, 50);
                    cmd.Parameters["@msg"].Direction = ParameterDirection.Output;
                    adapter.InsertCommand = cmd;
                    adapter.SelectCommand = cmd;
                    adapter.Fill(dt);


                }
                var msg = dt.Rows[0][0].ToString();
                return msg;
            }
            catch (Exception)
            {
                throw new Exception();
            }
           
        }

        public DataTable viewpayslip(payslip view)
        {
            try {
                DataTable dt = new DataTable();
                using (SqlConnection con = new SqlConnection(cs))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    SqlCommand cmd = new SqlCommand("spViewPayslip", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", view.salaryid);
                    cmd.Parameters.AddWithValue("@date", view.Date);
                    adapter.SelectCommand = cmd;
                    adapter.Fill(dt);
                }
                return dt;
            }
            catch (Exception)
            {
                throw new Exception();
            }
            
        }
    }
}