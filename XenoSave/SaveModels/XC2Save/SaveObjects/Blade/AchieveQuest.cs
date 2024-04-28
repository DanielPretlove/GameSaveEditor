﻿/*
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
    public class AchieveQuest : ISaveObject
    {
        public const int SIZE = 0x1C;

        public UInt16 QuestID { get; set; }
        public Byte[] Unk_0x02 { get; set; }
        public UInt32 Count { get; set; }
        public UInt32 MaxCount { get; set; }
        public Byte[] Unk_0x0C { get; set; }
        public UInt16 StatsID { get; set; }
        public UInt16 TaskType { get; set; }
        public UInt16 Column { get; set; }
        public UInt16 Row { get; set; }
        public UInt16 BladeID { get; set; }
        public UInt16 AchievementID { get; set; }
        public UInt16 Alignment { get; set; }

        public AchieveQuest(Byte[] data)
        {
            QuestID = BitConverter.ToUInt16(data.GetByteSubArray(0x0, 2), 0);
            Unk_0x02 = data.GetByteSubArray(0x2, 2);
            Count = BitConverter.ToUInt32(data.GetByteSubArray(0x4, 4), 0);
            MaxCount = BitConverter.ToUInt32(data.GetByteSubArray(0x8, 4), 0);
            Unk_0x0C = data.GetByteSubArray(0xC, 2);
            StatsID = BitConverter.ToUInt16(data.GetByteSubArray(0xE, 2), 0);
            TaskType = BitConverter.ToUInt16(data.GetByteSubArray(0x10, 2), 0);
            Column = BitConverter.ToUInt16(data.GetByteSubArray(0x12, 2), 0);
            Row = BitConverter.ToUInt16(data.GetByteSubArray(0x14, 2), 0);
            BladeID = BitConverter.ToUInt16(data.GetByteSubArray(0x16, 2), 0);
            AchievementID = BitConverter.ToUInt16(data.GetByteSubArray(0x18, 2), 0);
            Alignment = BitConverter.ToUInt16(data.GetByteSubArray(0x1A, 2), 0);
        }

        public Byte[] ToRawData()
        {
            List<Byte> result = new List<byte>();

            result.AddRange(BitConverter.GetBytes(QuestID));
            result.AddRange(Unk_0x02);
            result.AddRange(BitConverter.GetBytes(Count));
            result.AddRange(BitConverter.GetBytes(MaxCount));
            result.AddRange(Unk_0x0C);
            result.AddRange(BitConverter.GetBytes(StatsID));
            result.AddRange(BitConverter.GetBytes(TaskType));
            result.AddRange(BitConverter.GetBytes(Column));
            result.AddRange(BitConverter.GetBytes(Row));
            result.AddRange(BitConverter.GetBytes(BladeID));
            result.AddRange(BitConverter.GetBytes(AchievementID));
            result.AddRange(BitConverter.GetBytes(Alignment));

            if (result.Count != SIZE)
            {
                string message = "AchieveQuest: SIZE ALL WRONG!!!" + Environment.NewLine +
                "Size should be " + SIZE + " bytes..." + Environment.NewLine +
                "...but Size is " + result.Count + " bytes!";

                throw new Exception(message);
            }

            return result.ToArray();
        }
    }
}
