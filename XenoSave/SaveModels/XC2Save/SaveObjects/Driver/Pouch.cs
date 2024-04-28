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
    public class Pouch : ISaveObject
    {
        public const int SIZE = 0x8;
        public const int COUNT = 3;

        public float Time { get; set; }
        public UInt16 ItemId { get; set; }
        public bool IsEnabled { get; set; }

        public Pouch(Byte[] data)
        {
            Time = BitConverter.ToSingle(data.GetByteSubArray(0, 4), 0);
            ItemId = BitConverter.ToUInt16(data.GetByteSubArray(4, 2), 0);
            IsEnabled = BitConverter.ToUInt16(data.GetByteSubArray(6, 2), 0) == 1;
        }

        public Byte[] ToRawData()
        {
            List<Byte> result = new List<Byte>();

            result.AddRange(BitConverter.GetBytes(Time));
            result.AddRange(BitConverter.GetBytes(ItemId));
            result.AddRange(BitConverter.GetBytes((UInt16)(IsEnabled ? 1 : 0)));

            if (result.Count != SIZE)
            {
                string message = "Pouch: SIZE ALL WRONG!!!" + Environment.NewLine +
                "Size should be " + SIZE + " bytes..." + Environment.NewLine +
                "...but Size is " + result.Count + " bytes!";

                throw new Exception(message);
            }

            return result.ToArray();
        }
    }
}
