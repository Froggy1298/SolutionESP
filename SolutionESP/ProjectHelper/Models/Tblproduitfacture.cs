using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using ProjectHelper.Models.ModelsProduit;

namespace ProjetHelper.Models;

public partial class Tblproduitfacture
{
    public int IdProduitFacture { get; set; }

    public int IdProduit { get; set; }

    [NotMapped]
    public Tblproduit Produit { get; set; }

    public int IdFacture { get; set; }

    public int? NbFoisCommandee { get; set; }

    public decimal? CoutTotal { get; set; }

    public virtual Tblfacture IdFactureNavigation { get; set; } = null!;

    public virtual Tblproduit IdProduitNavigation { get; set; } = null!;
}
