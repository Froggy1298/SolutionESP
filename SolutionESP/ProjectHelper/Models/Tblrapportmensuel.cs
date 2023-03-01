using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
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


    public override bool Equals(object? obj)
    {
        if (obj == null)
            return false;
        

        if (!(obj is Tblrapportmensuel))
            return false;

        Tblrapportmensuel tempRapport = obj as Tblrapportmensuel;

        if (this.DateRapport.Month != tempRapport.DateRapport.Month)
            return false;


        if (this.SommeVente != tempRapport.SommeVente)
            return false;


        if (this.NbTransactionTotal != tempRapport.NbTransactionTotal)
            return false;


        if (this.ValeurMoyTransaction!= tempRapport.ValeurMoyTransaction)
            return false;

        return true;
    }
}
