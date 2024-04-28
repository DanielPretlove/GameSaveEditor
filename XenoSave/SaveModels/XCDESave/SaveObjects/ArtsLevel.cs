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

namespace dmm.XenoSave.XCDE
{
    public enum ArtsLevelUnlocked : byte
    {
        IV_Beginner      = 0,
        VII_Intermediate = 1,
        X_Expert         = 3,
        XII_Master       = 7
    }

    /*
    *   ArtsLevel Structure
    *   The Current Level + Max Level Unlocked for an Art
    *
    *   Offset  Type                        Name            Description
    *   0x0     uint8                       Level           = The Level of the Art (from 0~12)
    *   0x2     (uint8) ArtsLevelUnlocked   MaxLevelUnocked = The Max. Unlocked Level that the Art can be levelled up to in game.
    */
    public class ArtsLevel : ISaveObject
    {
        public const uint SIZE = 2;

        public byte Level { get; set; }
        public ArtsLevelUnlocked MaxLevelUnlocked { get; set; }

        public ArtsLevel() {}
        public ArtsLevel(byte[] data)
        {
            Level = data[0];
            MaxLevelUnlocked = (ArtsLevelUnlocked)data[1];
        }

        public byte[] ToRawData()
        {
            return new byte[] { Level, (byte)MaxLevelUnlocked };
        }

        public override string ToString()
        {
            return $"{Level},{MaxLevelUnlocked}";
        }
    }
}