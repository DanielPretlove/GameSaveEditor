/*
 * XenoSaveCheatCore
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

public class AffinityLinkLookup : IDataLookupObject
{
    public ushort   ID       { get; set; } = 0;
    public string[] Captions { get; set; } = new string[5];
    public string[] NPCs     { get; set; } = new string[2];
    public string Name
    {
        get
        {
            return NPCs[0] == "" && NPCs[1] == "" ? "" : $"{NPCs[0]} <-> {NPCs[1]}";
        }
    }

    public int             GetID() => ID;
    public string          GetName() => Name;
    public override bool   Equals(object? obj) => ((AffinityLinkLookup?)obj)?.ID == ID;
    public override string ToString() => $"{ID}: {GetName}";
    public override int    GetHashCode() => ID.GetHashCode();
}