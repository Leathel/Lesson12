<%@ Page Title="" Language="C#" MasterPageFile="~/master.Master" AutoEventWireup="true" CodeBehind="MainMenu.aspx.cs" Inherits="Lesson12.MainMenu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Main Menu</h1>


    <div class="well">
        <h3>Departments</h3>
        <ul class="list-group">
            <li class="list-group-item"><a href="departments.aspx">List Departments</a></li>
            <li class="list-group-item"><a href="department.aspx">Add Department</a></li>
        </ul>
    </div>

    <div class="well">
        <h3>Courses</h3>
        <ul class="list-group">
            <li class="list-group-item"><a href="courses.aspx">List Courses</a></li>
            <li class="list-group-item"><a href="course.aspx">Add Course</a></li>
        </ul>
    </div>

    <div class="well">
        <h3>Students</h3>
        <ul class="list-group">
            <li class="list-group-item"><a href="Students.aspx">List Students</a></li>
            <li class="list-group-item"><a href="Student.aspx">Add Student</a></li>
        </ul>
    </div>
</asp:Content>
