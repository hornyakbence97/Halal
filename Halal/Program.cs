using Halal.BL1;
using Halal.BL2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halal
{
    class Program
    {
        static void Main(string[] args)
        {

            //State state = new State();

            //Stack<string> inp = new Stack<string>();
            //inp.Push("C");
            //inp.Push("B");
            //inp.Push("D");
            //inp.Push("A");
            //inp.Push("E");

            //Stack<string> goal = new Stack<string>();
            //goal.Push("E");
            //goal.Push("D");
            //goal.Push("C");
            //goal.Push("B");
            //goal.Push("A");

            //var result = state.GetRouteWithHillClimbng(inp, goal);


            List<WorkToDo> workToDos = new List<WorkToDo>()
            {
                new WorkToDo() {Name = "A", Complexity = 9},
                new WorkToDo() {Name = "B", Complexity = 7},
                new WorkToDo() {Name = "C", Complexity = 16},
                new WorkToDo() {Name = "D", Complexity = 21},
                new WorkToDo() {Name = "E", Complexity = 15}
            };


            List<WorkerMan> workerMens = new List<WorkerMan>()
            {
                new WorkerMan() {Name = "1", Quality = 2, WorkingMinutes = 50},
                new WorkerMan() {Name = "2", Quality = 1, WorkingMinutes = 30},
                new WorkerMan() {Name = "3", Quality = 2, WorkingMinutes = 70},
                new WorkerMan() {Name = "4", Quality = 2, WorkingMinutes = 50},
                new WorkerMan() {Name = "5", Quality = 3, WorkingMinutes = 70},
                new WorkerMan() {Name = "6", Quality = 2, WorkingMinutes = 32},
                new WorkerMan() {Name = "7", Quality = 1, WorkingMinutes = 44},
                new WorkerMan() {Name = "8", Quality = 1, WorkingMinutes = 90}
            };

            OptimalizationTransform optimalizationTransform = new OptimalizationTransform(workToDos, workerMens, 1000000, 155);
            var solution = optimalizationTransform.HillClimbing();




            Console.ReadLine();
        }
    }
}
