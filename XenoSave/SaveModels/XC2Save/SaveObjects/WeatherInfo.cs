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
    public class WeatherInfo : ISaveObject
    {
        public const int SIZE = 0x20;
        public const int COUNT = 64;

        public string Name { get; set; }
        public Byte[] Unk_0x04 { get; set; }

        public WeatherInfo(Byte[] data)
        {
            Name = Encoding.ASCII.GetString(data.GetByteSubArray(0x00, 4));
            Unk_0x04 = data.GetByteSubArray(0x04, 0x1C);
        }

        public Byte[] ToRawData()
        {
            List<Byte> result = new List<byte>();

            result.AddRange(Encoding.ASCII.GetBytes(Name));
            result.AddRange(Unk_0x04);

            if (result.Count != SIZE)
            {
                string message = "Weather: SIZE ALL WRONG!!!" + Environment.NewLine +
                "Size should be " + SIZE + " bytes..." + Environment.NewLine +
                "...but Size is " + result.Count + " bytes!";

                throw new Exception(message);
            }

            return result.ToArray();
        }
    }
}
