<%@ Page Title="Serverfel" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="Tommy.Error" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Serverfel</h2>    
    <p>
        Ett fel har uppstått och vi inte kunde hantera din förfrågan.
    </p>


            <asp:Image ImageUrl="~/Images/Icons/error.jpg" ID="ErrorImage" runat="server"/>
    <br />
        <asp:HyperLink ID="HyperLink1" runat="server" Text="Tillbaka till startsidan" NavigateUrl='<%$ RouteUrl:routename=Default %>' CssClass="errorbutton"/>
</asp:Content>
