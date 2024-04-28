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
    public abstract class ItemHandle : ISaveObject
    {
        public Byte Type { get; set; }
        protected const int TYPE_BITS = 6;

        public UInt32 Serial { get; set; }

        protected ItemHandle(Byte[] data, int size)
        {
            UInt32 value;
            if (size <= 2)
                value = BitConverter.ToUInt16(data, 0);
            else
                value = BitConverter.ToUInt32(data, 0);

            int bits = size * 8;

            Serial = (value >> TYPE_BITS);
            Type = (Byte)(value & (Byte)(Math.Pow(2, TYPE_BITS) - 1));
        }

        public abstract Byte[] ToRawData();
    }

    public class ItemHandle16 : ItemHandle
    {
        public const int SIZE = 0x2;

        public ItemHandle16(Byte[] data) : base(data, SIZE) { }

        public override Byte[] ToRawData()
        {
            return BitConverter.GetBytes((UInt16)((Serial << TYPE_BITS) + Type));
        }
    }

    public class ItemHandle32 : ItemHandle
    {
        public const int SIZE = 0x4;

        public ItemHandle32(Byte[] data) : base(data, SIZE) { }

        public override Byte[] ToRawData()
        {
            return BitConverter.GetBytes(((Serial << TYPE_BITS) + Type));
        }
    }
}
