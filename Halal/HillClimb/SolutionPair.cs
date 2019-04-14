using Halal.BL2.Interfaces;
using Halal.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halal.BL2
{
    class SolutionPair : ISolutionPair
    {
        public WorkerMan WorkerMan { get; set; }
        public WorkToDo WorkToDo { get; set; }

    }
}
