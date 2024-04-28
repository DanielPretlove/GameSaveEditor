﻿/*
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
    public class BladeArt : ISaveObject
    {
        public const int SIZE = 0xC;

        public UInt16 ID { get; set; }
        public UInt16 RecastRev { get; set; }
        public Byte[] Unk_0x04 { get; set; }
        public Byte Level { get; set; }
        public Byte MaxLevel { get; set; }
        public Byte[] Unk_0x09 { get; set; }

        public BladeArt(Byte[] data)
        {
            ID = BitConverter.ToUInt16(data.GetByteSubArray(0x0, 2), 0);
            RecastRev = BitConverter.ToUInt16(data.GetByteSubArray(0x2, 2), 0);
            Unk_0x04 = data.GetByteSubArray(0x4, 3);
            Level = data[0x7];
            MaxLevel = data[0x8];
            Unk_0x09 = data.GetByteSubArray(0x9, 3);
        }

        public Byte[] ToRawData()
        {
            List<Byte> result = new List<Byte>();

            result.AddRange(BitConverter.GetBytes(ID));
            result.AddRange(BitConverter.GetBytes(RecastRev));
            result.AddRange(Unk_0x04);
            result.Add(Level);
            result.Add(MaxLevel);
            result.AddRange(Unk_0x09);

            if (result.Count != SIZE)
            {
                string message = "BladeArts: SIZE ALL WRONG!!!" + Environment.NewLine +
                "Size should be " + SIZE + " bytes..." + Environment.NewLine +
                "...but Size is " + result.Count + " bytes!";

                throw new Exception(message);
            }

            return result.ToArray();
        }
    }
}
