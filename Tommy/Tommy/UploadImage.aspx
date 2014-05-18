<%@ Page Title="Ladda upp bilder" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UploadImage.aspx.cs" Inherits="Tommy.UploadImage" %>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Ladda upp bilder</h2>
    <br />

    <%--Info--%>
    <asp:Panel ID="InfoPanel" runat="server">
        <div>
            <span>Innan du laddar upp en bild, se till så att den inte överstiger :</span>
            <br />
            <span>- 10 mb</span>
        </div>
        <br />
        <div>
            <span>Rekommenderade bild format :</span>
            <br />
            <span>Format : <b>jpg</b>, <b>png</b>, <b>bmp</b>, <b>gif</b></span>
        </div>
    </asp:Panel>
    <br />

    <%--Uppladdning--%>
    <label for="ImageTitleTextBox" id="HeaderLabel" runat="server">Bild rubrik</label>
    <asp:TextBox ID="ImageTitleTextBox" runat="server" Text="" MaxLength="255" CssClass="Header" />
    <asp:DropDownList ID="ImageCategoryDropDownList" runat="server"
        SelectMethod="ImageCategoryDropDownList_GetData"
        DataTextField="imagecategoryname"
        DataValueField="imagecategoryid" />
    <asp:FileUpload ID="FileUpload" runat="server" />
    <asp:Button ID="UploadButton" Text="Ladda upp" runat="server" OnClick="UploadButton_Click" /><br />
    <br />

    <%--Message--%>
    <asp:Label ID="LoginStatus" Text="" runat="server" />
    <asp:Label ID="SuccessLabel" Text="" runat="server" Visible="false" CssClass="success" />

    <%--Validation--%>

    <asp:RegularExpressionValidator ID="FileUploadRegularExpressionValidator" runat="server" ErrorMessage="Filen måste vara av formaten jpg, bmp, gif, png." ControlToValidate="FileUpload" Display="None" ValidationExpression=".*.(gif|jpg|jpeg|png|bmp|jpeg|GIF|JPG|PNG|BMP|JPEG)"></asp:RegularExpressionValidator>
    <asp:RequiredFieldValidator ID="ImageTitleRequiredFieldValidator" runat="server" ErrorMessage="Måste finnas rubrik." Display="None" ControlToValidate="ImageTitleTextBox"></asp:RequiredFieldValidator>
    <asp:RequiredFieldValidator ID="FileUploadRequiredFieldValidator" runat="server" ErrorMessage="En fil måste väljas." Display="None" ControlToValidate="FileUpload"></asp:RequiredFieldValidator>
    <asp:ValidationSummary ID="ValidationSummary" runat="server" CssClass="validation-summary-errors" />

    <br />
    <br />

    <%--Bild--%>
    <asp:ListView ID="ImageListView" runat="server"
        ItemType="Tommy.Model.Image"
        SelectMethod="ImageListView_GetData"
        DataKeyNames="imageid">
        <LayoutTemplate>
            <table>
                <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
            </table>
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
                <asp:Label ID="VideoName" runat="server" Text="<%# Item.imagetitle %>" CssClass="itemtitle" />
                <br />
                <asp:Image ImageUrl='<%#"~/Images/" + Item.imagename %>' ID="imgUserPhoto" runat="server" CssClass="imagestyle" /><br />

                <asp:HyperLink ID="EditButton" runat="server" Text="Redigera" NavigateUrl='<%# GetRouteUrl("editimage", new { id = Item.imageid }) %>' CssClass="cssbutton" />
                <asp:HyperLink ID="DeleteButton" runat="server" Text="Ta bort" NavigateUrl='<%# GetRouteUrl("deleteimage", new { id = Item.imageid }) %>' CssClass="cssbutton" />
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
