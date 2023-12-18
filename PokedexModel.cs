using CsvHelper.Configuration;

namespace PokemonJsonIterator
{
    public class PokedexModel
    {
        public int Id { get; set; }
        public string? English { get; set; }
        public string? French { get; set; }
        public string? Italian { get; set; }
        public string? German { get; set; }
        public string? Spanish { get; set; }
        public string? Korean { get; set; }
        public string? Japanese { get; set; }
    }

    public sealed class PokedexModelInputMap : ClassMap<PokedexModel>
    {
        public PokedexModelInputMap()
        {
            Map(m => m.English).Index(0);
            Map(m => m.French).Index(1);
            Map(m => m.Italian).Index(2);
            Map(m => m.German).Index(3);
            Map(m => m.Spanish).Index(4);
            Map(m => m.Korean).Index(5);
            Map(m => m.Japanese).Index(6);
        }
    }

}