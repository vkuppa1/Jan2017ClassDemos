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
        AlbumID.Text = "";
        AlbumTitle.Text = "";
        AlbumReleaseYear.Text = "";
        AlbumReleaseLabel.Text = "";
        ArtistList.SelectedIndex = 0;
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
        MessageUserControl2.HandleDataBoundException(e);
    }

    protected void AlbumList_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow agvrow = AlbumList.Rows[AlbumList.SelectedIndex];
        string albumid = (agvrow.FindControl("AlbumID") as Label).Text;
        string albumtitle = (agvrow.FindControl("Title") as Label).Text;
        string albumyear = (agvrow.FindControl("Year") as Label).Text;
        string albumlabel = (agvrow.FindControl("AlbumLabel") as Label).Text;
        string artistid = (agvrow.FindControl("ArtistID") as Label).Text;
        SelectedTitle.Text = albumtitle + " release in " + albumyear + " by " + albumlabel;
        AlbumID.Text = albumid;
        AlbumTitle.Text = albumtitle;
        AlbumReleaseYear.Text = albumyear;
        AlbumReleaseLabel.Text = albumlabel;
        ArtistList.SelectedValue = artistid;

    }

    protected void AddAlbum_Click(object sender, EventArgs e)
    {
        if (IsValid)
        {
            MessageUserControl2.TryRun(() =>
            {
                Album newalbum = new Album();
                newalbum.Title = AlbumTitle.Text;
                newalbum.ArtistId = int.Parse(ArtistList.SelectedValue);
                newalbum.ReleaseYear = int.Parse(AlbumReleaseYear.Text);
                newalbum.ReleaseLabel = string.IsNullOrEmpty(AlbumReleaseLabel.Text) ? null : AlbumReleaseLabel.Text;
                AlbumController sysmgr = new AlbumController();
                sysmgr.Albums_Add(newalbum);
            },"Add Album","Album has been successfully added to the database.");
        }
       
    }
    protected void UpdateAlbum_Click(object sender, EventArgs e)
    {
        if (IsValid)
        {
            if (string.IsNullOrEmpty(AlbumID.Text))
            {
                MessageUserControl2.ShowInfo("Missing data", "Missing Album Id. Use Find to locate the album you wish to maintain.");
            }
            else
            {
                int albumid = 0;
                if (int.TryParse(AlbumID.Text, out albumid))
                {
                    MessageUserControl2.TryRun(() =>
                    {
                        Album updatealbum = new Album();
                        updatealbum.AlbumId = albumid;
                        updatealbum.Title = AlbumTitle.Text;
                        updatealbum.ArtistId = int.Parse(ArtistList.SelectedValue);
                        updatealbum.ReleaseYear = int.Parse(AlbumReleaseYear.Text);
                        updatealbum.ReleaseLabel = string.IsNullOrEmpty(AlbumReleaseLabel.Text) ? null : AlbumReleaseLabel.Text;
                        AlbumController sysmgr = new AlbumController();
                        sysmgr.Albums_Update(updatealbum);
                    }, "Update Album", "Album has been successfully updated on the database.");
                }
                else
                {
                    MessageUserControl2.ShowInfo("Invalid data", "Album Id. Use Find to locate the album you wish to maintain.");
                }
            }
        }
    }
    protected void DeleteAlbum_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(AlbumID.Text))
        {
            MessageUserControl2.ShowInfo("Missing data", "Missing Album Id. Use Find to locate the album you wish to maintain.");
        }
        else
        {
            int albumid = 0;
            if (int.TryParse(AlbumID.Text, out albumid))
            {
                MessageUserControl2.TryRun(() =>
                {
                    AlbumController sysmgr = new AlbumController();
                    sysmgr.Album_Delete(albumid);
                }, "Delete Album", "Album has been successfully removed from the database.");
            }
            else
            {
                MessageUserControl2.ShowInfo("Invalid data", "Album Id. Use Find to locate the album you wish to maintain.");
            }
        }
    }
    protected void Clear_Click(object sender, EventArgs e)
    {
        AlbumID.Text = "";
        AlbumTitle.Text = "";
        AlbumReleaseYear.Text = "";
        AlbumReleaseLabel.Text = "";
        ArtistList.SelectedIndex = 0;
    }


}