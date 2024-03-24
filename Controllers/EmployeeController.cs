using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EmployeeMVCcurd.Data;
using EmployeeMVCcurd.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EmployeeMVCcurd.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IConfiguration _configuration;
        public EmployeeController(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        // GET: Employee
        public IActionResult Index()
        {
            DataTable dtbl = new DataTable();
            using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("ConString")))
            {    
                sqlConnection.Open();
                SqlDataAdapter ad = new SqlDataAdapter("EmployeeViewAll",sqlConnection);
                ad.SelectCommand.CommandType = CommandType.StoredProcedure;
                ad.Fill(dtbl);
            }
                return View(dtbl);
        }

        // GET: Employee/Edit/5
        public IActionResult AddorEdit(int? id)
        {
            EmployeeModelView model = new EmployeeModelView();
            if(id>0)
            {
                model= fetchEmployeeByID(id);
            }
            return View(model);
        }

        // POST: Employee/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddorEdit(int id, [Bind("EmpId,EmpName,EmpAge,empSalary,empDepartment,empGender")] EmployeeModelView employeeModelView)
        {


            if (ModelState.IsValid)
            {
                using (SqlConnection con= new SqlConnection(_configuration.GetConnectionString("ConString")))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("EmpAddorEdit",con);
                    cmd.CommandType= CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("EmpId", employeeModelView.EmpId);
                    cmd.Parameters.AddWithValue("EmpName",employeeModelView.EmpName);
                    cmd.Parameters.AddWithValue("EmpAge",employeeModelView.EmpAge);
                    cmd.Parameters.AddWithValue("EmpSalary", employeeModelView.empSalary);
                    cmd.Parameters.AddWithValue("EmpDepartment",employeeModelView.empDepartment);
                    cmd.Parameters.AddWithValue("EmpGender",employeeModelView.empGender);
                    cmd.ExecuteNonQuery();                  
                }
                return RedirectToAction(nameof(Index));
            }
            return View(employeeModelView);
        }
        // GET: Employee/Delete/5
        public IActionResult Delete(int? id)
        {   
            EmployeeModelView emp = fetchEmployeeByID(id);
            return View(emp);
        }

        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {

            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("ConString")))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("EmployeeDeleteByID", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("EmpId",id);
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction(nameof(Index));
        }

        [NonAction]
        public EmployeeModelView fetchEmployeeByID(int? id)
        {
            EmployeeModelView model= new EmployeeModelView();
            DataTable dtbl = new DataTable();
            using (SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("ConString")))
            {
                sqlConnection.Open();
                SqlDataAdapter ad = new SqlDataAdapter("EmployeeViewById", sqlConnection);
                ad.SelectCommand.CommandType = CommandType.StoredProcedure;
                ad.SelectCommand.Parameters.AddWithValue("EmpId", id);
                ad.Fill(dtbl);
                if(dtbl.Rows.Count==1)
                {
                    model.EmpId = Convert.ToInt32( dtbl.Rows[0]["emp_ID"].ToString());
                    model.EmpName = dtbl.Rows[0]["emp_Name"].ToString();
                    model.EmpAge = Convert.ToInt32(dtbl.Rows[0]["emp_Age"].ToString());
                    model.empSalary = Convert.ToInt32(dtbl.Rows[0]["emp_Salary"].ToString());
                    model.empDepartment = dtbl.Rows[0]["emp_Department"].ToString();
                    model.empGender = dtbl.Rows[0]["emp_Gender"].ToString();
                   
                }
                return model;   
            }
        }
        
    }
}
