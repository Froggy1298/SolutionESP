using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectHelper.Models.ModelsProduit;

namespace ProjectHelper.Models.ModelsProduit
{
    public class TblProduitPanierDTO
    {
        public int IdProduit { get; set; }
        public long Cup { get; set; }
        public string Nom { get; set; } = null!;
        public decimal Prix { get; set; }
        public sbyte VentePoids { get; set; }

        public sbyte Tps { get; set; }

        public sbyte Tvq { get; set; }

        public static TblProduitPanierDTO IngredientToDTO(Tblproduit p)
        {
            return new TblProduitPanierDTO
            {
                IdProduit = p.IdProduit,
                Cup = p.Cup,
                Nom = p.Nom,
                Prix = p.Prix,
                VentePoids = p.VentePoids,
                Tps = p.Tps,
                Tvq = p.Tvq
            };
        }


    }
}
