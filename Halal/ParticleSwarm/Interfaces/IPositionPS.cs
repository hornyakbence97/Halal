using Halal.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halal.ParticleSwarm.Interfaces
{
    interface IPositionPS
    {
        IVelocy Minus(IPositionPS position);
        IPositionPS AddVelocy(IVelocy velocy);
    }
}
