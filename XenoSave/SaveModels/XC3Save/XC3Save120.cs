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

public class XC3Save120 : XC3Save
{
    public const int SIZE = 0x1BE000;

    public byte[] Unk_0x1911F0 { get; set; }
    public byte[] PartyFormations { get; set; }
    public byte[] Unk_0x1BD430 { get; set; }

    private static readonly Dictionary<string, uint> LOC_100 = new()
    {
        { "Unk_0x1911F0", 0x1911F0 },
        { "PartyFormations", 0x19AFC0 },
        { "Unk_0x1BD430", 0x1BD430 }
    };

    public XC3Save120(byte[] data) : base(data)
    {
        Unk_0x1911F0 = data.GetByteSubArray(LOC_100["Unk_0x1911F0"], 0x9DD0);
        PartyFormations = data.GetByteSubArray(LOC_100["PartyFormations"], 0x22470);
        Unk_0x1BD430 = data.GetByteSubArray(LOC_100["Unk_0x1BD430"], 0xBD0);
    }

    public new byte[] ToRawData()
    {
        List<byte> result = new();

        result.AddRange(base.ToRawData());

        result.AddRange(Unk_0x1911F0);
        result.AddRange(PartyFormations);
        result.AddRange(Unk_0x1BD430);
        
        return result.ToArray();
    }
}