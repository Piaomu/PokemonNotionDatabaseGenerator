using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonJsonIterator
{
        public class PokemonInput
        {
            [JsonProperty("klass")]
            public string? Klass { get; set; }

            [JsonProperty("id")]
            public int Id { get; set; }

            [JsonProperty("dbSymbol")]
            public string DbSymbol { get; set; }
            [JsonProperty("pokedexEntry")]
            public string? PokedexEntry { get; set; }

            [JsonProperty("forms")]
            public List<PokemonForm?> Forms { get; set; } = new List<PokemonForm?>();
        }
        public class Condition
        {
            [JsonProperty("type")]
            public string? Type { get; set; }

            [JsonProperty("value")]
            public object? Value { get; set; }
        }

        public class Evolution
        {
            [JsonProperty("conditions")]
            public List<Condition?>? Conditions { get; set; }

            [JsonProperty("dbSymbol")]
            public string? DbSymbol { get; set; }

            [JsonProperty("form")]
            public int? Form { get; set; }
        }

        public class PokemonForm
        {
            [JsonProperty("form")]
            public int Form { get; set; }

            [JsonProperty("height")]
            public double? Height { get; set; }

            [JsonProperty("weight")]
            public double? Weight { get; set; }

            [JsonProperty("type1")]
            public string? Type1 { get; set; }

            [JsonProperty("type2")]
            public string? Type2 { get; set; }

            [JsonProperty("baseHp")]
            public int? BaseHp { get; set; }

            [JsonProperty("baseAtk")]
            public int? BaseAtk { get; set; }

            [JsonProperty("baseDfe")]
            public int? BaseDfe { get; set; }

            [JsonProperty("baseSpd")]
            public int? BaseSpd { get; set; }

            [JsonProperty("baseAts")]
            public int? BaseAts { get; set; }

            [JsonProperty("baseDfs")]
            public int? BaseDfs { get; set; }

            [JsonProperty("evHp")]
            public int? EvHp { get; set; }

            [JsonProperty("evAtk")]
            public int? EvAtk { get; set; }

            [JsonProperty("evDfe")]
            public int? EvDfe { get; set; }

            [JsonProperty("evSpd")]
            public int? EvSpd { get; set; }

            [JsonProperty("evAts")]
            public int? EvAts { get; set; }

            [JsonProperty("evDfs")]
            public int? EvDfs { get; set; }

            [JsonProperty("evolutions")]
            public List<Evolution?>? Evolutions { get; set; }

            [JsonProperty("experienceType")]
            public int? ExperienceType { get; set; }

            [JsonProperty("baseExperience")]
            public int? BaseExperience { get; set; }

            [JsonProperty("baseLoyalty")]
            public int? BaseLoyalty { get; set; }

            [JsonProperty("catchRate")]
            public int? CatchRate { get; set; }

            [JsonProperty("femaleRate")]
            public float? FemaleRate { get; set; }

            [JsonProperty("breedGroups")]
            public List<int?>? BreedGroups { get; set; }

            [JsonProperty("hatchSteps")]
            public int? HatchSteps { get; set; }

            [JsonProperty("babyDbSymbol")]
            public string? BabyDbSymbol { get; set; }

            [JsonProperty("babyForm")]
            public int? BabyForm { get; set; }

            [JsonProperty("itemHeld")]
            public List<ItemHeld?>? ItemHeld { get; set; }

            [JsonProperty("abilities")]
            public List<string?>? Abilities { get; set; }

            [JsonProperty("frontOffsetY")]
            public int? FrontOffsetY { get; set; }

            [JsonProperty("resources")]
            public Resources? Resources { get; set; }

            [JsonProperty("moveSet")]
            public List<MoveSet?>? MoveSet { get; set; }
        }

        public class ItemHeld
        {
            [JsonProperty("dbSymbol")]
            public string? DbSymbol { get; set; }

            [JsonProperty("chance")]
            public int? Chance { get; set; }
        }

        public class MoveSet
        {
            [JsonProperty("klass")]
            public string? Klass { get; set; }

            [JsonProperty("level")]
            public int? Level { get; set; }

            [JsonProperty("move")]
            public string? Move { get; set; }
        }

        public class Resources
        {
            [JsonProperty("icon")]
            public string? Icon { get; set; }

            [JsonProperty("iconShiny")]
            public string? IconShiny { get; set; }

            [JsonProperty("front")]
            public string? Front { get; set; }

            [JsonProperty("frontShiny")]
            public string? FrontShiny { get; set; }

            [JsonProperty("back")]
            public string? Back { get; set; }

            [JsonProperty("backShiny")]
            public string? BackShiny { get; set; }

            [JsonProperty("footprint")]
            public string? Footprint { get; set; }

            [JsonProperty("character")]
            public string? Character { get; set; }

            [JsonProperty("characterShiny")]
            public string? CharacterShiny { get; set; }

            [JsonProperty("cry")]
            public string? Cry { get; set; }

            [JsonProperty("hasFemale")]
            public bool? HasFemale { get; set; }
        }
}
