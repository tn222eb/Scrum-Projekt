<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UploadImage.aspx.cs" Inherits="Tommy.UploadImage" %>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
     <h2>Ladda upp bilder</h2><br />
    <asp:Label ID="LoginStatus" text="" runat="server" />

         <label for="ImageTitleTextBox" ID="HeaderLabel" runat="server">Video Titel</label>
         <asp:TextBox ID="ImageTitleTextBox" runat="server" Text="" MaxLength="255" CssClass="Header" />
    <asp:DropDownList ID="ImageCategoryDropDownList" runat="server"
                    SelectMethod="ImageCategoryDropDownList_GetData"
                    DataTextField="imagecategoryname"
                    DataValueField="imagecategoryid" />
    <asp:FileUpload ID="FileUpload" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="UploadButton" Text="Ladda upp" runat="server" onclick="UploadButton_Click" /><br /><br />
   
    <asp:Button ID="DeleteButton" Text="Ta bort" runat="server" onclick="DeleteButton_Click" /><br /><br />

    <asp:Label ID="SuccessLabel" text="" runat="server" Visible="false" CssClass="success"/>
    <asp:Label ID="LabelStatus" text="" runat="server"/>
    <br /><br />

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
            <asp:CheckBox special='<%# Item.imagename %>' ID="cbDelete" Text="Välj" runat="server" CssClass="left"/><br />
            <asp:ImageButton ImageUrl='<%#"~/Images/" + Item.imagename %>' ID="imgUserPhoto"  runat="server" CssClass="imagestyle" /><br />
            <div class="fb-comments" data-href="http://localhost:53189/comments/<%# Container.DataItem %>" data-numposts="1" data-colorscheme="dark" data-width="350px"></div>
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
