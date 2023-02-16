using System;
using System.Collections.Generic;
using ProjectHelper.Models.ModelsProduitFacture;

namespace ProjetHelper.Models;

public partial class Tblfacture
{
    public int IdFacture { get; set; }

    public DateTime Date { get; set; }

    public string? ModePaiment { get; set; }

    public string? Etat { get; set; }

    public decimal Tvqpercent { get; set; }

    public decimal Tpspercent { get; set; }

    public virtual ICollection<Tblproduitfacture> Tblproduitfactures { get; } = new List<Tblproduitfacture>();
}
