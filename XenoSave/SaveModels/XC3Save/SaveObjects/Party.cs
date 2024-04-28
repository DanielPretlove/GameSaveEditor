namespace dmm.XenoSave.XC3
{
    public class Party : ISaveObject
    {
        public const uint SIZE = 0x28;

        public CHARACTER[] Members { get; set; }
        public byte[] Unk_0x10 { get; set; }
        public byte MembersCount { get; set; }
        public byte[] Unk_0x21 { get; set; }

        private static readonly Dictionary<string, uint> LOC = new Dictionary<string, uint>()
        {
            { "Members", 0x00 },
            { "Unk_0x10", 0x10 },
            { "MembersCount", 0x20 },
            { "Unk_0x21", 0x21 }
        };

        public Party(byte[] data)
        {
            Members = new CHARACTER[8];
            for (uint i = 0; i < Members.Length; i++)
                Members[i] = (CHARACTER)BitConverter.ToUInt16(data.GetByteSubArray(LOC["Members"] + (i * 0x2), 0x2));
            
            Unk_0x10 = data.GetByteSubArray(LOC["Unk_0x10"], 0x10);
            MembersCount = data[LOC["MembersCount"]];
            Unk_0x21 = data.GetByteSubArray(LOC["Unk_0x21"], 0x07);
        }

        public byte[] ToRawData()
        {
            List<byte> result = new List<byte>();

            foreach (ushort m in Members)
                result.AddRange(BitConverter.GetBytes(m));
            result.AddRange(Unk_0x10);
            result.Add(MembersCount);
            result.AddRange(Unk_0x21);

            return result.ToArray();
        }
    }
}