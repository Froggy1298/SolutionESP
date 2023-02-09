using System;
using System.Collections.Generic;

namespace ProjetHelper.Models;

public partial class Tblrapporthebdomadaire
{
    public int IdRapportHebdomadaire { get; set; }

    public DateTime DateDebut { get; set; }

    public DateTime DateFin { get; set; }

    public decimal? MoyVenteTotalParJour { get; set; }

    public decimal? MoyNbTransactionParJour { get; set; }

    public decimal? MoyPrixTransaction { get; set; }
}
