<%@ Page Title="Ta bort bild" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DeleteImage.aspx.cs" Inherits="Tommy.DeleteImage" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <h1>
        Ta bort bild
    </h1>
    <asp:PlaceHolder runat="server" ID="ConfirmationPlaceHolder">
        <p>
            Är du säker på att du vill ta bort bilden</p>
    </asp:PlaceHolder>
    <div>
        <asp:LinkButton runat="server" ID="DeleteButton" Text="Ja, Ta bort"
            OnCommand="DeleteButton_Command" CommandArgument='<%$ RouteValue:id %>' CssClass="cssbutton"/>
        <asp:HyperLink ID="CancelButton" runat="server" NavigateUrl='<%$ RouteUrl:routename=uploadimage %>' Text="Nej, Tillbaka" CssClass="cssbutton"/>
    </div>
</asp:Content>
