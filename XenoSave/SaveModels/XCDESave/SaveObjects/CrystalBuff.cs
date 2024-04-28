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

/*
*   CrystalBuff Structure
*   A single Buff on a Crystal or Gem in player's inventory
*
*   Offset  Type    Name        Description
*   0x0     uint16  BuffID      = ID of the Buff
*   0x2     uint8   Value       = Amount Value of Buff (whether it is absolute or percent amount depends on the Buff)
*   0x3     uint8   Probability = Percent chance of the Buff being triggered (only for Buffs that use probability, 0 otherwise)
*/
 public class CrystalBuff : ISaveObject
 {
    public const int SIZE = 4;

    public ushort   BuffID      { get; set; }
    public byte     Value       { get; set; }
    public byte     Probability { get; set; }

    public CrystalBuff()
    {
        BuffID      = 0;
        Value       = 0;
        Probability = 0;
    }
    public CrystalBuff(byte[] data)
    {
        BuffID      = BitConverter.ToUInt16(data.GetByteSubArray(0, 2));
        Value       = data[2];
        Probability = data[3];
    }

    public byte[] ToRawData()
    {
        List<byte> result = new List<byte>();

        result.AddRange(BitConverter.GetBytes(BuffID));
        result.Add(Value);
        result.Add(Probability);

        return result.ToArray();
    }

    public bool IsEmpty()
    {
        return
            BuffID      == 0 &&
            Value       == 0 &&
            Probability == 0;
    }
}