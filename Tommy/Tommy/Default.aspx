<%@ Page Title="Startsida" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Tommy.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h4>
        Hej och välkommen käre användare! Söker du underhållning? Då har du hittat helt rätt.
        På Tommy.nu hittar du massor med roliga bilder, videoklipp och annat kul.
    </h4>

    <h6>
        Senaste bilderna
    </h6>
     <asp:ListView ID="ImageListView" runat="server"
        ItemType="Tommy.Model.Image"
        SelectMethod="ImageListView_GetData"
        DataKeyNames="imageid">
        <LayoutTemplate>
            <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
        </LayoutTemplate>
        <ItemTemplate>
            <span class="position">
                <asp:Label ID="ImageName" runat="server" Text="<%# Item.imagetitle %>" CssClass="itemtitle" />
                <br />
                <asp:Image ImageUrl='<%#"~/Images/" + Item.imagename %>' ID="image" runat="server" CssClass="thumbstyle" /><br />
            </span>
        </ItemTemplate>
        <EmptyDataTemplate>
            <table>
                <tr>
                    <td>
                        Bilder saknas.
                    </td>
                </tr>
            </table>
        </EmptyDataTemplate>

    </asp:ListView>
</asp:Content>
