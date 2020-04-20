using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 飞机大战_2020.Properties;

namespace 飞机大战_2020
{
    // 敌人飞机类
    class PlaneEnemy : PlaneFather
    {
        //导入敌人飞机的图片，保存到图片变量中
        private static Image img1 = Resources.enemy1;//小飞机
        private static Image img2 = Resources.enemy2;//中飞机
        private static Image img3 = Resources.enemy3;//大飞机

        static Random r = new Random();//生成一个随机值，用于设计敌人飞机运动轨迹
   
        //三种飞机不一样，大小，生命，速度都不一样，所以要标识当前飞机是哪一架
        //设一个EnmeyType 0--表示小飞机  1--表示中飞机  2--表示大飞机
        public int EnemyType
        {
            get;
            set;
        }

        //根据飞机类型返回图片
        public static Image GetImage(int type)
        {
            switch (type)
            {
                case 0:
                    return img1;
                case 1:
                    return img2;
                case 2:
                    return img3;       
            }
            return null;
        }

        //根据飞机的类型返回生命值
        public static int GetLife(int type)
        {
            switch (type)
            {
                case 0:
                    return 1;
                case 1:
                    return 2;
                case 2:
                    return 3;
            }
            return 0;
        }

        //根据飞机类型返回飞机速度
        public static int GetSpeed(int type)
        {
            switch (type)
            {
                case 0:
                    return 5;
                case 1:
                    return 6;
                case 2:
                    return 7;
            }
            return 0;
        }

        //构造函数，调用PlaneFather的构造函数
        public PlaneEnemy(int x,int y,int type):base(x,y,GetImage(type),GetSpeed(type),GetLife(type),Direction.Down)
        {
            this.EnemyType = type;
        }

        //敌人飞机需要重写GameObject中的抽象函数Draw(),将自己绘制到窗体上
        public override void Draw(Graphics g)
        {
            //随着敌人飞机绘制到窗体，就让他开始移动
            this.Move();
            //根据不同飞机类型绘制不同的飞机
            switch (this.EnemyType)
            {
                case 0:
                    g.DrawImage(img1, this.X, this.Y);
                    break;
                case 1:
                    g.DrawImage(img2, this.X, this.Y);
                    break;
                case 2:
                    g.DrawImage(img3, this.X, this.Y);
                    break;
            }

        }

        //重写Move函数，敌人飞机需要移动出窗体
        public override void Move()
        {
            switch (this.Dir)
            {
                case Direction.Up:
                    this.Y -= this.Speed; //应该是减小时间*速度，时间等会解决
                    break;
                case Direction.Down:
                    this.Y += this.Speed;
                    break;
                case Direction.Left:
                    this.X -= this.Speed;
                    break;
                case Direction.Right:
                    this.X = +this.Speed;
                    break;
            }

            if (this.X <= 0)
            {
                this.X = 0;
            }
            if (this.X >= 400)
            {
                this.X = 400;
            }
            if (this.Y <= 0)
            {
                this.Y = 0;
            }
            if (this.Y >= 700)
            {
                this.Y = 1000;//到达窗体底端，离开窗体
                //同时当敌人飞机离开窗体的时候，需要销毁敌人飞机
                SingleObject.GetSingle().RemoveGameObject(this);
            }

            #region 设置敌人飞机活动轨迹
            if (this.EnemyType == 0 && this.Y >=100 )//小飞机
            {
                if(this.X >=0 && this.X <= 220)
                {
                    //表示小飞机在窗体左侧，我们让他往右偏移一个随机值
                    this.X += r.Next(0, 100);
                }
                else //在窗体的右侧，我们让他往左偏移一个随机值
                {
                    this.X -= r.Next(0, 100);
                }
            }
            else if(this.EnemyType == 1 && this.Y >= 200)//中飞机
            {
                if (this.X >= 0 && this.X <= 250)
                {
                    this.X += r.Next(0, 50);
                }
                else 
                {
                    this.X -= r.Next(0, 50);
                }
            }
            else//大飞机只是加速
            {
                this.Speed += 1;
            }
            #endregion

            //百分之五的几率发射子弹
            if (r.Next(0, 100) > 95)
            {
                Fire();
            }

        }

        //重写死亡函数,判断是否死亡
        public override void IsOver()
        {
            if (this.Life == 0)
            {
                //敌机死亡 应该从游戏中移除
                SingleObject.GetSingle().RemoveGameObject(this);

                //播放敌人爆炸的图片
                SingleObject.GetSingle().AddGameObject(new EnemyBoom(this.X, this.Y,EnemyType));
                
                //敌人发生了爆炸，给玩家加分，根据敌机不同类型加不同的分数
                switch (this.EnemyType)
                {
                    case 0:
                        //需要获得单例中玩家分数的属性
                        SingleObject.GetSingle().Score += 100;
                        break;
                    case 1:
                        SingleObject.GetSingle().Score += 200;
                        break;
                    case 2:
                        SingleObject.GetSingle().Score += 300;
                        break;

                }
               
            }
        }

        //重写开火函数
        public override void Fire()
        {
            SingleObject.GetSingle().AddGameObject(new EnemyZiDan(this,25,1));
        }

    }
}
