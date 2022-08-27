using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdsmTriangulationInterface.ModelView
{
    public class TargetViewModel
    {
        public string Name { get; private set; }

        public TargetViewModel(string systemName)
        {
            this.Name = systemName;
        }
    }
}
