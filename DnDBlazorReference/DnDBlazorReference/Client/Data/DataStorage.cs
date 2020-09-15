using DnDBlazorReference.Shared.Models.BinaricPox;
using DnDBlazorReference.Shared.Models.FiveETools;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
//using Tewr.Blazor.FileReader;
using System.Text;

namespace DnDBlazorReference.Client.Data
{
    public class DataStorage
    {
        [Inject]
        public HttpClient Http { get; set; }

        //[Inject]
        //public IFileReaderService FileReaderService { get; set; }

        public delegate void StateChangedDelegate();
        public event StateChangedDelegate StorageStateChanged;

        public class Monster5eJson
        {
            [JsonPropertyName("monster")]
            public List<Monster5e> monsters { get; set; }
        }

        public class DnDToolsState
        {
            public List<Monster> Monsters { get; set; }
            public List<CreatureType> CreatureTypes { get; set; }
            public List<HarvestedItem> HarvestedItems { get; set; }
            public List<TrinketTable> TrinketTables { get; set; }

            public DnDToolsState()
            {
                Monsters = new List<Monster>();
                CreatureTypes = new List<CreatureType>();
                HarvestedItems = new List<HarvestedItem>();
                TrinketTables = new List<TrinketTable>();
            }

            public void AppendState(DnDToolsState otherState)
            {
                Monsters.AddRange(otherState.Monsters);
                CreatureTypes.AddRange(otherState.CreatureTypes);
                HarvestedItems.AddRange(otherState.HarvestedItems);
                TrinketTables.AddRange(otherState.TrinketTables);
            }
        }

        private DnDToolsState State { get; set; }

        public Dictionary<string, Monster> Monsters { get; set; }
        public List<Monster> SortedMonsters
        {
            get
            {
                var sortedList = Monsters.Values.ToList();
                sortedList.Sort();
                return sortedList;
            }
        }
        public Dictionary<string, CreatureType> CreatureTypes { get; set; }
        public List<CreatureType> SortedCreatureTypes
        {
            get
            {
                var sortedList = CreatureTypes.Values.ToList();
                sortedList.Sort();
                return sortedList;
            }
        }
        public Dictionary<string, HarvestedItem> HarvestedItems { get; set; }
        public List<HarvestedItem> SortedHarvestedItems
        {
            get
            {
                var sortedList = HarvestedItems.Values.ToList();
                sortedList.Sort();
                return sortedList;
            }
        }
        public Dictionary<string, TrinketTable> TrinketTables { get; set; }
        public List<TrinketTable> SortedTrinketTables
        {
            get
            {
                var sortedList = TrinketTables.Values.ToList();
                sortedList.Sort();
                return sortedList;
            }
        }

        public bool IsLoaded { get; private set; }

        // storing
        public JsonSerializerOptions JsonSerializeOptions => new JsonSerializerOptions
        {
            WriteIndented = true,
            IgnoreReadOnlyProperties = true,
        };
        // loading in
        public JsonSerializerOptions JsonDeserializeOptions => new JsonSerializerOptions
        {
            ReadCommentHandling = JsonCommentHandling.Skip,
            AllowTrailingCommas = true,
            IgnoreReadOnlyProperties = true,
            IgnoreNullValues = true,
        };

        public DataStorage()
        {
            State = null;

            Monsters = new Dictionary<string, Monster>();
            CreatureTypes = new Dictionary<string, CreatureType>();
            HarvestedItems = new Dictionary<string, HarvestedItem>();
            TrinketTables = new Dictionary<string, TrinketTable>();

            IsLoaded = false;
        }

        public void LoadState(DnDToolsState initialState)
        {
            if (State == null)
            {
                State = initialState;
            }
            else
            {
                State.AppendState(initialState);
            }

            State.HarvestedItems.ForEach(hItem => HarvestedItems.TryAdd(hItem.Name.ToLower(), hItem));
            State.CreatureTypes.ForEach(cType => CreatureTypes.TryAdd(cType.ToString().ToLower(), cType));
            State.TrinketTables.ForEach(tTable => TrinketTables.TryAdd(tTable.TrinketTableType.ToLower(), tTable));
            State.Monsters.ForEach(mon => Monsters.TryAdd(mon.Name.ToLower(), mon));

            UpdateDictionaries();

            IsLoaded = true;

            StorageStateChanged?.Invoke();
        }

        public void Store5eMonsters(List<Monster5e> monsters)
        {
            foreach (var monster5e in monsters)
            {
                TryStore5eMonster(monster5e);
            }

            UpdateDictionaries();
        }

        private void TryStore5eMonster(Monster5e monster5e)
        {
            // ignore NPCs for now
            if (monster5e.IsNPC || monster5e.HasCopyDirective) return;

            try
            {
                Monster monster = null;
                CreatureType creatureType = null;

                monster = monster5e.ConvertToUsable();
                creatureType = monster5e.CreatureType.ConvertToUsable();

                CreatureTypes.TryAdd(creatureType.ToString().ToLower(), creatureType);

                if (Monsters.TryGetValue(monster.Name.ToLower(), out Monster storedMonster))
                {
                    storedMonster.CopyInMissing(monster);
                }
                else
                {
                    Monsters.TryAdd(monster.Name.ToLower(), monster);
                }

                UpdateDictionaries();
            }
            catch (Exception e)
            {
                throw new Exception($"Damn it!! {monster5e.Name}");
            }
        }

        public void UpdateDictionaries()
        {
            foreach (Monster mon in Monsters.Values)
            {
                foreach (HarvestingTableRow htRow in mon.HarvestingTable.Rows)
                {
                    if (!string.IsNullOrEmpty(htRow.ItemNameRef)
                        && HarvestedItems.TryGetValue(htRow.ItemNameRef.ToLower(), out HarvestedItem hItem))
                    {
                        htRow.Item = hItem;
                    }
                }

                if (!string.IsNullOrEmpty(mon.CreatureTypeRef)
                    && CreatureTypes.TryGetValue(mon.CreatureTypeRef.ToLower(), out CreatureType cType))
                {
                    mon.CreatureType = cType;
                }

                if (!string.IsNullOrEmpty(mon.TrinketTableType)
                    && TrinketTables.TryGetValue(mon.TrinketTableType.ToLower(), out TrinketTable tTable))
                {
                    mon.TrinketTable = tTable;
                }
            }

            StorageStateChanged?.Invoke();
        }

        public string GetJsonState()
        {
            State = new DnDToolsState
            {
                Monsters = Monsters.Values.ToList(),
                CreatureTypes = CreatureTypes.Values.ToList(),
                HarvestedItems = HarvestedItems.Values.ToList(),
                TrinketTables = TrinketTables.Values.ToList(),
            };

            return JsonSerializer.Serialize<DnDToolsState>(State, JsonSerializeOptions);
        }

        //public async Task ProcessFile(IFileReference fileReference)
        //{
        //    using (var reader = await fileReference.OpenReadAsync())
        //    {
        //        try
        //        {
        //            DnDToolsState state = await JsonSerializer.DeserializeAsync<DnDToolsState>(reader, JsonDeserializeOptions);
        //            LoadState(state);
        //        }
        //        catch (Exception e)
        //        {
        //            throw e;
        //        }
        //    }
        //}

        //public async Task Process5eFile(IFileReference fileReference)
        //{
        //    using (var reader = await fileReference.OpenReadAsync())
        //    {
        //        Monster5eJson state = null;
        //        try
        //        {
        //            state = await JsonSerializer.DeserializeAsync<Monster5eJson>(reader, JsonDeserializeOptions);
        //        }
        //        catch (Exception e)
        //        {
        //            throw e;
        //        }

        //        try
        //        {
        //            if (state != null && state.monsters != null)
        //            {
        //                Store5eMonsters(state.monsters);
        //            }
        //        }
        //        catch (Exception e)
        //        {
        //            throw e;
        //        }
        //    }
        //}

        //public async Task<string> ProcessTSVFile(IFileReference fileReference)
        //{
        //    using (var reader = await fileReference.OpenReadAsync())
        //    {
        //        string fileAsString = string.Empty;

        //        int bufferSize = 1024;
        //        byte[] buffer = new byte[bufferSize];
        //        int readAmount = 0;
        //        while ((readAmount = await reader.ReadAsync(buffer, 0, bufferSize)) != 0)
        //        {
        //            string converted = Encoding.UTF8.GetString(buffer, 0, readAmount);

        //            fileAsString += converted;
        //        }

        //        return ProcessTSVLines(fileAsString);
        //    }
        //}

        private string ProcessTSVLines(string fileAsString)
        {
            string errorOutput = string.Empty;

            string[] lines = fileAsString.Split("\n");

            foreach (string line in lines)
            {
                string[] tokens = line.Split("\t");
                string monsterName = tokens[0].Trim();
                string cardSize = tokens[1].Trim();
                string source = tokens[2].Trim();
                string type = tokens[3].Trim();
                string cr = tokens[4].Trim();

                if (Monsters.TryGetValue(monsterName.ToLower(), out Monster monster))
                {
                    if (Enum.TryParse<ECardSize>(cardSize, true, out ECardSize result))
                    {
                        monster.ReferenceCardSize = result;
                    }
                    else if(string.IsNullOrEmpty(cardSize))
                    {
                        monster.ReferenceCardSize = ECardSize.None;
                    }
                    else
                    {
                        errorOutput += $"Failed to parse [{cardSize}] on [{monsterName}]\n";
                    }
                }
                else
                {
                    errorOutput += $"Failed to find [{monsterName}]\n";
                }
            }

            return errorOutput;
        }
    }
}
