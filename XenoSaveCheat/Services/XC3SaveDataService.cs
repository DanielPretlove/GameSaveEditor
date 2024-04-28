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
using XenoSaveCheat.Dto.XC3;

namespace XenoSaveCheat.Services;

public class XC3SaveDataService : IXC3SaveDataService
{
    private readonly HttpClient _httpClient;
    private static string API_BASE = "api/XenoSaveCheat/";

    public XC3SaveDataService(HttpClient httpClient) => _httpClient = httpClient;
    public async Task<BasicDataDto> GetBasicData(string file)
    {
        BasicDataDto result = new();

        result.Money = await _httpClient.GetFromJsonAsync<int>($"{API_BASE}{file}/{nameof(result.Money)}");
        result.Noponstone = await _httpClient.GetFromJsonAsync<int>($"{API_BASE}{file}/{nameof(result.Noponstone)}");

        return result;
    }

    public async Task UpdateBasicData(string file, BasicDataDto basicData)
    {
        foreach (PropertyInfo pi in basicData.GetType().GetProperties())
        {
            var input = new StringContent(JsonSerializer.Serialize(pi.GetValue(basicData, null)), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{API_BASE}{file}/{pi.Name}", input);
            Console.WriteLine(pi.Name);
        }
    }
}