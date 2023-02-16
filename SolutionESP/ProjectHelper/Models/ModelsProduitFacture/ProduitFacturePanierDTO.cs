using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Infrastructure;
using ProjectHelper.Models.ModelsProduit;
using ProjectHelper.Models.ModelsProduitFacture;

namespace ProjectHelper.Models.ModelsProduitFacture
{
    public class ProduitFacturePanierDTO
    {
        public ProduitFacturePanierDTO(ProduitPanierDTO product, decimal? nbFoisCommandee, decimal? coutProduit)
        {
            Product = product;
            NbFoisCommandee = nbFoisCommandee;
            CoutProduit = coutProduit;
        }

        public ProduitPanierDTO Product { get; set; }
        public decimal? NbFoisCommandee { get; set; }
        public decimal? CoutProduit { get; set; }

        public decimal CoutTotal
        {
            get
            {
                decimal? totalTemp = CoutProduit * NbFoisCommandee;
                return Math.Round((decimal)totalTemp, 2);
            }
        }

        public static Tblproduitfacture DTOToObject(ProduitFacturePanierDTO DTO, int idFacture)
        {
            return new Tblproduitfacture
            {
                IdProduit = DTO.Product.IdProduit,
                IdFacture = idFacture,
                NbFoisCommandee = DTO.NbFoisCommandee,
                CoutProduit = DTO.CoutProduit
            };
        }


    }
}
