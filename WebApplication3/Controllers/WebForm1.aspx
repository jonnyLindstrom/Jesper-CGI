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
        
        <asp:TextBox ID="TextBoxSearch" runat="server">Sök</asp:TextBox>
        <asp:Button ID="btnSearch" runat="server" Text="Sök" OnClick="btnSearch_Click" />
        
        <asp:Button ID="btnSortFirstName" runat="server" Text="Sortera på förnamn" OnClick="btnSortFirstName_Click" />
        <asp:Button ID="btnSortByAge" runat="server" Text="Sortera på ålder" OnClick="btnSortAge_Click" />
        
        <asp:Button ID="btnNext" runat="server" Text="next" OnClick="btnNext_Click" />
        <asp:Button ID="btnPrev" runat="server" Text="prev" OnClick="btnPrev_Click" />
        <asp:TextBox ID="TextBoxFirstName" runat="server">Förnamn</asp:TextBox>
        <asp:TextBox ID="TextBoxLastName" runat="server">Efternamn</asp:TextBox>
        <asp:TextBox ID="TextBoxAge" runat="server">Ålder</asp:TextBox>
        <asp:Button ID="btnSavePersonToFile" runat="server" Text="Spara" OnClick="btnSavePersonToFile_Click" />
        <br />
        <asp:Label ID="lblReadText" runat="server" ForeColor="Blue" Text=""></asp:Label><br/>
        <asp:ListBox ID="ListBox1" height="100%" width="200px" size="50" runat="server" Rows="65"></asp:ListBox>



    </form>
</body>
</html>
