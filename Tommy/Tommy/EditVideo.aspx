<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditVideo.aspx.cs" Inherits="Tommy.EditVideo" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
          <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="validation-summary-errors" />
    <asp:ValidationSummary ID="ValidationSummary2" runat="server" CssClass="validation-summary-errors" ValidationGroup="EditValidation" ShowModelStateErrors="false" />
       <asp:FormView ID="EditVideoFormView" runat="server"
        ItemType="Tommy.Model.Video"
        DataKeyNames="VideoID"
        DefaultMode="Edit"
        RenderOuterTable="false"
        SelectMethod="EditVideoFormView_GetItem"
        UpdateMethod="EditVideoFormView_UpdateItem" >
        <EditItemTemplate>
            <div class="editor-field">
                <asp:TextBox ID="Header" runat="server" Text='<%# BindItem.videotitle %>' MaxLength="255" />
                <asp:RequiredFieldValidator ID="HeaderRequiredFieldValidator" runat="server" ErrorMessage="Rubrik måste anges." ControlToValidate="Header" Display="None" ValidationGroup="EditValidation"></asp:RequiredFieldValidator>
            </div>

            <div>
                         <asp:DropDownList ID="VideoCategoryDropDownList" runat="server"
        SelectMethod="VideoCategoryDropDownList_GetData"
        DataTextField="videocategoryname"
        DataValueField="videocategoryid"
                             ItemType="Tommy.Model.VideoCategory"
                             Enabled="true"
                              SelectedValue='<%# Item.videocategoryid %>'/>
            </div>

            <div id="button-div">
                <asp:LinkButton ID="LinkButton1" runat="server" Text="Spara" CommandName="Update" ValidationGroup="EditValidation" CssClass="button-style" />
            </div>
        </EditItemTemplate>
    </asp:FormView>
</asp:Content>
