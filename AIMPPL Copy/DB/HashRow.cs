using System.Data;

namespace AIMPPL_Copy.DB
{
    public class HashRow : DataRow
    {
        internal HashRow(DataRowBuilder builder) : base(builder)
        {
            
        }

        public int Id => (int)base["ID"];

        public int ValueHash
        {
            get => (int)base["ValueHash"];
            set => base["ValueHash"] = value;
        }

        public string Value
        {
            get => (string)base["Value"];
            set => base["Value"] = value;
        }
    }
}
