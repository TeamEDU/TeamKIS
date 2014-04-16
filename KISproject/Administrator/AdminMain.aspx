<%@ Page Title="Администрирование" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="AdminMain.aspx.cs" Inherits="KISproject.Administrator.AdminMain" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Страница администрации</h2>
    <p>
        Добро пожаловать на страницу администрации. С помощью этой страницы
        вы сможите создавать:
    </p>
    <ul>
        <li>Создавать новых пользователей;</li>
        <li>Управлять пользователями;</li>
        <li>Создавать роли (группа определенных пользователей);</li>
        <li>Управлять ролями;</li>
    </ul>
</asp:Content>
