using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using Chinook.Data.Enitities;
using Chinook.Data.DTOs;
using Chinook.Data.POCOs;
using ChinookSystem.DAL;
using System.ComponentModel;
#endregion

namespace ChinookSystem.BLL
{
    [DataObject]
    public class TrackController
    {
        #region Business Processes
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<TrackList> Get_TracksForPlaylistSelection(int id, string fetchby)
        {
            List<TrackList> results = null;
            using (var context = new ChinookContext())
            {
                switch (fetchby)
                {
                    case "Artist":
                        {
                            results = (from x in context.Tracks
                                       where x.Album.ArtistId == id
                                       select new TrackList
                                       {
                                           TrackId = x.TrackId,
                                           Name = x.Name,
                                           Title = x.Album.Title,
                                           MediaName = x.MediaType.Name,
                                           GenreName = x.Genre.Name,
                                           Composer = x.Composer,
                                           Milliseconds = x.Milliseconds,
                                           Bytes = x.Bytes,
                                           UnitPrice = x.UnitPrice
                                       }).ToList();
                            break;
                        }
                    case "Media":
                        {
                            results = (from x in context.Tracks
                                       where x.MediaType.MediaTypeId == id
                                       select new TrackList
                                       {
                                           TrackId = x.TrackId,
                                           Name = x.Name,
                                           Title = x.Album.Title,
                                           MediaName = x.MediaType.Name,
                                           GenreName = x.Genre.Name,
                                           Composer = x.Composer,
                                           Milliseconds = x.Milliseconds,
                                           Bytes = x.Bytes,
                                           UnitPrice = x.UnitPrice
                                       }).ToList();
                            break;
                        }
                    case "Genre":
                        {
                            results = (from x in context.Tracks
                                       where x.Genre.GenreId == id
                                       select new TrackList
                                       {
                                           TrackId = x.TrackId,
                                           Name = x.Name,
                                           Title = x.Album.Title,
                                           MediaName = x.MediaType.Name,
                                           GenreName = x.Genre.Name,
                                           Composer = x.Composer,
                                           Milliseconds = x.Milliseconds,
                                           Bytes = x.Bytes,
                                           UnitPrice = x.UnitPrice
                                       }).ToList();
                            break;
                        }
                    default:
                        {
                            results = (from x in context.Tracks
                                       where x.Album.AlbumId == id
                                       select new TrackList
                                       {
                                           TrackId = x.TrackId,
                                           Name = x.Name,
                                           Title = x.Album.Title,
                                           MediaName = x.MediaType.Name,
                                           GenreName = x.Genre.Name,
                                           Composer = x.Composer,
                                           Milliseconds = x.Milliseconds,
                                           Bytes = x.Bytes,
                                           UnitPrice = x.UnitPrice
                                       }).ToList();
                            break;
                        }
                }
            }
            return results;
        }
        #endregion
    }
}
