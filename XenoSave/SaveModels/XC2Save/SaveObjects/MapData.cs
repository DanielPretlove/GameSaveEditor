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
    public class MapData : ISaveObject
    {
        public const int SIZE = 0xC4;

        public Vec3Padded[] DriverPositions { get; set; }
        public Vec3Padded[] BladePositions { get; set; }
        public Vec3Padded[] DriverRotations { get; set; }
        public Vec3Padded[] BladeRotations { get; set; }
        public UInt32 MapJumpID { get; set; }

        public MapData(Byte[] data)
        {
            DriverPositions = new Vec3Padded[3];
            for (int i = 0; i < DriverPositions.Length; i++)
                DriverPositions[i] = new Vec3Padded(data.GetByteSubArray(i * Vec3Padded.SIZE, Vec3Padded.SIZE));

            BladePositions = new Vec3Padded[3];
            for (int i = 0; i < BladePositions.Length; i++)
                BladePositions[i] = new Vec3Padded(data.GetByteSubArray(0x30 + (i * Vec3Padded.SIZE), Vec3Padded.SIZE));

            DriverRotations = new Vec3Padded[3];
            for (int i = 0; i < DriverRotations.Length; i++)
                DriverRotations[i] = new Vec3Padded(data.GetByteSubArray(0x60 + (i * Vec3Padded.SIZE), Vec3Padded.SIZE));

            BladeRotations = new Vec3Padded[3];
            for (int i = 0; i < BladeRotations.Length; i++)
                BladeRotations[i] = new Vec3Padded(data.GetByteSubArray(0x90 + (i * Vec3Padded.SIZE), Vec3Padded.SIZE));

            MapJumpID = BitConverter.ToUInt32(data.GetByteSubArray(0xC0, 4), 0);
        }

        public Byte[] ToRawData()
        {
            List<Byte> result = new List<Byte>();

            foreach (Vec3Padded vp in DriverPositions)
                result.AddRange(vp.ToRawData());

            foreach (Vec3Padded vp in BladePositions)
                result.AddRange(vp.ToRawData());

            foreach (Vec3Padded vp in DriverRotations)
                result.AddRange(vp.ToRawData());

            foreach (Vec3Padded vp in BladeRotations)
                result.AddRange(vp.ToRawData());

            result.AddRange(BitConverter.GetBytes(MapJumpID));

            if (result.Count != SIZE)
            {
                string message = "SDataMap: SIZE ALL WRONG!!!" + Environment.NewLine +
                "Size should be " + SIZE + " bytes..." + Environment.NewLine +
                "...but Size is " + result.Count + " bytes!";

                throw new Exception(message);
            }

            return result.ToArray();
        }
    }
}
