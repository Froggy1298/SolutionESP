using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using ProjectHelper.Models.ModelsProduit;
using ProjetHelper.Models;

namespace ProjectHelper.Models.ModelsProduitFacture;

public partial class Tblproduitfacture
{
    public int IdProduitFacture { get; set; }

    public int IdProduit { get; set; }

    public int IdFacture { get; set; }

    public decimal? NbFoisCommandee { get; set; }

    public decimal? CoutProduit { get; set; }

    public virtual Tblfacture IdFactureNavigation { get; set; } = null!;

    public virtual Tblproduit IdProduitNavigation { get; set; } = null!;
}
