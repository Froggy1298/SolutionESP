using System;
using System.Collections.Generic;

namespace ProjetHelper.Models;

public partial class Tblrapportmensuel
{
    public int IdRapportMensuel { get; set; }

    public DateTime DateRapport { get; set; }

    public decimal? SommeVente { get; set; }

    public int? NbTransactionTotal { get; set; }

    public decimal? ValeurMoyTransaction { get; set; }
}
