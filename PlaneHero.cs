using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using 飞机大战_2020.Properties;

namespace 飞机大战_2020
{
    //玩家飞机类
    class PlaneHero : PlaneFather 
    {
        //导入玩家飞机的图片，保存到图片变量中
        private static Image imgPlane = Resources.me1;//导入玩家飞机的图片

        //调用PlaneFather的构造函数
        public PlaneHero(int x, int y, int speed, int life, Direction dir) : base(x, y, imgPlane, speed, life, dir)
        { }

        //玩家飞机需要重写GameObject中的抽象函数Draw()函数，将自己绘制到窗体上
        public override void Draw(Graphics g)
        {
            g.DrawImage(imgPlane, this.X, this.Y,this.Width/2,this.Heigth/2);
        }

        //让玩家飞机跟着鼠标移动，这个函数需要借助参数e来得到鼠标的坐标
        public void MouseMove(MouseEventArgs e)
        {
            //-27 没有特殊意义，只是为了让鼠标停在玩家飞机的中间
            this.X = e.X - 27;
            this.Y = e.Y - 27;     
        }

        //重写玩家飞机开炮的函数
        public override void Fire()
        {
            //初始化玩家的子弹到游戏中
            SingleObject.GetSingle().AddGameObject(new HeroZiDan(this, 18, 1));//谁，速度，威力

        }

        //玩家飞机死亡的函数
        public override void IsOver()
        {
            //添加爆炸效果
            SingleObject.GetSingle().AddGameObject(new HeroBoom(this.X,this.Y));

            //死亡触发---未写
        }

    }

}
