
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#region Additional Namespaces
using ChinookSystem.BLL;
using Chinook.Data.POCOs;
#endregion

public partial class SamplePages_ManagePlayList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Request.IsAuthenticated)
            {
                TrackSearchList.DataSource = null;
            }
            else
            {
                Response.Redirect("~/Account/Login.aspx");
            }
            
        }
    }

    protected void CheckForException(object sender, ObjectDataSourceStatusEventArgs e)
    {
        MessageUserControl.HandleDataBoundException(e);
    }

    protected void Page_PreRenderComplete(object sender, EventArgs e)
    {
        // PreRenderComplete occurs just after databindings page events
        // And saves to viewstate
        DataPager thePager = TrackSearchList.FindControl("DataPager1") as DataPager;
        if (thePager != null)
        {
            // Trick on search to avoid "No data" on results when old page is greater than actual row count                
            if (thePager.StartRowIndex > thePager.TotalRowCount)
                thePager.SetPageProperties(0, thePager.MaximumRows, true);
        }
    }

    protected void GenreFetch_Click(object sender, EventArgs e)
    {
        MessageUserControl.TryRun(() =>
        {
            int id = int.Parse(GenreDDL.SelectedValue);
            TracksBy.Text = "Genre";
            SearchArgID.Text = id.ToString();
            TrackSearchList.DataBind();
        });
        
    }

    protected void ArtistFetch_Click(object sender, EventArgs e)
    {
        MessageUserControl.TryRun(() =>
        {
            int id = int.Parse(ArtistDDL.SelectedValue);
            TracksBy.Text = "Artist";
            SearchArgID.Text = id.ToString();
            TrackSearchList.DataBind();
        });
    }

    protected void MediaTypeFetch_Click(object sender, EventArgs e)
    {
        MessageUserControl.TryRun(() =>
        {
            int id = int.Parse(MediaTypeDDL.SelectedValue);
            TracksBy.Text = "MediaType";
            SearchArgID.Text = id.ToString();
            TrackSearchList.DataBind();
        });
    }

    protected void AlbumFetch_Click(object sender, EventArgs e)
    {
        MessageUserControl.TryRun(() =>
        {
            int id = int.Parse(AlbumDDL.SelectedValue);
            TracksBy.Text = "Album";
            SearchArgID.Text = id.ToString();
            TrackSearchList.DataBind();
        });
    }

    protected void TrackSearchList_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        if (string.IsNullOrEmpty(PlayListName.Text))
        {
            MessageUserControl.ShowInfo("Required Data", "Playlist Name is required to add a track.");
        }
        else
        {
            string username = User.Identity.Name;
            string playlistname = PlayListName.Text;
            //the trackid is attached to each ListView row via the CommandArgument parameter

            //this method does not make the value visible to the user (or in view source unless
            //   the hacker decompressed the hidden data)

            //access to the trackid is done via the ListViewCommandEventsArgs e and is treated
            //as an object, thus it needs to be cast to a string for the Parse to work
            ListViewDataItem rowcontents = e.Item as ListViewDataItem;

            //execution of the work in the BLL is done under the umbrella of the user friendly
            //error handler user control
            MessageUserControl.TryRun(() =>
            {
                PlaylistTrackController sysmgr = new PlaylistTrackController();
                sysmgr.Add_TrackToPlaylist(playlistname, username,
                    int.Parse(e.CommandArgument.ToString()));
                //List<UserPlaylistTrack> results = sysmgr.List_TracksForPlaylist(playlistname, username);
                //CurrentPlayList.DataSource = results;
                //CurrentPlayList.DataBind();
            });
        }
        
    }
}