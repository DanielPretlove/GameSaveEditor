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

using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Forms;

namespace XenoSaveCheat.Services;

public class SaveFileService : ISaveFileService
{
    private readonly HttpClient _httpClient;
    private static int MAX_FILE_SIZE = 2097152;
    private static string API_BASE = "api/XenoSaveCheat/";

    public SaveFileService(HttpClient httpClient) => _httpClient = httpClient;
    public async Task<List<string>> SendFilesToCore(IReadOnlyList<IBrowserFile> files)
    {
        using var content = new MultipartFormDataContent();
        foreach (var f in files)
        {
            var fileContent = new StreamContent(f.OpenReadStream(MAX_FILE_SIZE));
            fileContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            content.Add(fileContent, "\"files\"", f.Name);
        }

        var response = await _httpClient.PostAsync($"{API_BASE}files", content);
        Console.WriteLine("files loaded");
        return await response.Content.ReadFromJsonAsync<List<string>>() ?? new();
    }

    public async Task<Dictionary<string, string>> GetLoadedFilesInfo()
    {
        return await _httpClient.GetFromJsonAsync<Dictionary<string, string>>($"{API_BASE}files/info") ?? new();
    }

    public string GetFilesDownloadUrl()
    {
        return $"{_httpClient.BaseAddress}{API_BASE}files";
    }

    public string GetSingleFileDownloadUrl(string file)
    {
        return $"{_httpClient.BaseAddress}{API_BASE}{file}";
    }

    public async Task<string> GetSingleFileType(string file)
    {
        return await _httpClient.GetFromJsonAsync<string>($"{API_BASE}{file}/type") ?? string.Empty;
    }
}