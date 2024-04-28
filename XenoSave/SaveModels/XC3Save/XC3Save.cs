/*
 * XenoSave
 * Copyright (C) 2022-2023  damysteryman
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU Affero General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU Affero General Public License for more details.
 *
 * You should have received a copy of the GNU Affero General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

namespace dmm.XenoSave.XC3;

public abstract class XC3Save : ISaveFile, ISaveObject
{
    public uint FileMagic { get; set; }
    public uint FileVersion { get; set; }
    public byte[] Unk_0x000008 { get; set; }
    public uint TotalPlayTime { get; set; }
    public byte[] Unk_0x000014 { get; set; }
    public RealTime SaveTimestamp {get; set; }
    public UInt32 Money { get; set; }
    public byte[] Unk_0x000024 { get; set; }
    public Party Party { get; set; }
    public byte[] Unk_0x00E351 { get; set; }
    public Character[] Characters { get; set; }
    public Ouroboros[] Interlinks { get; set; }
    public byte[] GemData { get; set; }
    public byte[] Unk_0x053C30 { get; set; }
    public Inventory Inventory { get; set; }
    public byte[] Unk_0x064F60 { get; set; }

    protected static readonly Dictionary<string, uint> LOC = new Dictionary<string, uint>()
    {
        { "FileMagic", 0x000000 },
        { "FileVersion", 0x000004 },
        { "Unk_0x000008", 0x000008 },
        { "TotalPlayTime", 0x000010 },
        { "Unk_0x000014", 0x000014 },
        { "SaveTimestamp", 0x000018 },
        { "Money", 0x000020 },
        { "Unk_0x000024", 0x000024 },
        { "Party", 0x00E330 },
        { "Unk_0x00E351", 0x00E351 },
        { "Characters", 0x00E3A0 },
        { "Interlinks", 0x053AA0 },
        { "GemData", 0x053C08 },
        { "Unk_0x053C30", 0x053C30 },
        { "Inventory", 0x053C78 },
        { "Unk_0x064F60", 0x64F60 }
    };

    protected XC3Save(byte[] data)
    {
        FileMagic = BitConverter.ToUInt32(data.GetByteSubArray(LOC["FileMagic"], 0x4));
        FileVersion = BitConverter.ToUInt32(data.GetByteSubArray(LOC["FileVersion"], 0x4));
        Unk_0x000008 = data.GetByteSubArray(LOC["Unk_0x000008"], 0x8);
        TotalPlayTime = BitConverter.ToUInt32(data.GetByteSubArray(LOC["TotalPlayTime"], 0x4));
        Unk_0x000014 = data.GetByteSubArray(LOC["Unk_0x000014"], 0x4);
        SaveTimestamp = new RealTime(data.GetByteSubArray(LOC["SaveTimestamp"], RealTime.SIZE));
        Money = BitConverter.ToUInt32(data.GetByteSubArray(LOC["Money"], 0x4));
        Unk_0x000024 = data.GetByteSubArray(LOC["Unk_0x000024"], 0xE30C);
        Party = new Party(data.GetByteSubArray(LOC["Party"], Party.SIZE));
        Unk_0x00E351 = data.GetByteSubArray(LOC["Unk_0x00E351"], 0x48);
        Characters = new Character[64];
        for (uint i = 0; i < Characters.Length; i++)
            Characters[i] = new Character(data.GetByteSubArray(LOC["Characters"] + (i*Character.SIZE), Character.SIZE));
        Interlinks = new Ouroboros[6];
        for (uint i = 0; i < Interlinks.Length; i++)
            Interlinks[i] = new Ouroboros(data.GetByteSubArray(LOC["Interlinks"] + (i*Ouroboros.SIZE), Ouroboros.SIZE));
        GemData = data.GetByteSubArray(LOC["GemData"], 0x28);
        Unk_0x053C30 = data.GetByteSubArray(LOC["Unk_0x053C30"], 0x48);
        Inventory = new Inventory(data.GetByteSubArray(LOC["Inventory"], Inventory.SIZE));
        Unk_0x064F60 = data.GetByteSubArray(LOC["Unk_0x064F60"], 0x12C290);
    }

    public byte[] ToRawData()
    {
        List<byte> result = new List<byte>();

        result.AddRange(BitConverter.GetBytes(FileMagic));
        result.AddRange(BitConverter.GetBytes(FileVersion));
        result.AddRange(Unk_0x000008);
        result.AddRange(BitConverter.GetBytes(TotalPlayTime));
        result.AddRange(Unk_0x000014);
        result.AddRange(SaveTimestamp.ToRawData());
        result.AddRange(BitConverter.GetBytes(Money));
        result.AddRange(Unk_0x000024);
        result.AddRange(Party.ToRawData());
        result.AddRange(Unk_0x00E351);
        foreach (Character c in Characters)
            result.AddRange(c.ToRawData());
        foreach (Ouroboros o in Interlinks)
            result.AddRange(o.ToRawData());
        result.AddRange(GemData);
        result.AddRange(Unk_0x053C30);
        result.AddRange(Inventory.ToRawData());
        result.AddRange(Unk_0x064F60);

        return result.ToArray();
    }
}