using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using KilometerRechner.Models;

namespace KilometerRechner.Services
{
    public class FahrzeugDatenService
    {
        private readonly string _dateiPfad = "fahrzeugdaten.json";
        private List<FahrzeugDaten> _fahrzeugDatenListe = new();

        public async Task<List<FahrzeugDaten>> LadeFahrzeugDatenAsync()
        {
            try
            {
                if (File.Exists(_dateiPfad))
                {
                    var jsonString = await File.ReadAllTextAsync(_dateiPfad);
                    _fahrzeugDatenListe = JsonSerializer.Deserialize<List<FahrzeugDaten>>(jsonString) ?? new List<FahrzeugDaten>();
                }
            }
            catch (Exception)
            {
                _fahrzeugDatenListe = new List<FahrzeugDaten>();
            }
            return _fahrzeugDatenListe;
        }

        public async Task SpeichereFahrzeugDatenAsync(FahrzeugDaten fahrzeugDaten)
        {
            _fahrzeugDatenListe.Add(fahrzeugDaten);
            var jsonString = JsonSerializer.Serialize(_fahrzeugDatenListe, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(_dateiPfad, jsonString);
        }
    }
} 