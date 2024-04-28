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
    public class Weapon : ISaveObject
    {
        public const int SIZE = 0x14;

        public UInt32[] ArtIds { get; set; }
        public UInt32 WeaponPoints { get; set; }
        public UInt32 TotalWeaponPoints { get; set; }

        public Weapon(Byte[] data)
        {
            ArtIds = new UInt32[3];
            for (int i = 0; i < ArtIds.Length; i++)
                ArtIds[i] = BitConverter.ToUInt32(data.GetByteSubArray(i * 4, 4), 0);

            WeaponPoints = BitConverter.ToUInt32(data.GetByteSubArray(0xC, 4), 0);
            TotalWeaponPoints = BitConverter.ToUInt32(data.GetByteSubArray(0x10, 4), 0);
        }

        public Byte[] ToRawData()
        {
            List<Byte> result = new List<Byte>();

            foreach (UInt32 u in ArtIds)
                result.AddRange(BitConverter.GetBytes(u));

            result.AddRange(BitConverter.GetBytes(WeaponPoints));
            result.AddRange(BitConverter.GetBytes(TotalWeaponPoints));

            if (result.Count != SIZE)
            {
                string message = "Weapon: SIZE ALL WRONG!!!" + Environment.NewLine +
                "Size should be " + SIZE + " bytes..." + Environment.NewLine +
                "...but Size is " + result.Count + " bytes!";

                throw new Exception(message);
            }

            return result.ToArray();
        }
    }
}
