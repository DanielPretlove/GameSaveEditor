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
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using XenoSaveCheat.Services;
using BlazorDownloadFile;
using System.Text;

namespace XenoSaveCheat.Pages;

public partial class Index
{
    public string _filePath = "";
    public string _exception = "";
    public string thumbBase64 = "";

    [Inject]
    private dmm.XenoSaveCheatCore.Core Core { get; set; } = null!;
    [Inject]
    private DataLookupService DLS { get; set; } = null!;
    [Inject]
    private IBlazorDownloadFileService BlazorDownloadFileService { get; set; } = null!;
    
    private Dictionary<string, string> info = new();
    private List<string> fileNames = new();

    protected override void OnInitialized()
    {
        Core.FilesAllLoaded += OnFilesLoaded;
        UpdateFileinfo();
    }

    private async Task LoadFiles(IReadOnlyList<IBrowserFile> files)
    {
        Dictionary<string, byte[]> fileSet = new Dictionary<string, byte[]>();
        foreach (IBrowserFile f in files)
        {
            long len = f.Size;
            byte[] data = new byte[len];
            await f.OpenReadStream(2097152).ReadAsync(data, 0, (int)len);
            
            fileSet.Add(f.Name, data);
        }
        Core.LoadMultiple(fileSet);
    }

    private void OnFilesLoaded(object? sender, EventArgs e) => UpdateFileinfo();

    private void UpdateFileinfo() => info = Core.GetLoadedFileInfo();

    private async Task SaveOne(string file) => await BlazorDownloadFileService.DownloadFile(file, Core.SaveOne(file), "application/octet-stream");

    private async Task SaveAllAsZip() => await BlazorDownloadFileService.DownloadFile($"XSC-output_{DateTime.Now.ToString("yyyy-MM-dd_HHmmss")}.zip", Core.SaveAllAsZip(), "application/octet-stream");

    private async Task ExportOne(string file) => await BlazorDownloadFileService.DownloadFile(file.Replace(".sav", ".json"), Encoding.Unicode.GetBytes(Core.ExportOne(file)), "application/json");
}