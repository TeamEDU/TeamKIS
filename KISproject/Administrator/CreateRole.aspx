<%@ Page Title="Создание роли" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="CreateRole.aspx.cs" Inherits="KISproject.Administrator.AddRole" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <asp:Label ID="LabelNewRole" runat="server" Text="Имя новой роли:"></asp:Label>
    &nbsp;
    <asp:TextBox ID="txtBoxNewRoleName" runat="server" Width="174px"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RolenameFieldValidator" runat="server"
        ErrorMessage="Поле не может быть пустым!"
        ControlToValidate="txtBoxNewRoleName"
        ForeColor="Red"/>
    <br/>
    <asp:Button ID="btnAddRole" runat="server" Text="Добавить роль" OnClick="btnAddRole_Click" />
    <br/>
    <asp:Label ID="AddRoleAction" runat="server" Text="" ForeColor="Red"></asp:Label>
</asp:Content>
