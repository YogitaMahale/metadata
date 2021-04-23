<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Admin_Master.master" AutoEventWireup="true" CodeFile="changepassword.aspx.cs" Inherits="admin_changepassword" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <TD class=heading3 vAlign=top align=left>
            <TABLE cellSpacing=0 cellPadding=0 width="100%" align=center 
            border=0>
              <TBODY>
              <TR>
             
                <TD width="2%">&nbsp;</TD>
                <TD class=admin_heading vAlign=center align=left width="33%" 
                height=38>Welcome Admin</TD>
                <TD class=heading4 align=middle>Home</TD>
                <TD class=admin_heading vAlign=center align=right 
                  width="33%"><A 
                  href="Admin_Home.aspx">
                    Home</A>&nbsp;&nbsp;|&nbsp;&nbsp;<asp:LinkButton ID="linklogut" 
                        onclick="linklogut_Click"  runat ="server" Text ="Logout" 
                        CausesValidation="False"></asp:LinkButton> </TD>
                <TD width="2%">&nbsp;</TD></TR></TBODY></TABLE></TD>
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <script type="text/javascript" language="javascript">
        function chopText(elem, limit) {
            if (elem.value.length > limit) {
                elem.value = elem.value.substring(0, limit);
            }
        }

</script> 
<div style="border: 1px solid #dfdfdf; WIDTH: 975px; FLOAT: left; margin-bottom:10px">

    <table style="width:100%;">   
        <tr>
            <td class="style6" style="width: 313px">
                &nbsp;</td>
            <td class="style7" style="text-align: center;" colspan="2">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style6" style="width: 313px">
                
            </td>
            <td class="style7" style="text-align: left;" colspan="2">
                <asp:Label ID="lblmsg" runat="server" Text="Label" Visible="False" 
                    ForeColor="Red"></asp:Label>
                </td>
            <td>
                &nbsp;</td>
        </tr>
        </table>
        <div style="border: 1px solid #dfdfdf; WIDTH: 426px; FLOAT: left; height: 203px; margin-left:260px; margin-bottom:20px" >
        <table style="width: 100%">
        <tr>
            <td class="log_top_bg" style=" background-color:#FFFFFF; text-align: center;" 
                colspan="2">
                <asp:Label ID="Label1" runat="server" Text="Change Password"></asp:Label>
            </td>
        </tr>
            <tr>
            <td style="width: 363px; text-align: right; height: 3px;">
                </td>
            <td style="width: 300px; height: 3px;">
            </td>
            </tr>
            <tr>
            <td style="width: 363px; text-align: right;">
                Old Password :</td>
            <td class="style7" style="width: 300px">
                <asp:TextBox ID="txtoldpwd" runat="server" Width="150px" TextMode="Password"></asp:TextBox>
            &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
                    ControlToValidate="txtoldpwd" ErrorMessage="*" ></asp:RequiredFieldValidator>
            </td>
            </tr>
            <tr>
            <td style="width: 363px; text-align: right;">
                New Password :</td>
            <td style="width: 300px">
                <asp:TextBox ID="txtnewpwd" runat="server" Width="150px" TextMode="Password" onkeyup = "chopText(this,30)"></asp:TextBox>
            &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                    ControlToValidate="txtnewpwd" ErrorMessage="*" ></asp:RequiredFieldValidator>
            </td>
            </tr>
            <tr>
            <td style="width: 363px; text-align: right;">Confirm New Password :</td><td style="width: 300px">
                <asp:TextBox ID="txtconfirmnewpwd" runat="server" Width="150px" TextMode="Password" 
                    ></asp:TextBox>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                    ControlToValidate="txtconfirmnewpwd" ErrorMessage="*" 
                   ></asp:RequiredFieldValidator></td></tr><tr>
            <td style="width: 363px; text-align: right; height: 2px;">
                </td>
            <td style="width: 300px; height: 2px;">
            </td>
            </tr>
            </table>
            <table>
        <tr>
            <td style="width: 435px; text-align: right; height: 21px;">
                </td>
            <td style="width: 223px; height: 21px;">
                <asp:Button ID="btnchangepwd" runat="server" Text="Change Password" 
                    onclick="btnchangepwd_Click" CssClass="button1" CausesValidation="False" />
            </td>
            <td style="width: 300px; height: 21px;">
                <asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="button1" 
                    onclick="btncancel_Click" CausesValidation="False" />
            </td>
        </tr> 
        <tr>
            <td style="text-align: center; height: 1px;" colspan="3">
                <asp:CompareValidator ID="CompareValidator1" runat="server" 
                    ControlToCompare="txtnewpwd" ControlToValidate="txtconfirmnewpwd" 
                    ></asp:CompareValidator></td></tr></table></div><br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    </div>
</asp:Content>

