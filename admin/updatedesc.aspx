<%@ Page Title="Parameter description entry" Language="C#" MasterPageFile="~/admin/Admin_Master.master" AutoEventWireup="true" CodeFile="updatedesc.aspx.cs" Inherits="admin_updatedesc" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <TD class=heading3 vAlign=top align=left>
            <TABLE cellSpacing=0 cellPadding=0 width="100%" align=center 
            border=0>
              <TBODY>
              <TR>
             
                <TD width="2%">&nbsp;</TD>
                <TD class=admin_heading vAlign=center align=left width="33%" 
                height=38>Welcome Admin</TD>
                <TD class=heading4 align=middle>Parameter description entry</TD>
                <TD class=admin_heading vAlign=center align=right 
                  width="33%"><A 
                  href="Admin_Home.aspx">
                    Home</A>&nbsp;&nbsp;|&nbsp;&nbsp;<A 
                  href="adminlogin.aspx"><asp:LinkButton ID="linklogut"  runat ="server" Text ="Logout"></asp:LinkButton> </A></TD>
                <TD width="2%">&nbsp;</TD></TR></TBODY></TABLE></TD>
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    
    <script type="text/javascript">
        function limitChars1(textarea, limit, infodiv1) {

            var text = textarea.value;

            var textlength = text.length;

            var info = document.getElementById(infodiv1);

            if (textlength > limit) {

                info.innerHTML = 'You cannot write more then ' + limit + ' characters!';


                textarea.value = text.substr(0, limit);

                return false;

            }

            else {
                info.innerHTML = 'You have ' + (limit - textlength) + ' characters left.';

                return true;

            }

        }

    
    </script>

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>

    <div style="MARGIN-LEFT:10px; WIDTH: 947px; margin-top:10px ">
    <table align="center" style="width: 595px" >
       
        <tr>
            
            <td class="label_text" valign="top" align="right" style="width: 69px">
                &nbsp;</td>
            
            <td class="label_text" align="left" style="width: 133px">
              Parameter ID :</td>
            <td style="width: 253px">
                <asp:DropDownList ID="ddlparameterid" runat="server" Height="16px" Width="82px">
                    <asp:ListItem>abc</asp:ListItem>
                </asp:DropDownList>
            </td>
           
        </tr>
        <tr>
           
            <td class="label_text" align="right" valign="top" style="width: 69px">
                &nbsp;</td>
           
            <td class="label_text" align="left" style="width: 133px">
                Parameter Description:</td>
            <td valign="top" align="left" style="width: 253px">
                <asp:TextBox ID="txtparameterdesc" runat="server" class="text_field2" 
                    Width="267px"    onkeyup="limitChars1(this, 100, 'infodiv1')" ></asp:TextBox>
                <asp:FilteredTextBoxExtender ID="txtparameterdesc_FilteredTextBoxExtender" 
                    runat="server" FilterMode="InvalidChars" InvalidChars="=#@$/\&quot;^*" 
                    TargetControlID="txtparameterdesc">
                </asp:FilteredTextBoxExtender>
            </td>
            
        </tr>
        <tr>
           
            <td class="label_text" align="right" valign="top" style="width: 69px">
                &nbsp;</td>
           
            <td class="label_text" align="left" style="width: 133px">
                &nbsp;</td>
            <td valign="top" align="left" style="width: 253px">
                <div ID="infodiv1" style="font-weight:bold"></div> </td>
            
        </tr>
         </table>
         
     <table>
       
        <tr>
            <td style="width: 429px">
                &nbsp;</td>
            <td style="width: 79px" >
                <asp:Button ID="btnupdate" runat="server" Text="Update" CssClass="button1" 
                    onclick="btnupdate_Click"  />
                &nbsp;</td>
            <td style="width: 101px">
                <asp:Button ID="btnback" runat="server" Text="Back" CssClass="button1" 
                    onclick="btnback_Click" />
                &nbsp;</td>
            <td style="width: 3px">
                &nbsp;</td>
        </tr>
     </table>
     
</div>
</asp:Content>


