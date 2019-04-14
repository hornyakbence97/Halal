using Halal.GeneticAlgorithm.Interfaces;
using Halal.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halal.GeneticAlgorithm
{
    class GeneticAlgorithm : ISolver
    {
        private IGeneticAlgorithmProblem problem;
        private int numberOfNewParents;
        private int numberOfParentsK;
        private int numberOfInitParents;
        private int numberOfSelectParents;
        public GeneticAlgorithm(IGeneticAlgorithmProblem problem, int numberOfInitParents = 150, int numberOfSelectParents = 50, int numberOfNewParents = 150, int numberOfParentsK = 3)
        {
            this.problem = problem;
            this.numberOfInitParents = numberOfInitParents; //inicializáláskor hány random elem legyen a populációban
            this.numberOfSelectParents = numberOfSelectParents; //mennyit stelektáljunk ki
            this.numberOfNewParents = numberOfNewParents; //meddig töltsük fel az előző értékhez képest
            this.numberOfParentsK = numberOfParentsK; //hány szülőből csináljunk gyereket
        }

        public ISolution Start()
        {
            var population = this.problem.InitializeStart(this.numberOfInitParents);

            var evaulation = this.problem.Evaulate(population);

            ISolution best = this.BestSelect(evaulation);

            while (!this.problem.CanStop())
            {
                IParentsAndMAtingPool parentsAndMatingPool = this.problem.SelectParents(population, this.numberOfSelectParents);
                bool noMore = false;
                while (parentsAndMatingPool.Parents.Count() < this.numberOfNewParents && !noMore)
                {
                    IEnumerable<ISolution> selected = this.problem.Selection(parentsAndMatingPool.MatingPool, this.numberOfParentsK);
                    if (selected.Count() > 0)
                    {
                        ISolution c = this.problem.CrossOver(selected);
                        c = this.problem.Mutate(c);
                        parentsAndMatingPool.Parents.Add(c);
                    }
                    else
                    {
                        noMore = true;
                    }
                }
                population = parentsAndMatingPool.Parents;
                evaulation = this.problem.Evaulate(population);
                best = this.BestSelect(evaulation);

                //var ki = best;
                //Console.WriteLine();
                ////Console.WriteLine(currentIteration);
                //Console.WriteLine(ki.ToString());
                //ki.ConsoleWrite();
                //Console.WriteLine("Fitnesz: " + this.problem.Fitness(ki));

            }


            Console.WriteLine();

            Console.WriteLine(best.ToString());
            best.ConsoleWrite();
            Console.WriteLine("Fitnesz: " + this.problem.Fitness(best));

            return best;
        }

        private ISolution BestSelect(IEnumerable<IEvaulationElement> evaulation)
        {
            return evaulation
                .Where(y => (y.GetOrderNumber() == (evaulation.Min(x => x.GetOrderNumber()))))
                .FirstOrDefault().GetSolution();
        }
    }
}
