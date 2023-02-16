using System;
using System.Collections.Generic;
using ProjectHelper.Models.ModelsProduitFacture;
using ProjetHelper.Models;

namespace ProjectHelper.Models.ModelsProduit;

public partial class Tblproduit
{
    public int IdProduit { get; set; }

    public int IdDepartement { get; set; }

    public long Cup { get; set; }

    public string Nom { get; set; } = null!;

    public decimal QteInventaire { get; set; }

    public decimal Prix { get; set; }

    public sbyte VentePoids { get; set; }

    public sbyte Tps { get; set; }

    public sbyte Tvq { get; set; }

    public string? Description { get; set; }

    public virtual Tbldepartement IdDepartementNavigation { get; set; } = null!;

    public virtual ICollection<Tblproduitfacture> Tblproduitfactures { get; } = new List<Tblproduitfacture>();
}
