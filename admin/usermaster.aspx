<%@ Page Title="User Master" Language="C#" MasterPageFile="~/admin/Admin_Master.master" AutoEventWireup="true" CodeFile="usermaster.aspx.cs" Inherits="admin_usermaster" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<TD class="heading3" vAlign="top">
            <TABLE cellSpacing="0" cellPadding="0" width="100%" align="center" 
            border="0">
              <TBODY>
              <TR>
             
                
                <TD class="admin_heading" vAlign="middle" height="38">Welcome Admin</TD>
                <TD class="heading4" align="center">User Master</TD>
                <TD class="admin_heading" vAlign="center" align="right" width="33%">
                <A href="Admin_Home.aspx" >Home</A>&nbsp;&nbsp;|&nbsp;&nbsp;<asp:LinkButton ID="linklogut" onclick="linklogut_Click"  runat ="server" Text ="Logout"></asp:LinkButton> </TD>
                
               </TR></TBODY></TABLE></TD>
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">

    <script type="text/javascript" language="javascript">

    function chkvalid() 
    {
        var x = document.getElementById('txtnewpwd').value;

        if (x == "") 
        {
            alert("Required field");
        }
    
    }


</script>
  <div style="border: 1px solid #DFDFDF; MARGIN-LEFT:5px; WIDTH: 947px; background-color:#f7f7f7;">

       <table>
         <tr>
         <td style="width: 514px"> &nbsp;
                    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
                    </asp:ToolkitScriptManager>
         </td>
         <td style="width: 400px"> 
         &nbsp;<asp:Label ID="lblmsg" runat="server" Text="" ForeColor="Red"  ></asp:Label>
         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
             <asp:Label ID="lblmsgvalid" runat="server" ForeColor="Red"  ></asp:Label>
         </td>
         <td style="width: 355px"> &nbsp;
         </td>
         </tr>
      </table>
        <table >
           
            <tr>
           <td style="width: 361px"></td>
                <td style="width: 180px">
                    <asp:Label ID="lblreqfldmsg" runat="server" ForeColor="Red" 
                        Text="* Indicates required fields"></asp:Label>
                </td>
                <td class="black12" style="width: 171px">
                    &nbsp;</td>
                <td style="width: 196px">
                    &nbsp;</td>
                <td style="width: 251px">
                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" 
                        TargetControlID="txtstartdt" PopupButtonID="ImageButton3">
                        </asp:CalendarExtender> </td>
                <td>
                    &nbsp;</td>
            </tr>
           </table>
           <table align="center">
            <tr>
               <td style="width: 211px"></td>
                <td valign="top" align="right" class="label_text">
                    User ID:
                    </td><td>
                    <asp:RequiredFieldValidator 
                        ID="RequiredFieldValidator2" runat="server" 
                        ControlToValidate="txtuserid" ErrorMessage="*" ValidationGroup="save"></asp:RequiredFieldValidator>
                </td>
                <td valign="top" align="left">
                    <asp:TextBox ID="txtuserid" runat="server" AutoCompleteType="Disabled" 
                        CssClass="text_field2" MaxLength="10"></asp:TextBox>
                    </td>
                <td valign="top" align="right" class="label_text">
                     Start Date:
                     </td><td>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ControlToValidate="txtstartdt" ErrorMessage="*" ValidationGroup="save"></asp:RequiredFieldValidator>
               </td>
                <td valign="top" align="left" style="width: 164px">
                    <asp:TextBox ID="txtstartdt" runat="server" AutoCompleteType="Disabled" 
                         CssClass="text_field2" AutoPostBack="True" 
                        ontextchanged="txtstartdt_TextChanged"></asp:TextBox>
                 <asp:ImageButton ID="ImageButton3" runat="server" Height="15px" Width="20px" 
                        ImageUrl="~/images/calbtn.gif" ValidationGroup="cal"/>&nbsp;
                    </td>
                <td >
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" 
                        ControlToValidate="txtstartdt" ErrorMessage="* eg.01/01/2011" 
                        ValidationExpression="^\d{1,2}(\/)\d{1,2}(\/)\d{4}$"></asp:RegularExpressionValidator>
                </td>
              
            </tr>
            <tr>
            <td style="width: 211px"></td>
                <td valign="top" align="right" class="label_text">
                
                   User Name:
                   </td><td>
                   <asp:RequiredFieldValidator ID="RequiredFieldValidator5" 
                        runat="server" ControlToValidate="txtusername" ErrorMessage="*" 
                        ValidationGroup="save"></asp:RequiredFieldValidator>
                </td>
                <td valign="top" align="left">
                    <asp:TextBox ID="txtusername" runat="server" AutoCompleteType="Disabled"  
                        CssClass="text_field2" MaxLength="50"
                        ></asp:TextBox>
                </td>
                <td valign="top" align="right" class="label_text">
                    
                    Close Date:</td><td></td>
                <td valign="top" align="left" style="width: 164px">
                    <asp:TextBox ID="txtclosedt" runat="server" AutoCompleteType="Disabled" 
                        CssClass="text_field2" AutoPostBack="True" ontextchanged="txtclosedt_TextChanged"></asp:TextBox>
                    <asp:ImageButton ID="ImageButton4" runat="server"  Height="15px" Width="20px"
                        ImageUrl="~/images/calbtn.gif" />
                </td>
                <td>
                    <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" 
                        TargetControlID="txtclosedt" PopupButtonID="ImageButton4">
                    </asp:CalendarExtender> 
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" 
                        ControlToValidate="txtclosedt" ErrorMessage="* eg.01/01/2011" 
                        ValidationExpression="^\d{1,2}(\/)\d{1,2}(\/)\d{4}$"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td style="width: 211px"></td>
                <td valign="top" align="right" class="label_text">
                    <asp:Label ID="lblpassword" runat="server" Text="Password:" CssClass="label_text"></asp:Label>
                    </td><td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                        ControlToValidate="txtpassword" ErrorMessage="*" ValidationGroup="save"></asp:RequiredFieldValidator>
                </td>
                <td valign="top" align="left">
                    <asp:TextBox ID="txtpassword" runat="server" AutoCompleteType="Disabled" 
                        TextMode="Password"  CssClass="text_field2" MaxLength="30"
                        ></asp:TextBox>
                    
                </td>
                <td valign="top" align="right" class="label_text">
                    Status:                 </td><td></td>
                <td valign="top" align="left" style="width: 164px">
                    <asp:DropDownList ID="ddstatus" runat="server">
                        <asp:ListItem Value="A">Active</asp:ListItem>
                        <asp:ListItem Value="I">Inactive</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="width: 251px; height: 22px;">
                    </td>
            </tr>
            </table>
           
            <table style="width: 553px" >
                       
                       <tr>
                      <td style="width: 457px"></td>
                      <td  id="savecol" runat="server">
                          <asp:Button ID="btnsave" runat="server" Text="Save"  CssClass="button1" 
                              Width="75px" onclick="btnsave_Click" OnClientClick="return chkvalid()" ValidationGroup="save" />
                           </td> 
                      <td  id="updatecol" runat="server">
                          <asp:Button ID="btnupdate" runat="server" Text="Update" CssClass="button1" 
                              onclick="btnupdate_Click" ValidationGroup="save" />
                           </td>
                      <td >
                          <asp:Button ID="btnback" runat="server" Text="Back" CssClass="button1" 
                              onclick="btnback_Click" Width="72px" />
                           </td>
                       </tr>           
            </table>
<br />
</div>
</asp:Content>


