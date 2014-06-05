<%@ Page Title="Ta bort videoklipp" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DeleteVideo.aspx.cs" Inherits="Tommy.DeleteVideo" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2 id="down">
        Ta bort videoklipp
    </h2>

        <br />

    <asp:Label ID="LoginStatus" Text="" runat="server" />

    <asp:Panel ID="DeletionPanel" runat="server">
    <asp:PlaceHolder runat="server" ID="ConfirmationPlaceHolder">
        <p>
            Är du säker på att du vill ta bort videoklippet.</p>
    </asp:PlaceHolder>
    <div>
        <asp:LinkButton runat="server" ID="DeleteButton" Text="Ja, Ta bort"
            OnCommand="DeleteButton_Command" CommandArgument='<%$ RouteValue:id %>' CssClass="buttoneditdelete"/>
        <asp:HyperLink ID="CancelButton" runat="server" NavigateUrl='<%$ RouteUrl:routename=uploadvideo %>' Text="Nej, Tillbaka" CssClass="buttoneditdelete"/>
    </div>
        </asp:Panel>
</asp:Content>
