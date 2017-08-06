using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace AIMPPL_Copy.DB
{
    public class HistoryTable : DataTable, IEnumerable<HistoryRow>
    {
        public HistoryTable()
        {
            Columns.Add("TrackID", typeof(int));
            Columns.Add("Date", typeof(double));
            Columns.Add("Listened", typeof(int));
        }

        public HistoryRow this[int idx] => (HistoryRow)Rows[idx];

        public void Add(HistoryRow row)
        {
            Rows.Add(row);
        }

        public void Remove(HistoryRow row)
        {
            Rows.Remove(row);
        }

        public new HistoryRow NewRow() => (HistoryRow)base.NewRow();

        protected override Type GetRowType()
        {
            return typeof(HistoryRow);
        }

        protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
        {
            return new HistoryRow(builder);
        }

        public IEnumerator<HistoryRow> GetEnumerator()
        {
            foreach (var row in Rows.Cast<HistoryRow>())
                yield return row;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
