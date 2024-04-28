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
    public class ElapseTime : ISaveObject, IComparable
    {
        public const int SIZE = 0x4;

        public Byte Seconds { get; set; }
        private const int SECONDS_BITS = 6;
        public Byte Minutes { get; set; }
        private const int MINUTES_BITS = 6;
        public UInt32 Hours { get; set; }
        private const int HOURS_BITS = 20;

        public ElapseTime(Byte[] data)
        {
            UInt32 value = BitConverter.ToUInt32(data, 0);

            Hours = (value >> ((SIZE * 8) - HOURS_BITS));
            Minutes = (Byte)((value >> ((SIZE * 8) - HOURS_BITS - MINUTES_BITS)) & (Byte)(Math.Pow(2, MINUTES_BITS) - 1));
            Seconds = (Byte)(value & (Byte)(Math.Pow(2, SECONDS_BITS) - 1));
        }

        public ElapseTime()
        {
            Hours = 0;
            Minutes = 0;
            Seconds = 0;
        }

        public Byte[] ToRawData()
        {
            return BitConverter.GetBytes((UInt32)((Hours << ((SIZE * 8) - HOURS_BITS)) + (Minutes << SECONDS_BITS) + Seconds));
        }

        public override string ToString()
        {
            return Hours + ":" + Minutes.ToString("D2") + ":" + Seconds.ToString("D2");
        }

        public int CompareTo(object? obj)
        {
            if (obj == null)
                return -1;

            ElapseTime other = (ElapseTime)obj;

            if (Hours < other.Hours)
                return -1;
            else if (Hours > other.Hours)
                return 1;
            else
                if (Minutes < other.Minutes)
                    return -1;
                else if (Minutes > other.Minutes)
                    return 1;
                else
                    if (Seconds < other.Seconds)
                        return -1;
                    else if (Seconds > other.Seconds)
                        return 1;
                    else
                        return 0;
        }
    }
}
