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
    public partial class Student1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                // if save wasnt clicked and we have a student ID in the URL
                if (!IsPostBack && Request.QueryString.Count > 0)
                {
                    GetStudent();
                }
            }
            catch (System.Exception)
            {
                Response.Redirect("/Error.aspx");
            }

        }

        protected void GetStudent()
        {
            try
            {
                //populate form for edit
                //get the ID
                Int32 StudentID = Convert.ToInt32(Request.QueryString["StudentID"]);
                //connec tot he db using Entity
                using (GetWreckedEntities db = new GetWreckedEntities())
                {
                    //populate from a student instance with the student ID from the URL params
                    Student s = (from objs in db.Students where objs.StudentID == StudentID select objs).FirstOrDefault();

                    //mpa the student properties from the form controls

                    txtLastName.Text = s.LastName;
                    txtFirstName.Text = s.FirstMidName;
                    txtEnrollDate.Text = s.EnrollmentDate.ToShortDateString();


                    //enrollments - this code goes in the same method that populates the student form but below the existing code that's already in GetStudent()               
                    var objE = (from en in db.Enrollments
                                join c in db.Courses on en.CourseID equals c.CourseID
                                join d in db.Departments on c.DepartmentID equals d.DepartmentID
                                where en.StudentID == StudentID
                                select new { en.EnrollmentID, en.Grade, c.Title, d.Name });

                    grdCourses.DataSource = objE.ToList();
                    grdCourses.DataBind();
                }
            }
            catch (System.Exception)
            {
                Response.Redirect("/Error.aspx");
            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                using (GetWreckedEntities db = new GetWreckedEntities())
                {
                    Student s = new Student();
                    Int32 StudentID = 0;
                    if (Request.QueryString.Count > 0)
                    {
                        StudentID = Convert.ToInt32(Request.QueryString["StudentID"]);
                        //get the student from the entity ;D

                    }
                    s.LastName = txtLastName.Text;
                    s.FirstMidName = txtFirstName.Text;
                    s.EnrollmentDate = Convert.ToDateTime(txtEnrollDate.Text);

                    if (StudentID == 0)
                    {
                        db.Students.Add(s);
                    }

                    db.SaveChanges();
                }
            }
            catch (System.Exception)
            {
                Response.Redirect("/Error.aspx");
            }

        }
        protected void grdCourses_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (IsPostBack)
                {
                    if (e.Row.RowType == DataControlRowType.Header)
                    {
                        Image SortImage = new Image();

                        for (int i = 0; i <= grdCourses.Columns.Count - 1; i++)
                        {
                            if (grdCourses.Columns[i].SortExpression == Session["SortColumn"].ToString())
                            {
                                if (Session["SortDirection"].ToString() == "DESC")
                                {
                                    SortImage.ImageUrl = "images/desc.jpg";
                                    SortImage.AlternateText = "Sort Descending";
                                }
                                else
                                {
                                    SortImage.ImageUrl = "images/asc.jpg";
                                    SortImage.AlternateText = "Sort Ascending";
                                }

                                e.Row.Cells[i].Controls.Add(SortImage);

                            }
                        }
                    }

                }

            }
            catch (System.Exception)
            {
                Response.Redirect("/Error.aspx");
            }


        }

        protected void grdCourses_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                Int32 EnrollmentID = Convert.ToInt32(grdCourses.DataKeys[e.RowIndex].Values["EnrollmentID"]);
                using (GetWreckedEntities db = new GetWreckedEntities())
                {
                    Enrollment objE = (from en in db.Enrollments
                                       where en.EnrollmentID == EnrollmentID
                                       select en).FirstOrDefault();

                    db.Enrollments.Remove(objE);
                    db.SaveChanges();

                    GetStudent();
                }
            }
            catch (System.Exception)
            {
                Response.Redirect("/Error.aspx");
            }

        }
    }
}