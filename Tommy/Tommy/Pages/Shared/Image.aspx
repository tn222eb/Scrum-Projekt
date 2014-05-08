<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Image.aspx.cs" Inherits="Tommy.Pages.Shared.Image" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
         <h2>Alla</h2><br />

    <br /><br />
<%--    <asp:Repeater ID="ImagesRepeater" runat="server">
        <ItemTemplate>
            <span class="saucer" style="float: left; padding: 15px; ">
            <asp:ImageButton OnCommand="imgUserPhoto_Command" CommandArgument="<%# Container.DataItem %>" ImageUrl="<%# Container.DataItem %>" ID="imgUserPhoto"  runat="server" CssClass="imagestyle" /><br />
            <div class="fb-comments" data-href="http://localhost:53189/comments/<%# Container.DataItem %>" data-numposts="1" data-colorscheme="dark" data-width="350px"></div>
            </span>
        </ItemTemplate>
    </asp:Repeater>--%>

    <asp:ListView ID="ImageListView" runat="server"
                ItemType="Tommy.Model.Image"
                SelectMethod="ImageListView_GetData"
                DataKeyNames="imageid">
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
                                <span class="saucer" style="float: left; padding: 15px; ">
            <asp:ImageButton OnCommand="imgUserPhoto_Command" CommandArgument='<%#"../../../Images/" + Item.imagename %>' ImageUrl='<%#"~/Images/" + Item.imagename %>' ID="imgUserPhoto"  runat="server" CssClass="imagestyle" /><br />
                                    <div id="tommy">
            <div class="fb-comments" data-href="http://localhost:53189/comments/<%# Container.DataItem %>" data-numposts="1" data-colorscheme="dark" data-width="300px"></div>
                                        </div>
            </span>
                </ItemTemplate>
                <EmptyDataTemplate>
                    <table>
                        <tr>
                            <td>Bilder saknas.
                            </td>
                        </tr>
                    </table>
                </EmptyDataTemplate>

            </asp:ListView>


    <script src="http://slideshow.triptracker.net/slide.js" type="text/javascript"></script>

</asp:Content>
