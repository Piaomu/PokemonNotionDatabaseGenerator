using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonJsonIterator
{
    public class AbilityInput
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("dbSymbol")]
        public string DbSymbol { get; set; }

        [JsonProperty("textId")]
        public int? TextId { get; set; }
        [JsonProperty("abilityText")]
        public string? AbilityText { get; set; }

        [JsonProperty("klass")]
        public string Klass { get; set; }

    }
}
