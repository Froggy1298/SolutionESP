﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using ProjectHelper.Models.ModelsProduit;

namespace ProjetHelper.Models;

public partial class Tbldepartement
{
    public int IdDepartement { get; set; }

    public string NomDepartement { get; set; } = null!;

    public virtual ICollection<Tblproduit> Tblproduits { get; } = new List<Tblproduit>();

    public override string ToString()
    {
        return NomDepartement;
    }

    public override bool Equals(object? obj)
    {
        try
        {
            return (this.NomDepartement == ((Tbldepartement)obj).NomDepartement);

        }
        catch (Exception)
        {
            return false;
            throw;
        }
    }
}
