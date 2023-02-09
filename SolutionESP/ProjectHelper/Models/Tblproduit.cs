using System;
using System.Collections.Generic;

namespace ProjetHelper.Models;

public partial class Tblproduit
{
    public int IdProduit { get; set; }

    public int IdDepartement { get; set; }

    public int Cup { get; set; }

    public string Nom { get; set; } = null!;

    public int QteInventaire { get; set; }

    public decimal Prix { get; set; }

    public sbyte VentePoids { get; set; }

    public sbyte Tps { get; set; }

    public sbyte Tvq { get; set; }

    public string? Description { get; set; }

    public virtual Tbldepartement IdDepartementNavigation { get; set; } = null!;

    public virtual ICollection<Tblproduitfacture> Tblproduitfactures { get; } = new List<Tblproduitfacture>();
}
