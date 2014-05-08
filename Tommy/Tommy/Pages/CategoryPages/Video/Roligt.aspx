<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Roligt.aspx.cs" Inherits="Tommy.Pages.CategoryPages.Video.Roligt" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Roligt</h2><br />
    <br /><br />

       <asp:ListView ID="VideoListView" runat="server"
                ItemType="Tommy.Model.Video"
                SelectMethod="VideoListView_GetData"
                DataKeyNames="videoid">
                <LayoutTemplate>
                    <table>

                        <%-- Platshållare för nya rader --%>
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
   <a class="player" style="height: 350px; width: 350px; display: block" href='<%#"../../../Videos/" + Item.videoname %>'>
        </a>
        <div class="fb-comments" data-href="http://localhost:53189/comments/<%# Container.DataItem %>" data-numposts="1" data-colorscheme="dark" data-width="350px"></div>  
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