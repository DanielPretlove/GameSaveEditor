/*
 * XenoSaveCheat
 * Copyright (C) 2023  damysteryman
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
using dmm.XenoSave.XCDE;
namespace XenoSaveCheat.Services.DataLookup.XCDE;

public class TimeAttackLookup : IDataLookupObject
{
    public enum ChallengeType
    {
        Free,
        Restricted
    }

    public ushort        ID           { get; set; } = 0;
    public string        Name         { get; set; } = "";
    public ChallengeType Type         { get; set; }
    public int[]         NSRewardBase { get; set; } = new int[4];

    public int             GetID() => ID;
    public string          GetName() => Name;      
    public override bool   Equals(object? obj) => ((AffinityLinkLookup?)obj)?.ID == ID;
    public override string ToString() => $"{ID}: {GetName}";
    public override int    GetHashCode() => ID.GetHashCode();
}