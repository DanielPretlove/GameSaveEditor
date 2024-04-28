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
using System.Collections;
using System.Collections.Generic;

namespace dmm.XenoSave.XCDE;

public class TimeAttackScore : ISaveObject
{
    public enum Rank : byte
    {
        None,
        C,
        B,
        A,
        S
    }

    public const uint SIZE = 4;

    public Rank   AchievedRank   { get; set; }
    public Rank   NSRewardRank   { get; set; }
    public bool[] BonusNSRewards { get; set; } = new bool[2];
    public uint   Unk            { get; set; }
    public bool   IsNew          { get; set; }
    public bool   RewardAchieved { get; set; }

    public TimeAttackScore(byte[] data)
    {
        uint raw = BitConverter.ToUInt32(data);

        AchievedRank      = (Rank)(raw & 0b111);
        NSRewardRank      = (Rank)((raw >> 4) & 0b111);
        BonusNSRewards[1] = ((raw >> 8) & 1) == 1;
        BonusNSRewards[0] = ((raw >> 9) & 1) == 1;
        Unk               = (raw >> 10) & 0b11111111111111111111;
        IsNew             = ((raw >> 30) & 1) == 0;
        RewardAchieved    = ((raw >> 31) & 1) == 1;
    }

    public byte[] ToRawData()
    {
        uint result = (uint)AchievedRank
                    + ((uint)NSRewardRank << 4)
                    + ((uint)(BonusNSRewards[1] ? 1 : 0) << 8)
                    + ((uint)(BonusNSRewards[0] ? 1 : 0) << 9)
                    + (uint)(Unk << 10)
                    + ((uint)(IsNew ? 0 : 1) << 30)
                    + ((uint)(RewardAchieved ? 1 : 0) << 31);
        return BitConverter.GetBytes(result);
    }
}

public class TimeAttackData : ISaveObject
{
    public const uint SIZE = 240;

    public TimeAttackScore[] Scores { get; set; } = new TimeAttackScore[20];
    public byte[]            Unk    { get; set; }
    public ElapseTimeMS[]    Times  { get; set; } = new ElapseTimeMS[20];

    private enum LOC : uint
    {
        Scores = 0,
        Unk    = 80,
        Times  = 160
    }
    public TimeAttackData(byte[] data)
    {
        for (int i = 0; i < Scores.Length; i++)
        {
            Scores[i] = new TimeAttackScore(data.GetByteSubArray((uint)(i * TimeAttackScore.SIZE), TimeAttackScore.SIZE));
            Times[i]  = new ElapseTimeMS(data.GetByteSubArray((int)LOC.Times + (i * sizeof(uint)), sizeof(uint)));
        }
        Unk = data.GetByteSubArray((int)LOC.Unk, 80);
    }


    public byte[] ToRawData()
    {
        List<byte> result = new();

        foreach(var s in Scores) result.AddRange(s.ToRawData());
        result.AddRange(Unk);
        foreach(var t in Times)  result.AddRange(t.ToRawData());

        return result.ToArray();
    }
}

