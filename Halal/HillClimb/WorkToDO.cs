﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halal.BL2
{
    /*
*Van X perc munkaidő. Van M db munkás. Van N db feladat, és minden feladatnak van nehézségi szintje. 
* Nehézségi szint alapján tudni minden munkásról, hogy mennyi ideig fog tartani neki a munka.
* Illetve minden munkásnak van valami minőség értéke és hogy mennyit dolgozik egy nap.
* 
* Feladatok hozzárendelése az emberekhez a leghatékonyabban. 
*/
    class WorkToDo
    {
        private string name;
        private int complexity;

        public int Complexity
        {
            get { return complexity; }
            set { complexity = value; }
        }


        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public override string ToString()
        {
            return Name + ", Compleyity: " + complexity;
        }

    }
}
