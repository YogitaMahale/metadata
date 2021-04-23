<%@ Page Title="Parameter list" Language="C#" MasterPageFile="~/Admin/Admin_Master.master" AutoEventWireup="true" CodeFile="updatedesclist.aspx.cs" Inherits="Admin_SectorDetails" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>




 <%--
 <asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
 <TD class=heading3 vAlign=top align=left>
            <TABLE cellSpacing=0 cellPadding=0 width="100%" align=center 
            border=0>
              <TBODY>
              <TR>
             
                <TD width="2%">&nbsp;</TD>
                <TD class=admin_heading vAlign=center align=left width="33%" 
                height=38>Welcome Admin</TD>
                <TD class=heading4 align=middle>Parameter list</TD>
                <TD class=admin_heading vAlign=center align=right 
                  width="33%"><A 
                  href="Admin_Home.aspx">
                    Home</A>&nbsp;&nbsp;|&nbsp;&nbsp;<A 
                  href="adminlogin.aspx"><asp:LinkButton ID="linklogut" runat ="server" Text ="Logout" 
                            onclick="linklogout_Click"></asp:LinkButton> </A></TD>
                <TD width="2%">&nbsp;</TD></TR></TBODY></TABLE></TD>
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <div style="MARGIN-LEFT:5px; WIDTH: 947px; ">

    
    <table style="width: 100%;">
        <tr>
            <td style="width: 344px">
                &nbsp;
            </td>
            <td style="width: 576px">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="lblmessage" runat="server" ForeColor="Red"></asp:Label>
            </td>            
            <td style="height: 29px; width: 191px">
                &nbsp;
            </td>
        </tr>
        </table>
            <table id="tablesearchgriddata" runat="server" border="0" cellpadding="4" cellspacing="1" width="940" style=" margin-left:10px">
        <tr>
           
            <td >
                <asp:GridView ID="gvparameterdesc" runat="server" 
                         BorderWidth="0px" 
                    CellPadding="4" CellSpacing="1"   Width="600px" PageSize="10" 
                    AllowPaging="True" AutoGenerateColumns="False" 
                    onpageindexchanging="gvparameterdesc_PageIndexChanging" 
                    style="margin-left: 127px">
                       <RowStyle BackColor="White" VerticalAlign ="Middle" HorizontalAlign="Center"  />
                       <Columns>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <strong>PARAMETER ID</strong>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                           <asp:Label ID="lblparameterid" runat ="server" Text ='<%#Eval("parameterid") %>'></asp:Label>
                                            </ItemTemplate>
                                              <HeaderStyle BorderColor="#ffffff" VerticalAlign="Middle" HorizontalAlign="Left" ForeColor="White" Width="100px"></HeaderStyle>

<ItemStyle BorderColor="#ffffff" VerticalAlign="Middle" HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <strong>Parameter Description</strong>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                           <asp:Label ID="lblparameterdesc" runat ="server" Text ='<%#Eval("parameterdesc") %>'></asp:Label>
                                            </ItemTemplate>
                                              <HeaderStyle BorderColor="#ffffff" VerticalAlign="Middle" HorizontalAlign="Left" ForeColor="White"></HeaderStyle>

<ItemStyle BorderColor="#ffffff" VerticalAlign="Middle" HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="false">
                                            <HeaderTemplate>
                                                <strong>Sr No.</strong>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                           <asp:Label ID="lblsrno" runat ="server" Text ='<%#Eval("srno") %>'></asp:Label>
                                            </ItemTemplate>
                                            
                                              <HeaderStyle BorderColor="#ffffff" VerticalAlign="Middle" HorizontalAlign="Left" ForeColor="White"></HeaderStyle>

<ItemStyle BorderColor="#ffffff" VerticalAlign="Middle" HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <strong>Edit</strong>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                          <asp:ImageButton ID="btnedit" ImageUrl="../images/edit.png" runat="server" OnClick="btnedit_click"/>
                                            </ItemTemplate>
                                             <HeaderStyle BorderColor="#ffffff" VerticalAlign="Middle" HorizontalAlign="Center" ForeColor="White"></HeaderStyle>

<ItemStyle BorderColor="#ffffff" VerticalAlign="Middle" HorizontalAlign="Center"></ItemStyle>
                                        </asp:TemplateField>
                                        </Columns>
                       <FooterStyle BackColor="#CCCCCC" />
                                <PagerStyle BackColor="#b4d3e2" ForeColor="#CC3300" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#D7FFD7" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#62c0ef" Font-Bold="True"   VerticalAlign="Middle" HorizontalAlign="Center" Height="22px" />
                     <AlternatingRowStyle BackColor="#e3f3fb" />
                   </asp:GridView>
              
            </td>
        </tr>
         </table>
            <table style="width: 799px">
        <tr>
            <td style="width: 433px">
                &nbsp;
            </td>
            <td>

                <asp:Button ID="btnback" runat="server" CssClass="button1" Text="Back" 
                    Width="75px" onclick="btnback_Click" Height="24px" />
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="width: 433px">
                &nbsp;</td>
            <td>
            <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
            </asp:ToolkitScriptManager>
            </td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    <br />
    <br />
    
  
    <br />
    </div>
</asp:Content>
