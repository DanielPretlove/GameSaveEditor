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
using Microsoft.AspNetCore.Components.Forms;
using XenoSaveCheat.Dto.XC3;

namespace XenoSaveCheat.Services;

public class XCSaveThumbnailService : IXCSaveThumbnailService
{
    private readonly HttpClient _httpClient;
    private static string API_BASE = "api/XenoSaveCheat/";

    public XCSaveThumbnailService(HttpClient httpClient) => _httpClient = httpClient;
    
    public async Task<string> GetImageBase64(string file)
    {
        byte[]? RawBMP;

        if (!String.IsNullOrEmpty(file))
        {
            try
            {
                RawBMP = await _httpClient.GetFromJsonAsync<byte[]>($"{API_BASE}{file}/property/BMP");
                return RawBMP != null ? string.Format("data:image/bmp;base64,{0}",Convert.ToBase64String(RawBMP)) : "";
            }
            catch (Exception e)   // If fetching Image data fails then just return empty string
            {
                Console.WriteLine(e.StackTrace);
                return string.Empty;
            }
        }
        else
        {
            return string.Empty;
        }
    }
}
