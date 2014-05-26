<%@ Page Title="Serverfel" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="Tommy.Pages.Shared.Error" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <h1>
        Vi är beklagar att ett fel inträffade och vi inte kunde hantera din förfrågan.
    </h1>
        <asp:HyperLink ID="HyperLink1" runat="server" Text="Tillbaka till startsidan" NavigateUrl='<%$ RouteUrl:routename=Default %>' CssClass="cssbutton"/>
</asp:Content>
