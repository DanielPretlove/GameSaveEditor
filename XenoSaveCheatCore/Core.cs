/*
 * XenoSaveCheatCore
 * Copyright (C) 2022-2023  damysteryman
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

using System.IO.Compression;
using System.Reflection;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using dmm.XenoSave;
using dmm.XenoSave.XCDE;

namespace dmm.XenoSaveCheatCore;
public class Core
{
    public Dictionary<string, ISaveFile> SAVEDATA;

    public event EventHandler? FilesAllLoaded;

    public Core()
    {
        SAVEDATA = new Dictionary<string, ISaveFile>();
    }

    public void LoadOne(string name, byte[] data)
    {
        SAVEDATA.Add(name, SaveSerialization.Deserialize(data));
    }

    public void LoadMultiple(Dictionary<string, byte[]> data)
    {
        foreach (KeyValuePair<string, byte[]> file in data)
            LoadOne(file.Key, file.Value);
        FilesAllLoaded?.Invoke(this, EventArgs.Empty);
    }

    public void UnloadOne(string name, bool unloadThumb)
    {
        List<string> names = new();
        names.Add(name);
        if (unloadThumb)
            names.Add(name.Replace(".sav", ".tmb"));
        
        UnloadMultiple(names);
    }

    public void UnloadMultiple(List<string> names)
    {
        foreach (string name in names)
        {
            if (SAVEDATA.ContainsKey(name))
                SAVEDATA.Remove(name);
        }
        FilesAllLoaded?.Invoke(this, EventArgs.Empty);
    }

    public void UnloadAll()
    {
        SAVEDATA.Clear();
        FilesAllLoaded?.Invoke(this, EventArgs.Empty);
    }

    public byte[] SaveOne(string name) => SaveSerialization.Serialize(SAVEDATA[name]);

    public Dictionary<string, byte[]> SaveAll()
    {
        Dictionary<string, byte[]> data = new Dictionary<string, byte[]>();

        foreach (KeyValuePair<string, ISaveFile> save in SAVEDATA)
            data.Add(save.Key, SaveSerialization.Serialize(save.Value));

        return data;
    }

    public byte[] SaveAllAsZip()
    {
        Dictionary<string, byte[]> files = this.SaveAll();

        using (var ms = new MemoryStream())
        {
            using (var zip = new ZipArchive(ms, ZipArchiveMode.Create, true))
                foreach (KeyValuePair<string, byte[]> file in files)
                {
                    var zipEntry = zip.CreateEntry(file.Key, CompressionLevel.Fastest);
                    using (var zipStream = zipEntry.Open())
                        zipStream.Write(file.Value, 0, file.Value.Length);
                }

            return ms.ToArray();
        }
    }

    public string ExportOne(string name)
    {
        if (SAVEDATA.ContainsKey(name))
        {
            JsonSerializerOptions options = new()
            {
                WriteIndented = true,
                IgnoreReadOnlyProperties = true,
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
            };

            return JsonSerializer.Serialize(SAVEDATA[name], SAVEDATA[name].GetType(), options);
        }
        else
            throw new KeyNotFoundException($"{name} is not loaded in Core.");
    }

    public Dictionary<string, string> GetLoadedFileInfo() => SAVEDATA.ToDictionary(kv => kv.Key, kv => kv.Value.GetType().Name);

    public string GetFileType(string file) => SAVEDATA[file].GetType().Name;
    
    public List<string> GetPropNames(string file) => SAVEDATA[file]
                                                    .GetType()
                                                    .GetProperties()
                                                    .Select(p => p.Name)
                                                    .ToList<string>();

    public object? GetProp(string file, string property)
    {
        PropertyInfo? prop = SAVEDATA[file].GetType().GetProperty(property);

        return (prop != null) ? prop.GetValue(SAVEDATA[file], null) : null;
    }

    // Adapted from:
    // https://codereview.stackexchange.com/questions/102289/setting-the-value-of-properties-via-reflection
    public object? SetProp(string file, string property, object value)
    {
        Type ft = SAVEDATA[file].GetType();
        PropertyInfo? prop = ft.GetProperty(property);

        if (prop != null && prop.CanWrite)
        {
            Type pt = prop.PropertyType;
            Type dt = pt;

            // Check for nullable types
            if (pt.IsGenericType && pt.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                // Check for null or empty string value.
                if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
                {
                    prop.SetValue(SAVEDATA[file], null);
                    return null;
                }
                else
                    dt = pt.GetGenericArguments()[0];
            }

            value = Parse(dt, value.ToString() ?? "") ?? new object();
            prop.SetValue(SAVEDATA[file], value);
        }

        return null;
    }

    // Adapted from:
    // https://codereview.stackexchange.com/questions/102289/setting-the-value-of-properties-via-reflection
    private static object? Parse(Type type, string str)
    {
        try
        {
            var parse = type.GetMethod("Parse", new[] {typeof(string)});
            if (parse == null)
                throw new NotSupportedException();

            return parse.Invoke(null, new object[] { str });
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public dmm.XenoSave.XCDE.Item XCDEGetItem(string file, ItemType category, uint idx)
    {
        XCDESave save = SAVEDATA[file] as XCDESave ?? throw new Exception($"File {file} is null or missing.");
        switch (category)
        {
            case ItemType.Weapon:
                return save.Weapons[idx];
            case ItemType.HeadArmour:
                return save.HeadArmour[idx];
            case ItemType.TorsoArmour:
                return save.TorsoArmour[idx];
            case ItemType.ArmArmour:
                return save.ArmArmour[idx];
            case ItemType.LegArmour:
                return save.LegArmour[idx];
            case ItemType.FootArmour:
                return save.FootArmour[idx];
            case ItemType.Crystal:
                return save.Crystals[idx];
            case ItemType.Gem:
                return save.Gems[idx];
            case ItemType.Collectable:
                return save.Collectables[idx];
            case ItemType.Material:
                return save.Materials[idx];
            case ItemType.KeyItem:
                return save.KeyItems[idx];
            case ItemType.ArtsManual:
                return save.ArtsManuals[idx];
            default:
                throw new Exception($"XCDE ItemType {category.ToString()}");
        }
    }

    public dmm.XenoSave.XCDE.Item[] XCDEGetItemBox(string file, ItemType category)
    {
        XCDESave save = SAVEDATA[file] as XCDESave ?? throw new Exception($"File {file} is null or missing.");
        switch (category)
        {
            case ItemType.Weapon:
                return save.Weapons;
            case ItemType.HeadArmour:
                return save.HeadArmour;
            case ItemType.TorsoArmour:
                return save.TorsoArmour;
            case ItemType.ArmArmour:
                return save.ArmArmour;
            case ItemType.LegArmour:
                return save.LegArmour;
            case ItemType.FootArmour:
                return save.FootArmour;
            case ItemType.Crystal:
                return save.Crystals;
            case ItemType.Gem:
                return save.Gems;
            case ItemType.Collectable:
                return save.Collectables;
            case ItemType.Material:
                return save.Materials;
            case ItemType.KeyItem:
                return save.KeyItems;
            case ItemType.ArtsManual:
                return save.ArtsManuals;
            default:
                throw new Exception($"XCDE ItemType {category.ToString()}");
        }
    }

    public bool XCDESetItemBox(string file, ItemType category, Item[] value)
    {
        XCDESave save = SAVEDATA[file] as XCDESave ?? throw new Exception($"File {file} is null or missing.");
        switch(category)
        {
            case ItemType.Weapon:
                save.Weapons = (EquipItem[])value;
                return true;
            case ItemType.HeadArmour:
                save.HeadArmour = (EquipItem[])value;
                return true;
            case ItemType.TorsoArmour:
                save.TorsoArmour = (EquipItem[])value;
                return true;
            case ItemType.ArmArmour:
                save.ArmArmour = (EquipItem[])value;
                return true;
            case ItemType.LegArmour:
                save.LegArmour = (EquipItem[])value;
                return true;
            case ItemType.FootArmour:
                save.FootArmour = (EquipItem[])value;
                return true;
            case ItemType.Crystal:
                save.Crystals = (CrystalItem[])value;
                return true;
            case ItemType.Gem:
                save.Gems = (CrystalItem[])value;
                return true;
            case ItemType.Collectable:
                save.Collectables = value;
                return true;
            case ItemType.Material:
                save.Materials = value;
                return true;
            case ItemType.KeyItem:
                save.KeyItems = value;
                return true;
            case ItemType.ArtsManual:
                save.ArtsManuals = value;
                return true;
            default:
                return false;
        }
    }

    public dmm.XenoSave.XCDE.PartyMember XCDEGetPartyMember(string file, uint idx) => ((XCDESave)SAVEDATA[file]).PartyMembers[idx];

    public dmm.XenoSave.XCDE.ArtsLevel XCDEGetSingleArtsLevel(string file, uint idx) => ((XCDESave)SAVEDATA[file]).ArtsLevels[idx];

    public Dictionary<string, object> XCDEGetAllBasicData(string file)
    {
        Dictionary<string, object> result = new();
        XCDESave save = SAVEDATA[file] as XCDESave ?? throw new Exception($"File {file} is null or missing.");

        result.Add("Money", save.Money);
        result.Add("Noponstone", save.Noponstones);

        return result;
    }
    public Dictionary<string, object> XCDEGetAllPartyData(string file)
    {
        Dictionary<string, object> result = new();
        XCDESave save = SAVEDATA[file] as XCDESave ?? throw new Exception($"File {file} is null or missing.");

        result.Add("Party", save.Party);
        result.Add("PartyMembers", save.PartyMembers);
        result.Add("ArtsLevels", save.ArtsLevels);

        return result;
    }

    public Dictionary<string, object> XCDEGetAllUnknownData(string file)
    {
        Dictionary<string, object> result = new();
        XCDESave save = SAVEDATA[file] as XCDESave ?? throw new Exception($"File {file} is null or missing.");

        result.Add("Unk_0x000000", save.Unk_0x000000);
        result.Add("Unk_0x000018", save.Unk_0x000018);
        result.Add("Unk_0x03B5B0", save.Unk_0x03B5B0);
        result.Add("Unk_0x149BEC", save.Unk_0x149BEC);
        result.Add("Unk_0x151B44", save.Unk_0x151B44);
        result.Add("Unk_0x152331", save.Unk_0x152331);

        return result;
    }

    public bool XCDESetMoney(string file, uint value)
    {
        try
        {
            ((XCDESave)SAVEDATA[file]).Money = value;
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return false;
        }
    }

    public bool XCDESetNoponstone(string file, uint value)
    {
        try
        {
            ((XCDESave)SAVEDATA[file]).Noponstones = value;
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return false;
        }
    }

    public bool XCDESetParty(string file, Party value)
    {
        try
        {
            ((XCDESave)SAVEDATA[file]).Party = value;
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return false;
        }
    }

    public bool XCDESetPartyMember(string file, uint idx, PartyMember value)
    {
        try
        {
            ((XCDESave)SAVEDATA[file]).PartyMembers[idx] = value;
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return false;
        }
    }

    public bool XCDESetAllPartyMembers(string file, PartyMember[] value)
    {
        try
        {
            ((XCDESave)SAVEDATA[file]).PartyMembers = value;
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return false;
        }
    }
}
