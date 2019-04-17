using Halal.ParticleSwarm.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halal.ParticleSwarm
{
    class Velocy : IVelocy
    {
        private static Random random = new Random();
        private int[] velocy;
        private int numberOfWorkers;


        public Velocy(int numberOfJobs, int numberOfWorkers)
        {
            this.numberOfWorkers = numberOfWorkers;
            this.velocy = new int[numberOfJobs];
            for (int i = 0; i < numberOfJobs; i++)
            {
               var r =  random.NextDouble();
                if (r < 0.5)
                {
                    this.velocy[i] = random.Next(2) + 1;
                }
                else
                {
                    this.velocy[i] = (random.Next(2) + 1) * (-1);
                }
            }
        }

        public Velocy(int numberOfWorkers, int[] velocy)
        {
            this.numberOfWorkers = numberOfWorkers;
            this.velocy = velocy;
        }
        public IVelocy Add(IVelocy speed)
        {
            Velocy v = (speed as Velocy);
            int[] vel = new int[this.velocy.Length];
            for (int i = 0; i < this.velocy.Length; i++)
            {
                vel[i] = (this.velocy[i] + v.velocy[i]) % this.numberOfWorkers;
            }

            return new Velocy(this.numberOfWorkers, vel);
        }

        public IVelocy Multiple(double parameter)
        {
            int[] vel = new int[this.velocy.Length];
            for (int i = 0; i < this.velocy.Length; i++)
            {
                vel[i] = (int)Math.Round(this.velocy[i] * parameter);
            }

            return new Velocy(this.numberOfWorkers, vel);
        }

        public int[] GetVelocy()
        {
            return this.velocy;
        }
    }
}
