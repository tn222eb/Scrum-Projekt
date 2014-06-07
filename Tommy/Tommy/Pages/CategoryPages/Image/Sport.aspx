<%@ Page Title="Sport" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Sport.aspx.cs" Inherits="Tommy.Pages.CategoryPages.Image.Sport" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Sport</h1>
    <br />

    <br />
    <br />
    <asp:ListView ID="ImageListView" runat="server"
        ItemType="Tommy.Model.Image"
        SelectMethod="ImageListView_GetData"
        DataKeyNames="imageid" OnItemDataBound="ImageListView_OnItemDataBound">
        <LayoutTemplate>
            <table>

                <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
            </table>
                         <div id="clear">
            <asp:DataPager ID="DataPager" runat="server" PageSize="6">
                <Fields>
                    <asp:NextPreviousPagerField ShowFirstPageButton="True" FirstPageText=" Första " ShowNextPageButton="False" ShowPreviousPageButton="False" ButtonType="Button" ButtonCssClass="pagingbutton"/>
  <asp:NumericPagerField ButtonType="Link" CurrentPageLabelCssClass="currentPagerNumber" ButtonCount="6" NumericButtonCssClass="otherPagerNumber"/>
                    <asp:NextPreviousPagerField ShowLastPageButton="True" LastPageText=" Sista " ShowNextPageButton="False" ShowPreviousPageButton="False" ButtonType="Button" ButtonCssClass="pagingbutton"/>
                </Fields>
            </asp:DataPager>
        </LayoutTemplate>
        <ItemTemplate>
                       <span class="position">
                                <div id="line">
                <asp:Label ID="LabelForImageName" runat="server">Bildnamn:</asp:Label>
                <asp:Label ID="ImageName" runat="server" Text="<%# Item.imagetitle %>" CssClass="itemtitle" />
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

                <asp:Image ImageUrl='<%#"~/Images/" + Item.imagename %>' ID="image" runat="server" CssClass="imagestyle" /><br />

                           <%--Kommentarpopup--%>
                <asp:LinkButton ID="WindowButton" runat="server" OnCommand="WindowButton_Command" Visible="true" CssClass="cssbutton">Kommentar</asp:LinkButton>
                <asp:Label ID="Window" runat="server" Visible="false">
                   <div class="popupWindow">
                       <div class="fb-comments" data-href="http://mediaswag-001-site1.smarterasp.net/Images/<%# Item.imagename %>" data-numposts="2" data-colorscheme="light" data-width="345px"></div>
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
                    <td>Det finns inga bilder i denna kategorin.
                    </td>
                </tr>
            </table>
        </EmptyDataTemplate>
    </asp:ListView>
</asp:Content>
