using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halal.ParticleSwarm.Interfaces
{
    interface IVelocy
    {
        IVelocy Add(IVelocy speed);
        IVelocy Multiple(double parameter);
    }
}
