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
namespace dmm.XenoSave
{
    public static class Utils
    {
        public static byte[] GetByteSubArray(this byte[] data, uint startLoc, uint length)
        {
            byte[] value = new byte[length];

            for (int i = 0; i < value.Length; i++)
                value[i] = data[startLoc + i];

            return value;
        }

        public static byte[] GetByteSubArray(this byte[] data, int startLoc, int length)
        {
            byte[] value = new byte[length];

            for (int i = 0; i < value.Length; i++)
                value[i] = data[startLoc + i];

            return value;
        }

        public static Byte SanitizeByte(this Byte value)
        {
            if (value < 0 || value > Byte.MaxValue)
                return 0;
            else
                return value;
        }

        public static Byte SanitizeByte(this int value)
        {
            if (value < 0 || value > Byte.MaxValue)
                return 0;
            else
                return (Byte)value;
        }

        public static UInt16 SanitizeUInt16(this UInt16 value)
        {
            if (value < 0 || value > UInt16.MaxValue)
                return 0;
            else
                return value;
        }

        public static UInt16 SanitizeUInt16(this int value)
        {
            if (value < 0 || value > UInt16.MaxValue)
                return 0;
            else
                return (UInt16)value;
        }

        public static Int32 SanitizeInt32(this Int32 value)
        {
            if (value < 0 || value > Int32.MaxValue)
                return 0;
            else
                return value;
        }

        public static UInt32 SanitizeUInt32(this UInt32 value)
        {
            if (value < 0 || value > UInt32.MaxValue)
                return 0;
            else
                return value;
        }

        public static UInt64 SanitizeUInt16(this UInt64 value)
        {
            if (value < 0 || value > UInt64.MaxValue)
                return 0;
            else
                return value;
        }

    }
}
