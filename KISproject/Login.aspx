<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="KISproject.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="formLogin" runat="server">
    <div>
        <asp:Login ID="Login1" runat="server" OnLoggedIn="Login1_LoggedIn">
            <ValidatorTextStyle BorderColor="Red" />
        </asp:Login>
    </div>
    </form>
</body>
</html>
