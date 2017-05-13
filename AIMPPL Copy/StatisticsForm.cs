using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using Apex;
using Apex.Translation;
using Apex.Translation.Controls;

namespace AIMPPL_Copy
{
    public partial class StatisticsForm : Form
    {
        private readonly LanguageManager _lm;
        private readonly MainForm _parent;
        private readonly List<Playlist> _playlists;
        private Dictionary<TranslatableLabelFormat, string> _translationStrings;

        public StatisticsForm(LanguageManager languageManager, List<Playlist> playlists, MainForm parent)
        {
            InitializeComponent();

            _playlists = playlists;
            _parent = parent;
            _lm = languageManager;
            _lm.AddAllControls(this);
        }

        private async void StatisticsForm_Load(object sender, EventArgs e)
        {
            // Save the control translation string and set it to calculating.
            _translationStrings = new Dictionary<TranslatableLabelFormat, string>();

            var bindFlags = BindingFlags.Instance | BindingFlags.Public;
            var searchQueue = new Queue<Control>();
            foreach (Control control in Controls)
                searchQueue.Enqueue(control);

            while (searchQueue.Count > 0)
            {
                var component = searchQueue.Dequeue();
                if (component is TranslatableLabelFormat)
                {
                    var label = component as TranslatableLabelFormat;
                    _translationStrings.Add(label, label.TranslationString);
                    label.TranslationString = "STATS.LABEL.CALCULATING";
                }
                else
                {
                    var props = component.GetType().GetProperties(bindFlags);
                    foreach (var prop in props)
                        if (prop.Name == "Controls")
                        {
                            var children = (Control.ControlCollection)prop.GetValue(component);
                            foreach (Control child in children)
                                searchQueue.Enqueue(child);
                        }
                }
            }

            var groups = new List<Group>();
            var uniqueGroups = new List<Group>();
            var songs = new List<Song>();
            var uniqueSongs = new List<Song>();
            await Task.Run(() =>
            {
                foreach (var playlist in _playlists)
                    groups.AddRange(playlist.Groups);
                uniqueGroups.AddRange(groups.Distinct());

                foreach (var group in groups)
                    songs.AddRange(group.Songs);
                uniqueSongs.AddRange(songs.Distinct());
            });

            // Playlists.
            lblPlaylistsCount.Parameters = new object[] {_playlists.Count};
            lblPlaylistsCount.TranslationString = _translationStrings[lblPlaylistsCount];

            var songCount = 0;
            await Task.Run(() =>
            {
                var _lock = new object();
                Parallel.ForEach(groups, group =>
                {
                    var count = group.Songs.Count;
                    lock (_lock)
                        songCount += count;
                });
            });
            lblPlaylistsSongAverage.Parameters = new object[] {songCount / _playlists.Count};
            lblPlaylistsSongAverage.TranslationString = _translationStrings[lblPlaylistsSongAverage];

            var totalCoverSize = 0L;
            await Task.Run(() =>
            {
                var _lock = new object();
                Parallel.ForEach(groups, group =>
                {
                    var coverSize = group.Cover.Size;
                    lock (_lock)
                        totalCoverSize += coverSize;
                });
            });
            lblPlaylistsTotalCovers.Parameters = new object[] {Formatting.FormatBytes(totalCoverSize)};
            lblPlaylistsTotalCovers.TranslationString = _translationStrings[lblPlaylistsTotalCovers];

            lblPlaylistsAverageCovers.Parameters = new object[]
                {Formatting.FormatBytes(totalCoverSize / _playlists.Count)};
            lblPlaylistsAverageCovers.TranslationString = _translationStrings[lblPlaylistsAverageCovers];

            var totalScanSize = 0L;
            await Task.Run(() =>
            {
                var _lock = new object();
                Parallel.ForEach(groups, group =>
                {
                    var scanSize = group.ScanSize;
                    lock (_lock)
                        totalScanSize += scanSize;
                });
            });
            lblPlaylistsTotalScans.Parameters = new object[] {Formatting.FormatBytes(totalScanSize)};
            lblPlaylistsTotalScans.TranslationString = _translationStrings[lblPlaylistsTotalScans];

            lblPlaylistsAverageScans.Parameters = new object[]
                {Formatting.FormatBytes(totalScanSize / _playlists.Count)};
            lblPlaylistsAverageScans.TranslationString = _translationStrings[lblPlaylistsAverageScans];

            // Groups.
            lblGroupsCount.Parameters = new object[] {groups.Count};
            lblGroupsCount.TranslationString = _translationStrings[lblGroupsCount];

            lblGroupsAverageCount.Parameters = new object[] {groups.Count / _playlists.Count};
            lblGroupsAverageCount.TranslationString = _translationStrings[lblGroupsAverageCount];

            lblGroupsAverageSongs.Parameters = new object[] {songCount / groups.Count};
            lblGroupsAverageSongs.TranslationString = _translationStrings[lblGroupsAverageSongs];

            lblGroupsAverageCovers.Parameters = new object[] {Formatting.FormatBytes(totalCoverSize / groups.Count)};
            lblGroupsAverageCovers.TranslationString = _translationStrings[lblGroupsAverageCovers];

            lblGroupsAverageScans.Parameters = new object[] {Formatting.FormatBytes(totalScanSize / groups.Count)};
            lblGroupsAverageScans.TranslationString = _translationStrings[lblGroupsAverageScans];

            // Unique groups.
            lblUniqueGroupsCount.Parameters = new object[] {uniqueGroups.Count};
            lblUniqueGroupsCount.TranslationString = _translationStrings[lblUniqueGroupsCount];

            lblUniqueGroupsAverageCount.Parameters = new object[] {uniqueGroups.Count / _playlists.Count};
            lblUniqueGroupsAverageCount.TranslationString = _translationStrings[lblUniqueGroupsAverageCount];

            lblUniqueGroupsAverageSongs.Parameters = new object[] {songCount / uniqueGroups.Count};
            lblUniqueGroupsAverageSongs.TranslationString = _translationStrings[lblGroupsAverageSongs];

            lblUniqueGroupsAverageCovers.Parameters = new object[]
                {Formatting.FormatBytes(totalCoverSize / uniqueGroups.Count)};
            lblUniqueGroupsAverageCovers.TranslationString = _translationStrings[lblUniqueGroupsAverageCovers];

            lblUniqueGroupsAverageScans.Parameters = new object[]
                {Formatting.FormatBytes(totalScanSize / uniqueGroups.Count)};
            lblUniqueGroupsAverageScans.TranslationString = _translationStrings[lblUniqueGroupsAverageScans];

            // Songs.
            lblSongsCount.Parameters = new object[] {songs.Count};
            lblSongsCount.TranslationString = _translationStrings[lblSongsCount];

            var totalDuration = 0;
            await Task.Run(() =>
            {
                var _lock = new object();
                Parallel.ForEach(songs, song =>
                {
                    var duration = song.Duration;
                    lock (_lock)
                        totalDuration += duration;
                });
            });
            lblSongsTotalDuration.Parameters = new object[] {TimeSpan.FromMilliseconds(totalDuration)};
            lblSongsTotalDuration.TranslationString = _translationStrings[lblSongsTotalDuration];

            lblSongsAverageDuration.Parameters = new object[] {TimeSpan.FromMilliseconds(totalDuration / songs.Count)};
            lblSongsAverageDuration.TranslationString = _translationStrings[lblSongsAverageDuration];

            var totalSongSize = 0L;
            await Task.Run(() =>
            {
                var _lock = new object();
                Parallel.ForEach(songs, song =>
                {
                    var size = song.Size;
                    lock (_lock)
                        totalSongSize += size;
                });
            });
            lblSongsTotalSize.Parameters = new object[] {Formatting.FormatBytes(totalSongSize)};
            lblSongsTotalSize.TranslationString = _translationStrings[lblSongsTotalSize];

            lblSongsAverageSize.Parameters = new object[] {Formatting.FormatBytes(totalSongSize / songs.Count)};
            lblSongsAverageSize.TranslationString = _translationStrings[lblSongsAverageSize];

            // Unique Songs
            lblUniqueSongsCount.Parameters = new object[] {uniqueSongs.Count};
            lblUniqueSongsCount.TranslationString = _translationStrings[lblUniqueSongsCount];

            var totalUniqueDuration = 0;
            await Task.Run(() =>
            {
                var _lock = new object();
                Parallel.ForEach(uniqueSongs, song =>
                {
                    var duration = song.Duration;
                    lock (_lock)
                        totalUniqueDuration += duration;
                });
            });
            lblUniqueSongsTotalDuration.Parameters = new object[] {TimeSpan.FromMilliseconds(totalUniqueDuration)};
            lblUniqueSongsTotalDuration.TranslationString = _translationStrings[lblUniqueSongsTotalDuration];

            lblUniqueSongsAverageDuration.Parameters = new object[]
                {TimeSpan.FromMilliseconds(totalUniqueDuration / uniqueSongs.Count)};
            lblUniqueSongsAverageDuration.TranslationString = _translationStrings[lblUniqueSongsAverageDuration];

            var totalUniqueSize = 0L;
            await Task.Run(() =>
            {
                var _lock = new object();
                Parallel.ForEach(uniqueSongs, song =>
                {
                    var size = song.Size;
                    lock (_lock)
                        totalUniqueSize += size;
                });
            });
            lblUniqueSongsTotalSize.Parameters = new object[] {Formatting.FormatBytes(totalUniqueSize)};
            lblUniqueSongsTotalSize.TranslationString = _translationStrings[lblUniqueSongsTotalSize];

            lblUniqueSongsAverageSize.Parameters = new object[]
                {Formatting.FormatBytes(totalUniqueSize / uniqueSongs.Count)};
            lblUniqueSongsAverageSize.TranslationString = _translationStrings[lblUniqueSongsAverageSize];
        }

        private void StatisticsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _parent.ChildClosed(this);
        }
    }
}
