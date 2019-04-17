using Halal.ParticleSwarm.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halal.ParticleSwarm
{
    //class Position : IPosition
    //{
    //    static Random rnd = new Random();
    //    private int current;
    //    private int maxMultiple;
    //    public Position(int maxMultiple)
    //    {
    //        this.maxMultiple = maxMultiple;
    //        this.current = rnd.Next(maxMultiple);
    //    }
    //    public Position(int maxMultiple, int current)
    //    {
    //        this.maxMultiple = maxMultiple;
    //        this.current = current;
    //    }

    //    public IPosition AddSpeed(ISpeed speed)
    //    {
    //        var temp = this.current + speed.GetValue();
    //        this.current = temp % maxMultiple;

    //        return new Position(maxMultiple, this.current);
    //    }

    //    public int GetCurrent()
    //    {
    //        return this.current;
    //    }

    //    public ISpeed Minus(IPosition position)
    //    {
    //        var temp = this.current - position.GetCurrent();
    //        if (temp < 0)
    //        {
    //            temp = this.maxMultiple - temp;
    //        }

    //        return new Speed(temp);
    //    }

    //    public void SetMaxXmultipleY(int max)
    //    {
    //        this.maxMultiple = max;
    //    }
    //}
}
