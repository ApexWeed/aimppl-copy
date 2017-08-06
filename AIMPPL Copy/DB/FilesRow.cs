using System;
using System.CodeDom;
using System.Data;
using System.Runtime.Remoting.Messaging;

namespace AIMPPL_Copy.DB
{
    public class FilesRow : DataRow
    {
        internal FilesRow(DataRowBuilder builder) : base(builder)
        {
            
        }

        public int Id => (int)base["ID"];

        public char DiskLetter
        {
            get => (char)byte.Parse((string)base["DiskLetter"]);
            set => base["DiskLetter"] = ((byte)value).ToString();
        }

        public long DiskSerialNumber
        {
            get => (long)base["DiskSerialNumber"];
            set => base["DiskSerialNumber"] = value;
        }

        public int FileFormat
        {
            get => (int)base["FileFormat"];
            set => base["FileFormat"] = value;
        }

        public long FileSize
        {
            get => (long)base["FileSize"];
            set => base["FileSize"] = value;
        }

        public string FileName
        {
            get => (string)base["FileName"];
            set => base["FileName"] = value;
        }

        public string FilePath
        {
            get => $"{DiskLetter}{FileName}";
            set
            {
                DiskLetter = value[0];
                FileName = value.Substring(1);
            }
        }

        public int Bitrate
        {
            get => (int)base["Bitrate"];
            set => base["Bitrate"] = value;
        }

        public int Channels
        {
            get => (int)base["Channels"];
            set => base["Channels"] = value;
        }

        public double Duration
        {
            get => (double)base["Duration"];
            set => base["Duration"] = value;
        }

        public int SampleRate
        {
            get => (int)base["SampleRate"];
            set => base["SampleRate"] = value;
        }

        public string TrackNumber
        {
            get => (string)base["TrackNumber"];
            set => base["TrackNumber"] = value;
        }

        public string DiskNumber
        {
            get => (string)base["DiskNumber"];
            set => base["DiskNumber"] = value;
        }

        public string Title
        {
            get => (string)base["Title"];
            set => base["Title"] = value;
        }

        public string AlbumId
        {
            get => (string)base["AlbumID"];
            set => base["AlbumID"] = value;
        }

        public string AlbumArtistId
        {
            get => (string)base["AlbumArtistID"];
            set => base["AlbumArtistID"] = value;
        }

        public string ArtistId
        {
            get => (string)base["ArtistID"];
            set => base["ArtistID"] = value;
        }

        public string ComposerId
        {
            get => (string)base["ComposerID"];
            set => base["ComposerID"] = value;
        }

        public int CopyrightsId
        {
            get => (int)base["CopyrightsID"];
            set => base["CopyrightsID"] = value.ToString();
        }

        public string PublisherId
        {
            get => (string)base["PublisherID"];
            set => base["PublisherID"] = value;
        }

        public int Urlid
        {
            get => (int)base["URLID"];
            set => base["URLID"] = value;
        }

        public string GenreId
        {
            get => (string)base["GenreID"];
            set => base["GenreID"] = value;
        }

        public int YearId
        {
            get => (int)base["YearID"];
            set => base["YearID"] = value;
        }

        public int Bpm
        {
            get => (int)base["BPM"];
            set => base["BPM"] = value;
        }

        public string LabelsId
        {
            get => (string)base["LabelsID"];
            set => base["LabelsID"] = value;
        }

        public int UserMark
        {
            get => (int)base["UserMark"];
            set => base["UserMark"] = value;
        }

        public DateTime Added
        {
            get => DateTime.FromOADate((double)base["Added"]);
            set => base["Added"] = value.ToOADate();
        }

        public int PlaybackCount
        {
            get => (int)base["PlaybackCount"];
            set => base["PlaybackCount"] = value;
        }

        public double RatingRawScore
        {
            get => base["RatingRawScore"].IsNullOrDBNull() ? 0D : (double)base["RatingRawScore"];
            set => base["RatingRawScore"] = value;
        }

        public DateTime LastPlayback
        {
            get => DateTime.FromOADate((double)base["LastPlayback"]);
            set => base["LastPlayback"] = value.ToOADate();
        }

        public DateTime LastModification
        {
            get => DateTime.FromOADate((double)base["LastModification"]);
            set => base["LastModification"] = value.ToOADate();
        }
    }
}
