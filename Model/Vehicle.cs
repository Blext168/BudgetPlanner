using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using BudgetPlanner.Cache;

namespace BudgetPlanner.Model;

public sealed class Vehicle
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Bitte Hersteller angeben")]
    public string Manufacturer { get; set; }
    
    [Required(ErrorMessage = "Bitte Modell angeben")]
    public string Model { get; set; }
    
    [Required(ErrorMessage = "Bitte Kennzeichen eingeben")]
    public string LicensePlate { get; set; }

    [Required(ErrorMessage = "Bitte Kaufdatum eingeben")]
    public DateTime PurchaseDate { get; set; }

    [Required(ErrorMessage = "Bitte Kilometerstand beim Kauf eingeben")]
    [Range(0, int.MaxValue, ErrorMessage = "Kilometerstand muss positiv sein")]
    public int InitialOdometer { get; set; }

    [Required(ErrorMessage = "Bitte aktuelles Datum eingeben")]
    public DateTime CurrentDate { get; set; }

    [Required(ErrorMessage = "Bitte aktuellen Kilometerstand eingeben")]
    [Range(0, int.MaxValue, ErrorMessage = "Kilometerstand muss positiv sein")]
    public int CurrentOdometer { get; set; }
    
    [Required(ErrorMessage = "Bitte Fahrleistung pro Jahr eingeben")]
    [Range(0, int.MaxValue, ErrorMessage = "Fahrleistung pro Jahr muss positiv sein")]
    public int KilometersPerYear { get; set; }
    
    public int UserId { get; set; }

    public Vehicle()
    {
        UserId = UserCache.UserId;
    }

    /// <summary>
    /// Clone the current instance of the vehicle to prevent binding issues.
    /// </summary>
    /// <returns>Clone of instance</returns>
    public Vehicle Clone() =>
        new()
        {
            Id = Id,
            Manufacturer = Manufacturer,
            Model = Model,
            LicensePlate = LicensePlate,
            PurchaseDate = PurchaseDate,
            InitialOdometer = InitialOdometer,
            CurrentDate = CurrentDate,
            CurrentOdometer = CurrentOdometer,
            KilometersPerYear = KilometersPerYear,
            UserId = UserId
        };
    
    /// <summary>
    /// Calculates the difference between the actual kilometers driven and the expected kilometers driven
    /// based on the annual driving estimation and the time elapsed since the vehicle's purchase date.
    /// </summary>
    /// <returns>
    /// An integer representing the kilometer difference. A positive value indicates that the actual
    /// kilometers driven exceed the expected kilometers, while a negative value indicates that
    /// the actual kilometers driven are below the expected kilometers.
    /// </returns>
    private int GetKilometerDifference()
    {
        const double oneYearInDays = 365.0;
        
        // Berechne die vergangene Zeit seit Kauf in Jahren (auch Teiljahre)
        TimeSpan timeSpan = CurrentDate - PurchaseDate;
        double yearsElapsed = timeSpan.TotalDays / oneYearInDays;
    
        // Berechne die erwarteten Kilometer basierend auf der Jahresfahrleistung
        int expectedKilometers = (int)(KilometersPerYear * yearsElapsed);
    
        // Berechne die tatsächlich gefahrenen Kilometer
        int actualKilometers = CurrentOdometer - InitialOdometer;
    
        // Positive Zahl bedeutet mehr gefahren als erwartet
        // Negative Zahl bedeutet weniger gefahren als erwartet
        return actualKilometers - expectedKilometers;
    }

    /// <summary>
    /// Evaluates the difference between the actual kilometers driven and the expected kilometers driven
    /// based on the annual driving estimation. Returns a status message indicating if the vehicle
    /// is on track, exceeding, or below the expected kilometers.
    /// </summary>
    /// <returns>
    /// A string message indicating the kilometer status: "Genau im Plan" if on track,
    /// "X km über dem Durchschnitt" if driven more than expected, or
    /// "X km unter dem Durchschnitt" if driven less than expected.
    /// </returns>
    public string GetKilometerStatus()
    {
        try
        {
            var difference = GetKilometerDifference();
    
            return difference switch
            {
                0 => "Genau im Plan",
                > 0 => $"{difference:N0} km über dem Durchschnitt",
                < 0 => $"{Math.Abs(difference):N0} km unter dem Durchschnitt"
            };
        }
        catch (Exception e)
        {
            Debug.WriteLine(e);
            Console.WriteLine(e);
            return "Kann nicht berechnet werden";
        }
    }
}