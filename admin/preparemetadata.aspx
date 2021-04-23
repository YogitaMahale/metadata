<%@ Page Title="Prepare Metadata" Language="C#" MasterPageFile="~/admin/Admin_Master.master"
    AutoEventWireup="true" CodeFile="preparemetadata.aspx.cs" Inherits="admin_metadataupload1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%--<%@ Register Namespace="Microsoft.Samples.Alert" TagPrefix="ms" Assembly="Alert" %> 
--%>
<%--<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <TD class=heading3 vAlign=top align=left>
            <TABLE cellSpacing=0 cellPadding=0 width="100%" align=center 
            border=0>
              <TBODY>
              <TR>
             
                <TD width="2%">&nbsp;</TD>
                <TD class=admin_heading vAlign=center align=left width="33%" 
                height=38>Welcome SubAdmin</TD>
                <TD class=heading4 align=middle>Prepare Metadata</TD>
                <TD class=admin_heading vAlign=center align=right 
                  width="33%"><A 
                  href="Admin_Home.aspx">
                    Home</A>&nbsp;&nbsp;|&nbsp;&nbsp;<asp:LinkButton ID="linklogut" onclick="linklogut_Click"  runat ="server" Text ="Logout"></asp:LinkButton></TD>
                <TD width="2%">&nbsp;</TD></TR></TBODY></TABLE></TD>
</asp:Content>
--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <%--<div style=" MARGIN-LEFT:5px; WIDTH: 947px; background-color:#f7f7f7;">--%>
    <link href="../css/imgcss.css" rel="stylesheet" type="text/css" />

    <script src="../scripts/jquery-1.3.2.min.js" type="text/javascript"></script>

    <script src="../scripts/jquery.blockUI.js" type="text/javascript"></script>

    <script type="text/javascript">
        function BlockUI(elementID) {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_beginRequest(function() {
                $("#" + elementID).block({ message: '<table align = "Left"><tr><td>' +
     '<img src="../images/loadingAnim.gif"/></td><td></td></tr></table>',
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

    <div style="margin-left: 5px; width: 800px; background-color: #ffffff;">
        <asp:Label ID="AlertResult" runat="server" ForeColor="Red" Visible="false" />
        <%-- <ms:alert ID="ServerAlert" runat="server" Buttons="YesNo"  Title="Server Alert" OnChoice="ServerAlertChoice"
  Font-Names="Arial" HorizontalAlign="Center" Width="350px" Height="140px" 
            CssClass="button1" >
  <br /><h4 class="leble_text" >There is new data to be inserted</h4><i>Would you like to Continue?</i>.<br /><br />
</ms:alert>--%>
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" AsyncPostBackTimeout="9900">
        </asp:ToolkitScriptManager>
        <%--<asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>--%>
        <table style="width: 100%;">
            <tr>
                <td style="width: 77px">
                    &nbsp;
                </td>
                <td style="width: 77px">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="width: 77px">
                    &nbsp;
                </td>
                <td style="width: 77px">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr id="stationrow" runat="server">
                <td style="width: 77px; height: 36px;">
                </td>
                <td class="label_text" style="text-align: right; height: 36px;">
                    <b>Type:</b>
                </td>
                <td style="height: 36px">
                    <asp:DropDownList ID="ddltype" runat="server" Height="20px" Width="58px">
                        <asp:ListItem>HP</asp:ListItem>
                        <asp:ListItem>NHP</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width: 77px; height: 22px">
                </td>
                <td align="right" class="label_text" style="height: 22px" valign="top">
                    <b>File:</b>
                </td>
                <td style="width: 131px; height: 22px;">
                    <asp:FileUpload ID="FileUpload1" runat="server" CssClass="text_field2" Height="23px"
                        Width="450 px" BorderStyle="Solid" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="FileUpload1"
                        ErrorMessage="Enter filename" SetFocusOnError="True" ValidationGroup="pdata"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Only mdb allowed"
                        ControlToValidate="FileUpload1" ValidationExpression="^.+(.mdb|.MDB)$"></asp:RegularExpressionValidator>
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td style="width: 926px; height: 34px;">
                </td>
                <td style="height: 34px">
                    <asp:Button ID="Button2" runat="server" Text="Prepare Data" UseSubmitBehavior="False"
                        CssClass="button1" OnClick="btnprepare_Click" ValidationGroup="pdata" />
                </td>
                <td style="width: 25px; height: 34px;">
                </td>
                <td style="width: 874px; height: 34px;">
                    <asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="button1" OnClick="btncancel_Click" />
                </td>
            </tr>
            <tr>
                <td style="width: 926px">
                    <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" Visible="false">LinkButton</asp:LinkButton>
                </td>
                <td>
                    &nbsp;
                </td>
                <td style="width: 25px">
                    &nbsp;
                </td>
                <td style="width: 874px">
                    &nbsp;
                </td>
            </tr>
        </table>
        <table style="width: 900px;">
            <tr>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:Panel ID="pnlAddEdit" runat="server" CssClass="modalPopup" Height="93px" Style="display: none">
                                <table style="margin-left: 38px">
                                    <tr>
                                        <td>
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b> There is new data to be inserted<br />
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                Do you want to continue?</b>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <table style="margin-left: 92px">
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnYes" runat="server" Text="Yes" OnClick="Yes" CssClass="button1" />
                                        </td>
                                        <td>
                                            <asp:Button ID="Button1" runat="server" Text="No" CssClass="button1" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            </td>
                            <td align="center" style="width: 200px">
                                <asp:LinkButton ID="lnkFake" runat="server"></asp:LinkButton>
                            </td>
                            <asp:ModalPopupExtender ID="popup" runat="server" DropShadow="false" PopupControlID="pnlAddEdit"
                                TargetControlID="lnkFake" BackgroundCssClass="modalBackground">
                            </asp:ModalPopupExtender>
                            <%--<asp:ModalPopupExtender ID="datapopup" runat="server" DropShadow="false" 
       PopupControlID="pnldata"   BackgroundCssClass="modalBackground" TargetControlID="lnkFake2">
    </asp:ModalPopupExtender>--%>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnYes" />
                            <%--<asp:AsyncPostBackTrigger ControlID="btnok" />--%>
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
        <%-- <table>
    <tr>
    <td valign ="top" align="center" style="width: 304px">
    --%>
        <br />
        <br />
    </div>
</asp:Content>
