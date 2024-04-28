namespace dmm.XenoSave.XC3
{
    public class Ouroboros : ISaveObject
    {
        public const uint SIZE = 0x3C;

        public byte[] Unk_0x00 { get; set; }
        public uint SP { get; set; }
        public byte[] Unk_0x10 { get; set; }

        private static readonly Dictionary<string, uint> LOC = new Dictionary<string, uint>()
        {
            { "Unk_0x00", 0x0 },
            { "SP", 0xC },
            { "Unk_0x10", 0x10 }
        };

        public Ouroboros(byte[] data)
        {
            Unk_0x00 = data.GetByteSubArray(LOC["Unk_0x00"], 0xC);
            SP = BitConverter.ToUInt32(data.GetByteSubArray(LOC["SP"], 0x4));
            Unk_0x10 = data.GetByteSubArray(LOC["Unk_0x10"], 0x2C);
        }

        public byte[] ToRawData()
        {
            List<byte> result = new List<byte>();

            result.AddRange(Unk_0x00);
            result.AddRange(BitConverter.GetBytes(SP));
            result.AddRange(Unk_0x10);

            return result.ToArray();
        }
    }
}
