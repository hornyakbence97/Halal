using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halal.BL1
{
    public class State
    {
        private List<Stack<string>> state;
        private int heuristics;

        public List<Stack<string>> GetSetState { get => state; set => state = value; }
        public int GetSetHeuristics { get => heuristics; set => heuristics = value; }

        public State()
        {
            this.GetSetState = new List<Stack<string>>();

        }

        public State Copy()
        {
            State s = new State();
            s.GetSetHeuristics = this.GetSetHeuristics;
            foreach (var item in this.GetSetState)
            {
                Stack<string> temp = new Stack<string>();
                Stack<string> temp2 = new Stack<string>();
                foreach (var st in item)
                {
                    temp.Push(st);
                }
                foreach (var st in temp)
                {
                    temp2.Push(st);
                }
                s.GetSetState.Add(temp2);
            }
            return s;
        }

        public int GetHeuristicsValue
            (List<Stack<string>> currentState, Stack<string> goalState)
        {
            int heuristicValue = 0;
            heuristicValue = currentState.
                Select(x => GetHeuristicsValueForStack(x, currentState, goalState))
                .Sum(x => x);

            return heuristicValue;
        }

        private int GetHeuristicsValueForStack
            (Stack<string> stack, 
            List<Stack<string>> currentState,
            Stack<string> goalState)
        {
            int stackHeuristics = 0;
            bool isPositionCorrect = true;
            int goalStartIndex = 0;
            foreach (var item in stack)
            {
                if (isPositionCorrect && item == goalState.ElementAt(goalStartIndex))
                {
                    stackHeuristics += goalStartIndex;
                }
                else
                {
                    stackHeuristics -= goalStartIndex;
                    isPositionCorrect = false;
                }
                goalStartIndex++;
            }

            return stackHeuristics;
        }


        private State PushElementToNewStack(
            List<Stack<string>> currentStackList,
            string block,
            int currentStateHeuristics,
            Stack<string> goalStateStack)
        {
            State newState = null;
            Stack<string> newStack = new Stack<string>();
            newStack.Push(block);
            currentStackList.Add(newStack);

            int newStateHeuristics = GetHeuristicsValue(currentStackList, goalStateStack);
            if(newStateHeuristics > currentStateHeuristics)
            {
                newState = new State()
                {
                    GetSetHeuristics = newStateHeuristics,
                    GetSetState = currentStackList
                };
            }
            else
            {
                currentStackList.Remove(newStack);
            }

            return newState;
        }

        private State PushElementToExistingStacks(
            Stack<string> currentStack,
            List<Stack<string>> currentStackList,
            string block,
            int currentStateHEuristics,
            Stack<string> goalStateStack)
        {
            return currentStackList
                .Where(x => x != currentStack) //TODO
                .Select(x => PushElementToStack
                (x, block, currentStackList, currentStateHEuristics, goalStateStack))
                .Where(x => x != null)
                .FirstOrDefault();
                
        }

        private State PushElementToStack
            (Stack<string> stack,
            string block,
            List<Stack<string>> currentStackList,
            int currentStateHEuristics,
            Stack<string> goalStateStack)
        {
            stack.Push(block);
            int newStateHeuristics = GetHeuristicsValue(currentStackList, goalStateStack);
            if(newStateHeuristics > currentStateHEuristics)
            {
                return new State()
                {
                    GetSetState = currentStackList,
                    GetSetHeuristics = newStateHeuristics
                };
            }

            stack.Pop();
            return null;
        }



        //solve:

        public List<State> GetRouteWithHillClimbng(
            Stack<string> initStateStack,
            Stack<string> goalStateStack)
        {

            List<Stack<string>> ini = new List<Stack<string>>();
            ini.Add(initStateStack);


            List<State> resultPath = new List<State>();
            resultPath.Add(new State() { GetSetState = ini });

            State currentState = resultPath[0].Copy();
            bool noStateFound = false;

            while (!(currentState.GetSetState.ElementAt(0) == goalStateStack) || noStateFound)
            {
                noStateFound = true;
                State nextState = FindNextState(currentState, goalStateStack);
                if (nextState != null)
                {
                    noStateFound = false;
                    currentState = nextState;
                    Console.WriteLine(nextState.GetSetState.ToString()); //TODO
                    resultPath.Add(nextState.Copy());
                }
            }
            return resultPath;
        }

        private State[] AppendResultPath(State[] resultPath, State state)
        {
            State[] n = new State[resultPath.Length + 1];
            for (int i = 0; i < resultPath.Length; i++)
            {
                n[i] = resultPath[i];
            }
            n[resultPath.Length] = state;
            return n;
        }

        private State FindNextState(State currentState, Stack<string> goalStateStack)
        {
            List<Stack<string>> listOfStacks = currentState.GetSetState;
            int currentStateHeuristics = currentState.GetSetHeuristics;

            return listOfStacks
                .Select(x => ApplyOperationsOnState(listOfStacks, x, currentStateHeuristics, goalStateStack))
                .Where(x => x != null)
                .FirstOrDefault();

            
        }

        private State ApplyOperationsOnState(List<Stack<string>> listOfStacks,
            Stack<string> stack, int currentStateHeuristics, Stack<string> goalStateStack)
        {
            State tempState;
            List<Stack<string>> tempStackList = new List<Stack<string>>();
            string block = stack.Pop();
            if (stack.Count == 0)
            {
                tempStackList.Remove(stack);
            }
            tempState = PushElementToNewStack(tempStackList, block, currentStateHeuristics, goalStateStack);
            if (tempState == null)
            {
                tempState = PushElementToExistingStacks(stack, tempStackList, block, currentStateHeuristics, goalStateStack);
                stack.Push(block);
            }

            return tempState;
        }
    }


}
