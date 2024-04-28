/*
 * XenoSave
 * Copyright (C) 2023  damysteryman
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

namespace dmm.XenoSave.XCDE;

public class NonPartyMember : ISaveObject
{
    private const uint _Unk_1_SIZE = 0x1C;
    private const uint _Unk_2_SIZE = 0x98;
    public const uint SIZE = (NonPartyMemberItem.SIZE * 6) + _Unk_1_SIZE + _Unk_2_SIZE;

    public byte[]               Unk_1     { get; set; } = new byte[_Unk_1_SIZE];
    public NonPartyMemberItem[] Equipment { get; set; } = new NonPartyMemberItem[6];
    public byte[]               Unk_2     { get; set; } = new byte[_Unk_2_SIZE];

    private enum LOC
    {
        Unk_1     = 0,
        Equipment = (int)_Unk_1_SIZE,
        Unk_2     = (int)((NonPartyMemberItem.SIZE * 6) + LOC.Equipment)
    }

    public NonPartyMember(byte[] data)
    {
        Unk_1 = data.GetByteSubArray((uint) LOC.Unk_1, _Unk_1_SIZE);
        for (int i = 0; i < Equipment.Length; i++)
            Equipment[i] = new NonPartyMemberItem(data.GetByteSubArray(((uint)LOC.Equipment) + ((uint)i * NonPartyMemberItem.SIZE), NonPartyMemberItem.SIZE));
        Unk_2 = data.GetByteSubArray((uint) LOC.Unk_2, _Unk_2_SIZE);
    }

    public byte[] ToRawData()
    {
        List<byte> result = new();

        result.AddRange(Unk_1);
        foreach (NonPartyMemberItem item in Equipment)
            result.AddRange(item.ToRawData());
        result.AddRange(Unk_2);

        return result.ToArray();
    }
}

public class NonPartyMemberItem : ISaveObject
{
    private const uint _Unk_1_SIZE = 0x84;
    public  const uint SIZE   = EquipItem.SIZE + _Unk_1_SIZE;

    public EquipItem Item  { get; set; } = new();
    public byte[]    Unk_1 { get; set; } = new byte[_Unk_1_SIZE];

    public NonPartyMemberItem(byte[] data)
    {
        Item = new EquipItem(data.GetByteSubArray(0, EquipItem.SIZE));
        Unk_1 = data.GetByteSubArray(EquipItem.SIZE, _Unk_1_SIZE);
    }

    public byte[] ToRawData()
    {
        List<byte> result = new();
        
        result.AddRange(Item.ToRawData());
        result.AddRange(Unk_1);

        return result.ToArray();
    }
}