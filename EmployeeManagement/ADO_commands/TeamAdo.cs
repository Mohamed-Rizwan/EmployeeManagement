using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using EmployeeManagement.Models;

namespace EmployeeManagement.ADO_commands
{
    public class TeamAdo
    {
        public string cs = @"Data Source = IND-600\SQLEXPRESS ; Initial Catalog = EmployeeManagement ; Integrated Security= True";
        public DataTable GetTeam()
        {
            try {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cs))
                {
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("Execute spGetTeams", conn);
                    sqlDataAdapter.Fill(dt);


                }
                return (dt);
            }
            catch (Exception)
            {
                throw new Exception();
            }
            
        }

        public DataTable ViewTeam()
        {
            try {
                DataTable teams = new DataTable();
                using (SqlConnection conn = new SqlConnection(cs))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter("Execute spViewTeams", conn);
                    adapter.Fill(teams);
                }
                return (teams);
            }
            catch (Exception)
            {
                throw new Exception();
            }
             
        }
      
        public void AddTeam(TeamDetails team)
        {
            try {
                using (SqlConnection conn = new SqlConnection(cs))
                {
                    conn.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    SqlCommand Command = new SqlCommand("spAddTeam", conn);
                    Command.CommandType = System.Data.CommandType.StoredProcedure;
                    Command.Parameters.AddWithValue("@teamname", team.TeamName);
                    Command.Parameters.AddWithValue("@managerid", team.managerid);
                    adapter.InsertCommand = Command;
                    adapter.InsertCommand.ExecuteNonQuery();

                }
                return;
            }
            catch (Exception)
            {
                throw new Exception();
            }

           
        }

        public DataSet ViewTeamMembers(int id)
        {
            try {
                DataSet teammembers = new DataSet();
                DataTable teammember = new DataTable();
                DataTable manager = new DataTable();
                using (SqlConnection conn = new SqlConnection(cs))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    SqlCommand cmd = new SqlCommand("spGetTeamMembers", conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);
                    adapter.SelectCommand = cmd;
                    adapter.Fill(teammember);
                    SqlDataAdapter adapter2 = new SqlDataAdapter();
                    SqlCommand cmd2 = new SqlCommand("spTeamDetails", conn);
                    cmd2.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd2.Parameters.AddWithValue("@id", id);
                    adapter2.SelectCommand = cmd2;
                    adapter2.Fill(manager);
                }
                teammembers.Tables.Add(teammember);
                teammembers.Tables.Add(manager);
                return (teammembers);
            }
            catch (Exception)
            {
                throw new Exception();
            }
           
        }
    }
}