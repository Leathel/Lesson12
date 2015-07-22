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
    public partial class courses : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    Session["SortColumn"] = "CourseID";
                    Session["SortDirection"] = "Asc";
                    getCourses();
                }
            }
            catch (System.Exception)
            {
                Response.Redirect("/Error.aspx");
            }

        }
        protected void getCourses()
        {
            try
            {
                using (GetWreckedEntities db = new GetWreckedEntities())
                {
                    //query the students table using entity
                    var Courses = from c in db.Courses
                                  select new { c.CourseID, c.Title, c.Credits, c.Department.Name };

                    String SortString = Session["SortColumn"].ToString() + " " + Session["SortDirection"].ToString();
                    grdCourses.DataSource = Courses.AsQueryable().OrderBy(SortString).ToList();
                    grdCourses.DataBind();
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
                //Store which row was clicked
                Int32 selectedRow = e.RowIndex;

                //get the selected sudent ID using the grids data key collection
                Int32 CourseID = Convert.ToInt32(grdCourses.DataKeys[selectedRow].Values["CourseID"]);

                //use entity to find the object and remove it
                using (GetWreckedEntities db = new GetWreckedEntities())
                {
                    Course s = (from objs in db.Courses where objs.CourseID == CourseID select objs).FirstOrDefault();

                    //now we need to do the delete
                    db.Courses.Remove(s);
                    db.SaveChanges();
                }
                //refresh the grid
                getCourses();
            }
            catch (System.Exception)
            {
                Response.Redirect("/Error.aspx");
            }

        }

        protected void grdCourses_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdCourses.PageIndex = e.NewPageIndex;
            getCourses();
        }

        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            grdCourses.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            getCourses();
        }

        protected void grdCourses_Sorting(object sender, GridViewSortEventArgs e)
        {
            //get the column to sort by
            Session["SortColumn"] = e.SortExpression;

            //reload
            getCourses();

            //toggle the direction
            if (Session["SortDirection"].ToString() == "Asc")
            {
                Session["SortDirection"] = "ASC";
            }
            else
            {
                Session["SortDirection"] = "DESC";
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
    }
}