namespace dmm.XenoSave.XC3
{
    public enum CLASS : uint
    {
        NONE = 0,
        SWORDFIGHTER,
        ZEPHYR,
        MEDIC_GUNNER,
        TACTICIAN,
        HEAVY_GUARD,
        OGRE,
        FLASH_FENCER,
        YUMSMITH,
        STRATEGOS,
        TROUBADOR,
        SERAPH,
        LOST_VANGUARD,
        UNK_1,
        LIFESAGE,
        ROYAL_SUMMONER,
        WAR_MEDIC,
        GUARDIAN_COMMANDER,
        THAUMATURGE,
        INCURSOR,
        STALKER,
        LONE_EXILE,
        SOULHACKER,
        SIGNIFIER,
        MACHINE_ASSASSIN,
        MARTIAL_ARTIST,
        FULL_METAL_JAGUAR,
        UNK_2,
        LUCKY_SEVEN,
        LUCKY_SEVEN_2,
        SWORD_4,
        SHARPSHOOTER
    }

    public class ClassBuild : ISaveObject
    {
        public const uint SIZE = 0x44;

        public uint CP { get; set; }
        public ushort Restriction { get; set; }
        public byte Level { get; set; }
        public byte Unk_0x07 { get; set; }
        public byte[] Gems { get; set; }
        public byte[] Unk_0x0B { get; set; }
        public ushort[] ArtsSkills { get; set; }
        public byte[] Unk_0x2E { get; set; }
        public EquipItem[] EquippedItems { get; set; }
        public byte[] Unk_0x42 { get; set; }

        private static readonly Dictionary<string, uint> LOC = new Dictionary<string, uint>()
        {
            { "CP", 0x00 },
            { "Restriction", 0x04 },
            { "Level", 0x06 },
            { "Unk_0x07", 0x07 },
            { "Gems", 0x08 },
            { "Unk_0x0B", 0x0B },
            { "ArtsSkills", 0x12 },
            { "Unk_0x2E", 0x2E },
            { "EquippedItems", 0x30 },
            { "Unk_0x42", 0x42 }
        };

        public ClassBuild(byte[] data)
        {
            CP = BitConverter.ToUInt32(data.GetByteSubArray(LOC["CP"], 0x4));
            Restriction = BitConverter.ToUInt16(data.GetByteSubArray(LOC["Restriction"], 0x2));
            Level = data[LOC["Level"]];
            Unk_0x07 = data[LOC["Unk_0x07"]];
            Gems = data.GetByteSubArray(LOC["Gems"], 0x3);
            Unk_0x0B = data.GetByteSubArray(LOC["Unk_0x0B"], 0x07);
            ArtsSkills = new ushort[0xE];
            for (uint i = 0; i < ArtsSkills.Length; i++)
                ArtsSkills[i] = BitConverter.ToUInt16(data.GetByteSubArray(LOC["ArtsSkills"] + (i * 2), 0x2));
            Unk_0x2E = data.GetByteSubArray(LOC["Unk_0x2E"], 0x2);
            EquippedItems = new EquipItem[3];
            for (uint i = 0; i < EquippedItems.Length; i++)
                EquippedItems[i] = new EquipItem(data.GetByteSubArray(LOC["EquippedItems"] + i * EquipItem.SIZE, EquipItem.SIZE));
            Unk_0x42 = data.GetByteSubArray(LOC["Unk_0x42"], 0x2);
        }

        public byte[] ToRawData()
        {
            List<byte> result = new List<byte>();

            result.AddRange(BitConverter.GetBytes(CP));
            result.AddRange(BitConverter.GetBytes(Restriction));
            result.Add(Level);
            result.Add(Unk_0x07);
            result.AddRange(Gems);
            result.AddRange(Unk_0x0B);
            foreach (ushort asId in ArtsSkills)
                result.AddRange(BitConverter.GetBytes(asId));
            result.AddRange(Unk_0x2E);
            foreach (EquipItem acc in EquippedItems)
                result.AddRange(acc.ToRawData());
            result.AddRange(Unk_0x42);

            return result.ToArray();
        }
    }
}