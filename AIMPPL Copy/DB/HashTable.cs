using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;

namespace AIMPPL_Copy.DB
{
    public class HashTable : DataTable, IEnumerable<HashRow>
    {
        public enum CommitMode
        {
            Insert,
            Update,
            Delete,
            Upsert,
            All
        }

        public HashTable(string tableName)
        {
            Columns.Add("ID", typeof(int));
            Columns.Add("ValueHash", typeof(int));
            Columns.Add("Value", typeof(string));
            TableName = tableName;
        }

        public HashRow this[int idx] => (HashRow)Rows[idx];

        public void Add(HashRow row)
        {
            Rows.Add(row);
        }

        public void Remove(HashRow row)
        {
            Rows.Remove(row);
        }

        public void Commit(SQLiteConnection conn, CommitMode mode = CommitMode.All)
        {
            
        }

        public new HashRow NewRow() => (HashRow)base.NewRow();

        protected override Type GetRowType()
        {
            return typeof(HashRow);
        }

        protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
        {
            return new HashRow(builder);
        }

        public IEnumerator<HashRow> GetEnumerator()
        {
            foreach (var row in Rows.Cast<HashRow>())
                yield return row;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}