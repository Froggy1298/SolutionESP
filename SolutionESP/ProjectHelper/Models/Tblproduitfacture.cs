using System;
using System.Collections.Generic;

namespace ProjetHelper.Models;

public partial class Tblproduitfacture
{
    public int IdProduitFacture { get; set; }

    public int IdProduit { get; set; }

    public int IdFacture { get; set; }

    public int? NbFoisCommandee { get; set; }

    public decimal? CoutTotal { get; set; }

    public virtual Tblfacture IdFactureNavigation { get; set; } = null!;

    public virtual Tblproduit IdProduitNavigation { get; set; } = null!;
}
