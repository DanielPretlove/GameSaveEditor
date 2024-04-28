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
using Microsoft.AspNetCore.Components;
using XenoSaveCheat.Services;
using dmm.XenoSave.XCDE;

namespace XenoSaveCheat.Pages.XCDE;

public partial class Flags
{
    [Inject]
    public dmm.XenoSaveCheatCore.Core Core { get; set; } = null!;
    [Inject]
    public DataLookupService DLS { get; set; } = null!;

    private string _file = string.Empty;
    private Location[] collectLocations = new Location[]
    {
        Location.Colony9,
        Location.TephraCave,
        Location.BionisLeg,
        Location.Colony6,
        Location.EtherMine,
        Location.SatorlMarsh,
        Location.BionisInterior,
        Location.MaknaForest,
        Location.FrontierVillage,
        Location.ErythSea,
        Location.Alcamoth,
        Location.HighEntiaTomb,
        Location.ValakMountain,
        Location.SwordValley,
        Location.GalahadFortress,
        Location.FallenArm,
        Location.MechonisField,
        Location.CentralFactory,
        Location.Agniratha,
        Location.PrisonIsland,
        Location.MemorySpace,
        Location.BionisShoulderFC,
        Location.AlcamothFC
    };


    [Parameter]
    public string File { get => _file; set => _file = value.Replace('-', '.'); }

    private dmm.XenoSave.XCDE.XCDESave Save { get; set; } = null!;

    protected override void OnParametersSet() => Save = (dmm.XenoSave.XCDE.XCDESave)Core.SAVEDATA[File];

    private string GetLocationString(Location loc)
    {
        switch (loc)
        {
            case Location.AlcamothFC:       return "Alcamoth (FC)";
            case Location.BionisInterior:   return "Bionis' Interior";
            case Location.BionisLeg:        return "Bionis' Leg";
            case Location.BionisShoulderFC: return "Bionis' Shoulder (FC)";
            case Location.CentralFactory:   return "Central Factory";
            case Location.Colony6:          return "Colony 6";
            case Location.Colony9:          return "Colony 9";
            case Location.ErythSea:         return "Eryth Sea";
            case Location.EtherMine:        return "Ether Mine";
            case Location.FallenArm:        return "Fallen Arm";
            case Location.FrontierVillage:  return "Frontier Village";
            case Location.GalahadFortress:  return "Galahad Fortress";
            case Location.HighEntiaTomb:    return "High Entia Tomb";
            case Location.MaknaForest:      return "Makna Forest";
            case Location.MechonisField:    return "Mechonis Field";
            case Location.MemorySpace:      return "Other";
            case Location.PrisonIsland:     return "Prison Island";
            case Location.SatorlMarsh:      return "Satorl Marsh";
            case Location.SwordValley:      return "Sword Valley";
            case Location.TephraCave:       return "Tephra Cave";
            case Location.ValakMountain:    return "Valak Mountain";
            default:                        return loc.ToString();
        }
    }
}