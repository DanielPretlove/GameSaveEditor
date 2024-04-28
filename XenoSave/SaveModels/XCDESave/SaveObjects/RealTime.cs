/*
 * XenoSave
 * Copyright (C) 2020-2023  damysteryman
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

namespace dmm.XenoSave.XCDE
{
    /*
    *   RealTime Structure (Credits to NamelessMofo)
    *   The real wall clock timestamp is a uint64 with the following arrangement:
    *
    *   Field         # Bits    Bit Range
    *   Year            14       [63:50]
    *   Month            4       [49:46]
    *   UnknownA         9       [45:37]
    *   Day              5       [36:32]
    *   Hour             6       [31:26]
    *   Minute           6       [25:20]
    *   Second           6       [19:14]
    *   Millisecond     10        [13:4]
    *   UnknownB         4         [3:0]
    */
    public class RealTime : ISaveObject
    {
        public const int SIZE = 8;

        private const int YearBits    = 14;
        private const int MonthBits   = 4;
        private const int UnkABits    = 9;
        private const int DayBits     = 5;
        private const int HourBits    = 6;
        private const int MinuteBits  = 6;
        private const int SecondBits  = 6;
        private const int MSecondBits = 10;
        private const int UnkBBits    = 4;

        public UInt16 Year    { get; set; }
        public Byte   Month   { get; set; }
        public UInt16 UnkA    { get; set; }
        public Byte   Day     { get; set; }
        public Byte   Hour    { get; set; }
        public Byte   Minute  { get; set; }
        public Byte   Second  { get; set; }
        public UInt16 MSecond { get; set; }
        public Byte   UnkB    { get; set; }

        public TimeSpan? Time
        {
            get => new TimeSpan(Hour, Minute, Second);
            set
            {
                TimeSpan time = value != null ? (TimeSpan)value : new TimeSpan();
                Hour    = (byte)time.Hours;
                Minute  = (byte)time.Minutes;
                Second  = (byte)time.Seconds;
            }
        }

        public DateTime? Date
        {
            get => new DateTime(Year, Month, Day, Hour, Minute, Second);
            set
            {
                DateTime date = value != null ? (DateTime)value : new DateTime();
                Year    = (ushort)date.Year;
                Month   = (byte)date.Month;
                Day     = (byte)date.Day;
            }
        }

        public RealTime (Byte[] data)
        {
            UInt64 value = BitConverter.ToUInt64 (data, 0);

            Year    = (UInt16) (value  >> (64 - YearBits));
            Month   = (Byte)   ((value << YearBits) >> (64 - MonthBits));
            UnkA    = (UInt16) ((value << (YearBits + MonthBits)) >> (64 - UnkABits));
            Day     = (Byte)   ((value << (YearBits + MonthBits + UnkABits)) >> (64 - DayBits));
            Hour    = (Byte)   ((value << (YearBits + MonthBits + UnkABits + DayBits)) >> (64 - HourBits));
            Minute  = (Byte)   ((value << (YearBits + MonthBits + UnkABits + DayBits + HourBits)) >> (64 - MinuteBits));
            Second  = (Byte)   ((value << (YearBits + MonthBits + UnkABits + DayBits + HourBits + MinuteBits)) >> (64 - SecondBits));
            MSecond = (UInt16) ((value << (YearBits + MonthBits + UnkABits + DayBits + HourBits + MinuteBits + SecondBits)) >> (64 - MSecondBits));
            UnkB    = (Byte)   ((value << (YearBits + MonthBits + UnkABits + DayBits + HourBits + MinuteBits + SecondBits + MSecondBits)) >> (64 - UnkBBits));
        }

        public Byte[] ToRawData()
        {
            UInt64 value = 0;

            value += (UInt64) Year    << (64 - YearBits);
            value += (UInt64) Month   << (64 - YearBits - MonthBits);
            value += (UInt64) UnkA    << (64 - YearBits - MonthBits - UnkABits);
            value += (UInt64) Day     << (64 - YearBits - MonthBits - UnkABits - DayBits);
            value += (UInt64) Hour    << (64 - YearBits - MonthBits - UnkABits - DayBits - HourBits);
            value += (UInt64) Minute  << (64 - YearBits - MonthBits - UnkABits - DayBits - HourBits - MinuteBits);
            value += (UInt64) Second  << (64 - YearBits - MonthBits - UnkABits - DayBits - HourBits - MinuteBits - SecondBits);
            value += (UInt64) MSecond << (64 - YearBits - MonthBits - UnkABits - DayBits - HourBits - MinuteBits - SecondBits - MSecondBits);
            value += (UInt64) UnkB;

            return BitConverter.GetBytes (value);
        }

        public override string ToString()
        {
            return String.Format ("{0:0000}-{1:00}-{2:00} {3:00}:{4:00}:{5:00}.{6:000}", Year, Month, Day, Hour, Minute, Second, MSecond);
        }
    }
}
