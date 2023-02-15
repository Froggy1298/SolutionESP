using System;
using System.Collections.Generic;
using ProjectHelper.Models.ModelsProduit;

namespace ProjetHelper.Models;

public partial class Tbldepartement
{
    public int IdDepartement { get; set; }

    public string NomDepartement { get; set; } = null!;

    public virtual ICollection<Tblproduit> Tblproduits { get; } = new List<Tblproduit>();
}
