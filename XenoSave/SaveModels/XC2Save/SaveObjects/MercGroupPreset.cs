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
    public class MercGroupPreset : ISaveObject
    {
        public const int SIZE = 0xC;
        public const int COUNT = 8;

        public UInt16[] Members { get; set; }

        public MercGroupPreset(Byte[] data)
        {
            Members = new UInt16[6];
            for (int i = 0; i < Members.Length; i++)
                Members[i] = BitConverter.ToUInt16(data.GetByteSubArray(i * 2, 2), 0);
        }

        public Byte[] ToRawData()
        {
            List<Byte> result = new List<byte>();

            foreach (UInt16 m in Members)
                result.AddRange(BitConverter.GetBytes(m));

            if (result.Count != SIZE)
            {
                string message = "MercenaryTeamPreset: SIZE ALL WRONG!!!" + Environment.NewLine +
                "Size should be " + SIZE + " bytes..." + Environment.NewLine +
                "...but Size is " + result.Count + " bytes!";

                throw new Exception(message);
            }

            return result.ToArray();
        }
    }
}
