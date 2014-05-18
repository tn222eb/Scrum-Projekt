<%@ Page Title="Alla bilder" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Image.aspx.cs" Inherits="Tommy.Pages.Shared.Image" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Alla bilder</h2>
    <br />

    <br />
    <br />

    <asp:ListView ID="ImageListView" runat="server"
        ItemType="Tommy.Model.Image"
        SelectMethod="ImageListView_GetData"
        DataKeyNames="imageid">
        <LayoutTemplate>
            <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
            <asp:DataPager ID="DataPager" runat="server" PageSize="6">
                <Fields>
                    <asp:NextPreviousPagerField ShowFirstPageButton="True" FirstPageText=" Första " ShowNextPageButton="False" ShowPreviousPageButton="False" ButtonType="Button" ButtonCssClass="pagingbutton"/>
                    <asp:NumericPagerField ButtonType="Link" />
                    <asp:NextPreviousPagerField ShowLastPageButton="True" LastPageText=" Sista " ShowNextPageButton="False" ShowPreviousPageButton="False" ButtonType="Button" ButtonCssClass="pagingbutton"/>
                </Fields>
            </asp:DataPager>

        </LayoutTemplate>
        <ItemTemplate>

            <span class="position">
                <asp:Label ID="ImageName" runat="server" Text="<%# Item.imagetitle %>" CssClass="itemtitle" />
                <br />
                <asp:Image ImageUrl='<%#"~/Images/" + Item.imagename %>' ID="image" runat="server" CssClass="imagestyle" /><br />
                <asp:LinkButton ID="WindowButton" runat="server" OnCommand="WindowButton_Command" Visible="true" CssClass="cssbutton">Kommentar</asp:LinkButton>
                <asp:Label ID="Window" runat="server" Visible="false">
                   <div class="popupWindow">
                       <div class="fb-comments" data-href="http://localhost:8317/comments/<%# Item.imagename %>" data-numposts="2" data-colorscheme="light" data-width="300px"></div>
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
                    <td>Bilder saknas.
                    </td>
                </tr>
            </table>
        </EmptyDataTemplate>

    </asp:ListView>
</asp:Content>
