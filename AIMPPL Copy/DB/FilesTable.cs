using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;

namespace AIMPPL_Copy.DB
{
    public class FilesTable : DataTable, IEnumerable<FilesRow>
    {
        public enum CommitMode
        {
            Insert,
            Update,
            Delete,
            Upsert,
            All
        }

        public FilesTable()
        {
            Columns.Add("ID", typeof(int));
            Columns.Add("DiskLetter", typeof(string));
            Columns.Add("DiskSerialNumber", typeof(long));
            Columns.Add("FileFormat", typeof(int));
            Columns.Add("FileSize", typeof(long));
            Columns.Add("FileName", typeof(string));
            Columns.Add("Bitrate", typeof(int));
            Columns.Add("Channels", typeof(int));
            Columns.Add("Duration", typeof(double));
            Columns.Add("SampleRate", typeof(int));
            Columns.Add("TrackNumber", typeof(string));
            Columns.Add("DiskNumber", typeof(string));
            Columns.Add("Title", typeof(string));
            Columns.Add("AlbumID", typeof(string));
            Columns.Add("AlbumArtistID", typeof(string));
            Columns.Add("ArtistID", typeof(string));
            Columns.Add("ComposerID", typeof(string));
            Columns.Add("CopyrightsID", typeof(int));
            Columns.Add("PublisherID", typeof(string));
            Columns.Add("URLID", typeof(int));
            Columns.Add("GenreID", typeof(string));
            Columns.Add("YearID", typeof(int));
            Columns.Add("BPM", typeof(int));
            Columns.Add("LabelsID", typeof(string));
            Columns.Add("UserMark", typeof(int));
            Columns.Add("Added", typeof(double));
            Columns.Add("PlaybackCount", typeof(int));
            Columns.Add("RatingRawScore", typeof(double));
            Columns.Add("LastPlayback", typeof(double));
            Columns.Add("LastModification", typeof(double));
        }

        public FilesRow this[int idx] => (FilesRow)Rows[idx];

        public void Add(FilesRow row)
        {
            Rows.Add(row);
        }

        public void Remove(FilesRow row)
        {
            Rows.Remove(row);
        }

        public new FilesRow NewRow() => (FilesRow)base.NewRow();

        public void Commit(SQLiteConnection conn, CommitMode mode = CommitMode.All)
        {
            using (var trans = conn.BeginTransaction())
            {
                var command = conn.CreateCommand();

                var parId = command.Parameters.Add("@ID", DbType.Int32);
                var parDiskLetter = command.Parameters.Add("@DiskLetter", DbType.StringFixedLength, 1);
                var parDiskSerialNumber = command.Parameters.Add("@DiskSerialNumber", DbType.Int64);
                var parFileFormat = command.Parameters.Add("@FileFormat", DbType.Int32);
                var parFileSize = command.Parameters.Add("@FileSize", DbType.Int64);
                var parFileName = command.Parameters.Add("@FileName", DbType.String);
                var parBitrate = command.Parameters.Add("@Bitrate", DbType.Int32);
                var parChannels = command.Parameters.Add("@Channels", DbType.Int32);
                var parDuration = command.Parameters.Add("@Duration", DbType.Double);
                var parSampleRate = command.Parameters.Add("@SampleRate", DbType.Int32);
                var parTrackNumber = command.Parameters.Add("@TrackNumber", DbType.String);
                var parDiskNumber = command.Parameters.Add("@DiskNumber", DbType.String);
                var parTitle = command.Parameters.Add("@Title", DbType.String);
                var parAlbumId = command.Parameters.Add("@AlbumID", DbType.String);
                var parAlbumArtistId = command.Parameters.Add("@AlbumArtistID", DbType.String);
                var parArtistId = command.Parameters.Add("@ArtistID", DbType.String);
                var parComposerId = command.Parameters.Add("@ComposerID", DbType.String);
                var parCopyrightsId = command.Parameters.Add("@CopyrightsID", DbType.Int32);
                var parPublisherId = command.Parameters.Add("@PublisherID", DbType.String);
                var parUrlid = command.Parameters.Add("@URLID", DbType.Int32);
                var parGenreId = command.Parameters.Add("@GenreID", DbType.String);
                var parYearId = command.Parameters.Add("@YearID", DbType.Int32);
                var parBpm = command.Parameters.Add("@BPM", DbType.Int32);
                var parLabelsId = command.Parameters.Add("@LabelsID", DbType.String);
                var parUserMark = command.Parameters.Add("@UserMark", DbType.Int32);
                var parAdded = command.Parameters.Add("@Added", DbType.Double);
                var parPlaybackCount = command.Parameters.Add("@PlaybackCount", DbType.Int32);
                var parRatingRawScore = command.Parameters.Add("@RatingRawScore", DbType.Double);
                var parLastPlayback = command.Parameters.Add("@LastPlayback", DbType.Double);
                var parLastModification = command.Parameters.Add("@LastModification", DbType.Double);


                if (mode == CommitMode.All || mode == CommitMode.Delete)
                {
                    var toDelete = Rows.Cast<FilesRow>().Where(row => row.RowState == DataRowState.Deleted);
                    if (toDelete.Any())
                        throw new NotImplementedException("Ayy deletes ain't happening");
                }

                if (mode == CommitMode.All || mode == CommitMode.Upsert || mode == CommitMode.Insert)
                {
                    var toInsert = Rows.Cast<FilesRow>().Where(row => row.RowState == DataRowState.Added);
                    if (toInsert.Any())
                        throw new NotImplementedException("Ayy inserts ain't happening");
                }

                if (mode == CommitMode.All || mode == CommitMode.Upsert || mode == CommitMode.Update)
                {
                    var toUpdate = Rows.Cast<FilesRow>().Where(row => row.RowState == DataRowState.Modified);
                    if (toUpdate.Any())
                    {
                        command.CommandText = @"UPDATE TableFiles
                                                SET DiskLetter = @DiskLetter,
                                                DiskSerialNumber = @DiskSerialNumber,
                                                FileFormat = @FileFormat,
                                                FileSize = @FileSize,
                                                FileName = @FileName,
                                                Bitrate = @Bitrate,
                                                Channels = @Channels,
                                                Duration = @Duration,
                                                SampleRate = @SampleRate,
                                                TrackNumber = @TrackNumber,
                                                DiskNumber = @DiskNumber,
                                                Title = @Title,
                                                AlbumID = @AlbumID,
                                                AlbumArtistID = @AlbumArtistID,
                                                ArtistID = @ArtistID,
                                                ComposerID = @ComposerID,
                                                CopyrightsID = @CopyrightsID,
                                                PublisherID = @PublisherID,
                                                URLID = @URLID,
                                                GenreID = @GenreID,
                                                YearID = @YearID,
                                                BPM = @BPM,
                                                LabelsID = @LabelsID,
                                                UserMark = @UserMark,
                                                Added = @Added,
                                                PlaybackCount = @PlaybackCount,
                                                RatingRawScore = @RatingRawScore,
                                                LastPlayback = @LastPlayback,
                                                LastModification = @LastModification
                                                WHERE ID = @ID;";
                        foreach (var row in toUpdate)
                        {
                            parId.Value = row["ID"];
                            parDiskLetter.Value = row["DiskLetter"];
                            parDiskSerialNumber.Value = row["DiskSerialNumber"];
                            parFileFormat.Value = row["FileFormat"];
                            parFileSize.Value = row["FileSize"];
                            parFileName.Value = row["FileName"];
                            parBitrate.Value = row["Bitrate"];
                            parChannels.Value = row["Channels"];
                            parDuration.Value = row["Duration"];
                            parSampleRate.Value = row["SampleRate"];
                            parTrackNumber.Value = row["TrackNumber"];
                            parDiskNumber.Value = row["DiskNumber"];
                            parTitle.Value = row["Title"];
                            parAlbumId.Value = row["AlbumID"];
                            parAlbumArtistId.Value = row["AlbumArtistID"];
                            parArtistId.Value = row["ArtistID"];
                            parComposerId.Value = row["ComposerID"];
                            parCopyrightsId.Value = row["CopyrightsID"];
                            parPublisherId.Value = row["PublisherID"];
                            parUrlid.Value = row["URLID"];
                            parGenreId.Value = row["GenreID"];
                            parYearId.Value = row["YearID"];
                            parBpm.Value = row["BPM"];
                            parLabelsId.Value = row["LabelsID"];
                            parUserMark.Value = row["UserMark"];
                            parAdded.Value = row["Added"];
                            parPlaybackCount.Value = row["PlaybackCount"];
                            parRatingRawScore.Value = row["RatingRawScore"];
                            parLastPlayback.Value = row["LastPlayback"];
                            parLastModification.Value = row["LastModification"];

                            command.ExecuteNonQuery();
                        }
                    }
                }
                trans.Commit();
            }
        }

        protected override Type GetRowType()
        {
            return typeof(FilesRow);
        }

        protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
        {
            return new FilesRow(builder);
        }

        public IEnumerator<FilesRow> GetEnumerator()
        {
            foreach (var row in Rows.Cast<FilesRow>())
                yield return row;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
