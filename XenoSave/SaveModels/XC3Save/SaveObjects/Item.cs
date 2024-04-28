namespace dmm.XenoSave.XC3
{
    public class Item : ISaveObject
    {
        public const uint SIZE = 0x10;
        
        public ushort ItemID { get; set; }
        public ushort Serial { get; set; }
        public uint Type { get; set; }
        public uint GetSerial { get; set; }
        public ushort Count { get; set; }
        public ushort IsOldNewFlag { get; set; }

        private readonly Dictionary<string, uint> LOC = new Dictionary<string, uint>()
        {
            { "ItemID", 0x00 },
            { "Serial", 0x02 },
            { "Type", 0x04 },
            { "GetSerial", 0x08 },
            { "Count", 0x0C },
            { "IsOldNewFlag", 0x0E }
        };

        public Item(byte[] data)
        {
            ItemID = BitConverter.ToUInt16(data.GetByteSubArray(LOC["ItemID"], 0x2));
            Serial = BitConverter.ToUInt16(data.GetByteSubArray(LOC["Serial"], 0x2));
            Type = BitConverter.ToUInt16(data.GetByteSubArray(LOC["Type"], 0x4));
            GetSerial = BitConverter.ToUInt16(data.GetByteSubArray(LOC["GetSerial"], 0x4));
            Count = BitConverter.ToUInt16(data.GetByteSubArray(LOC["Count"], 0x2));
            IsOldNewFlag = BitConverter.ToUInt16(data.GetByteSubArray(LOC["IsOldNewFlag"], 0x2));
        }

        public byte[] ToRawData()
        {
            List<byte> result = new List<byte>();

            result.AddRange(BitConverter.GetBytes(ItemID));
            result.AddRange(BitConverter.GetBytes(Serial));
            result.AddRange(BitConverter.GetBytes(Type));
            result.AddRange(BitConverter.GetBytes(GetSerial));
            result.AddRange(BitConverter.GetBytes(Count));
            result.AddRange(BitConverter.GetBytes(IsOldNewFlag));

            return result.ToArray();
        }
    }
}