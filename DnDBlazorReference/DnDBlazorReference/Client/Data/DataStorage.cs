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

namespace DnDBlazorReference.Client.Data
{
    public class DataStorage
    {
        [Inject]
        public HttpClient Http { get; set; }

        public class Monster5eJson
        {
            [JsonPropertyName("monster")]
            public List<Monster5e> monsters { get; set; }
        }

        public class DnDToolsState
        {
            public List<Monster> Monsters { get; set; }
            public List<CreatureType> CreatureTypes { get; set; }

            public DnDToolsState()
            {
                Monsters = new List<Monster>();
                CreatureTypes = new List<CreatureType>();
            }
        }

        public DnDToolsState State { get; private set; }

        public DataStorage()
        {
            State = null;
        }

        public async Task LoadState(HttpClient client)
        {
            if (State != null)
            {
                return;
            }

            var options = new JsonSerializerOptions
            {
                ReadCommentHandling = JsonCommentHandling.Skip,
                AllowTrailingCommas = true
            };

            State = await client.GetFromJsonAsync<DnDToolsState>("system-data/system-state.json", options);

            if (State == null)
            {
                State = new DnDToolsState();
            }
        }

        public void StoreMonsters(List<Monster5e> monsters)
        {
            foreach (var monster5e in monsters)
            {
                TryStoreMonster(monster5e);
            }
        }

        private void TryStoreMonster(Monster5e monster5e)
        {
            Monster monster = monster5e.ConvertToUsable();
            CreatureType creatureType = monster5e.CreatureType.ConvertToUsable();

            if (!State.Monsters.Contains(monster))
            {
                State.Monsters.Add(monster);
            }

            if (!State.CreatureTypes.Contains(creatureType))
            {
                State.CreatureTypes.Add(creatureType);
            }
        }
    }
}
