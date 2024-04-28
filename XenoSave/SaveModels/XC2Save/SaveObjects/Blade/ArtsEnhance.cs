/*
 * XenoSave
 * Copyright (C) 2018-2023  damysteryman
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dmm.XenoSave.XC2
{
    public class ArtsEnhance : ISaveObject
    {
        public const int SIZE = 0xC;

        public UInt16 EnhanceID { get; set; }
        public UInt16 RecastRev { get; set; }
        public UInt16 ItemID { get; set; }
        public Byte[] Unk_0x06 { get; set; }
        public Byte[] Unk_0x08 { get; set; }
        public ItemHandle16 ItemHandle { get; set; }

        public ArtsEnhance(Byte[] data)
        {
            EnhanceID = BitConverter.ToUInt16(data.GetByteSubArray(0x0, 2), 0);
            RecastRev = BitConverter.ToUInt16(data.GetByteSubArray(0x2, 2), 0);
            ItemID = BitConverter.ToUInt16(data.GetByteSubArray(0x4, 2), 0);
            Unk_0x06 = data.GetByteSubArray(0x6, 2);
            Unk_0x08 = data.GetByteSubArray(0x8, 2);
            ItemHandle = new ItemHandle16(data.GetByteSubArray(0xA, ItemHandle16.SIZE));
        }

        public Byte[] ToRawData()
        {
            List<Byte> result = new List<Byte>();

            result.AddRange(BitConverter.GetBytes(EnhanceID));
            result.AddRange(BitConverter.GetBytes(RecastRev));
            result.AddRange(BitConverter.GetBytes(ItemID));
            result.AddRange(Unk_0x06);
            result.AddRange(Unk_0x08);
            result.AddRange(ItemHandle.ToRawData());

            if (result.Count != SIZE)
            {
                string message = "ArtsEnhance: SIZE ALL WRONG!!!" + Environment.NewLine +
                "Size should be " + SIZE + " bytes..." + Environment.NewLine +
                "...but Size is " + result.Count + " bytes!";

                throw new Exception(message);
            }

            return result.ToArray();
        }
    };
}
