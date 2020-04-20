using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 飞机大战_2020.Properties;

namespace 飞机大战_2020
{
    class EnemyZiDan : ZiDan
    {
        private static Image imgEnemy = Resources.bullet2;

        //pf.X + 10 + pf.Width/2  这里的 +10 是调整飞机中间发射子弹
        public EnemyZiDan(PlaneFather pf, int speed, int power) : base(pf, imgEnemy, speed, power, pf.X + 10 + pf.Width/2, pf.Y+pf.Heigth)
        { }

    }
}
