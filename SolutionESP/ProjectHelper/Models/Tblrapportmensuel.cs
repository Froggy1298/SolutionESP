using System;
using System.Collections.Generic;

namespace ProjetHelper.Models;

public partial class Tblrapportmensuel
{
    public int IdRapportMensuel { get; set; }

    public DateTime DateDebut { get; set; }

    public DateTime DateFin { get; set; }

    public decimal? SommeVente { get; set; }

    public int? NbTransactionTotal { get; set; }

    public decimal? ValeurMoyTransaction { get; set; }
}
