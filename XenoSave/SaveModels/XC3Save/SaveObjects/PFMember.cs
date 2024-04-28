namespace dmm.XenoSave.XC3;

public class PFMember : ISaveObject
{
    public const int SIZE = 0x90;

    public byte[] Unk_0x00 { get; set; }
    public ClassBuild Build { get; set; }
    public ushort ClassID { get; set; }
    public ushort CharID { get; set; }
    public byte[] Unk_0x4C { get; set; }

    public static readonly Dictionary<string, uint> LOC = new()
    {
        { "Unk_0x00", 0x00 },
        { "Build", 0x04 },
        { "ClassID", 0x48 },
        { "CharID", 0x4A },
        { "Unk_0x4C", 0x4C }
    };

    public PFMember(byte[] data)
    {
        Unk_0x00 = data.GetByteSubArray(LOC["Unk_0x00"], 0x4);
        Build = new ClassBuild(data.GetByteSubArray(LOC["Build"], ClassBuild.SIZE));
        ClassID = BitConverter.ToUInt16(data.GetByteSubArray(LOC["ClassID"], 0x2));
        CharID = BitConverter.ToUInt16(data.GetByteSubArray(LOC["CharID"], 0x2));
        Unk_0x4C = data.GetByteSubArray(LOC["Unk_0x4C"], 0x44);
    }

    public byte[] ToRawData()
    {
        List<byte> result = new();

        result.AddRange(Unk_0x00);
        result.AddRange(Build.ToRawData());
        result.AddRange(BitConverter.GetBytes(ClassID));
        result.AddRange(BitConverter.GetBytes(CharID));
        result.AddRange(Unk_0x4C);

        return result.ToArray();
    }
}