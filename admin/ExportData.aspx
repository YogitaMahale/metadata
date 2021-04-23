<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Admin_Master.master" AutoEventWireup="true" CodeFile="ExportData.aspx.cs" Inherits="admin_ExportData" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
 <div style="border: 1px solid #DFDFDF; MARGIN-LEFT:5px; WIDTH: 947px; background-color:#f7f7f7;">

 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    
          
       <table align="center">
    <tr>
    <td align="left" valign="top">
    <asp:Label ID="lblmessage" runat="server" ForeColor="Red"></asp:Label>
    </td>
    </tr>
    <tr>
   
    <%--<td align="left" valign="top" >
        <asp:DropDownList ID="ddlsearchby" runat="server" 
            AutoPostBack="True" > 
            <asp:ListItem>Choose Field</asp:ListItem>
            <asp:ListItem>User name</asp:ListItem>
            <asp:ListItem>Status</asp:ListItem>
            
        </asp:DropDownList>
    </td>
    <td   id ="colselstatus" runat="server" valign="top" align="left">
        <asp:DropDownList ID="ddlstatus" runat="server" CssClass="inputtext" 
            Height="20px" Width="74px">            
            <asp:ListItem Value="A">Active</asp:ListItem>
            <asp:ListItem Value="I">Inactive</asp:ListItem>
        </asp:DropDownList>
    </td>
    <td  id="colsrchtext" runat="server" visible="false" valign="top" align="left">
        <asp:TextBox ID="txtuname" runat="server" CssClass="text_field2" Width="115 px"></asp:TextBox>
    </td>--%>
    <td valign="top" align="left">
        <asp:Button ID="btnsearch" runat="server" Text="Export Excel Sheet" CssClass="button1" 
           onclick="btnsearch_Click"   />
    </td>
    <td valign="top" align="left">
        <asp:Button ID="btnadd" runat="server" Text="Add New"  CssClass="button1" 
           />
        </td>
  
    </tr>
    </table>
    
   <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
                </asp:ToolkitScriptManager>
            
   
    </ContentTemplate>
    </asp:UpdatePanel>
    </div>
</asp:Content>


