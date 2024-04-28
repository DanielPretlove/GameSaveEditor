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
    public abstract class PaddedString : ISaveObject
    {
        public string Str { get; set; }
        public int Length { get; set; }

        public PaddedString(Byte[] data, int size)
        {
            Str = "";
            Length = data[data.Length - 4];
            for (int i = 0; i < Length; i++)
                Str += (char)data[i];
        }

        public abstract Byte[] ToRawData();

        public Byte[] ToRawData(int size)
        {
            Length = Str.Length;

            Byte[] bytes = new Byte[size];

            for (int i = 0; i < bytes.Length - 4; i++)
            {
                if (i < Length)
                    bytes[i] = (Byte)Str[i];
                else
                    bytes[i] = 0x00;
            }

            bytes[bytes.Length - 4] = (Byte)Length;
            bytes[bytes.Length - 3] = 0x00;
            bytes[bytes.Length - 2] = 0x00;
            bytes[bytes.Length - 1] = 0x00;

            return bytes;
        }

        public override string ToString()
        {
            return Str;
        }
    }

    public class PaddedString16 : PaddedString
    {
        public const int SIZE = 0x14;

        public PaddedString16(Byte[] data) : base(data, SIZE) { }

        public override byte[] ToRawData()
        {
            return ToRawData(SIZE);
        }
    }

    public class PaddedString32 : PaddedString
    {
        public const int SIZE = 0x24;

        public PaddedString32(Byte[] data) : base(data, SIZE) { }

        public override byte[] ToRawData()
        {
            return ToRawData(SIZE);
        }
    }
}
