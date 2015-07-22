using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Lesson12.Models;
using System.Web.ModelBinding;
using System.Linq.Dynamic;

namespace Lesson12
{
    public partial class departments : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getDepartments();
            }
        }
        protected void getDepartments()
        {
            try
            {
                using (GetWreckedEntities db = new GetWreckedEntities())
                {
                    //query the students table using entity
                    var Departments = from d in db.Departments
                                      select d;

                    grdDepartments.DataSource = Departments.ToList();
                    grdDepartments.DataBind();
                }
            }
            catch (System.Exception)
            {
                Response.Redirect("/Error.aspx");
            }

        }

        protected void grdDepartments_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                //Store which row was clicked
                Int32 selectedRow = e.RowIndex;

                //get the selected sudent ID using the grids data key collection
                Int32 DepartmentID = Convert.ToInt32(grdDepartments.DataKeys[selectedRow].Values["DepartmentID"]);

                //use entity to find the object and remove it
                using (GetWreckedEntities db = new GetWreckedEntities())
                {
                    Department s = (from objs in db.Departments where objs.DepartmentID == DepartmentID select objs).FirstOrDefault();

                    //now we need to do the delete
                    db.Departments.Remove(s);
                    db.SaveChanges();
                }
                //refresh the grid
                getDepartments();
            }
            catch (System.Exception)
            {
                Response.Redirect("/Error.aspx");
            }

        }
    }
}