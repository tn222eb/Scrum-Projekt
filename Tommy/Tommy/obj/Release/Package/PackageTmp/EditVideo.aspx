<%@ Page Title="Redigera videoklipp" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditVideo.aspx.cs" Inherits="Tommy.EditVideo" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <h2 id="down">
        Redigera videoklipp
    </h2>

        <br />

    <asp:Label ID="LoginStatus" Text="" runat="server" />

    <asp:ValidationSummary ID="ValidationSummary" runat="server" CssClass="validation-summary-errors"/>
    <asp:FormView ID="EditVideoFormView" runat="server"
        ItemType="Tommy.Model.Video"
        DataKeyNames="VideoID"
        DefaultMode="Edit"
        RenderOuterTable="false"
        SelectMethod="EditVideoFormView_GetItem"
        UpdateMethod="EditVideoFormView_UpdateItem">
        <EditItemTemplate>
                <label for="VideoTitleTextBox" id="VideoTitleLabel" runat="server">Videorubrik</label>
                <asp:TextBox ID="VideoTitleTextBox" runat="server" Text='<%# BindItem.videotitle %>' MaxLength="35" CssClass="titletextbox"/>
                <asp:RequiredFieldValidator ID="VideoTitleRequiredFieldValidator" runat="server" ErrorMessage="Rubrik måste anges." ControlToValidate="VideoTitleTextBox" Display="None"></asp:RequiredFieldValidator>


                 <br />
                 <br />

            <div>
                <label for="VideoCategoryDropDownList" id="categori" runat="server">Videokategori</label>
                <asp:DropDownList ID="VideoCategoryDropDownList" runat="server"
                    SelectMethod="VideoCategoryDropDownList_GetData"
                    DataTextField="videocategoryname"
                    DataValueField="videocategoryid"
                    ItemType="Tommy.Model.VideoCategory"
                    Enabled="true"
                    SelectedValue='<%# Item.videocategoryid %>' CssClass="CategoryDropDownList" />
            </div>

            <div>
                <asp:LinkButton ID="UpdateButton" runat="server" Text="Spara" CommandName="Update" CssClass="buttoneditdelete" />
                <asp:HyperLink ID="CancelButton" runat="server" NavigateUrl='<%$ RouteUrl:routename=uploadvideo %>' Text="Avbryt" CssClass="buttoneditdelete" />
            </div>
        </EditItemTemplate>
    </asp:FormView>
</asp:Content>
