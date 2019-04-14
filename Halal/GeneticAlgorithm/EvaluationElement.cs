using Halal.GeneticAlgorithm.Interfaces;
using Halal.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halal.GeneticAlgorithm
{
    class EvaluationElement : IEvaulationElement
    {
        private int orderNum;
        private ISolution solution;
        public EvaluationElement(int orderNumber, ISolution solution)
        {
            this.orderNum = orderNumber;
            this.solution = solution;
        }
        public int GetOrderNumber()
        {
            return this.orderNum;
        }

        public ISolution GetSolution()
        {
            return this.solution;
        }
    }
}
