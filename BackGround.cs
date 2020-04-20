using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 飞机大战_2020.Properties;

namespace 飞机大战_2020
{
    // 背景类
    class BackGround : GameObject
    {
        //首先应该导入背景图片，背景图片需要绘制到窗体上
        private static Image imgBG = Resources.background3;//背景图片,这里的图片直接是文件夹里的文件，后缀没显示

        //写构造函数去调用父类的构造函数
        public BackGround(int x,int y,int speed) : base(x,y,imgBG.Width, imgBG.Height, speed, 0, Direction.Down)
        { }

        //重写Draw函数
        public override void Draw(Graphics g)
        {
            this.Y += this.Speed;
            if (this.Y==0)
            {
                this.Y = -1050;
            }
            //坐标改变完成之后，将背景不停的绘制到窗体中
            g.DrawImage(imgBG, this.X, this.Y);
            //不停绘制的功能在窗体的Timer里实现
        }
    }
}
