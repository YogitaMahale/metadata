<%@ Page Title="Display Prepared Data" Language="C#" MasterPageFile="~/admin/Admin_Master.master" AutoEventWireup="true" CodeFile="displaydata.aspx.cs" Inherits="admin_displaydata" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <td class="heading3" valign="top" align="left">
            <table cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
             <tbody>
             <tr>
              
                <td class="admin_heading" valign="middle" align="left" width="33%" 
                height="38">Welcome SubAdmin</td>
                <td class="heading4" align="center">Display Prepared Data</td>
                <td class="admin_heading" valign="middle" align="right" width="33%">
                <A href="Admin_Home.aspx">Home</A>&nbsp;&nbsp;|&nbsp;&nbsp;<asp:LinkButton ID="linklogut" onclick="linklogut_Click"  runat ="server" Text ="Logout"></asp:LinkButton></td>
               </tr></tbody></table></td>
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <div style=" MARGIN-LEFT:5px; WIDTH: 947px; ">
<table style="width: 850px">
        <tr>
            <td style="width: 188px">
                  &nbsp;</td>
            <td style="width: 197px">
                  &nbsp;</td>
            <td style="width: 165px">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width: 188px">
                  &nbsp;</td>
            <td style="width: 197px">
                 <b> No. of Locations newly added:</b>
            </td>
            <td style="width: 165px">
                <asp:LinkButton ID="lnklocation" runat="server" onclick="lnklocation_Click">LinkButton</asp:LinkButton>
            </td>
            <td>
                &nbsp;
                <asp:LinkButton ID="LinkButton1" runat="server" Visible="false">LinkButton</asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td style="width: 188px">
                &nbsp;</td>
            <td style="width: 197px">
               <b> No. of Parameters newly added: </b>
            </td>
            <td style="width: 165px">
                <asp:LinkButton ID="lnkparameter" runat="server" onclick="lnkparameter_Click">LinkButton</asp:LinkButton>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
      
         </table>
         <br />
         
        <center >
            <b> 
                <asp:Label ID="lblmsng" runat="server" Text="New data to be inserted" Font-Size="Small"></asp:Label> 
            <br />
            </b>
        </center>
        <table width="940px">
        <tr>
           <td style="width: 23px">
           </td>
            <td>
              <asp:GridView ID="gridview1" runat="server" 
                       Width="900px" BorderWidth="0px" CellPadding="4" CellSpacing="1"
                        PageSize="20" AllowPaging="True" AlternatingRowStyle-BackColor="#F5FBEC"
                       AutoGenerateColumns="False"  EmptyDataText="No Records found"
                    onpageindexchanging="gridview1_PageIndexChanging" 
                    style="margin-left: 0px" >
                       <RowStyle BackColor="White" VerticalAlign="Middle" HorizontalAlign="Center" Height="20px" />
                       <Columns>
                       <asp:TemplateField  ItemStyle-BorderColor="#dfdfdf" HeaderStyle-BorderColor="#72ba00" >
                                            <HeaderTemplate>
                                               <asp:Label ID="Label1" runat="server" Text="Station" ForeColor="#ffffff"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                           <asp:Label ID="lblrealstat" runat ="server" Text ='<%#Eval("realstat") %>'></asp:Label>
                                            </ItemTemplate>

<HeaderStyle BorderColor="#ffffff" VerticalAlign="Middle" HorizontalAlign="Left"></HeaderStyle>

<ItemStyle BorderColor="#ffffff" VerticalAlign="Middle" HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-BorderColor="#dfdfdf" HeaderStyle-BorderColor="#72ba00">
                                            <HeaderTemplate>
                                               
                                                    <asp:Label ID="Label3" runat="server" Text="Parameter" ForeColor="#ffffff"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                           <asp:Label ID="lblparam" runat ="server" Text ='<%#Eval("parameterid") %>'></asp:Label>
                                            </ItemTemplate>


<HeaderStyle BorderColor="#ffffff" VerticalAlign="Middle" HorizontalAlign="Left"></HeaderStyle>

<ItemStyle BorderColor="#ffffff" VerticalAlign="Middle" HorizontalAlign="Left"></ItemStyle>


                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-BorderColor="#dfdfdf" HeaderStyle-BorderColor="#72ba00">
                                            <HeaderTemplate>
                                               
                                                    <asp:Label ID="Lablefreq" runat="server" Text="Frequency" ForeColor="#ffffff"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                           <asp:Label ID="lblfreq" runat ="server" Text ='<%#Eval("frequency") %>'></asp:Label>
                                            </ItemTemplate>


<HeaderStyle BorderColor="#ffffff" VerticalAlign="Middle" HorizontalAlign="Left"></HeaderStyle>

<ItemStyle BorderColor="#ffffff" VerticalAlign="Middle" HorizontalAlign="Left"></ItemStyle>


                                        </asp:TemplateField>
                                         <asp:TemplateField ItemStyle-BorderColor="#dfdfdf" HeaderStyle-BorderColor="#72ba00">
                                            <HeaderTemplate>
                                               
                                                    <asp:Label ID="Label3" runat="server" Text="Availability" ForeColor="#ffffff"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                           <asp:Label ID="lblavlbl" runat ="server" Text ='<%#Eval("availability") %>'></asp:Label>
                                            </ItemTemplate>


<HeaderStyle BorderColor="#ffffff" VerticalAlign="Middle" HorizontalAlign="Left"></HeaderStyle>

<ItemStyle BorderColor="#ffffff" VerticalAlign="Middle" HorizontalAlign="Left"></ItemStyle>


                                        </asp:TemplateField>
                                        <asp:TemplateField  ItemStyle-BorderColor="#dfdfdf" HeaderStyle-BorderColor="#72ba00">
                                            <HeaderTemplate>
                                                <asp:Label ID="Label2" runat="server" Text="Missing Year" ForeColor="#ffffff"></asp:Label> 
                                              
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                           <asp:Label ID="lblrolename" runat ="server" Text ='<%#Eval("year") %>'></asp:Label>
                                            </ItemTemplate>

<HeaderStyle BorderColor="#ffffff" VerticalAlign="Middle" HorizontalAlign="Left"></HeaderStyle>

<ItemStyle BorderColor="#ffffff" VerticalAlign="Middle" HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-BorderColor="#dfdfdf" HeaderStyle-BorderColor="#72ba00">
                                            <HeaderTemplate>
                                               
                                                    <asp:Label ID="Label3" runat="server" Text="Missing years now available" ForeColor="#ffffff"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                           <asp:Label ID="lblmsngavlbl" runat ="server" Text ='<%#Eval("msngavlbl") %>'></asp:Label>
                                            </ItemTemplate>


<HeaderStyle BorderColor="#ffffff" VerticalAlign="Middle" HorizontalAlign="Left"></HeaderStyle>

<ItemStyle BorderColor="#ffffff" VerticalAlign="Middle" HorizontalAlign="Left"></ItemStyle>


                                        </asp:TemplateField>
                                        
                                        
                     </Columns>
                        <FooterStyle BackColor="#CCCCCC" />
                       <PagerStyle BackColor="#b4d3e2" ForeColor="#CC3300" HorizontalAlign="Center" />
                       <SelectedRowStyle BackColor="#D7FFD7" Font-Bold="True" ForeColor="White" />
                       <HeaderStyle BackColor="#62c0ef" Font-Bold="True"   VerticalAlign="Middle" HorizontalAlign="Center" Height="22px" />
                   <AlternatingRowStyle BackColor="#e3f3fb"></AlternatingRowStyle>
                   </asp:GridView>
            </td>
           
        </tr>
    </table>
    
    <table align="center">
        <tr>
            <td>
                <asp:Button ID="Button1" runat="server" Text="Generate Report" CssClass="button1"
                    onclick="Button1_Click" Width="121px"/>
               
            </td>
            <td>
               
                <asp:Button ID="Button2" runat="server" Text="Back" CssClass="button1" 
                    onclick="Button2_Click"/>
            </td>
        </tr>
      
    </table>
  
<br />
    <br />
    </div>
</asp:Content>


