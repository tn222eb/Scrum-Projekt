<%@ Page Title="Mina bilder" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UploadImage.aspx.cs" Inherits="Tommy.UploadImage" %>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Mina bilder</h1>

    <br />
    <asp:Label ID="LoginStatus" Text="" runat="server" />



    <asp:Label ID="UploadBoxContainer" runat="server">
        <div id="UploadBox">

            <h2>Ladda upp bild</h2>
            <br />

            <%--Info--%>
            <asp:Panel ID="InfoPanel" runat="server">
                <div>
                    <span>Innan du laddar upp en bild, se till så att den inte överstiger:</span>
                    <span id="bold">10 MB</span>
                </div>
                <br />
                <div>
                    <span>Format: <b>JPG</b>, <b>PNG</b>, <b>BMP</b>, <b>GIF</b></span>
                </div>
            </asp:Panel>
            <br />

            <%--Uppladdning--%>
            <label for="ImageTitleTextBox" id="HeaderLabel" runat="server">Bildrubrik</label>
            <asp:TextBox ID="ImageTitleTextBox" runat="server" Text="" MaxLength="35" CssClass="titletextbox" />
            <br />
            <br />
            <div>
                <label for="ImageCategoryDropDownList" id="categori" runat="server">Bildkategori</label>
                <asp:DropDownList ID="ImageCategoryDropDownList" runat="server"
                    SelectMethod="ImageCategoryDropDownList_GetData"
                    DataTextField="imagecategoryname"
                    DataValueField="imagecategoryid" CssClass="CategoryDropDownList" />
            </div>
            <br />
            <asp:FileUpload ID="FileUpload" runat="server" />
            <br />
            <asp:Button ID="UploadButton" Text="Ladda upp" runat="server" OnClick="UploadButton_Click" /><br />

            <div class="loading">
                <asp:Image ID="loadingbar" runat="server" ImageUrl="~/Images/Icons/loader.gif" /><span>Laddar. Var god och vänta.</span>
            </div>

            <%--Message--%>

             <asp:Panel ID="SuccessMessage" runat="server" Visible="false">
             <asp:Image ID="correct" runat="server" ImageUrl="~/Images/Icons/correct.png" /><asp:Label ID="SuccessLabel" Text="" runat="server" CssClass="success" />
             </asp:Panel>
            <%--Validation--%>

            <asp:RegularExpressionValidator ID="FileUploadRegularExpressionValidator" runat="server" ErrorMessage="Filen måste vara av formaten jpg, bmp, gif, png." ControlToValidate="FileUpload" Display="None" ValidationExpression=".*.(gif|jpg|jpeg|png|bmp|jpeg|GIF|JPG|PNG|BMP|JPEG)"></asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="ImageTitleRequiredFieldValidator" runat="server" ErrorMessage="Måste finnas bildrubrik." Display="None" ControlToValidate="ImageTitleTextBox"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="FileUploadRequiredFieldValidator" runat="server" ErrorMessage="En fil måste väljas." Display="None" ControlToValidate="FileUpload"></asp:RequiredFieldValidator>
            <asp:ValidationSummary ID="ValidationSummary" runat="server" CssClass="validation-summary-errors" />

        </div>
    </asp:Label>

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
                    <asp:Label ID="LabelForImageName" runat="server">Bildnamn:</asp:Label>
                    <asp:Label ID="ImageName" runat="server" Text="<%# Item.imagetitle %>" CssClass="itemtitle" />
                    <br />
                    <asp:Label ID="Label1" runat="server">Datum:</asp:Label>
                    <span id="Span1"><%# Item.createddate.ToString("yyyy/MM/dd") %></span>
                    <br />
                </div>

                <asp:Image ImageUrl='<%#"~/Images/" + Item.imagename %>' ID="image" runat="server" CssClass="imagestyle" /><br />

                <asp:HyperLink ID="EditButton" runat="server" Text="Redigera" NavigateUrl='<%# GetRouteUrl("editimage", new { id = Item.imageid }) %>' CssClass="buttonstyling" />
                <asp:HyperLink ID="DeleteButton" runat="server" Text="Ta bort" NavigateUrl='<%# GetRouteUrl("deleteimage", new { id = Item.imageid }) %>' CssClass="buttonstyling" />
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


