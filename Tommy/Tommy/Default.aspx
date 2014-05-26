<%@ Page Title="Startsida" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Tommy.Default" %>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Välkommen till Mediaswag</h1>
    <br />
    <p>
        Söker du underhållning? Då har du hittat helt rätt.
        På Mediaswag hittar du massor med roliga bilder, videoklipp och annat kul.
    </p>

    <br />


    <asp:Panel ID="ImagePanel1" runat="server" CssClass="DefaultPics">
    <p>Bilder</p>
      <asp:HyperLink ID="HyperLink7" runat="server" NavigateUrl='<%$ RouteUrl:routename=image %>' > 
      <asp:Image ImageUrl="~/Images/Icons/bildicon.png" ID="Image1" runat="server" CssClass="imagesize"/>
      </asp:HyperLink>
    </asp:Panel>

    <asp:Panel ID="ImagePanel2" runat="server" CssClass="DefaultPics">
    <p>Videoklipp</p>
    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%$ RouteUrl:routename=video %>' > 
    <asp:Image ImageUrl="~/Images/Icons/videoklippicon.jpg" ID="Image2" runat="server" CssClass="imagesize"/>
    </asp:HyperLink>
    </asp:Panel>

   
    <asp:Panel ID="ImagePanel3" runat="server" CssClass="DefaultPics">
    <p>Kontakta oss</p>
    <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl='<%$ RouteUrl:routename=contactus %>' > 
    <asp:Image ImageUrl="~/Images/Icons/emailicon.png" ID="Image3" runat="server" CssClass="imagesize"/>
    </asp:HyperLink>
    </asp:Panel>
 
</asp:Content>
