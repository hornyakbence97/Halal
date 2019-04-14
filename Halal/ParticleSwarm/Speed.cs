using Halal.ParticleSwarm.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halal.ParticleSwarm
{
    class Speed : ISpeed
    {
        int value;

        public Speed(int value)
        {
            this.value = value;
        }


        public ISpeed Add(ISpeed speed)
        {
            int newS = this.value + speed.GetValue();
            return new Speed(newS);
        }

        public int GetValue()
        {
            return this.value;
        }

        public ISpeed Multiple(double parameter)
        {
            return new Speed((int)(this.value*parameter));
        }

        public void SetValue(int value)
        {
            this.value = value;
        }
    }
}
