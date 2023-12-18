using CsvHelper.Configuration;
using CsvHelper;
using System.Globalization;
using System.Text;
using Newtonsoft.Json;

namespace PokemonJsonIterator
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Enter 'pokemon', 'moves', 'abilities', or 'pokemon and moves' and press Enter:");

            string choice = Console.ReadLine()?.ToLower();

            string directoryPath;
            string apiUrl;
            List<PokedexModel> pokedex = new();
            List<PokemonAbilitiesDialogueFile> abilities = new();
            
            if(choice == "pokemon")
            {
                directoryPath = @"C:\Users\drift\source\repos\KewayGame\Data\Studio\pokemon";
                apiUrl = "https://localhost:44365/api/pokemon/createNotion";
                string csvFilePath = @"C:\Users\drift\source\repos\KewayGame\Data\Text\Dialogs\100002.csv";

                pokedex = AggregatePokedexData(csvFilePath);
            }

            else if (choice == "moves")
            {
                directoryPath = @"C:\Users\drift\source\repos\KewayGame\Data\Studio\moves";
                apiUrl = "https://localhost:44365/api/pokemon/createNotionMove";
            }

            else if (choice == "pokemon and moves")
            {
                directoryPath = @"C:\Users\drift\source\repos\KewayGame\Data\Studio\pokemon";
                apiUrl = "https://localhost:44365/api/pokemon/UpdateNotionPokemonMoves";
            }
            else if (choice == "abilities")
            {
                directoryPath = @"C:\Users\drift\source\repos\KewayGame\Data\Studio\abilities";
                apiUrl = "https://localhost:44365/api/pokemon/createNotionAbility";
                string csvFilePath = @"C:\Users\drift\source\repos\KewayGame\Data\Text\Dialogs\100005.csv";


                abilities = AggregateAbilities(csvFilePath);
            }

            else
            {
                Console.WriteLine("Invalid choice. Exiting program.");
                return;
            }

            Console.WriteLine($"Iterating through JSON files in the directory at {apiUrl}");


            foreach (var filePath in Directory.EnumerateFiles(directoryPath, "*.json"))
            {
                try
                {
                    string? jsonContent = File.ReadAllText(filePath);

                    if(jsonContent is null) 
                    {
                        //TODO: Log error and continue
                        continue; 
                    }

                    Console.WriteLine(jsonContent);

                    if(apiUrl == "https://localhost:44365/api/pokemon/createNotionAbility")
                    {
                        jsonContent = AppendAbility(jsonContent, abilities);
                    }
                    if(apiUrl == "https://localhost:44365/api/pokemon/createNotion")
                    {
                        jsonContent = AppendPokedexData(jsonContent, pokedex);
                    }

                    using (HttpClient client = new HttpClient())
                    {
                        StringContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                        HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                        if (response.IsSuccessStatusCode)
                        {
                            Console.WriteLine($"Successfully posted {Path.GetFileName(filePath)} to the API.");
                        }
                        else
                        {
                            Console.WriteLine($"Failed to post {Path.GetFileName(filePath)}. Status Code: {response.StatusCode}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing file {Path.GetFileName(filePath)}: {ex.Message}");
                }
            }

            await Task.Delay(1100);

            Console.WriteLine("Processing complete. Press any key to exit.");
            Console.ReadKey();
        }

        private static string? AppendAbility(string jsonContent, List<PokemonAbilitiesDialogueFile> abilities)
        {
            if(jsonContent is not null)
            {
                AbilityInput? input = JsonConvert.DeserializeObject<AbilityInput>(jsonContent);

                if(input is null)
                {
                    throw new Exception($"Problem deserializing input {jsonContent}");
                }

                PokemonAbilitiesDialogueFile? dialogue = abilities.SingleOrDefault(a => a.Id == input.TextId);

                if(dialogue is null)
                {
                    return jsonContent;
                }

                input.AbilityText = dialogue.English;

                string jsonWithAbilityText = JsonConvert.SerializeObject(input);

                return jsonWithAbilityText;
            }

            return jsonContent;
        }

        private static List<PokemonAbilitiesDialogueFile> AggregateAbilities(string filePath)
        {
            var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HeaderValidated = null
            };

            List<PokemonAbilitiesDialogueFile> abilities = new();

            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, configuration))
            {
                csv.Context.RegisterClassMap<PokemonAbilitiesDialogueFileInputMap>();
                var records = csv.GetRecords<PokemonAbilitiesDialogueFile>().ToList();

                // The first non-header row is an empty row. This record must be removed to generate proper ids that correspond with abilities ids for matching.
                var garbageRecord = records.FirstOrDefault();

                //if (garbageRecord is null)
                //{
                //    throw new Exception("Shit, something is off with your csv file.");
                //}

                //records.Remove(garbageRecord);

                int idCounter = 0;

                foreach (var record in records)
                {
                    record.Id = idCounter;

                    abilities.Add(record);

                    idCounter++;
                }
            }

            return abilities;
        }

        private static string? AppendPokedexData(string jsonContent, List<PokedexModel> pokedex)
        {
            if(jsonContent is not null)
            {
                PokemonInput? pokemonInput = JsonConvert.DeserializeObject<PokemonInput>(jsonContent);

                if (pokemonInput is null)
                {
                    throw new Exception($"Problem deserializing input {jsonContent}");
                }

                PokedexModel? dexEntryModel = pokedex.SingleOrDefault(p => p.Id == pokemonInput?.Id);

                if(dexEntryModel is null) 
                {
                    return jsonContent;
                }

                pokemonInput.PokedexEntry = dexEntryModel.English;

                string jsonWithDexEntry = JsonConvert.SerializeObject(pokemonInput);

                return jsonWithDexEntry;
            }

            return jsonContent;
        }

        static List<PokedexModel> AggregatePokedexData(string filePath)
        {
            var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HeaderValidated = null
            };

            List<PokedexModel> dex = new();

            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, configuration))
            {
                csv.Context.RegisterClassMap<PokedexModelInputMap>();
                var records = csv.GetRecords<PokedexModel>().ToList();

                // The first non-header row is an empty row. This record must be removed to generate proper ids that correspond with Pokemon DB ids for matching.
                var garbageRecord = records.FirstOrDefault();

                if (garbageRecord is null)
                {
                    throw new Exception("Shit, something is off with your csv file.");
                }

                records.Remove(garbageRecord);

                int idCounter = 1;

                foreach (var record in records)
                {
                    record.Id = idCounter;

                    dex.Add(record);

                    idCounter++;
                }
            }

            return dex;
        }
    }
}