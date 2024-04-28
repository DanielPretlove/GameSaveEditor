namespace dmm.XenoSave.XC3
{
    public class Inventory
    {
        public const uint SIZE = 0x112E8;

        public uint TotalCount { get; set; }
        public byte[] Unk_0x00004 { get; set; }
        public Item[] EtherCylinders { get; set; }
        public Item[] Gems { get; set; }
        public Item[] Collectibles { get; set; }
        public Item[] UnkType { get; set; }
        public Item[] Accessories { get; set; }
        public Item[] KeyItems { get; set; }

        private static readonly Dictionary<string, uint> LOC = new Dictionary<string, uint>()
        {
            { "TotalCount", 0x00000 },
            { "Unk_0x00004", 0x00004 },
            { "EtherCylinders", 0x00028 },
            { "Gems", 0x00128 },
            { "Collectibles", 0x013E8 },
            { "UnkType", 0x071A8 },
            { "Accessories", 0x0A3A8 },
            { "KeyItems", 0x10168 }
        };

        public Inventory(byte[] data)
        {
            TotalCount = BitConverter.ToUInt32(data.GetByteSubArray(LOC["TotalCount"], 0x4));
            Unk_0x00004 = data.GetByteSubArray(LOC["Unk_0x00004"], 0x24);

            EtherCylinders = new Item[16];
            for (uint i = 0; i < EtherCylinders.Length; i++)
                EtherCylinders[i] = new Item(data.GetByteSubArray(LOC["EtherCylinders"] + (i*Item.SIZE), Item.SIZE));

            Gems = new Item[300];
            for (uint i = 0; i < Gems.Length; i++)
                Gems[i] = new Item(data.GetByteSubArray(LOC["Gems"] + (i*Item.SIZE), Item.SIZE));

            Collectibles = new Item[1500];
            for (uint i = 0; i < Collectibles.Length; i++)
                Collectibles[i] = new Item(data.GetByteSubArray(LOC["Collectibles"] + (i*Item.SIZE), Item.SIZE));

            UnkType = new Item[800];
            for (uint i = 0; i < UnkType.Length; i++)
                UnkType[i] = new Item(data.GetByteSubArray(LOC["UnkType"] + (i*Item.SIZE), Item.SIZE));

            Accessories = new Item[1500];
            for (uint i = 0; i < Accessories.Length; i++)
                Accessories[i] = new Item(data.GetByteSubArray(LOC["Accessories"] + (i*Item.SIZE), Item.SIZE));
            
            KeyItems = new Item[280];
            for (uint i = 0; i < KeyItems.Length; i++)
                KeyItems[i] = new Item(data.GetByteSubArray(LOC["KeyItems"] + (i*Item.SIZE), Item.SIZE));
        }

        public byte[] ToRawData()
        {
            List<byte> result = new List<byte>();

            result.AddRange(BitConverter.GetBytes(TotalCount));
            result.AddRange(Unk_0x00004);

            foreach (Item i in EtherCylinders)
                result.AddRange(i.ToRawData());
            
            foreach (Item i in Gems)
                result.AddRange(i.ToRawData());

            foreach (Item i in Collectibles)
                result.AddRange(i.ToRawData());

            foreach (Item i in UnkType)
                result.AddRange(i.ToRawData());

            foreach (Item i in Accessories)
                result.AddRange(i.ToRawData());

            foreach (Item i in KeyItems)
                result.AddRange(i.ToRawData());

            return result.ToArray();
        }
    }
}