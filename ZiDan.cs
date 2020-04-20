using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 飞机大战_2020
{
    //子弹的父类
    class ZiDan : GameObject
    {
        //存储子弹图片
        public Image imgZiDan;

        //记录子弹的威力
        public int Power
        {
            get;
            set;
        }

        //2020.4.16改
        //子弹的发送者，图片，速度，威力，横坐标x，纵坐标y
        public ZiDan(PlaneFather pf, Image img, int speed, int power, int x, int y) : base(x, y, img.Width, img.Height, speed, 0, pf.Dir)
        {
            this.imgZiDan = img;
            this.Power = power;
        }

        //绘制子弹到窗体
        public override void Draw(Graphics g)
        {
            this.Move();
         // g.DrawImage(imgZiDan, this.X, this.Y);
            g.DrawImage(imgZiDan, this.X, this.Y,this.Width,this.Heigth);//可重载设计子弹的大小
        }

        //之前的Move函数当移动到了窗体边缘的时候是停在窗体边缘，而不是射出去，这里做修改
        public override void Move()
        {
            //根据游戏对象当前的方向进行移动       
            switch (this.Dir)
            {
                //窗体的左上角为坐标的00原点
                case Direction.Up:
                    this.Y -= this.Speed; //应该是减小时间*速度，时间等会解决
                    break;
                case Direction.Down:
                    this.Y += this.Speed;
                    break;
            }
            //子弹发出后，控制一下子弹的坐标
            if(this.Y <= 0)
            {
                this.Y = -100;
                //在游戏中移除子弹对象
            }
            if(this.Y >= 780)
            {
                this.Y = 1000;
            }
        }
    }
}
