<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Admin_Master.master" AutoEventWireup="true" CodeFile="verifydata.aspx.cs" Inherits="admin_verifydata" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <TD class="heading3" valign="top" align="left">
            <TABLE cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
              <TBODY>
              <TR>
             
               
                <TD class="admin_heading" valign="middle" align="left" width="33%" 
                height="38%">Welcome SubAdmin</TD>
                <TD class="heading4" align="center">Verify Data</TD>
                <TD class="admin_heading" vAlign="center" align="right" 
                  width="33%">
                  <A href="Admin_Home.aspx">Home</A>&nbsp;&nbsp;|&nbsp;&nbsp;<asp:LinkButton ID="linklogut" onclick="linklogut_Click"  runat ="server" Text ="Logout"></asp:LinkButton></TD>
                  
               </TR></TBODY></TABLE></TD>
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <div style=" MARGIN-LEFT:5px; WIDTH: 947px; ">
<link href="../css/imgcss.css" rel="stylesheet" type="text/css" />
   <script src="../scripts/jquery-1.3.2.min.js"type="text/javascript"></script>
<script src="../scripts/jquery.blockUI.js" type="text/javascript"></script>
<script type = "text/javascript">
    function BlockUI(elementID) {
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_beginRequest(function() {
            $("#" + elementID).block({ message: '<table align = "Left"><tr><td>' +
     '<img src="../images/loadingAnim.gif"/></td></tr></table>',
                imgcss: {},
                overlayCSS: { backgroundColor: '#000000', opacity: 0.6
                }
            });
        });
        prm.add_endRequest(function() {
            $("#" + elementID).unblock();
        });
    }
    $(document).ready(function() {

    BlockUI("<%=pnlAddEdit.ClientID %>");
        $.blockUI.defaults.css = {};
    });
    function Hidepopup() {
        $find("popup").hide();
        return false;
    }
</script> 
   
    
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" AsyncPostBackTimeout="3600">
        </asp:ToolkitScriptManager>
          

<table style="width: 850px">
        <tr>
            <td style="width: 188px">
                  &nbsp;</td>
            <td style="width: 197px">
                  &nbsp;</td>
            <td style="width: 197px">
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
            <td style="width: 197px">
                <asp:LinkButton ID="lnklocation" runat="server" onclick="lnklocation_Click">LinkButton</asp:LinkButton>
            </td>
            <td>
                &nbsp;
                <asp:LinkButton ID="LinkButton2" runat="server" Visible="false" 
                    >LinkButton</asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td style="width: 188px">
                &nbsp;</td>
            <td style="width: 197px">
               <b> No. of Parameters newly added: </b>
            </td>
            <td style="width: 197px">
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
        
        <table>
        <tr>
          <table  border="0" cellpadding="4" cellspacing="1" width="940">
            <td>
              <asp:GridView ID="gridview1" runat="server" 
                       Width="900px" BorderWidth="0px" CellPadding="4" CellSpacing="1"
                        PageSize="10" AllowPaging="True" AlternatingRowStyle-BackColor="#F5FBEC"
                       AutoGenerateColumns="False"  EmptyDataText ="No Records found" 
                    onpageindexchanging="gridview1_PageIndexChanging" >
                       <RowStyle BackColor="White" VerticalAlign="Middle" HorizontalAlign="Center" Height="20px" />
                       <Columns>
                       <asp:TemplateField  ItemStyle-BorderColor="#dfdfdf" HeaderStyle-BorderColor="#72ba00" >
                                            <HeaderTemplate>
                                               <asp:Label ID="Label1" runat="server" Text="Station." ForeColor="#ffffff"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                           <asp:Label ID="lblrealstat" runat ="server" Text ='<%#Eval("realstat") %>'></asp:Label>
                                            </ItemTemplate>

<HeaderStyle BorderColor="#ffffff"></HeaderStyle>

<ItemStyle BorderColor="#ffffff"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-BorderColor="#dfdfdf" HeaderStyle-BorderColor="#72ba00">
                                            <HeaderTemplate>
                                               
                                                    <asp:Label ID="Label3" runat="server" Text="Parameter" ForeColor="#ffffff"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                           <asp:Label ID="lblparam" runat ="server" Text ='<%#Eval("parameterid") %>'></asp:Label>
                                            </ItemTemplate>


<HeaderStyle BorderColor="#ffffff"></HeaderStyle>

<ItemStyle BorderColor="#ffffff"></ItemStyle>


                                        </asp:TemplateField>
                                         <asp:TemplateField ItemStyle-BorderColor="#dfdfdf" HeaderStyle-BorderColor="#72ba00">
                                            <HeaderTemplate>
                                               
                                                    <asp:Label ID="lablfreq" runat="server" Text="Frequency" ForeColor="#ffffff"></asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                           <asp:Label ID="lblfrequency" runat ="server" Text ='<%#Eval("frequency") %>'></asp:Label>
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
                                <HeaderStyle BackColor="#62c0ef" Font-Bold="True" Height="22px" 
                                    HorizontalAlign="Center" VerticalAlign="Middle" />
                                <AlternatingRowStyle BackColor="#e3f3fb" />
                   </asp:GridView>
            </td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
     
   
    <table style="width: 100%; height: 88px;">
        <tr>
            <td style="width: 411px">
                &nbsp;
            </td>
            <td style="width: 78px">
               
               
               
          
                <asp:Button ID="Button1" runat="server" Text="Verify" CssClass="button1" 
                    onclick="Button1_Click"/>
               
               
               
          
            </td>
            <td>
                &nbsp;
                <asp:Button ID="Button2" runat="server" Text="Cancel" CssClass="button1" 
                    onclick="Button2_Click"/>
            </td>
        </tr>
        <tr>
            <td style="width: 411px">
                &nbsp;
                <asp:LinkButton ID="LinkButton1" runat="server" Visible="false">LinkButton</asp:LinkButton>
            </td>
            <td style="width: 78px">
                
            
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
      
    </table>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
<ContentTemplate>
     <asp:Panel ID="pnlAddEdit" runat="server" CssClass="modalPopup" Height="93px" style = "display:none"    > 
      <table style="margin-left: 38px" >
      <tr >
            <td >
                  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b> Do You Really Want To Cancel Data?<br />
                  
            </td>
      </tr>
      </table>
      <br />
      
      <table style="margin-left: 92px">
      <tr>
      <td>
        <asp:Button ID="btnYes" runat="server" Text="Yes" OnClick = "Yes"  CssClass="button1"  />
        </td>
        <td>
         <asp:Button ID="Button3" runat="server" Text="No" CssClass="button1"  />
        </td>
        </tr>
      </table>
 </asp:Panel>

       </td>
       
   <td valign ="top" align="center">
   
    <table>
    <tr>
<td style="width: 259px; " ><asp:LinkButton ID="lnkFake" runat="server"></asp:LinkButton></td>

        <caption>
            <%--&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;--%>
        </caption>
        </td>
</tr>
    
    </table>
   
   
    
  
    <br />
    <br />
</DIV>
<asp:ModalPopupExtender ID="popup" runat="server" DropShadow="false"
PopupControlID="pnlAddEdit" TargetControlID = "lnkFake"
BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>

 </ContentTemplate> 
<Triggers>
<asp:AsyncPostBackTrigger ControlID = "btnYes" />

<%--<asp:AsyncPostBackTrigger ControlID="btnok" />--%>


</Triggers> 
</asp:UpdatePanel>
  
</div>
</asp:Content>


