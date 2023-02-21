using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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

    [NotMapped]
    public decimal CoutPartiel
    {
        get
        {
            decimal? totalTemp = 0m;
            foreach(Tblproduitfacture x in Tblproduitfactures)
            {
                totalTemp += x.CoutProduit * x.NbFoisCommandee;
            }
            return Math.Round((decimal)totalTemp, 2);
        }
    }

    [NotMapped]
    public decimal CoutTotal => Math.Round(CoutTPS + CoutTVQ + CoutPartiel, 2);

    [NotMapped]
    public decimal CoutTPS
    {
        get
        {
            decimal? totalTemp = 0m;
            foreach (Tblproduitfacture x in Tblproduitfactures)
            {
                if(x.IdProduitNavigation.Tps == 1)
                    totalTemp += (x.IdProduitNavigation.Prix * x.NbFoisCommandee) * Tpspercent;
            }
            return Math.Round((decimal)totalTemp, 2);
        }
    }

    [NotMapped]
    public decimal CoutTVQ
    {
        get
        {
            decimal? totalTemp = 0m;
            foreach (Tblproduitfacture x in Tblproduitfactures)
            {
                if (x.IdProduitNavigation.Tps == 1)
                    totalTemp += (x.IdProduitNavigation.Prix * x.NbFoisCommandee) * Tvqpercent;
            }
            return Math.Round((decimal)totalTemp, 2);
        }
    }

}
