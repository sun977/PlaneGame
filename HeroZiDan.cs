using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 飞机大战_2020.Properties;

namespace 飞机大战_2020
{
    class HeroZiDan : ZiDan
    {
        private static Image imgHero = Resources.bullet1;

        //4.16改
        //pf.X+pf.Width/2-27, pf.Y 中的 /2-27 数值自己调，让飞机从中间发射子弹
        public HeroZiDan(PlaneFather pf, int speed, int power) : base(pf, imgHero, speed, power, pf.X+pf.Width/2-27, pf.Y)
        { }

    }
}
