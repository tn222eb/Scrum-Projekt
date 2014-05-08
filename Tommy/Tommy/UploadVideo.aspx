<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UploadVideo.aspx.cs" Inherits="Tommy.UploadVideo" %>

<%@ Register Assembly="ASPNetFlashVideo.NET3" Namespace="ASPNetFlashVideo" TagPrefix="ASPNetFlashVideo" %>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Ladda upp videoklipp</h2> <br />
    <asp:Label ID="LoginStatus" Text="" runat="server" />

        <label for="VideoTitleTextBox" ID="HeaderLabel" runat="server">Video rubrik</label>


        <asp:TextBox ID="VideoTitleTextBox" runat="server" Text="" MaxLength="255" CssClass="Header" />
        <asp:RequiredFieldValidator ID="HeaderRequiredFieldValidator" runat="server" ErrorMessage="Video rubrik måste anges." ControlToValidate="VideoTitleTextBox" Display="None" ValidationGroup="TextBoxValidation"></asp:RequiredFieldValidator>
         <asp:DropDownList ID="VideoCategoryDropDownList" runat="server"
        SelectMethod="VideoCategoryDropDownList_GetData"
        DataTextField="videocategoryname"
        DataValueField="videocategoryid" CssClass="CategoryDropDownList" />

    <asp:FileUpload ID="FileUpload" runat="server" />
   
    <asp:Button ID="UploadButton" Text="Upload" runat="server" OnClick="btnUpload_Click" /><br />

    <asp:Button ID="DeleteButton" Text="Ta bort" runat="server" OnClick="btnDelete_Click" CssClass="DeleteButton" /><br />

    <asp:Label ID="SuccessLabel" Text="" runat="server" Visible="false" CssClass="success" />
    <asp:ValidationSummary ID="ValidationSummary2" runat="server" CssClass="validation-summary-errors" ValidationGroup="TextBoxValidation" ShowModelStateErrors="false"/>
    <asp:Label ID="LabelStatus" Text="" runat="server" />
    <br />
    <br />

    <asp:ListView ID="VideoListView" runat="server"
        ItemType="Tommy.Model.Video"
        SelectMethod="VideoListView_GetData"
        DataKeyNames="videoid">
        <LayoutTemplate>
            <table>
                <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
            </table>
            <asp:DataPager ID="DataPager" runat="server" PageSize="8">
                <Fields>
                    <asp:NextPreviousPagerField ShowFirstPageButton="True" FirstPageText=" Första " ShowNextPageButton="False" ShowPreviousPageButton="False" ButtonType="Button" />
                    <asp:NumericPagerField ButtonType="Link" />
                    <asp:NextPreviousPagerField ShowLastPageButton="True" LastPageText=" Sista " ShowNextPageButton="False" ShowPreviousPageButton="False" ButtonType="Button" />
                </Fields>
            </asp:DataPager>
        </LayoutTemplate>
        <ItemTemplate>
            <span class="saucer" style="float: left; padding: 15px;">
                <asp:CheckBox special='<%# Item.videoname %>' ID="cbDelete" Text="Välj" runat="server" />
                <br />
                <a class="player" style="height: 350px; width: 350px; float: left; margin-top: 5px;" href='<%#"../../Videos/" + Item.videoname %>'></a>
                <asp:Label ID="VideoName" runat="server" Text="<%# Item.videotitle %>"/>

                <asp:HyperLink ID="HyperLink1" runat="server" Text="Redigera" NavigateUrl='<%# GetRouteUrl("editvideo", new { id = Item.videoid }) %>' />
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

</asp:Content>
