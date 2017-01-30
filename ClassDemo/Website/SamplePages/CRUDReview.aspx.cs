using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#region Additional Namespaces
using Chinook.Data.Enitities;
using ChinookSystem.BLL;
using Chinook.UI;
#endregion

public partial class SamplePages_CRUDReview : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SelectedTitle.Text = "";
    }

    protected void Search_Click(object sender, EventArgs e)
    {

        if (string.IsNullOrEmpty(SearchArg.Text))
        {
            MessageUserControl1.ShowInfo("Enter an album title or part of the title.");
        }
        else
        {
            MessageUserControl1.TryRun(() =>
            {
                AlbumController sysmgr = new AlbumController();
                List<Album> albumlist = sysmgr.Albums_GetbyTitle(SearchArg.Text);
                if (albumlist.Count == 0)
                {
                    MessageUserControl1.ShowInfo("Search Result", "No data for album title or partial title " + SearchArg.Text);
                    AlbumList.DataSource = null;
                    AlbumList.DataBind();
                }
                else
                {
                    MessageUserControl1.ShowInfo("Search Result", "Select the desired album for maintanence");
                    AlbumList.DataSource = albumlist;
                    AlbumList.DataBind();
                }
            });
        }
    }

    protected void CheckForException(object sender, ObjectDataSourceStatusEventArgs e)
    {
        MessageUserControl1.HandleDataBoundException(e);
    }

    protected void AlbumList_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow agvrow = AlbumList.Rows[AlbumList.SelectedIndex];
        string albumid = (agvrow.FindControl("AlbumID") as Label).Text;
        string albumtitle = (agvrow.FindControl("Title") as Label).Text;
        string albumyear = (agvrow.FindControl("Year") as Label).Text;
        string albumlabel = (agvrow.FindControl("AlbumLabel") as Label).Text;
        SelectedTitle.Text = albumtitle + " release in " + albumyear + " by " + albumlabel;
        AlbumID.Text = albumid;
        AlbumTitle.Text = albumtitle;
        AlbumReleaseYear.Text = albumyear;
        AlbumReleaseLabel.Text = albumlabel;

    }

    protected void AddAlbum_Click(object sender, EventArgs e)
    {
        MessageUserControl2.ShowInfo("inside Add");
    }
    protected void UpdateAlbum_Click(object sender, EventArgs e)
    {
        MessageUserControl2.ShowInfo("inside Update");
    }
    protected void DeleteAlbum_Click(object sender, EventArgs e)
    {
        MessageUserControl2.ShowInfo("inside Delete");
    }
}