namespace dmm.XenoSave.XC3;

public class RealTime : ISaveObject
{
    public const int SIZE = 0x8;

    // 13 bits
    public ushort Year { get; set; }
    private const int YEAR_BITS = 14;

    // 4 bits
    public byte Month { get; set; }
    private const int MONTH_BITS = 4;

    // 9 bits
    public ushort UnkB { get; set; }
    private const int UNKB_BITS = 9;

    // 5 bits
    public byte Day { get; set; }
    private const int DAY_BITS = 5;

    // 5 bits
    public byte Hour { get; set; }
    private const int HOUR_BITS = 6;

    // 6 bits
    public byte Minute { get; set; }
    private const int MINUTE_BITS = 6;

    // 6 bits
    public uint UnkA { get; set; }
    private const int UNKA_BITS = 20;

    public RealTime(Byte[] data)
    {
        UInt64 value = BitConverter.ToUInt64(data, 0);

        Year = (ushort)(value >> (64 - YEAR_BITS));
        Month = (byte)((value << YEAR_BITS) >> (64 - MONTH_BITS));
        UnkB = (ushort)((value << (YEAR_BITS + MONTH_BITS)) >> (64 - UNKB_BITS));
        Day = (byte)((value << (YEAR_BITS + MONTH_BITS + UNKB_BITS)) >> (64 - DAY_BITS));
        Hour = (byte)((value << (YEAR_BITS + MONTH_BITS + UNKB_BITS + DAY_BITS)) >> (64 - HOUR_BITS));
        Minute = (byte)((value << (YEAR_BITS + MONTH_BITS + UNKB_BITS + DAY_BITS + HOUR_BITS)) >> (64 - MINUTE_BITS));
        UnkA = (uint)((value << (YEAR_BITS + MONTH_BITS + UNKB_BITS + DAY_BITS + HOUR_BITS + MINUTE_BITS)) >> (64 - UNKA_BITS));
    }

    public Byte[] ToRawData()
    {
        UInt64 derp = 0;

        derp += (UInt64)Year << (64 - YEAR_BITS);
        derp += (UInt64)Month << (64 - YEAR_BITS - MONTH_BITS);
        derp += (UInt64)UnkB << (64 - YEAR_BITS - MONTH_BITS - UNKB_BITS);
        derp += (UInt64)Day << (64 - YEAR_BITS - MONTH_BITS - UNKB_BITS - DAY_BITS);
        derp += (UInt64)Hour << (64 - YEAR_BITS - MONTH_BITS - UNKB_BITS - DAY_BITS - HOUR_BITS);
        derp += (UInt64)Minute << (64 - YEAR_BITS - MONTH_BITS - UNKB_BITS - DAY_BITS - HOUR_BITS - MINUTE_BITS);
        derp += (UInt64)UnkA;

        return BitConverter.GetBytes(derp);
    }

    public override string ToString()
    {
        return String.Format("{0:0000}-{1:00}-{2:00} {3:00}:{4:00}", Year, Month, Day, Hour, Minute);
    }
}