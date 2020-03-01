<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WebApplication3.Controllers.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>Läsa textfilen</div> 
        <asp:Button ID="btnReadFile" runat="server" Text="Read text From file" OnClick="btnReadFile_Click" /><br />
        <asp:Label ID="lblReadText" runat="server" ForeColor="Blue" Text=""></asp:Label><br />
        <asp:ListBox ID="ListBox1" size="50" runat="server"></asp:ListBox>
    </form>
</body>
</html>
