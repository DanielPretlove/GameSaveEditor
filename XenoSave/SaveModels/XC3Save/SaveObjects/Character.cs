namespace dmm.XenoSave.XC3
{
    public enum CHARACTER : ushort
    {
        NONE = 0,
        NOAH,
        MIO,
        EUNIE,
        TAION,
        LANZ,
        SENA,
        ETHEL,
        RIKU_MANANA,
        UNK_1,
        ISURD,
        MIYABI,
        CAMMURAVI,
        MONICA,
        JORAN,
        NIA,
        MELIA,
        VALDI,
        ZEON,
        TEACH,
        ALEXANDRIA,
        JUNIPER,
        ASHERA,
        TRITON,
        FIONA,
        SEGIRI,
        GHONDOR,
        GRAY,
        NIMUE,
        MWAMBA
    }
    public class Character : ISaveObject
    {
        public const int SIZE = 0x115C;

        public ushort Level { get; set; }
        public byte[] Unk_0x0002 { get; set; }
        public uint EXP { get; set;}
        public uint BonusEXP { get; set; }
        public uint ClothingAccessory { get; set; }
        public CLASS CurrentClass { get; set; }
        public ClassBuild[] ClassBuilds { get; set; }
        public ushort Clothing { get; set; }
        public byte[] Unk_0x1116 { get; set; }

        private static readonly Dictionary<string, uint> LOC = new Dictionary<string, uint>()
        {
            { "Level", 0x0000 },
            { "Unk_0x0002", 0x0002 },
            { "EXP", 0x0004 },
            { "BonusEXP", 0x0008 },
            { "ClothingAccessory", 0x000C },
            { "CurrentClass", 0x0010 },
            { "ClassBuilds", 0x0014 },
            { "Clothing", 0x1114 },
            { "Unk_0x1116", 0x1116 }
        };

        public Character(byte[] data)
        {
            Level = BitConverter.ToUInt16(data.GetByteSubArray(LOC["Level"], 0x2));
            Unk_0x0002 = data.GetByteSubArray(LOC["Unk_0x0002"], 0x2);
            EXP = BitConverter.ToUInt32(data.GetByteSubArray(LOC["EXP"], 0x4));
            BonusEXP = BitConverter.ToUInt32(data.GetByteSubArray(LOC["BonusEXP"], 0x4));
            ClothingAccessory = BitConverter.ToUInt32(data.GetByteSubArray(LOC["ClothingAccessory"], 0x4));
            CurrentClass = (CLASS)BitConverter.ToUInt32(data.GetByteSubArray(LOC["CurrentClass"], 0x4));
            ClassBuilds = new ClassBuild[0x40];
            for (uint i = 0; i < ClassBuilds.Length; i++)
                ClassBuilds[i] = new ClassBuild(data.GetByteSubArray(LOC["ClassBuilds"] + (i * ClassBuild.SIZE), ClassBuild.SIZE));
            Clothing = BitConverter.ToUInt16(data.GetByteSubArray(LOC["Clothing"], 0x2));
            Unk_0x1116 = data.GetByteSubArray(LOC["Unk_0x1116"], 0x46);
        }

        public byte[] ToRawData()
        {
            List<byte> result = new List<byte>();

            result.AddRange(BitConverter.GetBytes(Level));
            result.AddRange(Unk_0x0002);
            result.AddRange(BitConverter.GetBytes(EXP));
            result.AddRange(BitConverter.GetBytes(BonusEXP));
            result.AddRange(BitConverter.GetBytes(ClothingAccessory));
            result.AddRange(BitConverter.GetBytes((uint)CurrentClass));
            foreach (ClassBuild cb in ClassBuilds)
                result.AddRange(cb.ToRawData());
            result.AddRange(BitConverter.GetBytes(Clothing));
            result.AddRange(Unk_0x1116);

            return result.ToArray();
        }
    }
}