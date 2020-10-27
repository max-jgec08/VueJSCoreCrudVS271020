using System;
using System.Linq;
using System.Data;
using VueJSCORE.Models;
using System.Globalization;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;


namespace VueJSCORE.DataAccessLayer
{
    public class EmployeeLayer:ConnectionString
    {
        /// <summary>
        /// SOURAV MAJI | 28/07/2020 | To get all employees details
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public IEnumerable<EmployeeModel> GetAllEmployees(IConfiguration configuration)
        {
            try
            {
                List<EmployeeModel> lstemployee = new List<EmployeeModel>();
                EmployeeModel employee = null;
                using (SqlConnection con = new SqlConnection(GetConnStr(configuration)))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    string query = "SELECT * FROM employee_master";
                    SqlCommand cmd = new SqlCommand(query, con);                   
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        employee = new EmployeeModel();
                        employee.EmpID = Convert.ToInt32(rdr["EmployeeID"]);
                        employee.FirstName = rdr["FirstName"].ToString();
                        employee.LastName = rdr["LastName"].ToString();
                        employee.Gender = rdr["Gender"].ToString();
                        employee.Designation = rdr["Designation"].ToString();
                        employee.EmailID = rdr["EmailID"].ToString();
                        employee.Address = rdr["City"].ToString();
                        lstemployee.Add(employee);
                    }
                    con.Close();
                }
                return lstemployee;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// SOURAV MAJI | 28/07/2020 | To Add/Edit employee record
        /// </summary>
        /// <param name="employee"></param>
        /// <param name="configuration"></param>        
        public void InsertUpdateEmployee(EmployeeModel employee, IConfiguration configuration)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(GetConnStr(configuration)))
                {
                    if (con.State == ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    string query = null;
                    if (employee.EmpID == 0)
                    {
                        query = "INSERT INTO employee_master(FirstName, LastName, Gender, DOB, Designation, EmailID, Address, InsertDate) VALUES(@FirstName, @LastName, @Gender, @DOB, @Designation, @EmailID, @Address, @InsertDate)";
                    }
                    else
                    {
                        query = "UPDATE employee_master SET FirstName=@FirstName, LastName=@LastName, Gender=@Gender, DOB=@DOB, Designation=@Designation, EmailID=@EmailID, Address=@Address, LastUpdate=@LastUpdate WHERE EmployeeID = " + employee.EmpID;
                    }

                    CultureInfo provider = CultureInfo.InvariantCulture;
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@FirstName", employee.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", employee.LastName);
                    cmd.Parameters.AddWithValue("@Gender", employee.Gender);
                    //DateTime dtDOB = DateTime.ParseExact(employee.DOB, "dd-MM-yyyy", provider);
                    cmd.Parameters.AddWithValue("@DOB", employee.DOB);
                    cmd.Parameters.AddWithValue("@Designation", employee.Designation);
                    cmd.Parameters.AddWithValue("@EmailID", employee.EmailID);
                    cmd.Parameters.AddWithValue("@Address", employee.Address);
                    if (employee.EmpID == 0) { cmd.Parameters.AddWithValue("@InsertDate", DateTime.Now); }
                    else { cmd.Parameters.AddWithValue("@LastUpdate", DateTime.Now); }
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// SOURAV MAJI | 28/07/2020 | Get the details of a particular employee
        /// </summary>
        /// <param name="EmpID"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>        
        public EmployeeModel GetEmployeeData(int EmpID, IConfiguration configuration)
        {
            try
            {                
                using (SqlConnection con = new SqlConnection(GetConnStr(configuration)))
                {
                    if(con.State == ConnectionState.Closed) { con.Open(); }
                    string query = "SELECT * FROM employee_master WHERE EmployeeID = " + EmpID;
                    SqlCommand cmd = new SqlCommand(query, con);
                    EmployeeModel employee = new EmployeeModel();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {                        
                        employee.EmpID = Convert.ToInt32(sdr["EmployeeID"]);
                        employee.FirstName = Convert.ToString(sdr["FirstName"]);
                        employee.LastName = Convert.ToString(sdr["LastName"]);
                        employee.Gender = Convert.ToString(sdr["Gender"]);
                        if (!Object.ReferenceEquals(DBNull.Value, sdr["DOB"]))
                        {
                            DateTime dtDOB = Convert.ToDateTime(sdr["DOB"]);
                            employee.DOB = dtDOB.ToString("MM/dd/yyyy");
                        }
                        employee.Designation = Convert.ToString(sdr["Designation"]);
                        employee.EmailID = Convert.ToString(sdr["EmailID"]);
                        employee.Address = Convert.ToString(sdr["City"]);
                    }

                    return employee;
                }                
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        //To Delete the record on a particular employee
        public void DeleteEmployee(int EmpID, IConfiguration configuration)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(GetConnStr(configuration)))
                {
                    con.Open();
                    string query = "DELETE FROM employee_master WHERE EmployeeID = " + EmpID;
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
