<%@ Page Title="Ta bort videoklipp" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DeleteVideo.aspx.cs" Inherits="Tommy.DeleteVideo" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h1>
        Ta bort videoklipp
    </h1>
    <asp:PlaceHolder runat="server" ID="ConfirmationPlaceHolder">
        <p>
            Är du säker på att du vill ta bort videoklippet</p>
    </asp:PlaceHolder>
    <div>
        <asp:LinkButton runat="server" ID="DeleteButton" Text="Ja, Ta bort"
            OnCommand="DeleteButton_Command" CommandArgument='<%$ RouteValue:id %>' CssClass="cssbutton"/>
        <asp:HyperLink ID="CancelButton" runat="server" NavigateUrl='<%$ RouteUrl:routename=uploadvideo %>' Text="Nej, Tillbaka" CssClass="cssbutton"/>
    </div>
</asp:Content>
