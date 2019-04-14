using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halal.ParticleSwarm.Interfaces
{
    interface ISpeed
    {
        ISpeed Add(ISpeed speed);
        ISpeed Multiple(double parameter);
        void SetValue(int value);
        int GetValue();
    }
}
