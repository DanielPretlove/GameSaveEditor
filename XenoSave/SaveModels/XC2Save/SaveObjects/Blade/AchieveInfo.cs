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
    public class AchieveInfo : ISaveObject
    {
        public const int SIZE = 0x90;

        public UInt16 ID { get; set; }
        public UInt16 Alignment { get; set; }
        public AchieveQuest[] AchieveQuests { get; set; }

        public AchieveInfo(Byte[] data)
        {
            ID = BitConverter.ToUInt16(data.GetByteSubArray(0, 2), 0);
            Alignment = BitConverter.ToUInt16(data.GetByteSubArray(2, 2), 0);

            AchieveQuests = new AchieveQuest[5];
            for (int i = 0; i < AchieveQuests.Length; i++)
                AchieveQuests[i] = new AchieveQuest(data.GetByteSubArray(4 + (i * AchieveQuest.SIZE), AchieveQuest.SIZE));
        }

        public Byte[] ToRawData()
        {
            List<Byte> result = new List<Byte>();

            result.AddRange(BitConverter.GetBytes(ID));
            result.AddRange(BitConverter.GetBytes(Alignment));

            foreach (AchieveQuest aq in AchieveQuests)
                result.AddRange(aq.ToRawData());

            if (result.Count != SIZE)
            {
                string message = "AchieveInfo: SIZE ALL WRONG!!!" + Environment.NewLine +
                "Size should be " + SIZE + " bytes..." + Environment.NewLine +
                "...but Size is " + result.Count + " bytes!";

                throw new Exception(message);
            }

            return result.ToArray();
        }
    }
}
