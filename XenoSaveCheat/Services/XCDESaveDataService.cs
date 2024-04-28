/*
 * XenoSaveCheat
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

using System.Net.Http.Json;
using System.Reflection;
using System.Text;
using System.Text.Json;
using dmm.XenoSave.XCDE;
using XenoSaveCheat.Dto.XCDE;

namespace XenoSaveCheat.Services;

public class XCDESaveDataService : IXCDESaveDataService
{
    private readonly HttpClient _httpClient;
    private static string API_BASE = "api/XenoSaveCheat/";

    public XCDESaveDataService(HttpClient httpClient) => _httpClient = httpClient;
    public async Task<BasicDataDto> GetBasicData(string file)
    {
        
        if (!String.IsNullOrEmpty(file))
            try
            {
                return new BasicDataDto()
                {
                    Money = await _httpClient.GetFromJsonAsync<int>($"{API_BASE}{file}/Money"),
                    Noponstone = await _httpClient.GetFromJsonAsync<int>($"{API_BASE}{file}/Noponstone")
                };
            }
            catch (Exception)
            {
                return new BasicDataDto();
            }
        else
            return new BasicDataDto();
    }

    public async Task<PartyDto> GetPartyData(string file)
    {
        if (!String.IsNullOrEmpty(file))
            try
            {
                return new PartyDto()
                {
                    Party = await _httpClient.GetFromJsonAsync<Party>($"{API_BASE}{file}/Party") ?? throw new Exception($"Error Fetching {file}:Party")
                };
            }
            catch (Exception)
            {
                return new PartyDto();
            }
        else
            return new PartyDto();
    }

    public async Task<InventoryDto> GetInvData(string file)
    {
        if (!String.IsNullOrEmpty(file))
            try
            {
                return new InventoryDto()
                {
                    Weapons = await _httpClient.GetFromJsonAsync<List<EquipItem>>($"{API_BASE}{file}/Weapons") ?? throw new Exception($"Error Fetching {file}:Weapons"),
                    HeadArmour = await _httpClient.GetFromJsonAsync<List<EquipItem>>($"{API_BASE}{file}/HeadArmour") ?? throw new Exception($"Error Fetching {file}:HeadArmour"),
                    TorsoArmour = await _httpClient.GetFromJsonAsync<List<EquipItem>>($"{API_BASE}{file}/TorsoArmour") ?? throw new Exception($"Error Fetching {file}:TorsoArmour"),
                    ArmArmour = await _httpClient.GetFromJsonAsync<List<EquipItem>>($"{API_BASE}{file}/ArmArmour") ?? throw new Exception($"Error Fetching {file}:ArmArmour"),
                    LegArmour = await _httpClient.GetFromJsonAsync<List<EquipItem>>($"{API_BASE}{file}/LegArmour") ?? throw new Exception($"Error Fetching {file}:LegArmour"),
                    FootArmour = await _httpClient.GetFromJsonAsync<List<EquipItem>>($"{API_BASE}{file}/FootArmour") ?? throw new Exception($"Error Fetching {file}:FootArmour"),
                    Crystals = await _httpClient.GetFromJsonAsync<List<CrystalItem>>($"{API_BASE}{file}/Crystals") ?? throw new Exception($"Error Fetching {file}:Crystals"),
                    Gems = await _httpClient.GetFromJsonAsync<List<CrystalItem>>($"{API_BASE}{file}/Gems") ?? throw new Exception($"Error Fetching {file}:Gems"),
                    Collectables = await _httpClient.GetFromJsonAsync<List<Item>>($"{API_BASE}{file}/Collectables") ?? throw new Exception($"Error Fetching {file}:Collectables"),
                    Materials = await _httpClient.GetFromJsonAsync<List<Item>>($"{API_BASE}{file}/Materials") ?? throw new Exception($"Error Fetching {file}:Materials"),
                    KeyItems = await _httpClient.GetFromJsonAsync<List<Item>>($"{API_BASE}{file}/KeyItems") ?? throw new Exception($"Error Fetching {file}:KeyItems"),
                    ArtsManuals = await _httpClient.GetFromJsonAsync<List<Item>>($"{API_BASE}{file}/ArtsManuals") ?? throw new Exception($"Error Fetching {file}:ArtsManuals")
                };
            }
            catch (Exception)
            {
                return new InventoryDto();
            }
        else
            return new InventoryDto();
    }

    public async Task SendSimpleDto<T>(string file, T dto)
    {
        if (dto != null)
            foreach (PropertyInfo pi in dto.GetType().GetProperties())
            {
                var input = new StringContent(JsonSerializer.Serialize(pi.GetValue(dto, null)), Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"{API_BASE}{file}/{pi.Name}", input);
                Console.WriteLine($"Data for {file}:{pi.Name} sent to API");
            }
        else
            throw new Exception("Supplied DTO is null");
    }

    public async Task<CharacterDto> GetCharData(string file)
    {
        if (!String.IsNullOrEmpty(file))
            try
            {
                return new CharacterDto()
                {
                    PartyMembers = await _httpClient.GetFromJsonAsync<List<PartyMember>>($"{API_BASE}{file}/PartyMembers") ?? throw new Exception($"Error Fetching {file}:Characters"),
                };
            }
            catch (Exception)
            {
                return new CharacterDto();
            }
        else
            return new CharacterDto();
    }
}