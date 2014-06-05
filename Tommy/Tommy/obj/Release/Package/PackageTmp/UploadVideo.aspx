<%@ Page Title="Mina videoklipp" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UploadVideo.aspx.cs" Inherits="Tommy.UploadVideo" %>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Mina videoklipp</h1>

    <br />

    <asp:Label ID="LoginStatus" Text="" runat="server" />

    <asp:Label ID="UploadBoxContainer" runat="server">
        <div id="UploadBox">
            <h2>Ladda upp videoklipp</h2>
            <br />

            <%--Info--%>
            <asp:Panel ID="InfoPanel" runat="server">
                <div>
                    <span>Innan du laddar upp ett videoklipp, se till så att den inte överstiger:</span>
                    <span id="bold">30 MB</span>
                </div>
                <br />
                <div>
                    <span>Format: </span>
                    <span id="bold2">MP4</span> (H264)
                </div>
            </asp:Panel>
            <br />

            <%--Uppladdning--%>
            <label for="VideoTitleTextBox" id="HeaderLabel" runat="server">Videorubrik</label>
            <asp:TextBox ID="VideoTitleTextBox" runat="server" Text="" MaxLength="35" CssClass="titletextbox" />
            <br />
            <br />
            <div>
                <label for="VideoCategoryDropDownList" id="categori" runat="server">Videokategori</label>
                <asp:DropDownList ID="VideoCategoryDropDownList" runat="server"
                    SelectMethod="VideoCategoryDropDownList_GetData"
                    DataTextField="videocategoryname"
                    DataValueField="videocategoryid" CssClass="CategoryDropDownList" />
            </div>

            <br />
            <asp:FileUpload ID="FileUpload" runat="server" />
            <br />
            <asp:Button ID="UploadButton" Text="Ladda upp" runat="server" OnClick="UploadButton_Click" CausesValidation="True" /><br />

            <div class="loading">
                <asp:Image ID="loadingbar" runat="server" ImageUrl="~/Images/Icons/loader.gif" /><span>Laddar. Var god och vänta.</span>
            </div>

            <%--Message--%>
            <asp:Panel ID="SuccessMessage" runat="server" Visible="false">
                <asp:Image ID="correct" runat="server" ImageUrl="~/Images/Icons/correct.png" /><asp:Label ID="SuccessLabel" Text="" runat="server" CssClass="success" />
            </asp:Panel>


            <%--Validation--%>
            <asp:RequiredFieldValidator ID="VideoTitleRequiredFieldValidator" runat="server" ErrorMessage="Det måste finnas en video rubrik." Display="None" ControlToValidate="VideoTitleTextBox"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="FileUploadRequiredFieldValidator" runat="server" ErrorMessage="Ingen fil har valts." Display="None" ControlToValidate="FileUpload"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator" runat="server" ErrorMessage="Endast videoklipp av formatet mp4 är tillåtna" ControlToValidate="FileUpload" Display="None" ValidationExpression=".*.(mp4)"></asp:RegularExpressionValidator>
            <asp:ValidationSummary ID="ValidationSummary" runat="server" CssClass="validation-summary-errors" />
        </div>
    </asp:Label>
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
            <div id="clear">
                <asp:DataPager ID="DataPager" runat="server" PageSize="6">
                    <Fields>
                        <asp:NextPreviousPagerField ShowFirstPageButton="True" FirstPageText=" Första " ShowNextPageButton="False" ShowPreviousPageButton="False" ButtonType="Button" ButtonCssClass="pagingbutton" />
                      <asp:NumericPagerField ButtonType="Link" CurrentPageLabelCssClass="currentPagerNumber" ButtonCount="6" NumericButtonCssClass="otherPagerNumber"/>
                        <asp:NextPreviousPagerField ShowLastPageButton="True" LastPageText=" Sista " ShowNextPageButton="False" ShowPreviousPageButton="False" ButtonType="Button" ButtonCssClass="pagingbutton" />
                    </Fields>
                </asp:DataPager>
        </LayoutTemplate>
        <ItemTemplate>
            <span class="position">
                <div id="line">
                    <asp:Label ID="LabelForVideoName" runat="server">Videonamn:</asp:Label>
                    <asp:Label ID="VideoName" runat="server" Text="<%# Item.videotitle %>" CssClass="itemtitle" />

                    <br />
                    <asp:Label ID="Label1" runat="server">Datum:</asp:Label>
                    <span id="Span1"><%# Item.createddate.ToString("yyyy/MM/dd") %></span>
                    <br />
                </div>
                <a class="player" href='<%#"../../Videos/" + Item.videoname %>'></a>

                <asp:HyperLink ID="EditButton" runat="server" Text="Redigera" NavigateUrl='<%# GetRouteUrl("editvideo", new { id = Item.videoid }) %>' CssClass="buttonstyling" />
                <asp:HyperLink ID="DeleteButton" runat="server" Text="Ta bort" NavigateUrl='<%# GetRouteUrl("deletevideo", new { id = Item.videoid }) %>' CssClass="buttonstyling" />
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
