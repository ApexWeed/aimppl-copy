using System;
using System.Data;

namespace AIMPPL_Copy.DB
{
    public class HistoryRow : DataRow
    {
        internal HistoryRow(DataRowBuilder builder) : base(builder)
        {
            
        }

        public int TrackId => (int)base["TrackID"];

        public DateTime Date
        {
            get => DateTime.FromOADate((double)base["Date"]);
            set => base["Date"] = value.ToOADate();
        }

        public int Listened
        {
            get => (int)base["Listened"];
            set => base["Listened"] = value;
        }
    }
}
