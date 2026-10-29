using PharmacieApp.Models;

namespace PharmacieApp.Services;

public class PharmacieService
{
    private readonly DataStore _db;
    public PharmacieService(DataStore db) => _db = db;

    // ---- Médicaments ----
    public Medicament AjouterMedicament(string nom, string categorie, decimal prix, int qte, DateTime exp, int seuil = 10)
    {
        var m = new Medicament
        {
            Id = _db.Medicaments.Count == 0 ? 1 : _db.Medicaments.Max(x => x.Id) + 1,
            Nom = nom, Categorie = categorie, Prix = prix,
            Quantite = qte, DateExpiration = exp, SeuilAlerte = seuil
        };
        _db.Medicaments.Add(m);
        _db.Sauvegarder();
        return m;
    }

    public List<Medicament> ListerMedicaments() => _db.Medicaments.OrderBy(m => m.Nom).ToList();

    public List<Medicament> Rechercher(string terme) =>
        _db.Medicaments.Where(m => m.Nom.Contains(terme, StringComparison.OrdinalIgnoreCase)
                                || m.Categorie.Contains(terme, StringComparison.OrdinalIgnoreCase)).ToList();

    public bool Reapprovisionner(int id, int qte)
    {
        var m = _db.Medicaments.FirstOrDefault(x => x.Id == id);
        if (m == null || qte <= 0) return false;
        m.Quantite += qte;
        _db.Sauvegarder();
        return true;
    }

    public bool SupprimerMedicament(int id)
    {
        var ok = _db.Medicaments.RemoveAll(m => m.Id == id) > 0;
        if (ok) _db.Sauvegarder();
        return ok;
    }

    public List<Medicament> Alertes() =>
        _db.Medicaments.Where(m => m.StockBas || m.EstExpire
                                || m.DateExpiration <= DateTime.Today.AddDays(30)).ToList();

    // ---- Clients ----
    public Client AjouterClient(string nom, string tel)
    {
        var c = new Client { Id = _db.Clients.Count == 0 ? 1 : _db.Clients.Max(x => x.Id) + 1, Nom = nom, Telephone = tel };
        _db.Clients.Add(c);
        _db.Sauvegarder();
        return c;
    }

    public List<Client> ListerClients() => _db.Clients;

    // ---- Ventes ----
    public (Vente? vente, string erreur) Vendre(int? clientId, List<(int medId, int qte)> panier)
    {
        if (panier.Count == 0) return (null, "Panier vide.");
        var vente = new Vente
        {
            Id = _db.Ventes.Count == 0 ? 1 : _db.Ventes.Max(v => v.Id) + 1,
            ClientId = clientId
        };

        foreach (var (medId, qte) in panier)
        {
            var m = _db.Medicaments.FirstOrDefault(x => x.Id == medId);
            if (m == null) return (null, $"Médicament #{medId} introuvable.");
            if (m.EstExpire) return (null, $"{m.Nom} est expiré.");
            if (qte <= 0 || qte > m.Quantite) return (null, $"Stock insuffisant pour {m.Nom} (dispo : {m.Quantite}).");
            vente.Lignes.Add(new LigneVente { MedicamentId = m.Id, Nom = m.Nom, Quantite = qte, PrixUnitaire = m.Prix });
        }

        foreach (var l in vente.Lignes)
            _db.Medicaments.First(x => x.Id == l.MedicamentId).Quantite -= l.Quantite;

        _db.Ventes.Add(vente);
        _db.Sauvegarder();
        return (vente, "");
    }

    public List<Vente> HistoriqueVentes() => _db.Ventes.OrderByDescending(v => v.Date).ToList();

    public decimal ChiffreAffairesDuJour() =>
        _db.Ventes.Where(v => v.Date.Date == DateTime.Today).Sum(v => v.Total);
}
