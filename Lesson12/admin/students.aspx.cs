using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Lesson12.Models;
using System.Web.ModelBinding;

namespace Lesson12
{
    public partial class students : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getStudents();
            }
        }
        protected void getStudents()
        {
            try
            {
                using (GetWreckedEntities db = new GetWreckedEntities())
                {
                    //query the students table using entity
                    var Students = from s in db.Students
                                   select s;

                    grdStudents.DataSource = Students.ToList();
                    grdStudents.DataBind();
                }
            }
            catch (System.Exception)
            {
                Response.Redirect("/Error.aspx");
            }

        }

        protected void grdStudents_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                //Store which row was clicked
                Int32 selectedRow = e.RowIndex;

                //get the selected sudent ID using the grids data key collection
                Int32 StudentID = Convert.ToInt32(grdStudents.DataKeys[selectedRow].Values["StudentID"]);

                //use entity to find the object and remove it
                using (GetWreckedEntities db = new GetWreckedEntities())
                {
                    Student s = (from objs in db.Students where objs.StudentID == StudentID select objs).FirstOrDefault();

                    //now we need to do the delete
                    db.Students.Remove(s);
                    db.SaveChanges();
                }
                //refresh the grid
                getStudents();
            }
            catch (System.Exception)
            {
                Response.Redirect("/Error.aspx");
            }

        }
    }
}