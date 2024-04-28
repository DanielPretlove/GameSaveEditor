namespace dmm.XenoSave.XC3;

public class PartyFormation : ISaveObject
{
    public const int SIZE = 0x2490;

    public ushort Name { get; set; }
    public ushort Number { get; set; }
    public ushort Color { get; set; }
    public byte[] Unk_0x0006 { get; set; }
    public Party Party { get; set; }
    public PFMember[] MemberBuilds { get; set; }
    public byte[] Unk_0x2430 { get; set; }

    public static readonly Dictionary<string, uint> LOC = new()
    {
        { "Name", 0x0000 },
        { "Number", 0x0002 },
        { "Color", 0x0004 },
        { "Unk_0x0006", 0x0006 },
        { "Party", 0x0008 },
        { "MemberBuilds", 0x0030 },
        { "Unk_0x2430", 0x2430 }
    };

    public PartyFormation(byte[] data)
    {
        Name = BitConverter.ToUInt16(data.GetByteSubArray(LOC["Name"], 0x2));
        Number = BitConverter.ToUInt16(data.GetByteSubArray(LOC["Number"], 0x2));
        Color = BitConverter.ToUInt16(data.GetByteSubArray(LOC["Color"], 0x2));
        Unk_0x0006 = data.GetByteSubArray(LOC["Unk_0x0006"], 0x2);
        Party = new Party(data.GetByteSubArray(LOC["Party"], Party.SIZE));
        MemberBuilds = new PFMember[64];
        for (int i = 0; i < MemberBuilds.Length; i++)
            MemberBuilds[i] = new PFMember(data.GetByteSubArray((uint)(LOC["MemberBuilds"] + (i * PFMember.SIZE)), PFMember.SIZE));
        Unk_0x2430 = data.GetByteSubArray(LOC["Unk_0x2430"], 0x60);
    }

    public byte[] ToRawData()
    {
        List<byte> result = new();

        result.AddRange(BitConverter.GetBytes(Name));
        result.AddRange(BitConverter.GetBytes(Number));
        result.AddRange(BitConverter.GetBytes(Color));
        result.AddRange(Unk_0x0006);
        result.AddRange(Party.ToRawData());
        foreach (PFMember m in MemberBuilds)
            result.AddRange(m.ToRawData());
        result.AddRange(Unk_0x2430);

        return result.ToArray();
    }
}