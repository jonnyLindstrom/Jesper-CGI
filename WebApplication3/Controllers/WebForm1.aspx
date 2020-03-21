<%@ Page Language="C#" AutoEventWireup="true"  CodeBehind="WebForm1.aspx.cs" CodeFile="WebForm1.aspx.cs" Inherits="WebApplication3.Controllers.WebForm1"  %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html>
<link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/themes/blitzer/jquery-ui.css" rel="Stylesheet" type="text/css" /> <!-- used for autocomplete -->


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label valign="top" ID="lblText" runat ="server">Label</asp:Label>
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
        <asp:TextBox ID="TextBoxSearch" runat="server" AutoCompleteType="Enabled" Rows="3"></asp:TextBox> 
        <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" 
            TargetControlID="TextBoxSearch" 
            ServiceMethod="GetNames" 
            Enabled="True"
            EnableCaching="true" 
            MinimumPrefixLength="1" 
            CompletionSetCount="1" 
            CompletionInterval="10" UseContextKey="True">
        </cc1:AutoCompleteExtender>       
        <asp:Button ID="btnSearch" runat="server" Text="Sök" OnClick="btnSearch_Click" />    
        
        <asp:Label ID="Label1" runat="server"/>
        <asp:Button ID="btnSortFirstName" runat="server" Text="Sortera på förnamn" OnClick="btnSortFirstName_Click" />
        <asp:Button ID="btnSortByAge" runat="server" Text="Sortera på ålder" OnClick="btnSortAge_Click" />
        
        <asp:Button ID="btnNext" runat="server" Text="next" OnClick="btnNext_Click" />
        <asp:Button ID="btnPrev" runat="server" Text="prev" OnClick="btnPrev_Click" /><br/>

        <asp:TextBox ID="TextBoxFirstName" runat="server">Förnamn</asp:TextBox>
        <asp:TextBox ID="TextBoxLastName" runat="server">Efternamn</asp:TextBox>
        <asp:TextBox ID="TextBoxAge" runat="server">Ålder</asp:TextBox>
        <asp:Button ID="btnSavePersonToFile" runat="server" Text="Spara" OnClick="btnSavePersonToFile_Click" />
        <asp:Button ID="btnSaveListToExcel" runat="server" Text="Spara lista till excel" OnClick="btnSaveListToExcel_Click" />
        <br />
        <asp:Label ID="lblReadText" runat="server" ForeColor="Blue" Text=""></asp:Label><br/>
        <asp:ListBox ID="ListBox1" height="300px" width="200px" size="50" runat="server" Rows="65">
        </asp:ListBox>
    </form>
</body>
</html>
