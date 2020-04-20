using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 飞机大战_2020.Properties;

namespace 飞机大战_2020
{
    class HeroBoom : Boom
    {
        //存储玩家飞机爆炸图片
        private Image[] imgs =  
        {
            Resources.me_destroy_1,
            Resources.me_destroy_2,
            Resources.me_destroy_3,
            Resources.me_destroy_4
        };

        //构造函数调用Boom的构造函数
        public HeroBoom(int x,int y) : base(x, y) { }

        //重写绘制自己到窗体的函数Draw()
        public override void Draw(Graphics g)
        {
            for (int i = 0; i < imgs.Length; i++)
            {
                g.DrawImage(imgs[i], this.X, this.Y,imgs[i].Width/2,imgs[i].Height/2);
            }
            //绘制完成后将爆炸的图片移除
            SingleObject.GetSingle().RemoveGameObject(this);
        }

    }
}
