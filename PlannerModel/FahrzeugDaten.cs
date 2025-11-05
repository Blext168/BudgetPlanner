using System;

namespace KilometerRechner.Models
{
    public class FahrzeugDaten
    {
        public string FahrzeugName { get; set; } = string.Empty;
        public DateTime Anschaffungsdatum { get; set; }
        public int StartKilometerstand { get; set; }
        public int AktuellerKilometerstand { get; set; }
        public DateTime AuslesungsDatum { get; set; }
        public int JaehrlicheFahrleistung { get; set; }
    }
} 