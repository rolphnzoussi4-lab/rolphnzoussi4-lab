# 💊 PharmacieManager

Application de gestion d'une pharmacie développée en C# et SQL Server.

## 📋 Description

PharmacieManager est une application permettant d'automatiser la gestion quotidienne d'une pharmacie.

L'application offre la gestion des médicaments, des ventes, des clients, des fournisseurs ainsi que du stock en temps réel.

## 🚀 Fonctionnalités

### Gestion des médicaments
- Ajouter un médicament
- Modifier un médicament
- Supprimer un médicament
- Rechercher un médicament
- Gestion des dates d'expiration

### Gestion des clients
- Ajouter un client
- Modifier un client
- Supprimer un client
- Consulter l'historique des achats

### Gestion des fournisseurs
- Ajouter un fournisseur
- Modifier un fournisseur
- Supprimer un fournisseur

### Gestion des ventes
- Enregistrer une vente
- Calcul automatique du montant
- Génération de factures

### Gestion du stock
- Entrée de stock
- Sortie de stock
- Alerte stock faible
- Alerte produits expirés

## 🛠 Technologies utilisées

- C#
- .NET 8
- SQL Server
- Entity Framework Core
- Visual Studio

## 📂 Structure du projet

```
PharmacieManager/
│
├── Models/
│   ├── Medicament.cs
│   ├── Client.cs
│   ├── Fournisseur.cs
│   └── Vente.cs
│
├── Data/
│   └── DbConnection.cs
│
├── Services/
│   ├── StockService.cs
│   └── VenteService.cs
│
├── Database/
│   └── PharmacieDB.sql
│
└── README.md
```

## 🗄 Base de données

Principales tables :

- Medicament
- Client
- Fournisseur
- Vente
- Utilisateur

## ✅ Installation

1. Cloner le dépôt

```bash
git clone https://github.com/votre-utilisateur/PharmacieManager.git
```

2. Ouvrir la solution dans Visual Studio

3. Exécuter le script SQL situé dans :

```text
Database/PharmacieDB.sql
```

4. Configurer la chaîne de connexion SQL Server.

5. Lancer l'application.

## 📈 Évolutions futures

- Authentification des utilisateurs
- Génération PDF
- Lecteur de code-barres
- Tableau de bord statistique
- Application web ASP.NET Core

## 👨‍💻 Auteur

Développé dans le cadre d'un projet de gestion de pharmacie en C#.

## 📄 Licence

Projet à but éducatif.

