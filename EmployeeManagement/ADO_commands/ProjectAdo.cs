using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using EmployeeManagement.Models;

namespace EmployeeManagement.ADO_commands
{
    public class ProjectAdo
    {
        public string cs = @"Data Source = IND-600\SQLEXPRESS ; Initial Catalog = EmployeeManagement ; Integrated Security= True";
        public void addproject(ProjectDetails project)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cs))
                {
                    SqlCommand cmd = new SqlCommand("spAddProjects", conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@projectname", project.ProjectName);
                    cmd.Parameters.AddWithValue("@teamid", project.TeamId);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw new Exception();
            }

        }

        public DataTable viewallproject()
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection conn = new SqlConnection(cs))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter("Execute spViewAllProjects", conn);

                    adapter.Fill(dt);

                }
                return (dt);
            }
            catch (Exception)
            {
                throw new Exception();
            }

        }

        public ProjectDetails getprojectdetails(int id)
        {
            try
            {
                DataTable table = new DataTable();
                ProjectDetails project = new ProjectDetails();
                using (SqlConnection conn = new SqlConnection(cs))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    SqlCommand cmd = new SqlCommand("spGetProjectDetails", conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);
                    adapter.SelectCommand = cmd;
                    adapter.Fill(table);


                }
                project.ProjectId = Convert.ToInt32(table.Rows[0][0].ToString());
                project.ProjectName = table.Rows[0][1].ToString();
                project.TeamId = Convert.ToInt32(table.Rows[0][2].ToString());
                project.projectstatus = table.Rows[0][3].ToString();

                return (project);
            }
            catch (Exception)
            {
                throw new Exception();
            }

        }

        public void Updateproject(ProjectDetails project)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cs))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    SqlCommand cmd = new SqlCommand("spUpdateProject", conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", project.TeamId);
                    cmd.Parameters.AddWithValue("@projectname", project.ProjectName);
                    cmd.Parameters.AddWithValue("@projectstatus", project.projectstatus);
                    cmd.Parameters.AddWithValue("@projectid", project.ProjectId);
                    adapter.UpdateCommand = cmd;
                    conn.Open();
                    adapter.UpdateCommand.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw new Exception();
            }

        }

        public DataTable Filterprojectbystatus(int id)
        {
            try
            {
                DataTable table = new DataTable();
                string status;
                if (id == 1)
                {
                    status = "Yet To Start";
                }
                else if (id == 2)
                {
                    status = "On Going";
                }
                else if (id == 3)
                {
                    status = "Completed";
                }
                else
                {
                    return (table);
                }
                using (SqlConnection conn = new SqlConnection(cs))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    SqlCommand cmd = new SqlCommand("spFilterProjectByStatus", conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@status", status);
                    adapter.SelectCommand = cmd;
                    adapter.Fill(table);
                }
                return (table);
            }
            catch (Exception)
            {
                throw new Exception();
            }

        }

        public DataTable viewprojectbyteam(int id)
        {
            try
            {
                DataTable table = new DataTable();
                using (SqlConnection conn = new SqlConnection(cs))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    SqlCommand cmd = new SqlCommand("spViewProjectByTeam", conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);
                    adapter.SelectCommand = cmd;
                    adapter.Fill(table);
                }
                return (table);
            }
            catch (Exception)
            {
                throw new Exception();
            }

        }

        public void DeleteProject(int id)
        {
            using (SqlConnection conn = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("spDeleteProject",conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                cmd.ExecuteNonQuery();

            }
        }

        
    }
}