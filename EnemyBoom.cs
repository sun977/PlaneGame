using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 飞机大战_2020.Properties;

namespace 飞机大战_2020
{
    class EnemyBoom : Boom
    {
        //导入每种飞机爆炸时候的图片
        private Image[] imgs1 =
        {
            Resources.enemy1_down1,
            Resources.enemy1_down2,
            Resources.enemy1_down3,
            Resources.enemy1_down4
        };

        private Image[] imgs2 =
        {
            Resources.enemy2_down1,
            Resources.enemy2_down2,
            Resources.enemy2_down3,
            Resources.enemy2_down4
        };

        private Image[] imgs3 =
        {
            Resources.enemy3_down1,
            Resources.enemy3_down2,
            Resources.enemy3_down3,
            Resources.enemy3_down4,
            Resources.enemy3_down5,
            Resources.enemy3_down6
        };

        //爆炸的时候需要知道爆炸是哪一架飞机
        //根据敌人飞机的类型类播放对应的爆炸图片

        public int Type
        {
            get;
            set;
        }

        public EnemyBoom(int x,int y,int type) : base(x, y)
        {
            this.Type = type;
        }

        public override void Draw(Graphics g)
        {
            //将爆炸图片绘制到窗体的时候，需要当前飞机的类型类绘制
            switch (this.Type)
            {
                case 0:
                    for (int i = 0; i < imgs1.Length ; i++)
                    {
                        g.DrawImage(imgs1[i], this.X, this.Y);
                    }
                    break;
                case 1:
                    for (int i = 0; i < imgs2.Length; i++)
                    {
                        g.DrawImage(imgs2[i], this.X, this.Y);
                    }
                    break;
                case 2:
                    for (int i = 0; i < imgs3.Length; i++)
                    {
                        g.DrawImage(imgs3[i], this.X, this.Y);
                    }
                    break;
            }

            //播放完成之后要移除爆炸图片
            SingleObject.GetSingle().RemoveGameObject(this);

        }
    }
}
