<%@ Page Title="Sport" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Sport.aspx.cs" Inherits="Tommy.Pages.CategoryPages.Video.Sport" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Sport</h2>
    <br />
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
                <%--Videoklipp--%>
                <asp:Label ID="VideoName" runat="server" Text="<%# Item.videotitle %>" CssClass="itemtitle" />
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
                    <td>Videoklipp saknas.
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
