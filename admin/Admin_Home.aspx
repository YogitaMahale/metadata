<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Admin_Master.master" AutoEventWireup="true"
    CodeFile="Admin_Home.aspx.cs" Inherits="Design_Default"  %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%--<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">




 <TD class="heading3" valign="middle" align="left">
            <table cellSpacing="0" cellPadding="0" width="100%" align="center" 
            border="0">
              <tbody>
              <tr>
             
                
                <td class="admin_heading" valign="middle" align="left" width="33%" height="38">Welcome Admin</TD>
                
                <td class="heading4" align="center">Home</td>
                <td class="admin_heading" valign="middle" align="right" width="33%">
                <A href="Admin_Home.aspx">Home</A>&nbsp;&nbsp;|&nbsp;&nbsp;<asp:LinkButton ID="linklogut" onclick="linklogut_Click"  runat ="server" Text ="Logout"></asp:LinkButton> </td>
                
                </tr></tbody></table></td>
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <%--<script type ="text/javascript">
         window.history.forward();
         function noback() {window.history.forward();
         } 
         
         </script>--%>
    <div style="margin-left: 5px; width: 947px;">
        <link href="../css/imgcss.css" rel="stylesheet" type="text/css" />

        <script src="../scripts/jquery-1.3.2.min.js" type="text/javascript"></script>

        <script src="../scripts/jquery.blockUI.js" type="text/javascript"></script>

        <script type="text/javascript">

            function clickButton(e, buttonid) {

                var evt = e ? e : window.event;
                var bt = document.getElementById(buttonid);
                if (bt) {
                    if (evt.KeyCode == 13) {
                        bt.click();
                        return false;
                    }
                }     
            }
            function BlockUI(elementID) {


                var prm = Sys.WebForms.PageRequestManager.getInstance();
                prm.add_beginRequest(function() {

                    $("#" + elementID).block({ message: '<table align = "center"><tr><td>' +
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
                BlockUI("<%=pnltruncate.ClientID %>");

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

        <br />
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" AsyncPostBackTimeout ="3600" runat="server">
        </asp:ToolkitScriptManager>
        <%--<asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>--%>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
            <ContentTemplate>
                <div style="margin-left: 5px; width: 947px;">
                    <table style="width: 931px">
                        <tr>
                            <td valign="top" align="center" style="width: 289px">
                                &nbsp;
                            </td>
                            <td valign="top" align="center" style="width: 384px">
                                <asp:ImageButton ID="userimg0" runat="server" ImageUrl="~/images/changepwd.png" PostBackUrl="~/admin/changepassword.aspx" />
                                <%--<asp:Image  />--%>
                            </td>
                            <td valign="top" align="center" style="width: 497px">
                                <asp:ImageButton ID="userimg" runat="server" ImageUrl="~/images/mange user.gif" PostBackUrl="~/admin/usermasterlist.aspx" />
                            </td>
                            <td valign="top" align="center" style="width: 419px">
                                <asp:ImageButton ID="Image1" runat="server" ImageUrl="~/images/datalist.png" PostBackUrl="~/admin/preparemetadata.aspx" />
                            </td>
                             <td valign="top" align="center" style="width: 384px">
                                <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/images/excel.jpg" PostBackUrl="~/admin/ExportData.aspx" Width="70" Height="70" />
                                <%--<asp:Image  />--%>
                            </td>
                            <td align="center" style="width: 392px" valign="top">
                                &nbsp;
                            </td>
                            <tr>
                                <td valign="top" align="center" style="width: 289px">
                                    &nbsp;
                                </td>
                                <td valign="top" align="center" style="width: 384px">
                                    <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/admin/changepassword.aspx">Change Password</asp:HyperLink>
                                </td>
                                <td valign="top" align="center" style="width: 497px">
                                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/admin/usermasterlist.aspx">User Creation</asp:HyperLink>
                                </td>
                                <td valign="top" align="center" style="width: 419px">
                                    <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/admin/preparemetadata.aspx">Prepare Metadata</asp:HyperLink>
                                </td>
                                 <td valign="top" align="center" style="width: 419px">
                                    <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="~/admin/ExportData.aspx">Export Excel Files</asp:HyperLink>
                                </td>
                                <td align="center" class="leble_text" style="width: 392px" valign="top">
                                    &nbsp;
                                </td>
                                <tr>
                                    <td valign="top" align="center" style="width: 289px">
                                        &nbsp;
                                    </td>
                                    <td valign="top" align="center" style="width: 384px">
                                        &nbsp;
                                    </td>
                                    <td valign="top" align="center" style="width: 497px">
                                        &nbsp;
                                    </td>
                                    <td valign="top" align="center" style="width: 419px">
                                        &nbsp;
                                    </td>
                                    <td align="center" style="width: 392px" valign="top">
                                        &nbsp;
                                    </td>
                                    <tr>
                                        <td valign="top" align="center" style="width: 289px">
                                            &nbsp;
                                        </td>
                                        <td valign="top" align="center" style="width: 384px">
                                            <asp:ImageButton ID="Image2" runat="server" ImageUrl="~/images/1301988201_Database-Upload.png"
                                                OnClick="LinkButton1_Click" />
                                        </td>
                                        <td valign="top" align="center" style="width: 497px">
                                            <asp:ImageButton ID="imgverify" runat="server" ImageUrl="~/images/verify.jpg" PostBackUrl="~/admin/verifydata.aspx" />
                                        </td>
                                        <td valign="top" align="center" style="width: 419px">
                                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="../images/truncatedata.png"
                                                OnClick="LinkButton3_Click" />
                                        </td>
                                        <td align="center" style="width: 392px" valign="top">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top" align="center" style="width: 289px">
                                            &nbsp;
                                        </td>
                                        <td valign="top" align="center" style="width: 384px">
                                            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton11_Click">Upload Metadata</asp:LinkButton>
                                            <asp:Panel ID="pnlAddEdit" runat="server" CssClass="modalPopup" Height="151px" Style="display: none">
                                                <%--<table id="datatable" runat=server >
      <tr>
            <td>
                   Do you Really Want to Upload Data?
            </td>
      </tr>
      </table>--%>
                                                <asp:Panel ID="pnllogin" runat="server" DefaultButton ="btnlogin" Height="120px" Width="270px">
                                                    <div id="logindiv" runat="server">
                                                        <asp:Label ID="lbllogin" Text="Enter your website's login id and passsword for getting access to database"
                                                            runat="server"></asp:Label>
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
                                                                    Userid:
                                                                </td>
                                                                <td style="width: 106px">
                                                                    <asp:TextBox ID="txtuser" runat="server" CssClass="inputtext"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 80px" align="right">
                                                                    Password:
                                                                </td>
                                                                <td style="width: 106px">
                                                                    <asp:TextBox ID="txtpswd" runat="server" CssClass="inputtext" onkeypress="javascript:return clickButton(event,'Button1');" 
                                                                        TextMode="Password"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <table style="width: 100%;">
                                                            <tr>
                                                                <td style="width: 84px">
                                                                    &nbsp;
                                                                </td>
                                                                <td align="left" style="width: 69px">
                                                                    <asp:Button ID="btnlogin"  runat="server" Text="Login" CssClass="button1" OnClick="btnlogin_Click" />
                                                                </td>
                                                                <td align="left">
                                                                    <asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="button1" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </asp:Panel>
                                                <asp:Panel ID="pnlupload" runat="server" Height="150px" Width="300px">
                                                    <div id="datauploaddiv" runat="server" visible="false">
                                                        <table id="datatable" runat="server">
                                                            <tr>
                                                                <td>
                                                                    Do you Really Want to Upload Data?
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <input id="btnSave" type="submit" value="Yes" runat="server" class="button1" onserverclick="Save" />
                                                                    <%-- <asp:Button ID="btnSave" runat="server" Text="Yes"   CssClass="button1" OnClick="Save"/>--%>
                                                                </td>
                                                                <td>
                                                                    <asp:Button ID="btnno" runat="server" Text="No" CssClass="button1" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </asp:Panel>
                                            </asp:Panel>
                                        </td>
                                        <td valign="top" align="center" style="width: 497px">
                                            <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click">Verify Data </asp:LinkButton>
                                        </td>
                                        <td valign="top" align="center" style="width: 419px">
                                            <asp:LinkButton ID="LinkButton3" runat="server" OnClick="LinkButton3_Click">Truncate Database</asp:LinkButton>
                                        </td>
                                        <td align="center" class="leble_text" style="width: 392px" valign="top">
                                            &nbsp;
                                        </td>
                                        <tr>
                                            <td valign="top" align="center" style="width: 289px">
                                                &nbsp;
                                            </td>
                                            <td valign="top" align="center" style="width: 384px">
                                            </td>
                                            <td valign="top" align="center" style="width: 497px">
                                                <asp:LinkButton ID="lnkFake" runat="server"></asp:LinkButton>
                                            </td>
                                            <td valign="top" align="center" style="width: 419px">
                                                <%-- style="display:none"--%>
                                                <asp:Panel ID="pnltruncate" runat="server" CssClass="modalPopup" Height="130px" Style="display: none"
                                                    Width="299px">
                                                    <asp:Panel ID="Panel1" runat="server" DefaultButton ="btntrunclogin" Height="120px" Width="270px">
                                                        <div id="Div1" runat="server">
                                                            <asp:Label ID="Label1" Text="Enter your website's login id and passsword for getting access to database"
                                                                runat="server"></asp:Label>
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
                                                                        Userid:
                                                                    </td>
                                                                    <td style="width: 106px">
                                                                        <asp:TextBox ID="TextBox1" runat="server" CssClass="inputtext"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 80px" align="right">
                                                                        Password:
                                                                    </td>
                                                                    <td style="width: 106px">
                                                                        <asp:TextBox ID="TextBox2" runat="server" CssClass="inputtext" TextMode="Password"></asp:TextBox>
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <table style="width: 100%;">
                                                                <tr>
                                                                    <td style="width: 84px">
                                                                        &nbsp;
                                                                    </td>
                                                                    <td align="left" style="width: 69px">
                                                                        <asp:Button ID="btntrunclogin" runat="server" Text="Login" CssClass="button1" OnClick="btntrunclogin_Click" />
                                                                    </td>
                                                                    <td align="left">
                                                                        <asp:Button ID="Button4" runat="server" Text="Cancel" CssClass="button1" OnClick="Button4_Click" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </asp:Panel>
                                                    <asp:Panel ID="paneltruncate" runat="server" Height="120px" Width="270px">
                                                        <div id="truncatediv" runat="server" visible="false">
                                                            <table style="width: 100%;">
                                                                <tr>
                                                                    <td style="width: 21px">
                                                                        &nbsp;
                                                                    </td>
                                                                    <td>
                                                                        <b>You Really Want to Truncate Table ?</b>
                                                                    </td>
                                                                    <td>
                                                                        &nbsp;
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <br />
                                                            <table style="width: 39%; height: 16px; margin-left: 5px;">
                                                                <tr>
                                                                    <td style="width: 70px" align="left">
                                                                        <asp:Button ID="Button1" runat="server" CssClass="button1" Text="Yes" OnClick="Button1_Click"
                                                                            Width="67px" />
                                                                    </td>
                                                                    <td style="width: 76px" align="left">
                                                                        <asp:Button ID="Button2" runat="server" Text="No" CssClass="button1" Height="24px"
                                                                            Width="67px" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </asp:Panel>
                                                </asp:Panel>
                                                <asp:LinkButton ID="lnkfake1" runat="server"></asp:LinkButton>
                                            </td>
                                            <td align="center" style="width: 392px" valign="top">
                                                &nbsp;
                                            </td>
                    </table>
                    <asp:ModalPopupExtender ID="popup" runat="server" DropShadow="false" PopupControlID="pnlAddEdit"
                        TargetControlID="lnkfake" BackgroundCssClass="modalBackground">
                    </asp:ModalPopupExtender>
                    <asp:ModalPopupExtender ID="truncatepopup" runat="server" DropShadow="false" PopupControlID="pnltruncate"
                        TargetControlID="lnkfake1" BackgroundCssClass="modalBackground">
                    </asp:ModalPopupExtender>
                    <%--lnkFake--%>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSave" />
                <asp:AsyncPostBackTrigger ControlID="Button1" />
            </Triggers>
        </asp:UpdatePanel>
        <br />
        <br />
        <br />
    </div>
</asp:Content>
