<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ManagePlayList.aspx.cs" Inherits="SamplePages_ManagePlayList" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <div >
        <h1>Manage Playlist</h1>
    </div>
    <uc1:MessageUserControl runat="server" id="MessageUserControl" />
     <div class="row">
        <div class="col-sm-2">
            
            <asp:DropDownList ID="ArtistDDL" runat="server"
                 DataSourceID="ArtistListODS"
                 DataTextField="DisplayText" 
                DataValueField="IDValueField"  
                Width="150px">
            </asp:DropDownList><br />
            <asp:Label ID="Label6" runat="server" Text="Artist" Width="50px"></asp:Label>&nbsp;&nbsp;
            <asp:Button ID="ArtistFetch" runat="server" Text="Fetch" CssClass="btn btn-primary" OnClick="ArtistFetch_Click"  /><br /><br />
            
            <asp:DropDownList ID="MediaTypeDDL" runat="server"
                Width="150px" 
                DataSourceID="MediaTypeODS" 
                DataTextField="DisplayText" 
                DataValueField="IDValueField">
            </asp:DropDownList><br />
            <asp:Label ID="Label9" runat="server" Text="Media" Width="50px"></asp:Label>&nbsp;&nbsp;
            <asp:Button ID="MediaTypeFetch" runat="server" Text="Fetch" CssClass="btn btn-primary" OnClick="MediaTypeFetch_Click"   /><br /><br />
            
            <asp:DropDownList ID="GenreDDL" runat="server"
                Width="150px" 
                DataSourceID="GenreListODS" 
                DataTextField="DisplayText" 
                DataValueField="IDValueField">
            </asp:DropDownList><br />
            <asp:Label ID="Label7" runat="server" Text="Genre" Width="50px"></asp:Label>&nbsp;&nbsp;
            <asp:Button ID="GenreFetch" runat="server" Text="Fetch" CssClass="btn btn-primary" OnClick="GenreFetch_Click" /><br /><br />
           
             <asp:DropDownList ID="AlbumDDL" runat="server"
                Width="150px" 
                DataSourceID="AlbumListODS" 
                DataTextField="DisplayText" 
                DataValueField="IDValueField">
            </asp:DropDownList><br />
            <asp:Label ID="Label8" runat="server" Text="Album" Width="50px"></asp:Label>&nbsp;&nbsp;
            <asp:Button ID="AlbumFetch" runat="server" Text="Fetch" CssClass="btn btn-primary" OnClick="AlbumFetch_Click"  /><br />
        </div>
        <div class="col-sm-10">
            <%-- TracksBy and SearchArgID (can be hidden) are needed by the ListView ODS --%>
            <asp:Label ID="Label1" runat="server" Text="Tracks by "></asp:Label>
            <asp:Label ID="TracksBy" runat="server" ></asp:Label>
            <asp:Label ID="SearchArgID" runat="server" ></asp:Label>
            <%-- ItemCommand use for the + event on each row
                 I am hiding the TrackId for each row in ListView using CommandArgument --%>
            <asp:ListView ID="TrackSearchList" runat="server" 
                DataSourceID="TracksForSelectionODS"
                 OnItemCommand="TrackSearchList_ItemCommand">
                <AlternatingItemTemplate>
                    <tr style="background-color: #FFFFFF; color: #284775;">
                        <td>
                            <asp:LinkButton ID="AddToPlayList" runat="server"
                                 CommandArgument='<%# Eval("TrackId") %>'
                                 CssClass="btn"><span aria-hidden="true" class="glyphicon glyphicon-plus"></span>
                            </asp:LinkButton>
                           <%-- <asp:Label Text='<%# Eval("TrackId") %>' runat="server" ID="TrackIdLabel" />--%>
                        </td>
                        <td>
                            <asp:Label Text='<%# Eval("Name") %>' runat="server" ID="NameLabel" /></td>
                        <td>
                            <asp:Label Text='<%# Eval("Title") %>' runat="server" ID="TitleLabel" /></td>
                        <td>
                            <asp:Label Text='<%# Eval("MediaName") %>' runat="server" ID="MediaNameLabel" /></td>
                        <td>
                            <asp:Label Text='<%# Eval("GenreName") %>' runat="server" ID="GenreNameLabel" /></td>
                        <td>
                            <asp:Label Text='<%# Eval("Composer") %>' runat="server" ID="ComposerLabel" /></td>
                        <td>
                            <asp:Label Text='<%# Eval("Milliseconds") %>' runat="server" ID="MillisecondsLabel" /></td>
                        <td>
                            <asp:Label Text='<%# string.Format("{0:0.00}", (int)Eval("Bytes") / 1000000m) %>' runat="server" ID="BytesLabel" /></td>
                        <td>
                            <asp:Label Text='<%# Eval("UnitPrice") %>' runat="server" ID="UnitPriceLabel" /></td>
                    </tr>
                </AlternatingItemTemplate>
               
                <EmptyDataTemplate>
                    <table runat="server" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px;">
                        <tr>
                            <td>No data was returned.</td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
               
                <ItemTemplate>
                    <tr style="background-color: #E0FFFF; color: #333333;">
                        <td>
                            <asp:LinkButton ID="AddToPlayList" runat="server"
                                 CommandArgument='<%# Eval("TrackId") %>'
                                 CssClass="btn"><span aria-hidden="true" class="glyphicon glyphicon-plus"></span>
                            </asp:LinkButton>
                           <%-- <asp:Label Text='<%# Eval("TrackId") %>' runat="server" ID="TrackIdLabel" />--%>
                        </td>
                        <td>
                            <asp:Label Text='<%# Eval("Name") %>' runat="server" ID="NameLabel" /></td>
                        <td>
                            <asp:Label Text='<%# Eval("Title") %>' runat="server" ID="TitleLabel" /></td>
                        <td>
                            <asp:Label Text='<%# Eval("MediaName") %>' runat="server" ID="MediaNameLabel" /></td>
                        <td>
                            <asp:Label Text='<%# Eval("GenreName") %>' runat="server" ID="GenreNameLabel" /></td>
                        <td>
                            <asp:Label Text='<%# Eval("Composer") %>' runat="server" ID="ComposerLabel" /></td>
                        <td>
                            <asp:Label Text='<%# Eval("Milliseconds") %>' runat="server" ID="MillisecondsLabel" /></td>
                        <td>
                            <asp:Label Text='<%# string.Format("{0:0.00}", (int)Eval("Bytes") / 1000000m) %>' runat="server" ID="BytesLabel" /></td>
                        <td>
                            <asp:Label Text='<%# Eval("UnitPrice") %>' runat="server" ID="UnitPriceLabel" /></td>
                    </tr>
                </ItemTemplate>
                <LayoutTemplate>
                    <table runat="server"> 
                        <tr runat="server">
                            <td runat="server">
                                <table runat="server" id="itemPlaceholderContainer" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px; font-family: Verdana, Arial, Helvetica, sans-serif;" border="1">
                                    <tr runat="server" style="background-color: #E0FFFF; color: #333333;">
                                        <th runat="server"></th>
                                        <th runat="server">Name</th>
                                        <th runat="server">Title</th>
                                        <th runat="server">Media</th>
                                        <th runat="server">Genre</th>
                                        <th runat="server">Composer</th>
                                        <th runat="server">Msec</th>
                                        <th runat="server">(MB)</th>
                                        <th runat="server">Price</th>
                                    </tr>
                                    <tr runat="server" id="itemPlaceholder"></tr>
                                </table>
                            </td>
                        </tr>
                        <tr runat="server">
                            <td runat="server" style="text-align: center; background-color: #c0c0c0; 
                                font-family: Verdana, Arial, Helvetica, sans-serif; color: #000000">
                                <asp:DataPager runat="server" ID="DataPager1" PageSize="5" 
                                    PagedControlID="TrackSearchList">
                                    <Fields>
                                        <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False"></asp:NextPreviousPagerField>
                                        <asp:NumericPagerField></asp:NumericPagerField>
                                        <asp:NextPreviousPagerField ButtonType="Button" ShowLastPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False"></asp:NextPreviousPagerField>
                                    </Fields>
                                </asp:DataPager>
                            </td>
                        </tr>
                    </table>
                </LayoutTemplate>
              
            </asp:ListView>
            
           
            <br />
       
            <asp:Label ID="Label5" runat="server" Text="PlayList"></asp:Label>&nbsp;&nbsp;
             <asp:TextBox ID="PlayListName" runat="server"></asp:TextBox>&nbsp;&nbsp;
            <asp:Button ID="PlayListFetch" runat="server" Text="Fetch" CssClass="btn btn-primary"  />&nbsp;&nbsp;
            <asp:LinkButton ID="MoveUp" runat="server" CssClass="btn"  >
                <span aria-hidden="true" class="glyphicon glyphicon-chevron-up"></span>
            </asp:LinkButton>&nbsp;&nbsp;
            <asp:LinkButton ID="MoveDown" runat="server" CssClass="btn"  >
                <span aria-hidden="true" class="glyphicon glyphicon-chevron-down"></span>
            </asp:LinkButton>&nbsp;&nbsp;
             <asp:LinkButton ID="DeleteTrack" runat="server" CssClass="btn"  >
                <span aria-hidden="true" class="glyphicon glyphicon-remove" style="color:red"></span>
            </asp:LinkButton>
           <br /><br />
 <%--
            <asp:GridView ID="CurrentPlayList" runat="server" AutoGenerateColumns="False" 
                Caption="PlayList" GridLines="Horizontal" BorderStyle="None">
                <Columns>
                    <asp:TemplateField HeaderText="Name">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# Bind("Name") %>' ID="Label1"></asp:Label>&nbsp;&nbsp;
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Album">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# Bind("Title") %>' ID="Label2"></asp:Label>&nbsp;&nbsp;
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="MSec">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# Bind("Milliseconds") %>' ID="Label3"></asp:Label>&nbsp;&nbsp;
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="$">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# Bind("UnitPrice") %>' ID="Label4"></asp:Label>&nbsp;&nbsp;
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Pd">
                        <ItemTemplate>
                            <asp:CheckBox runat="server" Checked='<%# Bind("Purchased") %>' Enabled="false" ID="CheckBox1"></asp:CheckBox>&nbsp;&nbsp;
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# Bind("TrackNumber") %>' ID="TrackNumber" ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Label ID="TrackId" runat="server" Text='<%# Eval("TrackId") %>' Visible="false"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    No current tracks on play list.
                </EmptyDataTemplate>
                <SelectedRowStyle BackColor="Aqua" />
            </asp:GridView>--%>
       </div>
    </div>
    <asp:ObjectDataSource ID="ArtistListODS" runat="server" OldValuesParameterFormatString="original_{0}" 
        SelectMethod="List_ArtistNames" TypeName="ChinookSystem.BLL.ArtistController"
         OnSelected="CheckForException"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="MediaTypeODS" runat="server" OldValuesParameterFormatString="original_{0}" 
        SelectMethod="List_MediaTypeNames" TypeName="ChinookSystem.BLL.MediaTypeController"
        OnSelected="CheckForException"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="GenreListODS" runat="server" OldValuesParameterFormatString="original_{0}" 
        SelectMethod="List_GenreNames" TypeName="ChinookSystem.BLL.GenreController"
        OnSelected="CheckForException"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="AlbumListODS" runat="server" OldValuesParameterFormatString="original_{0}" 
        SelectMethod="List_AlbumTitles" TypeName="ChinookSystem.BLL.AlbumController"
        OnSelected="CheckForException"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="TracksForSelectionODS" runat="server"
        OldValuesParameterFormatString="original_{0}"
        SelectMethod="List_TracksForPlaylistSelection"
        TypeName="ChinookSystem.BLL.TrackController"
        OnSelected="CheckForException">
        <SelectParameters>
            <asp:ControlParameter ControlID="SearchArgID" PropertyName="Text" Name="id" Type="Int32"></asp:ControlParameter>
            <asp:ControlParameter ControlID="TracksBy" PropertyName="Text" Name="fetchby" Type="String"></asp:ControlParameter>
        </SelectParameters>

    </asp:ObjectDataSource>
</asp:Content>

