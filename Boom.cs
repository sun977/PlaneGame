using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 飞机大战_2020
{
    // 飞机爆炸函数
    abstract class Boom : GameObject
    {
        //只需要调用父类的构造函数
        //需要知道爆炸的坐标，然后播放爆炸图片
        public Boom(int x,int y) : base(x, y) { }
    }
}
