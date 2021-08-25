using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayer.model
{
    public class MusicInfo
    {
        private string id;
        private String name;
        private String title;
        private String code;
        private String playlist_id;
        private String artists_names;
        private String performer;
        private String type;
        private String link;
        private String lyric;
        private String thumbnail;
        private String thumbnailRank;
        private int position;
        private string duration;

        public string Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Title { get => title; set => title = value; }
        public string Code { get => code; set => code = value; }
        public string Playlist_id { get => playlist_id; set => playlist_id = value; }
        public string Artists_names { get => artists_names; set => artists_names = value; }
        public string Performer { get => performer; set => performer = value; }
        public string Type { get => type; set => type = value; }
        public string Link { get => link; set => link = value; }
        public string Lyric { get => lyric; set => lyric = value; }
        public string Thumbnail { get => thumbnail; set => thumbnail = value; }
        public int Position { get => position; set => position = value; }
        public string Duration { get => duration; set => duration = value; }
        public string ThumbnailRank { get => thumbnailRank; set => thumbnailRank = value; }
    }
    
}
