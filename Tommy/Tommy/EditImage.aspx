<%@ Page Title="Redigera bild" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditImage.aspx.cs" Inherits="Tommy.EditImage" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ValidationSummary ID="ValidationSummary" runat="server" CssClass="validation-summary-errors"/>
    <asp:FormView ID="EditImageFormView" runat="server"
        ItemType="Tommy.Model.Image"
        DataKeyNames="ImageID"
        DefaultMode="Edit"
        RenderOuterTable="false"
        SelectMethod="EditImageFormView_GetItem"
        UpdateMethod="EditImageFormView_UpdateItem">
        <EditItemTemplate>
            <div class="editor-field">
                <label for="ImageTitleTextBox" id="ImageTitleLabel" runat="server">Bild rubrik</label>
                <asp:TextBox ID="ImageTitleTextBox" runat="server" Text='<%# BindItem.imagetitle %>' MaxLength="35" />
                <asp:RequiredFieldValidator ID="ImageTitleRequiredFieldValidator" runat="server" ErrorMessage="Rubrik måste anges." ControlToValidate="ImageTitleTextBox" Display="None"></asp:RequiredFieldValidator>
            </div>

            <div>
                <asp:DropDownList ID="ImageCategoryDropDownList" runat="server"
                    SelectMethod="ImageCategoryDropDownList_GetData"
                    DataTextField="imagecategoryname"
                    DataValueField="imagecategoryid"
                    ItemType="Tommy.Model.ImageCategory"
                    Enabled="true"
                    SelectedValue='<%# Item.imagecategoryid %>' CssClass="dplmargin" />
            </div>

            <div>
                <asp:LinkButton ID="UpdateButton" runat="server" Text="Spara" CommandName="Update" CssClass="cssbutton" />
                <asp:HyperLink ID="CancelButton" runat="server" NavigateUrl='<%$ RouteUrl:routename=uploadimage %>' Text="Avbryt" CssClass="cssbutton" />
            </div>
        </EditItemTemplate>
    </asp:FormView>
</asp:Content>
