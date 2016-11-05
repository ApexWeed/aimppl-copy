using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Apex;
using Apex.Translation;
using Apex.Translation.Controls;

namespace AIMPPL_Copy
{
    public partial class StatisticsForm : Form
    {
        new private MainForm Parent;
        private List<Playlist> Playlists;
        private LanguageManager LM;
        private Dictionary<TranslatableLabelFormat, string> translationStrings;

        public StatisticsForm(LanguageManager LanguageManager, List<Playlist> Playlists, MainForm Parent)
        {
            InitializeComponent();

            this.Playlists = Playlists;
            this.Parent = Parent;
            this.LM = LanguageManager;
            LM.AddAllControls(this);
        }

        private async void StatisticsForm_Load(object sender, EventArgs e)
        {
            // Save the control translation string and set it to calculating.
            translationStrings = new Dictionary<TranslatableLabelFormat, string>();

            var bindFlags = BindingFlags.Instance | BindingFlags.Public;
            var searchQueue = new Queue<Control>();
            foreach (Control control in Controls)
            {
                searchQueue.Enqueue(control);
            }

            while (searchQueue.Count > 0)
            {
                var component = searchQueue.Dequeue();
                if (component is TranslatableLabelFormat)
                {
                    var label = component as TranslatableLabelFormat;
                    translationStrings.Add(label, label.TranslationString);
                    label.TranslationString = "STATS.LABEL.CALCULATING";
                }
                else
                {
                    var props = component.GetType().GetProperties(bindFlags);
                    foreach (var prop in props)
                    {
                        if (prop.Name == "Controls")
                        {
                            var children = (Control.ControlCollection)prop.GetValue(component);
                            foreach (Control child in children)
                            {
                                searchQueue.Enqueue(child);
                            }
                        }
                    }
                }
            }

            var groups = new List<Group>();
            var uniqueGroups = new List<Group>();
            var songs = new List<Song>();
            var uniqueSongs = new List<Song>();
            await Task.Run(() =>
            {
                foreach (var playlist in Playlists)
                {
                    groups.AddRange(playlist.Groups);
                }
                uniqueGroups.AddRange(groups.Distinct());

                foreach (var group in groups)
                {
                    songs.AddRange(group.Songs);
                }
                uniqueSongs.AddRange(songs.Distinct());
            });

            // Playlists.
            lblPlaylistsCount.Parameters = new object[] { Playlists.Count };
            lblPlaylistsCount.TranslationString = translationStrings[lblPlaylistsCount];

            var songCount = 0;
            await Task.Run(() =>
            {
                foreach (var group in groups)
                {
                    songCount += group.Songs.Count;
                }
            });
            lblPlaylistsSongAverage.Parameters = new object[] { songCount / Playlists.Count };
            lblPlaylistsSongAverage.TranslationString = translationStrings[lblPlaylistsSongAverage];

            var totalCoverSize = 0L;
            await Task.Run(() =>
            {
                foreach (var group in groups)
                {
                    totalCoverSize += group.Cover.Size;
                }
            });
            lblPlaylistsTotalCovers.Parameters = new object[] { Formatting.FormatBytes(totalCoverSize) };
            lblPlaylistsTotalCovers.TranslationString = translationStrings[lblPlaylistsTotalCovers];

            lblPlaylistsAverageCovers.Parameters = new object[] { Formatting.FormatBytes(totalCoverSize / Playlists.Count) };
            lblPlaylistsAverageCovers.TranslationString = translationStrings[lblPlaylistsAverageCovers];

            var totalScanSize = 0L;
            await Task.Run(() =>
            {
                foreach (var group in groups)
                {
                    totalScanSize += group.ScanSize;
                }
            });
            lblPlaylistsTotalScans.Parameters = new object[] { Formatting.FormatBytes(totalScanSize) };
            lblPlaylistsTotalScans.TranslationString = translationStrings[lblPlaylistsTotalScans];

            lblPlaylistsAverageScans.Parameters = new object[] { Formatting.FormatBytes(totalScanSize / Playlists.Count) };
            lblPlaylistsAverageScans.TranslationString = translationStrings[lblPlaylistsAverageScans];

            // Groups.
            lblGroupsCount.Parameters = new object[] { groups.Count };
            lblGroupsCount.TranslationString = translationStrings[lblGroupsCount];

            lblGroupsAverageCount.Parameters = new object[] { groups.Count / Playlists.Count };
            lblGroupsAverageCount.TranslationString = translationStrings[lblGroupsAverageCount];

            lblGroupsAverageSongs.Parameters = new object[] { songCount / groups.Count };
            lblGroupsAverageSongs.TranslationString = translationStrings[lblGroupsAverageSongs];

            lblGroupsAverageCovers.Parameters = new object[] { Formatting.FormatBytes(totalCoverSize / groups.Count) };
            lblGroupsAverageCovers.TranslationString = translationStrings[lblGroupsAverageCovers];

            lblGroupsAverageScans.Parameters = new object[] { Formatting.FormatBytes(totalScanSize / groups.Count) };
            lblGroupsAverageScans.TranslationString = translationStrings[lblGroupsAverageScans];

            // Unique groups.
            lblUniqueGroupsCount.Parameters = new object[] { uniqueGroups.Count };
            lblUniqueGroupsCount.TranslationString = translationStrings[lblUniqueGroupsCount];

            lblUniqueGroupsAverageCount.Parameters = new object[] { uniqueGroups.Count / Playlists.Count };
            lblUniqueGroupsAverageCount.TranslationString = translationStrings[lblUniqueGroupsAverageCount];

            lblUniqueGroupsAverageSongs.Parameters = new object[] { songCount / uniqueGroups.Count };
            lblUniqueGroupsAverageSongs.TranslationString = translationStrings[lblGroupsAverageSongs];

            lblUniqueGroupsAverageCovers.Parameters = new object[] { Formatting.FormatBytes(totalCoverSize / uniqueGroups.Count) };
            lblUniqueGroupsAverageCovers.TranslationString = translationStrings[lblUniqueGroupsAverageCovers];

            lblUniqueGroupsAverageScans.Parameters = new object[] { Formatting.FormatBytes(totalScanSize / uniqueGroups.Count) };
            lblUniqueGroupsAverageScans.TranslationString = translationStrings[lblUniqueGroupsAverageScans];

            // Songs.
            lblSongsCount.Parameters = new object[] { songs.Count };
            lblSongsCount.TranslationString = translationStrings[lblSongsCount];

            var totalDuration = 0;
            await Task.Run(() =>
            {
                foreach (var song in songs)
                {
                    totalDuration += song.Duration;
                }
            });
            lblSongsTotalDuration.Parameters = new object[] { TimeSpan.FromMilliseconds(totalDuration) };
            lblSongsTotalDuration.TranslationString = translationStrings[lblSongsTotalDuration];

            lblSongsAverageDuration.Parameters = new object[] { TimeSpan.FromMilliseconds(totalDuration / songs.Count) };
            lblSongsAverageDuration.TranslationString = translationStrings[lblSongsAverageDuration];

            var totalSongSize = 0L;
            await Task.Run(() =>
            {
                foreach (var song in songs)
                {
                    totalSongSize += song.Size;
                }
            });
            lblSongsTotalSize.Parameters = new object[] { Formatting.FormatBytes(totalSongSize) };
            lblSongsTotalSize.TranslationString = translationStrings[lblSongsTotalSize];

            lblSongsAverageSize.Parameters = new object[] { Formatting.FormatBytes(totalSongSize / songs.Count) };
            lblSongsAverageSize.TranslationString = translationStrings[lblSongsAverageSize];

            // Unique Songs
            lblUniqueSongsCount.Parameters = new object[] { uniqueSongs.Count };
            lblUniqueSongsCount.TranslationString = translationStrings[lblUniqueSongsCount];

            var totalUniqueDuration = 0;
            await Task.Run(() =>
            {
                foreach (var song in uniqueSongs)
                {
                    totalUniqueDuration += song.Duration;
                }
            });
            lblUniqueSongsTotalDuration.Parameters = new object[] { TimeSpan.FromMilliseconds(totalUniqueDuration) };
            lblUniqueSongsTotalDuration.TranslationString = translationStrings[lblUniqueSongsTotalDuration];

            lblUniqueSongsAverageDuration.Parameters = new object[] { TimeSpan.FromMilliseconds(totalUniqueDuration / uniqueSongs.Count) };
            lblUniqueSongsAverageDuration.TranslationString = translationStrings[lblUniqueSongsAverageDuration];

            var totalUniqueSize = 0L;
            await Task.Run(() =>
            {
                foreach (var song in uniqueSongs)
                {
                    totalUniqueSize += song.Size;
                }
            });
            lblUniqueSongsTotalSize.Parameters = new object[] { Formatting.FormatBytes(totalUniqueSize) };
            lblUniqueSongsTotalSize.TranslationString = translationStrings[lblUniqueSongsTotalSize];

            lblUniqueSongsAverageSize.Parameters = new object[] { Formatting.FormatBytes(totalUniqueSize / uniqueSongs.Count) };
            lblUniqueSongsAverageSize.TranslationString = translationStrings[lblUniqueSongsAverageSize];
        }

        private void StatisticsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Parent.ChildClosed(this);
        }
    }
}
