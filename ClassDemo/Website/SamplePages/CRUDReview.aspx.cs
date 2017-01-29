using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#region Additional Namespaces
using Chinook.Data.Enitities;
using ChinookSystem.BLL;
#endregion

public partial class SamplePages_CRUDReview : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SelectedTitle.Text = "";
    }

    protected void Search_Click(object sender, EventArgs e)
    {
        MessageUserControl.TryRun(() =>
        {
            if (string.IsNullOrEmpty(SearchArg.Text))
            {
                throw new Exception("Enter an album title or part of the title.");
            }
            else
            {
                AlbumController sysmgr = new AlbumController();
                List<Album> albumlist = sysmgr.Albums_GetbyTitle(SearchArg.Text);
                if (albumlist.Count == 0)
                {
                    MessageUserControl.ShowInfo("Search Result", "No data for album title or partial title " + SearchArg.Text);
                    AlbumList.DataSource = null;
                    AlbumList.DataBind();
                }
                else
                {
                    MessageUserControl.ShowInfo("Search Result","Select the desired album for maintanence");
                    AlbumList.DataSource = albumlist;
                    AlbumList.DataBind();
                }
            }
        });
    }

    protected void CheckForException(object sender, ObjectDataSourceStatusEventArgs e)
    {
        MessageUserControl.HandleDataBoundException(e);
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
}