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
    public class ElapseTime : ISaveObject
    {
        public const int SIZE = 4;

        public UInt16 Hours   { get; set; }
        public Byte   Minutes { get; set; }
        public Byte   Seconds { get; set; }

        public ElapseTime (Byte[] data)
        {
            UInt32 value = BitConverter.ToUInt32 (data, 0);

            Hours   = (UInt16) (value / 3600);
            Minutes = (Byte) ((value % 3600) / 60);
            Seconds = (Byte) (value % 60);
        }

        public ElapseTime()
        {
            Hours   = 0;
            Minutes = 0;
            Seconds = 0;
        }

        public uint TotalSeconds()
        {
            return (UInt32) (((UInt32) Hours * 3600) + ((UInt32) Minutes * 60) + ((UInt32) Seconds));
        }

        public virtual Byte[] ToRawData()
        {
            return BitConverter.GetBytes ((UInt32) (((UInt32) Hours * 3600) + ((UInt32) Minutes * 60) + ((UInt32) Seconds)));
        }

        public override string ToString()
        {
            return Hours + ":" + Minutes.ToString("D2") + ":" + Seconds.ToString("D2");
        }
    }

    public class ElapseTimeMS : ElapseTime
    {
        public ushort MilliSeconds { get; set; }

        public ElapseTimeMS(Byte[] data) : this(BitConverter.ToUInt32(data)) { }

        public ElapseTimeMS(uint _ms)
        {
            TimeSpan ts = TimeSpan.FromMilliseconds(_ms);
            Hours = (ushort)ts.Hours;
            Minutes = (byte)ts.Minutes;
            Seconds = (byte)ts.Seconds;
            MilliSeconds = (ushort)ts.Milliseconds;
        }

        public uint TotalMilliseconds()
        {
            return TotalSeconds() * 1000 + MilliSeconds;
        }

        public override Byte[] ToRawData()
        {
            return BitConverter.GetBytes(TotalMilliseconds());
        }
    }
}
