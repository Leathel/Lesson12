<%@ Page Title="" Language="C#" MasterPageFile="~/master.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="Lesson12.Register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Registration Details</h1>
    <h5>All fields are required</h5>
    <div class="form-group-lg">
        <asp:Label ID="lblStatus" runat="server" CssClass="label label-danger" />
    </div>
    <fieldset>
        <label for="txtUserName" class="col-sm-2">UserName: </label>
        <asp:TextBox ID="txtUserName" runat="server" required="" MaxLength="50" />
    </fieldset>
    <fieldset>
        <label for="txtPassword" class="col-sm-2">Password: </label>
        <asp:TextBox ID="txtPassword" runat="server" required="" MaxLength="50" TextMode="Password"/>
    </fieldset>
    <fieldset>
        <label for="txtConfirmPass" class="col-sm-2">Confirm Password: </label>
        <asp:TextBox ID="txtConfirmPass" runat="server" required="" MaxLength="50" TextMode="Password" />
        <asp:CompareValidator
            ID="PasswordCompare"
            ControlToValidate="txtPassword"
            ValueToCompare="txtPassword"
            ControlToCompare="txtConfirmPass"
            Type="String"
            Operator="Equal"
            ErrorMessage="Your passwords must match"
            Text="Confirm your password"
            ForeColor="black"
            BackColor="white"
            runat="server"> 
        </asp:CompareValidator>

        <div>
            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnSave_Click" />
        </div>
    </fieldset>

</asp:Content>
