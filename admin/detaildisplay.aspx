<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Admin_Master.master" AutoEventWireup="true" CodeFile="detaildisplay.aspx.cs" Inherits="admin_detaildatadisplay" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <td class="heading3" valign="top" align="left">
            <table cellSpacing="0" cellPadding="0" width="100%" align="center" 
            border=0>
            <tbody>
            <tr>
             
               
                <td class="admin_heading" valign="middle" align="left"  width="33%" 
                height="38%">Welcome SubAdmin</td>
                <td class="heading4" align="center">New added data</td>
                <td class="admin_heading" valign="middle" align="right" 
                  width="33%">
                  <A href="Admin_Home.aspx">Home</A>&nbsp;&nbsp;|&nbsp;&nbsp;<asp:LinkButton ID="linklogut" onclick="linklogut_Click"  runat ="server" Text ="Logout"></asp:LinkButton></td>
               </tr></tbody></table></td>
</asp:Content>--%>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <div style="border: 1px solid #DFDFDF; MARGIN-LEFT:5px; WIDTH: 947px; background-color:#f7f7f7;">
<table style="width: 100%;">
        <tr>
            <td style="width: 212px">
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr id="locationrow" runat="server">
            <td style="width: 212px">
                &nbsp;
            </td>
            <td>
                <asp:GridView ID="gridview1" runat="server" 
                       Width="543px" BorderWidth="0px" CellPadding="0" CellSpacing="1"
                        PageSize="30" AllowPaging="True" AlternatingRowStyle-BackColor="#F5FBEC"
                       AutoGenerateColumns="False" EmptyDataText="No Record Found" 
                    onpageindexchanging="gridview1_PageIndexChanging" >
                       <RowStyle BackColor="White" VerticalAlign="Middle" HorizontalAlign="Center" Height="20px" />
                       <Columns>
                       <asp:TemplateField  ItemStyle-BorderColor="#dfdfdf" HeaderStyle-BorderColor="#72ba00" >
                                            <HeaderTemplate>
                                               <asp:Label ID="Label1" runat="server" Text="Station Code" ForeColor="#ffffff"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                           <asp:Label ID="lblrealstat" runat ="server" Text ='<%#Eval("realstat") %>'></asp:Label>
                                            </ItemTemplate>

<HeaderStyle BorderColor="#ffffff"></HeaderStyle>

<ItemStyle BorderColor="#ffffff"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-BorderColor="#dfdfdf" HeaderStyle-BorderColor="#72ba00">
                                            <HeaderTemplate>
                                               
                                                    <asp:Label ID="Label3" runat="server" Text="Station Name" ForeColor="#ffffff"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                           <asp:Label ID="lblparam" runat ="server" Text ='<%#Eval("statname") %>'></asp:Label>
                                            </ItemTemplate>


<HeaderStyle BorderColor="#ffffff"></HeaderStyle>

<ItemStyle BorderColor="#ffffff"></ItemStyle>


                                        </asp:TemplateField>
                                         
                                        
                                        
                     </Columns>
                       <FooterStyle BackColor="#CCCCCC" />
                       <PagerStyle BackColor="#b4d3e2" ForeColor="#CC3300" HorizontalAlign="Center" />
                       <SelectedRowStyle BackColor="#D7FFD7" Font-Bold="True" ForeColor="White" />
                       <HeaderStyle BackColor="#62c0ef" Font-Bold="True"   VerticalAlign="Middle" HorizontalAlign="Center" Height="22px" />

<AlternatingRowStyle BackColor="#e3f3fb"></AlternatingRowStyle>
                   </asp:GridView>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr id="parameterrow" runat="server">
            <td style="width: 212px">
                &nbsp;
            </td>
            <td>
               <asp:GridView ID="gridview2" runat="server" 
                       Width="543px" BorderWidth="0px" CellPadding="0" CellSpacing="1"
                        PageSize="30" AllowPaging="True" AlternatingRowStyle-BackColor="#F5FBEC"
                       AutoGenerateColumns="False" EmptyDataText="No Record Found" 
                    onpageindexchanging="gridview2_PageIndexChanging" >
                       <RowStyle BackColor="White" VerticalAlign="Middle" HorizontalAlign="Center" Height="20px" />
                       <Columns>
                       <asp:TemplateField  ItemStyle-BorderColor="#dfdfdf" HeaderStyle-BorderColor="#72ba00" >
                                            <HeaderTemplate>
                                               <asp:Label ID="Label1" runat="server" Text="Parameter" ForeColor="#ffffff"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                           <asp:Label ID="lblrealstat" runat ="server" Text ='<%#Eval("parameterid") %>'></asp:Label>
                                            </ItemTemplate>

<HeaderStyle BorderColor="#ffffff"></HeaderStyle>

<ItemStyle BorderColor="#ffffff"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-BorderColor="#dfdfdf" HeaderStyle-BorderColor="#72ba00">
                                            <HeaderTemplate>
                                               
                                                    <asp:Label ID="Label3" runat="server" Text="Description" ForeColor="#ffffff"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                           <asp:Label ID="lblparam" runat ="server" Text ='<%#Eval("parameterdesc") %>'></asp:Label>
                                            </ItemTemplate>


<HeaderStyle BorderColor="#ffffff"></HeaderStyle>

<ItemStyle BorderColor="#ffffff"></ItemStyle>


                                        </asp:TemplateField>
                                                                               
                                        
                     </Columns>
                       <FooterStyle BackColor="#CCCCCC" />
                       <PagerStyle BackColor="#b4d3e2" ForeColor="#CC3300" HorizontalAlign="Center" />
                       <SelectedRowStyle BackColor="#D7FFD7" Font-Bold="True" ForeColor="White" />
                       <HeaderStyle BackColor="#62c0ef" Font-Bold="True"   VerticalAlign="Middle" HorizontalAlign="Center" Height="22px" />

<AlternatingRowStyle BackColor="#e3f3fb"></AlternatingRowStyle>
                   </asp:GridView>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    
    <table style="width: 100%;">
        <tr>
            <td style="width: 441px">
                &nbsp;
            </td>
            <td style="width: 234px">
                &nbsp;
                <asp:Button ID="btnback" runat="server" Text="Back"  CssClass="button1" 
                    onclick="btnback_Click"/>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="width: 441px">
                &nbsp;
            </td>
            <td style="width: 234px">
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="width: 441px">
                &nbsp;
            </td>
            <td style="width: 234px">
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
  </div>
</asp:Content>

