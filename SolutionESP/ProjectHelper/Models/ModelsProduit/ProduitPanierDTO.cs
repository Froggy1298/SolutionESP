using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectHelper.Models.ModelsProduit;

namespace ProjectHelper.Models.ModelsProduit
{
    public class ProduitPanierDTO
    {
        public int IdProduit { get; set; }
        public long Cup { get; set; }
        public string Nom { get; set; } = null!;
        public decimal Prix { get; set; }
        public bool VentePoids { get; set; }

        public bool Tps { get; set; }

        public bool Tvq { get; set; }

        public static ProduitPanierDTO ProduitToDTO(Tblproduit p)
        {
            return new ProduitPanierDTO
            {
                IdProduit = p.IdProduit,
                Cup = p.Cup,
                Nom = p.Nom,
                Prix = p.Prix,
                VentePoids = Convert.ToBoolean(p.VentePoids),
                Tps = Convert.ToBoolean(p.Tps),
                Tvq = Convert.ToBoolean(p.Tvq)
            };
        }


    }
}
