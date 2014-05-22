<%@ Page Title="Redigera videoklipp" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditVideo.aspx.cs" Inherits="Tommy.EditVideo" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ValidationSummary ID="ValidationSummary" runat="server" CssClass="validation-summary-errors"/>
    <asp:FormView ID="EditVideoFormView" runat="server"
        ItemType="Tommy.Model.Video"
        DataKeyNames="VideoID"
        DefaultMode="Edit"
        RenderOuterTable="false"
        SelectMethod="EditVideoFormView_GetItem"
        UpdateMethod="EditVideoFormView_UpdateItem">
        <EditItemTemplate>
            <div class="editor-field">
                <label for="VideoTitleTextBox" id="VideoTitleLabel" runat="server">Video rubrik</label>
                <asp:TextBox ID="VideoTitleTextBox" runat="server" Text='<%# BindItem.videotitle %>' MaxLength="35" />
                <asp:RequiredFieldValidator ID="VideoTitleRequiredFieldValidator" runat="server" ErrorMessage="Rubrik måste anges." ControlToValidate="VideoTitleTextBox" Display="None"></asp:RequiredFieldValidator>
            </div>

            <div>
                <asp:DropDownList ID="VideoCategoryDropDownList" runat="server"
                    SelectMethod="VideoCategoryDropDownList_GetData"
                    DataTextField="videocategoryname"
                    DataValueField="videocategoryid"
                    ItemType="Tommy.Model.VideoCategory"
                    Enabled="true"
                    SelectedValue='<%# Item.videocategoryid %>' CssClass="dplvideomargin" />
            </div>

            <div>
                <asp:LinkButton ID="UpdateButton" runat="server" Text="Spara" CommandName="Update" CssClass="cssbutton" />
                <asp:HyperLink ID="CancelButton" runat="server" NavigateUrl='<%$ RouteUrl:routename=uploadvideo %>' Text="Avbryt" CssClass="cssbutton" />
            </div>
        </EditItemTemplate>
    </asp:FormView>
</asp:Content>
