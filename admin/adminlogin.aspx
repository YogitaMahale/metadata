<%@ Page Title="Login" Language="C#" AutoEventWireup="true"  CodeFile="adminlogin.aspx.cs" Inherits="adminlogin"%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
     <link href="../css/css.css" rel="stylesheet" type="text/css" />
     
    <script type ="text/javascript">
        window.history.forward();

        function noback() {
            window.history.forward();
            history.go(+1);
            if (history.length > 0) {
                history.go(+1);
            }

        }
        


</script> 

</head>
<body  onload ="javascript:noback();"  >
         <style type="text/css">
        #textfield
        {
            width: 120px;
        }
        #textfield2
        {
            width: 120px;
        }
                       
             .style14
             {
                 height: 22px;
             }
                               
             .style16
             {
                 height: 22px;
                 width: 5px;
             }
                               
             .style17
             {
                 width: 5px;
             }
                               
    </style>
    
    <form id="form1" runat="server" >
     <link href="../css/imgcss.css" rel="stylesheet" type="text/css" /> 
<script src="../scripts/jquery-1.3.2.min.js"type="text/javascript"></script>
<script src="../scripts/jquery.blockUI.js" type="text/javascript"></script>
<script type = "text/javascript">
    function BlockUI(elementID) {
      
   
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_beginRequest(function()
         {
                 
//            $("#" + elementID).block({ message: '<table align = "center"><tr><td>' +
//     '<img src="../images/loadingAnim.gif"/></td></tr></table>',
//                imgcss: {},
//                overlayCSS: { backgroundColor: '#000000', opacity: 0.6
//                }
//            });
           
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
    function btnsaveclick() {
  
        BlockUI("<%=pnlAddEdit.ClientID %>");
        $.blockUI.defaults.css = {};
    }
    
   </script>

<table width="980" cellspacing="0" cellpadding="0" border="0" align="center">
  <TBODY>
  <TR>
  <%--ada--%>
    <td width="10" valign="top" align="left" class="show-right">&nbsp;</td>
    <td width="960">
    <table width="960" border="0" align="center" cellpadding="0" cellspacing="0" style="border-left:solid 1px #cccccc; border-right:solid 1px #cccccc;">
 <tr>
    <td class="header_bg">
	<div class="logo"><a href=""></a></div>
	</td>
  </tr>
  <tr>
    <td class="nev"><table width="100%" border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td width="10">&nbsp;</td>
        <td width="250">&nbsp;</td>
        <td height="35" align="center" valign="middle" class="comman_heading">Admin Control Panel</td>
        <td width="250">&nbsp;</td>
        <td width="10">&nbsp;</td>
      </tr>
    </table></td>
  </tr>

<tr>
    <td height="400" valign="middle" align="center">
    
       <asp:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server">
    </asp:ToolkitScriptManager>
    
    <asp:Panel ID="Panel1"  runat="server"  Width="341px">
   

 <table width="350" cellspacing="0" cellpadding="0" border="0" >
        <TBODY>
       
 <TR>
           <td class="log_top_left_cor"></td>
           <td valign="middle" align="center" class="log_top_bg">Admin Login</td>
        <td class="log_top_right_cor"></td>
        </TR>
        
        <TR>
        <td class="log_left_bg">
        
        </td>
        <td bgcolor="#fafafa" >
      
        <table width="100%" cellspacing="0" cellpadding="0" border="0" style="margin-left:40px">
          <tbody><tr>
            <td width="27%">&nbsp;</td>
            <td width="73%" align="left" colspan="3"> <asp:Label ID="lblmsg" runat="server" ForeColor="Red" 
                        Text="*Indicates required fields"></asp:Label></td>
          </tr>
          <tr>
              <td align="right" class="login_text" height="30" valign="middle">
                  Username</td>
              <td align="left" valign="middle" class="style17">
                  <asp:Label ID="lblerq_unm" runat="server" ForeColor="Red" Text="*"></asp:Label>
              </td>
              <td align="left"  valign="middle">
                  <asp:TextBox ID="txtuserid" runat="server" style="text-align: left" 
                      Width="135px"></asp:TextBox>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                      ControlToValidate="txtuserid" ErrorMessage="*" ValidationGroup="login"></asp:RequiredFieldValidator>
                 
              </td>
              </tr>
          <tr>
            <td height="30" valign="middle" align="right" class="login_text">Password</td>
            <td valign="middle" align="left" class="style17">
            
            <asp:Label ID="lblerqpwd" runat="server" Text="*" ForeColor="Red"></asp:Label></td>
            <td valign="middle" align="left">
              <asp:TextBox ID="txtpassword" runat="server" style="text-align: left" 
                              Width="135px" TextMode="Password"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                              ControlToValidate="txtpassword" ErrorMessage="*" 
                    ValidationGroup="login"></asp:RequiredFieldValidator>
              </td>
          </tr>
          <tr>
            <td height="35" valign="middle" align="left">&nbsp;</td>
            <td valign="middle" align="left" colspan="2">
                
                <asp:Button ID="btnlogin" runat="server" CssClass="button1" 
                    onclick="btnlogin_Click" Text="Login" ValidationGroup="login" Width="79px" />
                

                
              </td>
          </tr>
          <tr>
            <td valign="middle" align="left" class="style14"></td>
            <td valign="middle" align="left" class="style16">&nbsp;</td>
          </tr>
        </tbody></table>
        
        
        
        </td>
<td class="log_right_bg"></td>
<tr>
        <td class="log_bot_left_cor"></td>
        <td class="log_bot_bg"></td>
        <td class="log_bot_right_cor"></td>
      </tr>
      
     
      </TBODY>
      </table>
  </asp:Panel>
  
        </td>
      </tr>
     <tr>
     <td>
     <asp:UpdatePanel ID="pnl1" runat="server">
                <ContentTemplate>
                <asp:Panel ID="pnlAddEdit" runat="server" CssClass="modalPopup"  
                        Width="278px"  style="display:none"    > 
      <%--<table id="datatable" runat=server >
      <tr>
            <td>
                   Do you Really Want to Upload Data?
            </td>
      </tr>
      </table>--%>
      
      <asp:Panel ID="pnllogin" runat="server" Height="122px"  
             Width="270px"  >
      <div id="logindiv" runat="server" > &nbsp;&nbsp;
      <asp:Label ID="lbllogin" Text="Select Role by which you want to log in.." runat="server"  CssClass="login_text"></asp:Label>
      <table style="width: 100%;">
             <tr>
                 <td style="width: 80px; height: 4px;">
                     </td>
                 <td style="width: 106px; height: 4px;">
                     </td>
                 <td style="height: 4px">
                     </td>
             </tr>
             <tr>
                 <td style="width: 80px" align="right">
                     &nbsp;</td>
                 <td style="width: 106px">
                     <asp:RadioButtonList ID="RadioButtonList1" runat="server" >
                     </asp:RadioButtonList>
                  
                 </td>
                 <td>
                     &nbsp;</td>
             </tr>
             <tr>
                 <td style="width: 80px" align="right">
                     &nbsp;</td>
                 <td style="width: 106px">
                     &nbsp;</td>
                 <td>
                     &nbsp;</td>
             </tr>
         </table>
         
         </div>
       </asp:Panel>
       </asp:Panel>
                
                
    <br /> 
     <asp:LinkButton ID="lnkfake1" runat="server" ></asp:LinkButton>              
    <br />
    <br />
<asp:ModalPopupExtender ID="popup" runat="server" DropShadow="false"
PopupControlID="pnlAddEdit" TargetControlID = "lnkfake1"
BackgroundCssClass="modalBackground">

</asp:ModalPopupExtender>
</ContentTemplate>
<Triggers>
<asp:AsyncPostBackTrigger ControlID = "btnlogin" />
</Triggers>
</asp:UpdatePanel>
     </td>
     </tr>
      
      <tr>
    <td valign="middle" align="center" class="footer_bg"><span class="footer_text">&copy; 2011 All Rights Reserved. Hydrology Project (SW), Nashik</span></td>
  </tr>
</table>
</td>
<td width="10" valign="top" align="left" class="show-left"></td>
</TR>
</table>
 
    
      <%--<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">--%>
    
<%--</asp:Content>--%>

    
                
 </form>
</body>
</html>
