<%@ Page Title="Startsida" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Tommy.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h4>
        Hej och välkommen käre användare! Söker du underhållning? Då har du hittat helt rätt.
        På Tommy.nu hittar du massor med roliga bilder, videoklipp och annat kul.
    </h4>

    <h6>
        Senaste bilderna
    </h6>
     <asp:ListView ID="ImageListView" runat="server"
        ItemType="Tommy.Model.Image"
        SelectMethod="ImageListView_GetData"
        DataKeyNames="imageid">
        <LayoutTemplate>
            <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
        </LayoutTemplate>
        <ItemTemplate>
            <span class="position">
                <asp:Label ID="ImageName" runat="server" Text="<%# Item.imagetitle %>" CssClass="itemtitle" />
                <br />
                <asp:Image ImageUrl='<%#"~/Images/" + Item.imagename %>' ID="image" runat="server" CssClass="thumbstyle" /><br />
            </span>
        </ItemTemplate>
        <EmptyDataTemplate>
            <table>
                <tr>
                    <td>
                        Bilder saknas.
                    </td>
                </tr>
            </table>
        </EmptyDataTemplate>

    </asp:ListView> 

    <div id ="list">
     
    <h6>
        Senaste Videoklippen
    </h6>
      <asp:ListView ID="VideoListView" runat="server"
        ItemType="Tommy.Model.Video"
        SelectMethod="VideoListView_GetData"
        DataKeyNames="videoid">
        <LayoutTemplate>
            <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
        </LayoutTemplate>
        <ItemTemplate>
            <span class="position">
                <asp:Label ID="videoname" runat="server" Text="<%# Item.videotitle %>" CssClass="itemtitle" />
                <br />
                
               
                 <a class="player1" href='<%#"../../Videos/" + Item.videoname %>'></a>
                    
            </span>
        </ItemTemplate>
        <EmptyDataTemplate>
            <table>
                <tr>
                    <td>
                        Video saknas.
                    </td>
                </tr>
            </table>
        </EmptyDataTemplate>

    </asp:ListView>
        </div>


    <script src="../../FlowPlayer/flowplayer-3.2.12.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        flowplayer("a.player1", "../../FlowPlayer/flowplayer-3.2.16.swf", {
            plugins: {
                pseudo: { url: "../../FlowPlayer/flowplayer.pseudostreaming-3.2.12.swf" }
            },
            clip: { provider: 'pseudo', autoPlay: false, autoBuffering: true },
        });
    </script>
</asp:Content>
