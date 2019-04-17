using Halal.ParticleSwarm.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halal.ParticleSwarm
{
    class PositionPS : IPositionPS
    {
        private static Random random = new Random();
        private int[] position;
        private int numberOfWorkers;
        public PositionPS(int numberOfJobs, int numberOfWorkers)
        {
            this.numberOfWorkers = numberOfWorkers;
            this.position = new int[numberOfJobs];

            for (int i = 0; i < numberOfJobs; i++)
            {
                this.position[i] = random.Next(numberOfWorkers);
            }
        }

        public PositionPS(int[] position, int numberOfWorkers)
        {
            this.position = position;
            this.numberOfWorkers = numberOfWorkers;
        }

        public IPositionPS AddVelocy(IVelocy velocy)
        {
            Velocy v = (velocy as Velocy);
            int[] vec = v.GetVelocy();
            int[] outPos = new int[this.position.Length];
            for (int i = 0; i < this.position.Length; i++)
            {
               outPos[i] = (this.position[i] + vec[i]) % this.numberOfWorkers;
                if (outPos[i] < 0)
                {
                    outPos[i] *= (-1);
                }
            }

            return new PositionPS(outPos, this.numberOfWorkers);
        }

        public IVelocy Minus(IPositionPS position)
        {
            PositionPS ps = (position as PositionPS);
            int[] vect = new int[this.position.Length];

            for (int i = 0; i < vect.Length; i++)
            {
                vect[i] = (this.position[i] - ps.position[i]) % this.numberOfWorkers;
            }

            return new Velocy(this.numberOfWorkers, vect);
        }

        public int[] GetPosition()
        {
            return this.position;
        }
    }
}
