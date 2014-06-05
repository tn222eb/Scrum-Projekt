<%@ Page Title="Musik" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Musik.aspx.cs" Inherits="Tommy.Pages.CategoryPages.Video.Musik" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Musik</h1>
    <br />
    <br />
    <br />

    <asp:ListView ID="VideoListView" runat="server"
        ItemType="Tommy.Model.Video"
        SelectMethod="VideoListView_GetData"
        DataKeyNames="videoid" OnItemDataBound="VideoListView_OnItemDataBound">
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
                                    <div id="uploader">
                <asp:Label ID="Label2" runat="server">Uppladdad av:</asp:Label>
                <asp:LinkButton ID="UploadersName" Text="{0}" OnCommand="UploadersName_Command" CommandArgument='<%# Item.userid %>' runat="server" />
                </div>
                                       </div>
                    </div>
                <a class="player" href='<%#"../../../Videos/" + Item.videoname %>'></a>
                <%--Kommentarpopup--%>
                <asp:LinkButton ID="WindowButton" runat="server" OnCommand="WindowButton_Command" Visible="true" CssClass="cssbutton">Kommentar</asp:LinkButton>
                <asp:Label ID="Window" runat="server" Visible="false">
                   <div class="popupWindow">
                       <div class="fb-comments" data-href="http://localhost:8317/comments/<%# Item.videoname %>" data-numposts="2" data-colorscheme="light" data-width="300px"></div>
                       </div>
                </asp:Label>
                <asp:Label ID="Close" runat="server" Visible="false">
                    <asp:LinkButton ID="CommentCloseButton" runat="server" OnCommand="CommentCloseButton_Command" Visible="true" CssClass="cssbutton">Stäng</asp:LinkButton>
                </asp:Label>
            </span>
        </ItemTemplate>
        <EmptyDataTemplate>
            <table>
                <tr>
                    <td>Det finns inga videoklipp i denna kategorin.
                    </td>
                </tr>
            </table>
        </EmptyDataTemplate>

    </asp:ListView>

    <script src="../../../FlowPlayer/flowplayer-3.2.12.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        flowplayer("a.player", "../../../FlowPlayer/flowplayer-3.2.16.swf", {
            plugins: {
                pseudo: { url: "../../../FlowPlayer/flowplayer.pseudostreaming-3.2.12.swf" }
            },
            clip: { provider: 'pseudo', autoPlay: false, autoBuffering: true },
        });
    </script>


</asp:Content>
