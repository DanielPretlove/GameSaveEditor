/*
 * XenoSaveCheatREST
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

using Microsoft.AspNetCore.Mvc;
using System;  
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using dmm.XenoSaveCheatCore;
using dmm.XenoSave.XCDE;

namespace dmm.XenoSaveCheatREST.Controllers
{
    [ApiController]
    public class XenoSaveCheatController : BaseApiController
    {
        private readonly ILogger<XenoSaveCheatController> _logger;
        private readonly Core _cheatCore;

        public XenoSaveCheatController(ILogger<XenoSaveCheatController> logger, Core cheatCore)
        {
            _logger = logger;
            _cheatCore = cheatCore;
        }

        [HttpGet("{file}")]
        public IActionResult DownloadSingleFile(string file) => File(_cheatCore.SaveOne(file), "application/octet-stream", file);

        [HttpGet("files")]
        public IActionResult DownloadAllFilesZIP()
        {
            string zipName = $"XSC-output_{DateTime.Now.ToString("yyyy-MM-dd_HHmmss")}.zip";
            return File(_cheatCore.SaveAllAsZip(), "application/zip", zipName);
        }    

        [HttpPost("files")]
        public ActionResult<List<string>> UploadFiles(List<IFormFile> files)
        {
            List<string> results = new List<string>();

            foreach (var file in files)
                if (file.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        file.CopyTo(ms);
                        _cheatCore.LoadOne(file.FileName, ms.ToArray());
                    }
                    results.Add(file.FileName);
                }

            return results;
        }
        [HttpGet("files/json")]
        public IActionResult GetAllFilesJSON() => new JsonResult(_cheatCore.SaveAll());
        [HttpPost("files/json")]
        public IActionResult UploadNewFilesFromJSON([FromBody] Dictionary<string, byte[]> files)
        {
            _cheatCore.LoadMultiple(files);
            return new JsonResult(null);
        }

        [HttpGet("files/info")]
        public IActionResult GetLoadedFileInfo()
        {
            Dictionary<string, string> result = _cheatCore.GetLoadedFileInfo();
            if (result.Count > 0)
            {
                foreach (var kv in result)
                    Console.WriteLine($"Key:{kv.Key}\t Value:{kv.Value}");
            }
            else
                Console.WriteLine("SORRY NOTHING");
            return new JsonResult(_cheatCore.GetLoadedFileInfo());
        }

        [HttpGet("{file}/propnames")]
        public IActionResult GetPropNames(string file) => new JsonResult(_cheatCore.GetPropNames(file));

        [HttpGet("{file}/property/{property}")]
        public IActionResult GetProp(string file, string property) => new JsonResult(_cheatCore.GetProp(file, property));
        [HttpPut("{file}/property/{property}")]
        public IActionResult SetProp(string file, string property, [FromBody] object value) => new JsonResult(_cheatCore.SetProp(file, property, value));

        [HttpGet("{file}/type")]
        public IActionResult GetFileType(string file) => new JsonResult(_cheatCore.GetFileType(file));

        #region XCDESave

        #region Basic
        [HttpGet("{file}/Money")]
        public IActionResult XCDEGetMoney(string file) => new JsonResult(_cheatCore.GetProp(file, "Money"));

        [HttpPut("{file}/Money")]
        public IActionResult XCDESetMoney(string file, [FromBody] uint value) => new JsonResult(_cheatCore.XCDESetMoney(file, value));

        [HttpGet("{file}/Noponstone")]
        public IActionResult XCDEGetNoponstone(string file) => new JsonResult(_cheatCore.GetProp(file, "Noponstones"));

        [HttpPut("{file}/Noponstone")]
        public IActionResult XCDESetNoponstone(string file, [FromBody] uint value) => new JsonResult(_cheatCore.XCDESetNoponstone(file, value));

        [HttpGet("{file}/AllBasicData")]
        public IActionResult XCDEGetAllBasicData(string file) => new JsonResult(_cheatCore.XCDEGetAllBasicData(file));

        // [HttpPut("{file}/AllBasicData")]
        // public IActionResult XCDESetAllBasicData(string file, [FromBody] Dictionary<string, object> value) => new JsonResult(_cheatCore.XCDESetAllBasicData(file, value));
        #endregion

        #region Items

        [HttpGet("{file}/Weapons")]
        public IActionResult XCDEGetAllWeapons(string file) => new JsonResult(_cheatCore.XCDEGetItemBox(file, ItemType.Weapon));

        [HttpGet("{file}/HeadArmour")]
        public IActionResult XCDEGetAllHeadArmour(string file) => new JsonResult(_cheatCore.XCDEGetItemBox(file, ItemType.HeadArmour));

        [HttpGet("{file}/TorsoArmour")]
        public IActionResult XCDEGetAllTorsoArmour(string file) => new JsonResult(_cheatCore.XCDEGetItemBox(file, ItemType.TorsoArmour));

        [HttpGet("{file}/ArmArmour")]
        public IActionResult XCDEGetAllArmArmour(string file) => new JsonResult(_cheatCore.XCDEGetItemBox(file, ItemType.ArmArmour));

        [HttpGet("{file}/LegArmour")]
        public IActionResult XCDEGetAllLegArmour(string file) => new JsonResult(_cheatCore.XCDEGetItemBox(file, ItemType.LegArmour));

        [HttpGet("{file}/FootArmour")]
        public IActionResult XCDEGetAllFootArmour(string file) => new JsonResult(_cheatCore.XCDEGetItemBox(file, ItemType.FootArmour));

        [HttpGet("{file}/Crystals")]
        public IActionResult XCDEGetAllCrystals(string file) => new JsonResult(_cheatCore.XCDEGetItemBox(file, ItemType.Crystal));

        [HttpGet("{file}/Gems")]
        public IActionResult XCDEGetAllGems(string file) => new JsonResult(_cheatCore.XCDEGetItemBox(file, ItemType.Gem));

        [HttpGet("{file}/Collectables")]
        public IActionResult XCDEGetAllCollectables(string file) => new JsonResult(_cheatCore.XCDEGetItemBox(file, ItemType.Collectable));

        [HttpGet("{file}/Materials")]
        public IActionResult XCDEGetAllMaterials(string file) => new JsonResult(_cheatCore.XCDEGetItemBox(file, ItemType.Material));

        [HttpGet("{file}/KeyItems")]
        public IActionResult XCDEGetAllKeyItems(string file) => new JsonResult(_cheatCore.XCDEGetItemBox(file, ItemType.KeyItem));

        [HttpGet("{file}/ArtsManuals")]
        public IActionResult XCDEGetAllArtsManuals(string file) => new JsonResult(_cheatCore.XCDEGetItemBox(file, ItemType.ArtsManual));

        [HttpPut("{file}/Weapons")]
        public IActionResult XCDESetAllWeapons(string file, [FromBody] EquipItem[] value) => new JsonResult(_cheatCore.XCDESetItemBox(file, ItemType.Weapon, value));

        [HttpPut("{file}/HeadArmour")]
        public IActionResult XCDESetAllHeadArmour(string file, [FromBody] EquipItem[] value) => new JsonResult(_cheatCore.XCDESetItemBox(file, ItemType.HeadArmour, value));

        [HttpPut("{file}/TorsoArmour")]
        public IActionResult XCDESetAllTorsoArmour(string file, [FromBody] EquipItem[] value) => new JsonResult(_cheatCore.XCDESetItemBox(file, ItemType.TorsoArmour, value));

        [HttpPut("{file}/ArmArmour")]
        public IActionResult XCDESetAllArmArmour(string file, [FromBody] EquipItem[] value) => new JsonResult(_cheatCore.XCDESetItemBox(file, ItemType.ArmArmour, value));

        [HttpPut("{file}/LegArmour")]
        public IActionResult XCDESetAllLegArmour(string file, [FromBody] EquipItem[] value) => new JsonResult(_cheatCore.XCDESetItemBox(file, ItemType.LegArmour, value));

        [HttpPut("{file}/FootArmour")]
        public IActionResult XCDESetAllFootArmour(string file, [FromBody] EquipItem[] value) => new JsonResult(_cheatCore.XCDESetItemBox(file, ItemType.FootArmour, value));

        [HttpPut("{file}/Crystals")]
        public IActionResult XCDESetAllCrystals(string file, [FromBody] CrystalItem[] value) => new JsonResult(_cheatCore.XCDESetItemBox(file, ItemType.Crystal, value));

        [HttpPut("{file}/Gems")]
        public IActionResult XCDESetAllGems(string file, [FromBody] CrystalItem[] value) => new JsonResult(_cheatCore.XCDESetItemBox(file, ItemType.Gem, value));

        [HttpPut("{file}/Collectables")]
        public IActionResult XCDESetAllCollectables(string file, [FromBody] Item[] value) => new JsonResult(_cheatCore.XCDESetItemBox(file, ItemType.Collectable, value));

        [HttpPut("{file}/Materials")]
        public IActionResult XCDESetAllMaterials(string file, [FromBody] Item[] value) => new JsonResult(_cheatCore.XCDESetItemBox(file, ItemType.Material, value));

        [HttpPut("{file}/KeyItems")]
        public IActionResult XCDESetAllKeyItems(string file, [FromBody] Item[] value) => new JsonResult(_cheatCore.XCDESetItemBox(file, ItemType.KeyItem, value));

        [HttpPut("{file}/ArtsManuals")]
        public IActionResult XCDESetAllArtsManuals(string file, [FromBody] Item[] value) => new JsonResult(_cheatCore.XCDESetItemBox(file, ItemType.ArtsManual, value));

        [HttpGet("{file}/Weapons/{idx}")]
        public IActionResult XCDEGetWeapon(string file, uint idx) => new JsonResult(_cheatCore.XCDEGetItem(file, ItemType.Weapon, idx));
        [HttpGet("{file}/HeadArmour/{idx}")]
        public IActionResult XCDEGetHeadArmour(string file, uint idx) => new JsonResult(_cheatCore.XCDEGetItem(file, ItemType.HeadArmour, idx));
        [HttpGet("{file}/TorsoArmour/{idx}")]
        public IActionResult XCDEGetTorsoArmour(string file, uint idx) => new JsonResult(_cheatCore.XCDEGetItem(file, ItemType.TorsoArmour, idx));
        [HttpGet("{file}/ArmArmour/{idx}")]
        public IActionResult XCDEGetArmArmour(string file, uint idx) => new JsonResult(_cheatCore.XCDEGetItem(file, ItemType.ArmArmour, idx));
        [HttpGet("{file}/LegArmour/{idx}")]
        public IActionResult XCDEGetLegArmour(string file, uint idx) => new JsonResult(_cheatCore.XCDEGetItem(file, ItemType.LegArmour, idx));
        [HttpGet("{file}/FootArmour/{idx}")]
        public IActionResult XCDEGetFootArmour(string file, uint idx) => new JsonResult(_cheatCore.XCDEGetItem(file, ItemType.FootArmour, idx));
        [HttpGet("{file}/Crystals/{idx}")]
        public IActionResult XCDEGetCrystal(string file, uint idx) => new JsonResult(_cheatCore.XCDEGetItem(file, ItemType.Crystal, idx));
        [HttpGet("{file}/Gems/{idx}")]
        public IActionResult XCDEGetGem(string file, uint idx) => new JsonResult(_cheatCore.XCDEGetItem(file, ItemType.Gem, idx));
        [HttpGet("{file}/Collectables/{idx}")]
        public IActionResult XCDEGetCollectable(string file, uint idx) => new JsonResult(_cheatCore.XCDEGetItem(file, ItemType.Collectable, idx));
        [HttpGet("{file}/Materials/{idx}")]
        public IActionResult XCDEGetMaterial(string file, uint idx) => new JsonResult(_cheatCore.XCDEGetItem(file, ItemType.Material, idx));
        [HttpGet("{file}/KeyItems/{idx}")]
        public IActionResult XCDEGetKeyItem(string file, uint idx) => new JsonResult(_cheatCore.XCDEGetItem(file, ItemType.KeyItem, idx));
        [HttpGet("{file}/ArtsManuals/{idx}")]
        public IActionResult XCDEGetArtsManual(string file, uint idx) => new JsonResult(_cheatCore.XCDEGetItem(file, ItemType.ArtsManual, idx));
        #endregion

        #region Party
        [HttpGet("{file}/AllPartyData")]
        public IActionResult XCDEGetAllPartyData(string file) => new JsonResult(_cheatCore.XCDEGetAllPartyData(file));
        [HttpGet("{file}/Party")]
        public IActionResult XCDEGetParty(string file) => new JsonResult(_cheatCore.GetProp(file, "Party"));
        [HttpGet("{file}/PartyMembers")]
        public IActionResult XCDEGetAllPartyMembers(string file) => new JsonResult(_cheatCore.GetProp(file, "PartyMembers"));
        [HttpGet("{file}/PartyMembers/{idx}")]
        public IActionResult XCDEGetSinglePartyMember(string file, uint idx) => new JsonResult(_cheatCore.XCDEGetPartyMember(file, idx));
        [HttpGet("{file}/ArtsLevels")]
        public IActionResult XCDEGetAllArtsLevels(string file) => new JsonResult(_cheatCore.GetProp(file, "ArtsLevels"));
        [HttpGet("{file}/ArtsLevels/{idx}")]
        public IActionResult XCDEGetSingleArtsLevel(string file, uint idx) => new JsonResult(_cheatCore.XCDEGetSingleArtsLevel(file, idx));

        [HttpPut("{file}/Party")]
        public IActionResult XCDESetParty(string file, [FromBody] Party value) => new JsonResult(_cheatCore.XCDESetParty(file, value));
        [HttpPut("{file}/PartyMembers")]
        public IActionResult XCDESetPartyMembers(string file, [FromBody] PartyMember[] value) => new JsonResult(_cheatCore.XCDESetAllPartyMembers(file, value));
        #endregion

        #region Unknown
        [HttpGet("{file}/AllUnknownData")]
        public IActionResult XCDEGetAllIUnknownData(string file) => new JsonResult(_cheatCore.XCDEGetAllUnknownData(file));
        [HttpGet("{file}/Unk_0x000000")]
        public IActionResult XCDEGetUnk_0x000000(string file) => new JsonResult(_cheatCore.GetProp(file, "Unk_0x000000"));

        #endregion

        #endregion
    }
}