<%@ Page Title="Redigera bild" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditImage.aspx.cs" Inherits="Tommy.EditImage" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <h2 id="down">
        Redigera bild
    </h2>

        <br />

    <asp:Label ID="LoginStatus" Text="" runat="server" />
    <asp:ValidationSummary ID="ValidationSummary" runat="server" CssClass="validation-summary-errors"/>
    <asp:FormView ID="EditImageFormView" runat="server"
        ItemType="Tommy.Model.Image"
        DataKeyNames="ImageID"
        DefaultMode="Edit"
        RenderOuterTable="false"
        SelectMethod="EditImageFormView_GetItem"
        UpdateMethod="EditImageFormView_UpdateItem">
        <EditItemTemplate>

                <label for="ImageTitleTextBox" id="ImageTitleLabel" runat="server">Bildrubrik</label>
                <asp:TextBox ID="ImageTitleTextBox" runat="server" Text='<%# BindItem.imagetitle %>' MaxLength="35"  CssClass="titletextbox"/>
                <asp:RequiredFieldValidator ID="ImageTitleRequiredFieldValidator" runat="server" ErrorMessage="Rubrik måste anges." ControlToValidate="ImageTitleTextBox" Display="None"></asp:RequiredFieldValidator>

         <br />
          <br />
            <div>
                <label for="ImageCategoryDropDownList" id="categori" runat="server">Bildkategori</label>
                <asp:DropDownList ID="ImageCategoryDropDownList" runat="server"
                    SelectMethod="ImageCategoryDropDownList_GetData"
                    DataTextField="imagecategoryname"
                    DataValueField="imagecategoryid"
                    ItemType="Tommy.Model.ImageCategory"
                    Enabled="true"
                    SelectedValue='<%# Item.imagecategoryid %>' CssClass="CategoryDropDownList" />
            </div>

            <div>
                <asp:LinkButton ID="UpdateButton" runat="server" Text="Spara" CommandName="Update" CssClass="buttoneditdelete" />
                <asp:HyperLink ID="CancelButton" runat="server" NavigateUrl='<%$ RouteUrl:routename=uploadimage %>' Text="Avbryt" CssClass="buttoneditdelete" />
            </div>
        </EditItemTemplate>
    </asp:FormView>
</asp:Content>
