using System;
using System.Collections.Generic;
using System.Drawing;  //使用图片
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 飞机大战_2020
{
    /// <summary>
    /// 所有飞机的父类 抽象类
    /// </summary>
    abstract class PlaneFather : GameObject 
    {
        //声明一个变量，用于存储不同类型飞机的图片
        private Image imgPlane;

        //构造函数调用GameObject构造函数
        public PlaneFather(int x,int y,Image img,int speed,int life,Direction dir):base(x,y,img.Width,img.Height,speed,life,dir)
        {
            this.imgPlane = img;
        }

        //玩家飞机和敌人飞机都会发射子弹
        public abstract void Fire();  //具体的实施不一样，所以写成抽象函数

        //玩家飞机和敌人飞机都会死亡
        public abstract void IsOver();//判断死亡的函数

    }
    
}
