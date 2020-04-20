using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 飞机大战_2020
{

    /// <summary>
    /// 方向
    /// </summary>
    enum Direction // 枚举类型
    {
        Up,
        Down,
        Left,
        Right
    }

    /// <summary>
    /// 所有游戏对象的父类，封装所有子类所共有的成员
    /// </summary>
    abstract class GameObject  //包含抽象成员的类必须也是抽象类
    {
        #region 横纵坐标，高度，宽度，速度，方向，生命值
        public int X //横坐标
        {
            get;
            set;
        }

        public int Y //纵坐标
        {
            get;
            set;
        }

        public int Heigth //高度
        {
            get;
            set;
        }

        public int Width //宽度
        {
            get;
            set;
        }

        public int Speed //速度
        {
            get;
            set;
        }

        public int Life //生命值
        {
            get;
            set;
        }

        public Direction Dir //方向
        {
            get;
            set;
        }
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="heigth"></param>
        /// <param name="speed"></param>
        /// <param name="life"></param>
        /// <param name="dir"></param>
        public GameObject(int x,int y,int width,int heigth,int speed,int life,Direction dir)//构造函数
        {
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Heigth = heigth;
            this.Speed = speed;
            this.Life = life;
            this.Dir = dir;
        }

        /// <summary>
        /// 构造函数重载，用于Boom函数的构造
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public GameObject(int x,int y)
        {
            this.X = x;
            this.Y = y;
        }

        //每个游戏对象在GDI+对象绘制自己到窗体的时候绘制的方式不一样。
        //所以我们需要在父类中提供一个绘制对象的抽象函数。

        /// <summary>
        /// 绘制抽象函数，用于绘制对象到窗体
        /// </summary>
        /// <param name="g"></param>
        public abstract void Draw(Graphics g);

        //提供一个碰撞检测的方法，这里返回图片的矩形，判断图片边缘是否重叠，以此来写碰撞检测函数Collision()，在SingleObject里实现

        /// <summary>
        /// 用于 碰撞检测
        /// </summary>
        /// <returns></returns>
        public Rectangle GetRectangle()
        {
            return new Rectangle(this.X, this.Y, this.Heigth, this.Width);
        }

        //需要一个对象移动的函数，子类的移动的方式不一样，所以写成虚拟方法
      
        /// <summary>
        /// 移动的虚拟方法，每一个子类移动方式不一样的自己重写
        /// </summary>
        public virtual void Move()
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
                case Direction.Left:
                    this.X -= this.Speed;
                    break;
                case Direction.Right:
                    this.X = +this.Speed;
                    break;
            }

            //移动完成之后要判断游戏对象是否超出了窗体边框
            //边框宽480，长850，根据背景图片的大小而定

            //每个对象移动方式不一样，所以判断超出的方式也不一样
            //这里主类写成玩家飞机的，敌人飞机需要重写这个方法

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
                this.Y = 700;
            }

        }


    }
}
