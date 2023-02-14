using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CaisseEnregistreuse.Event
{
    internal class ChangePageEventArgs : EventArgs
    {
        public Page thePage { get; set; }
        public ChangePageEventArgs(Page thePage)
        {
            this.thePage = thePage;
        }
    }
}
 