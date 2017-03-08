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
    public class PlaylistTrackController
    {
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<UserPlaylistTrack> List_TracksForPlaylist(string playlistname, string username)
        {
            using (var context = new ChinookContext())
            {
                var results = from x in context.PlaylistTracks
                              where x.Playlist.Name.Equals(playlistname)
                              && x.Playlist.UserName == username
                              orderby x.TrackNumber
                              select new UserPlaylistTrack
                              {
                                  TrackId = x.TrackId,
                                  TrackNumber = x.TrackNumber,
                                  Name = x.Track.Name,
                                  Title = x.Track.Album.Title,
                                  Milliseconds = x.Track.Milliseconds,
                                  UnitPrice = x.Track.UnitPrice,
                                  Purchased = true
                              };
                return results.ToList();

            }
        }//eom
        public void Add_TrackToPlaylist(string playlistname, string username, int trackid)
        {            /*
            two approaches:
            one:
             wrap using context inside a TransactionScope. this alows for multiple
             SaveChanges() within code. Note: SaveChanges() internally has its own
             TransactionScope. One can nest TransactionsScopes.
             requires reference setting System.TransactionScope and using System.TransactionScope

            commented code is this approach

            two:
            Add a HashSet<T> to any entity that has ICollection<T> see Playlist entity
            This HashSet<T> works with EntityFramework to internally create all entries
            to the database and the need for generated values (identity).

            All work of adding or updating is done at one time (SaveChanges()). Logically
            you "assume" that the identity value is known when you do your coding.

            The adding of ICollection<T> records is done using a navigatitional approach.

                parent.NavigationProperty.Add(childentity)

            */
            //using (TransactionScope scope = new TransactionScope())
            //{ 
            using (var context = new ChinookContext())
            {
                //if user has multiple playlists of the same name return the first occurance
                Playlist exists = (from x in context.Playlists
                                   where x.UserName.Equals(username, StringComparison.OrdinalIgnoreCase)
                                     && x.Name.Equals(playlistname, StringComparison.OrdinalIgnoreCase)
                                   select x).FirstOrDefault();
                PlaylistTrack newTrack = null;
                int tracknumber = 0;
                if (exists == null)
                {
                    //create a new playlist
                    exists = new Playlist();
                    exists.Name = playlistname;
                    exists.UserName = username;
                    exists = context.Playlists.Add(exists);
                    tracknumber = 1;
                }
                else
                {
                    //find the next logical tracknumber 
                    tracknumber = exists.PlaylistTracks.Count() + 1;
                    //see if the track is already on the list (no duplicate tracks in a playlist)
                    newTrack = exists.PlaylistTracks.SingleOrDefault(x => x.TrackId == trackid);
                    if (newTrack != null)
                    {
                        throw new Exception("Playlist already has requested track.");
                    }
                }

                //for testing
                //    if (playlistname.Equals("Boom"))
                //    {
                //        throw new Exception("Playlist test rollback.");
                //    }

                newTrack = new PlaylistTrack();

                //newTrack.PlaylistId = exists.PlaylistId;
                //newTrack.TrackId = trackid;
                //newTrack.TrackNumber = tracknumber;
                //context.PlaylistTracks.Add(newTrack);

                
                newTrack.TrackId = trackid;
                newTrack.TrackNumber = tracknumber;
                exists.PlaylistTracks.Add(newTrack);

                context.SaveChanges();
            }
            // scope.Complete();
            //}
        }//eom
    }
}
