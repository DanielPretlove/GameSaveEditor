namespace dmm.XenoSave.XC3
{
    public class EquipItem : ISaveObject
    {
        public const uint SIZE = 0x6;

        public ushort ItemID { get; set; }
        public ushort Serial { get; set; }
        public ushort Type { get; set; }

        private static readonly Dictionary<string, uint> LOC = new Dictionary<string, uint>()
        {
            { "ItemID", 0x0 },
            { "Serial", 0x2 },
            { "Type", 0x4 }
        };

        public EquipItem(byte[] data)
        {
            ItemID = BitConverter.ToUInt16(data.GetByteSubArray(LOC["ItemID"], 0x2));
            Serial = BitConverter.ToUInt16(data.GetByteSubArray(LOC["Serial"], 0x2));
            Type = BitConverter.ToUInt16(data.GetByteSubArray(LOC["Type"], 0x2));
        }

        public byte[] ToRawData()
        {
            List<byte> result = new List<byte>();

            result.AddRange(BitConverter.GetBytes(ItemID));
            result.AddRange(BitConverter.GetBytes(Serial));
            result.AddRange(BitConverter.GetBytes(Type));

            return result.ToArray();
        }
    }
}