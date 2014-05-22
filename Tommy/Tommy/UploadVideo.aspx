<%@ Page Title="Ladda upp videoklipp" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UploadVideo.aspx.cs" Inherits="Tommy.UploadVideo" %>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Ladda upp videoklipp</h2>
    <br />

    <%--Info--%>
    <asp:Panel ID="InfoPanel" runat="server">
        <div>
            <span>Innan du laddar upp ett videoklipp, se till så att den inte överstiger :</span>
            <br />
            <span>- 20 mb</span>
        </div>
        <br />
        <div>
            <span>Rekommenderade bild format :</span>
            <br />
            <span>Format: endast <b>mp4</b></span>
        </div>
    </asp:Panel>
    <br />

    <%--Uppladdning--%>
    <label for="VideoTitleTextBox" id="HeaderLabel" runat="server">Video rubrik</label>
    <asp:TextBox ID="VideoTitleTextBox" runat="server" Text="" MaxLength="35" CssClass="Header" />
    <asp:DropDownList ID="VideoCategoryDropDownList" runat="server"
        SelectMethod="VideoCategoryDropDownList_GetData"
        DataTextField="videocategoryname"
        DataValueField="videocategoryid" CssClass="CategoryDropDownList" />
    <asp:FileUpload ID="FileUpload" runat="server" />
    <asp:Button ID="UploadButton" Text="Upload" runat="server" OnClick="UploadButton_Click" /><br />


    <%--Message--%>
    <asp:Label ID="LoginStatus" Text="" runat="server" />
    <asp:Label ID="SuccessLabel" Text="" runat="server" CssClass="success" />


    <%--Validation--%>
    <asp:RequiredFieldValidator ID="VideoTitleRequiredFieldValidator" runat="server" ErrorMessage="Det måste finnas en video rubrik." Display="None" ControlToValidate="VideoTitleTextBox"></asp:RequiredFieldValidator>
    <asp:RequiredFieldValidator ID="FileUploadRequiredFieldValidator" runat="server" ErrorMessage="Ingen fil har valts." Display="None" ControlToValidate="FileUpload"></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="RegularExpressionValidator" runat="server" ErrorMessage="Endast videoklipp av formatet mp4 är tillåtna" ControlToValidate="FileUpload" Display="None" ValidationExpression=".*.(mp4)"></asp:RegularExpressionValidator>
    <asp:ValidationSummary ID="ValidationSummary" runat="server" CssClass="validation-summary-errors" />

    <br />
    <br />

    <%--Videoklipp--%>
    <asp:ListView ID="VideoListView" runat="server"
        ItemType="Tommy.Model.Video"
        SelectMethod="VideoListView_GetData"
        DataKeyNames="videoid">
        <LayoutTemplate>
            <table>
                <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
            </table>
            <asp:DataPager ID="DataPager" runat="server" PageSize="6">
                <Fields>
                    <asp:NextPreviousPagerField ShowFirstPageButton="True" FirstPageText=" Första " ShowNextPageButton="False" ShowPreviousPageButton="False" ButtonType="Button" ButtonCssClass="pagingbutton" />
                    <asp:NumericPagerField ButtonType="Link" />
                    <asp:NextPreviousPagerField ShowLastPageButton="True" LastPageText=" Sista " ShowNextPageButton="False" ShowPreviousPageButton="False" ButtonType="Button" ButtonCssClass="pagingbutton" />
                </Fields>
            </asp:DataPager>
        </LayoutTemplate>
        <ItemTemplate>
            <span class="position">
                <asp:Label ID="VideoName" runat="server" Text="<%# Item.videotitle %>" CssClass="itemtitle" />
                <br />
                <a class="player" href='<%#"../../Videos/" + Item.videoname %>'></a>
                <br />
                <asp:HyperLink ID="EditButton" runat="server" Text="Redigera" NavigateUrl='<%# GetRouteUrl("editvideo", new { id = Item.videoid }) %>' CssClass="cssbutton" />
                <asp:HyperLink ID="DeleteButton" runat="server" Text="Ta bort" NavigateUrl='<%# GetRouteUrl("deletevideo", new { id = Item.videoid }) %>' CssClass="cssbutton" />
            </span>
        </ItemTemplate>
        <EmptyDataTemplate>
            <table>
                <tr>
                    <td>Videoklipp saknas.
                    </td>
                </tr>
            </table>
        </EmptyDataTemplate>
    </asp:ListView>

    <script src="../../FlowPlayer/flowplayer-3.2.12.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        flowplayer("a.player", "../../FlowPlayer/flowplayer-3.2.16.swf", {
            plugins: {
                pseudo: { url: "../../FlowPlayer/flowplayer.pseudostreaming-3.2.12.swf" }
            },
            clip: { provider: 'pseudo', autoPlay: false, autoBuffering: true },
        });
    </script>
    <script>
        $(document).ready(function () {
            var $statusText = $("#MainContent_SuccessLabel");
            if ($statusText.length) {
                setTimeout(function () {
                    $statusText.fadeOut();
                }, 3000);
            }
        });
    </script>
</asp:Content>
