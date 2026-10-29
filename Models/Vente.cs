namespace PharmacieApp.Models;

public class LigneVente
{
    public int MedicamentId { get; set; }
    public string Nom { get; set; } = "";
    public int Quantite { get; set; }
    public decimal PrixUnitaire { get; set; }
    public decimal Total => Quantite * PrixUnitaire;
}

public class Vente
{
    public int Id { get; set; }
    public DateTime Date { get; set; } = DateTime.Now;
    public int? ClientId { get; set; }
    public List<LigneVente> Lignes { get; set; } = new();
    public decimal Total => Lignes.Sum(l => l.Total);
}
