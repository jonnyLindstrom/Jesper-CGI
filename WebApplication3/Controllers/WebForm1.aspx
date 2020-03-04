<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WebApplication3.Controllers.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>Jespers arbetsprov</div>
        <asp:Label ID="Label1" runat="server"/>

        <asp:Button ID="btnSortFirstName" runat="server" Text="Sortera på förnamn" OnClick="btnSortFirstName_Click" />
        <asp:Button ID="btnSortByAge" runat="server" Text="Sortera på ålder" OnClick="btnSortAge_Click" />
        
        <asp:Button ID="btnNext" runat="server" Text="next" OnClick="btnNext_Click" />
        <asp:Button ID="btnPrev" runat="server" Text="prev" OnClick="btnPrev_Click" />
        <br />
        <asp:Label ID="lblReadText" runat="server" ForeColor="Blue" Text=""></asp:Label><br/>
        <asp:ListBox ID="ListBox1" height="100%" width="200px" size="50" runat="server" Rows="65"></asp:ListBox>

    </form>
</body>
</html>
