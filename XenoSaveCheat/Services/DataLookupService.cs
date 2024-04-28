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

using System.Net.Http.Json;
using dmm.XenoSave.XCDE;
using XenoSaveCheat.Services.DataLookup;
using XenoSaveCheat.Services.DataLookup.XCDE;

namespace XenoSaveCheat.Services;
public class DataLookupService
{
    private string BaseAddr = "";
    
    public Dictionary<ushort, ItemLookup>           XCDEItems           = new();
    public Dictionary<ushort, GenericDataLookup>    XCDECrystalElements = new();
    public Dictionary<ushort, GenericDataLookup>    XCDECrystalRanks    = new();
    public Dictionary<ushort, CrystalBuffLookup>    XCDECrystalBuffs    = new();
    public Dictionary<ushort, ArtsLookup>           XCDEArts            = new();
    public Dictionary<ushort, SkillLookup>          XCDESkills          = new();
    public Dictionary<ushort, GenericDataLookup>    XCDEHearttoHearts   = new();
    public Dictionary<ushort, GenericDataLookup>    XCDEPartyAffinity   = new();
    public Dictionary<ushort, AffinityLinkLookup>   XCDEAffinityLinks   = new();
    public Dictionary<ushort, AchievementLookup>    XCDEAchievements    = new();
    public Dictionary<ushort, CollectopaediaLookup> XCDECollectopaedia  = new();
    public Dictionary<ushort, TimeAttackLookup>     XCDETimeAttack      = new();
    
    public Dictionary<Character, Dictionary<ushort, GenericDataLookup>>   XCDEWeaponCosmeticsLookup = new();
    public Dictionary<Character, Dictionary<ushort, ArmorCosmeticLookup>> XCDEArmorCosmeticsLookup  = new();
    public Dictionary<Character, Dictionary<ushort, GenericDataLookup>>   XCDESkillTreesLookup      = new();
    
    public Dictionary<ItemType,  Dictionary<ushort, GenericDataLookup>>   XCDEItemIdxLookup         = new();


    public DataLookupService(string baseAddr)
    {
        BaseAddr = baseAddr;
    }

    public async Task Initialize()
    {
        Console.WriteLine("Loading DataLookupService... ");   
        HttpClient Http = new HttpClient() { BaseAddress = new Uri(BaseAddr) };

        XCDEItems           = (await Http.GetFromJsonAsync<IEnumerable<ItemLookup>>("DataLookup/XCDE/Items.json") ?? new ItemLookup[0]).ToDictionary(x => x.ID, x => x);
        XCDECrystalElements = (await Http.GetFromJsonAsync<IEnumerable<GenericDataLookup>>("DataLookup/XCDE/CrystalElement.json") ?? new GenericDataLookup[0]).ToDictionary(x => x.ID, x => x);
        XCDECrystalRanks    = (await Http.GetFromJsonAsync<IEnumerable<GenericDataLookup>>("DataLookup/XCDE/CrystalRank.json") ?? new GenericDataLookup[0]).ToDictionary(x => x.ID, x => x);
        XCDECrystalBuffs    = (await Http.GetFromJsonAsync<IEnumerable<CrystalBuffLookup>>("DataLookup/XCDE/CrystalBuffs.json") ?? new CrystalBuffLookup[0]).ToDictionary(x => x.ID, x => x);
        XCDEArts            = (await Http.GetFromJsonAsync<IEnumerable<ArtsLookup>>("DataLookup/XCDE/Arts.json") ?? new ArtsLookup[0]).ToDictionary(x => x.ID, x => x);
        XCDESkills          = (await Http.GetFromJsonAsync<IEnumerable<SkillLookup>>("DataLookup/XCDE/Skills.json") ?? new SkillLookup[0]).ToDictionary(x => x.ID, x => x);
        XCDEHearttoHearts   = (await Http.GetFromJsonAsync<IEnumerable<GenericDataLookup>>("DataLookup/XCDE/HearttoHearts.json") ?? new GenericDataLookup[0]).ToDictionary(x => x.ID, x => x);
        XCDEPartyAffinity   = (await Http.GetFromJsonAsync<IEnumerable<GenericDataLookup>>("DataLookup/XCDE/PartyAffinity.json") ?? new GenericDataLookup[0]).ToDictionary(x => x.ID, x => x);
        XCDEAffinityLinks   = (await Http.GetFromJsonAsync<IEnumerable<AffinityLinkLookup>>("DataLookup/XCDE/AffinityLinks.json") ?? new AffinityLinkLookup[0]).ToDictionary(x => x.ID, x => x);
        XCDEAchievements    = (await Http.GetFromJsonAsync<IEnumerable<AchievementLookup>>("DataLookup/XCDE/Achievements.json") ?? new AchievementLookup[0]).ToDictionary(x => x.ID, x => x);
        XCDECollectopaedia  = (await Http.GetFromJsonAsync<IEnumerable<CollectopaediaLookup>>("DataLookup/XCDE/Collectopaedia.json") ?? new CollectopaediaLookup[0]).ToDictionary(x => x.ID, x => x);
        XCDETimeAttack      = (await Http.GetFromJsonAsync<IEnumerable<TimeAttackLookup>>("DataLookup/XCDE/TimeAttack.json") ?? new TimeAttackLookup[0]).ToDictionary(x => x.ID, x => x);

        for (int i = 1; i < 9; i++)
        {
            XCDESkillTreesLookup.Add((Character)i, (await Http.GetFromJsonAsync<IEnumerable<GenericDataLookup>>($"DataLookup/XCDE/SkillTreePc{i.ToString("D2")}.json") ?? new GenericDataLookup[0]).ToDictionary(x => x.ID, x => x));
        }

        for (int i = 1; i < 16; i++)
        {
            XCDEWeaponCosmeticsLookup.Add((Character)i, (await Http.GetFromJsonAsync<IEnumerable<GenericDataLookup>>($"DataLookup/XCDE/StyleWeaponPc{i.ToString("D2")}.json") ?? new GenericDataLookup[0]).ToDictionary(x => x.ID, x => x));
            XCDEArmorCosmeticsLookup.Add((Character)i, (await Http.GetFromJsonAsync<IEnumerable<ArmorCosmeticLookup>>($"DataLookup/XCDE/StyleArmorPc{i.ToString("D2")}.json") ?? new ArmorCosmeticLookup[0]).ToDictionary(x => x.ID, x => x));
            if (i == 8)
                i = 13;
        }

        Console.WriteLine("DataLookupService Loaded.");
    }

    public void XCDERegenItemIdxLookup(dmm.XenoSave.XCDE.Item[] _items, dmm.XenoSave.XCDE.ItemType _type)
    {
        XCDEItemIdxLookup[_type] = new();
        var items = _items.Where(x => x.Exists && !x.FullID.IsEmpty(_type == ItemType.Gem));
        foreach (var item in items)
            XCDEItemIdxLookup[_type].Add(item.Index, new GenericDataLookup()
            {
                ID   = item.Index,
                Name = _type == ItemType.Gem
                        ? $"{(item.FullID.ID == 0 ? XCDECrystalBuffs[((CrystalItem)item).Buffs[0].BuffID].Name : XCDEItems[item.FullID.ID].Name)} [SerialNo: {item.SerialNo}]"
                        : $"{XCDEItems[item.FullID.ID].Name} [SerialNo: {item.SerialNo}]"
                        
            });
    }

    public void XCDERegenGemIdxLookup(dmm.XenoSave.XCDE.CrystalItem[] _items)
    {
        XCDEItemIdxLookup[ItemType.Gem] = new();
        var items = _items.Where(x => x.Exists && !x.FullID.IsEmpty(true));
        foreach (var item in items)
            XCDEItemIdxLookup[ItemType.Gem].Add(item.Index, new GenericDataLookup()
            { 
                ID   = item.Index,
                Name = $"{(item.FullID.ID == 0 ? XCDECrystalBuffs[item.Buffs[0].BuffID].Name : XCDEItems[item.FullID.ID].Name)} [SerialNo: {item.SerialNo}]"
            });
    }

    public void XCDEUpdateSingleItemIdxLookup(dmm.XenoSave.XCDE.Item _item, dmm.XenoSave.XCDE.ItemType _type)
    {
        XCDEItemIdxLookup[_type][_item.Index] = new GenericDataLookup()
        {
            ID   = _item.Index,
            Name = _type == ItemType.Gem
                    ? $"{XCDEItems[_item.FullID.ID].Name} [SerialNo: {_item.SerialNo}]"
                    : $"{(_item.FullID.ID == 0 ? XCDECrystalBuffs[((CrystalItem)_item).Buffs[0].BuffID].Name : XCDEItems[_item.FullID.ID].Name)} [SerialNo: {_item.SerialNo}]"
        };
    }
}