<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Admin_Master.master" AutoEventWireup="true" CodeFile="usermasterlist.aspx.cs" Inherits="admin_usermasterlist" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <TD class="heading3" valign="top" align="left">
            <TABLE cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
              <TBODY>
              <TR>
             
                
                <td class="admin_heading" valign="middle" height="38">Welcome Admin</td>
                <td class="heading4" align="center">User Creation</td>
                <td class="admin_heading" valign="middle" align="right" 
                  width="33%"><a  href="Admin_Home.aspx">Home</a>&nbsp;&nbsp;|&nbsp;&nbsp;<asp:LinkButton ID="linklogut" onclick="linklogut_Click"  runat ="server" Text ="Logout"></asp:LinkButton></tr></tbody></table></td>
</asp:Content>--%>
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
   
    <td align="left" valign="top" >
        <asp:DropDownList ID="ddlsearchby" runat="server" 
            AutoPostBack="True" onselectedindexchanged="ddlsearchby_SelectedIndexChanged"> 
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
    </td>
    <td valign="top" align="left">
        <asp:Button ID="btnsearch" runat="server" Text="Search" CssClass="button1" 
            onclick="btnsearch_Click"  />
    </td>
    <td valign="top" align="left">
        <asp:Button ID="btnadd" runat="server" Text="Add New"  CssClass="button1" 
            onclick="btnadd_Click"/>
        </td>
  
    </tr>
    </table>
    
    
 <table  border="0" cellpadding="4" cellspacing="1" width="940">
        <tr>
          
            <td>
                <asp:GridView ID="gvusermaster" runat="server" BorderWidth="0px" 
                    CellPadding="0" CellSpacing="1"  Width="900px" PageSize="4"                     
                    AllowPaging="True" AutoGenerateColumns="False"   
                    AlternatingRowStyle-BackColor="#ECF7DA" 
                    onpageindexchanging="gvusermaster_PageIndexChanging" >
                       <RowStyle BackColor="White" VerticalAlign ="Middle" HorizontalAlign="Center" Height="20px"  />
                       <Columns>
           <asp:TemplateField ItemStyle-BorderColor="#DFDFDF" HeaderStyle-BorderColor ="#72BA00" >
                                            <HeaderTemplate>
                                                <strong>
                                                    <asp:Label ID="lblid" runat="server" Text="User Id" ForeColor="White"></asp:Label></strong>
                                            </HeaderTemplate>
                                            <ItemTemplate  >
                                           <asp:Label ID="lbluserid" runat ="server" Text ='<%#Eval("userid") %>'></asp:Label>
                                            </ItemTemplate>

  <HeaderStyle BorderColor="#ffffff" HorizontalAlign="Left" VerticalAlign="Middle" />
    <ItemStyle BorderColor="#ffffff" HorizontalAlign="Left" VerticalAlign="Middle"/>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-BorderColor="#DFDFDF" HeaderStyle-BorderColor ="#72BA00">
                                            <HeaderTemplate>
                                                <strong><asp:Label ID="lblUserName" runat="server" Text="User Name" ForeColor="White"></asp:Label></strong>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                           <asp:Label ID="lblusername" runat ="server" Text ='<%#Eval("username") %>'></asp:Label>
                                            </ItemTemplate>

  <HeaderStyle BorderColor="#ffffff" HorizontalAlign="Left" VerticalAlign="Middle" />
   <ItemStyle BorderColor="#ffffff" HorizontalAlign="Left" VerticalAlign="Middle"/>
                                        </asp:TemplateField>
                                         <asp:TemplateField ItemStyle-BorderColor="#DFDFDF" HeaderStyle-BorderColor ="#72BA00" Visible="false">
                                            <HeaderTemplate>
                                                <strong><asp:Label ID="lblsrno" runat="server" Text=" Srno" ForeColor="White"></asp:Label></strong>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                           <asp:Label ID="lblsrno" runat ="server" Text ='<%#Eval("srno") %>'></asp:Label>
                                            </ItemTemplate>

  <HeaderStyle BorderColor="#ffffff" HorizontalAlign="Left" VerticalAlign="Middle" />
 <ItemStyle BorderColor="#ffffff" HorizontalAlign="Left" VerticalAlign="Middle"/>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-BorderColor="#DFDFDF" HeaderStyle-BorderColor ="#72BA00">
                                            <HeaderTemplate>
                                                 <strong><asp:Label ID="lblStatus" runat="server" Text="Status" ForeColor="White"></asp:Label></strong>
                                            </HeaderTemplate>
                                          
                                          <ItemTemplate>
                                           <asp:Label ID="lblstatus" runat ="server" Text ='<%#Eval("status") %>'></asp:Label>
                                            </ItemTemplate>

  <HeaderStyle BorderColor="#ffffff" HorizontalAlign="Left" VerticalAlign="Middle" />
<ItemStyle BorderColor="#ffffff" HorizontalAlign="Left" VerticalAlign="Middle"/>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-BorderColor="#DFDFDF" HeaderStyle-BorderColor ="#72BA00" >
                                            <HeaderTemplate>
                                                 <strong><asp:Label ID="lbledit" runat="server" Text="Edit" ForeColor="White" Width="50px"></asp:Label></strong>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                           
                                               <asp:ImageButton ID="btnedit" runat="server"  OnClick="btnedit_click"  ImageUrl="~/images/edit.png" />
                                                
                                            </ItemTemplate>

  <HeaderStyle BorderColor="#ffffff" HorizontalAlign="Center" VerticalAlign="Middle" />
<ItemStyle BorderColor="#ffffff" HorizontalAlign="Center" VerticalAlign="Middle"/>
                                        </asp:TemplateField>
                                        </Columns>
                       <FooterStyle BackColor="#CCCCCC" />
                                <PagerStyle BackColor="#b4d3e2" ForeColor="#CC3300" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#D7FFD7" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#62c0ef" Font-Bold="True" Height="22px" 
                                    HorizontalAlign="Center" VerticalAlign="Middle" />
                                <AlternatingRowStyle BackColor="#e3f3fb" />

                   </asp:GridView>
            </td>
           
        </tr>
         </table>
        
          <table align="center">
        <tr>
            
            <td>

                <asp:Button ID="btnback" runat="server" CssClass="button1" Text="Back" 
                    Width="75px" onclick="btnback_Click" Height="24px" />
            </td>
            
        </tr>
        <tr>
          
            <td>
                <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
                </asp:ToolkitScriptManager>
            </td>
          
        </tr>
    </table>
   
    </ContentTemplate>
    </asp:UpdatePanel>
    </div>
</asp:Content>


