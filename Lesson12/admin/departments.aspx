<%@ Page Title="" Language="C#" MasterPageFile="~/master.Master" AutoEventWireup="true" CodeBehind="departments.aspx.cs" Inherits="Lesson12.departments" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h1>Departments</h1>
    <a href="Department.aspx">Add Department</a>
    <asp:GridView ID="grdDepartments" runat="server" CssClass="table table-striped table-hover" AutoGenerateColumns="false" OnRowDeleting="grdDepartments_RowDeleting"
         DataKeyNames="DepartmentID">
        <Columns>
            <asp:BoundField DataField="DepartmentID" HeaderText="Department ID" />
            <asp:BoundField DataField="Name" HeaderText="Name" />
            <asp:BoundField DataField="Budget" HeaderText="Budget" />
            <asp:HyperLinkField HeaderText="Edit"  Text="Edit" NavigateUrl="~/addDepartment.aspx" DataNavigateUrlFields="DepartmentID" 
                DataNavigateUrlFormatString="department.aspx?departmentID={0}&Name={0}" />
            <asp:CommandField HeaderText="Delete" DeleteText="Delete" ShowDeleteButton="true"/>
        </Columns>

    </asp:GridView>
</asp:Content>
