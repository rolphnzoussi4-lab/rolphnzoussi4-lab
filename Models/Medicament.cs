namespace PharmacieApp.Models;

public class Medicament
{
    public int Id { get; set; }
    public string Nom { get; set; } = "";
    public string Categorie { get; set; } = "";
    public decimal Prix { get; set; }
    public int Quantite { get; set; }
    public int SeuilAlerte { get; set; } = 10;
    public DateTime DateExpiration { get; set; }

    public bool EstExpire => DateExpiration.Date < DateTime.Today;
    public bool StockBas => Quantite <= SeuilAlerte;
}
