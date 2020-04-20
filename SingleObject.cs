using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 飞机大战_2020
{
    // 单例类负责所用对象和窗体的交互
    class SingleObject
    {
        //单例类的设计模式
        //1、构造函数私有化
        public SingleObject()
        { }

        //2、声明全局唯一的对象
        private static SingleObject single = null;

        //3、由于创建不了对象，所以提供一个静态函数用于返回一个唯一的对象
        public static SingleObject GetSingle()
        {
            if(single == null)
            {
                single = new SingleObject();
            }
            return single;
        }

        //存储的背景---游戏中唯一的对象
        public BackGround BG
        {
            get;
            set;
        }

        //存储的玩家飞机---游戏中唯一的对象
        public PlaneHero PH
        {
            get;
            set;
        }

        //声明一个集合对象用来 存储玩家子弹对象
        List<HeroZiDan> listHearoZiDan = new List<HeroZiDan>();

        //声明一个集合对象用来 存储敌人子弹对象
        List<EnemyZiDan> listEnemyZiDan = new List<EnemyZiDan>();

        //声明一个集合对象来 存储敌人飞机对象
         public List<PlaneEnemy> listPlaneEnemy = new List<PlaneEnemy>(); //之所以公开，是为了在timer中实时监测还有几架飞机没有销毁

        //声明一个集合对象来 存储敌人飞机爆炸的对象
        List<EnemyBoom> listEnemyBoom = new List<EnemyBoom>();

        //声明一个集合对象来 存储玩家飞机爆炸的对象
        List<HeroBoom> listHeroBoom = new List<HeroBoom>();


        //写一个函数，将创建的对象添加到窗体中
        public void AddGameObject(GameObject go)//由于不知道对象的具体类型，所以传入父类
        {
            if(go is BackGround)//如果当前添加的对象是背景
            {
                this.BG = go as BackGround;//go转成背景赋值给当前的属性
            }
            else if (go is PlaneHero)
            {
                this.PH = go as PlaneHero;
            }
            else if (go is PlaneEnemy)
            {
                listPlaneEnemy.Add(go as PlaneEnemy);
            }
            else if (go is HeroZiDan)
            {
                listHearoZiDan.Add(go as HeroZiDan);
            }
            else if (go is EnemyZiDan)
            {
                listEnemyZiDan.Add(go as EnemyZiDan);
            }
            else if (go is HeroBoom)
            {
                listHeroBoom.Add(go as HeroBoom);
            }
            else if(go is EnemyBoom)
            {
                listEnemyBoom.Add(go as EnemyBoom);
            }


        }

        //写一个函数，将创建的对象从窗体中移除
        public void RemoveGameObject(GameObject go)
        {
            //移除玩家飞机的子弹
            if (go is HeroZiDan)
            {
                listHearoZiDan.Remove(go as HeroZiDan);
            }
            //移除敌人飞机
            else if (go is PlaneEnemy)
            {
                listPlaneEnemy.Remove(go as PlaneEnemy);
            }
            //移除敌人飞机爆炸的图片
            else if (go is EnemyBoom)
            {
                listEnemyBoom.Remove(go as EnemyBoom);
            }
            //移除玩家飞机爆炸的图片
            else if (go is HeroBoom)
            {
                listHeroBoom.Remove(go as HeroBoom);
            }
            //移除敌人飞机的子弹
            else if(go is EnemyZiDan)
            {
                listEnemyZiDan.Remove(go as EnemyZiDan);
            }
            //移除玩家飞机图片
        }

        //这里的Draw函数只是调用其他类本身的Draw函数
        public void Draw(Graphics g)
        {
            this.BG.Draw(g);//向窗体中绘制背景[调用BG的Draw函数绘制背景]
            this.PH.Draw(g);//向窗体中绘制玩家飞机[调用PH的Draw函数绘制玩家飞机]

            //绘制玩家子弹
            for (int i = 0; i < listHearoZiDan.Count; i++)
            {
                listHearoZiDan[i].Draw(g);
            }

            //绘制敌人飞机[三种]
            for (int i = 0; i < listPlaneEnemy.Count; i++)
            {
                listPlaneEnemy[i].Draw(g);
            }

            //绘制敌人飞机爆炸图片
            for (int i = 0; i < listEnemyBoom.Count; i++)
            {
                listEnemyBoom[i].Draw(g);
            }

            //绘制玩家飞机爆炸的图片
            for (int i = 0; i < listHeroBoom.Count; i++)
            {
                listHeroBoom[i].Draw(g);
            }

            //绘制敌人子弹到窗体
            for (int i = 0; i < listEnemyZiDan.Count; i++)
            {
                listEnemyZiDan[i].Draw(g);
            }

        }

        //声明一个属性，用来记分
        public int Score
        {
            get;
            set;
        }

        //碰撞检测函数
        public void Collision()
        {
            #region 判断玩家子弹是否击中敌人飞机
            for (int i = 0; i < listHearoZiDan.Count; i++)//循环玩家子弹
            {
                for (int j = 0; j < listPlaneEnemy.Count; j++)//循环敌人飞机
                {
                    if (listHearoZiDan[i].GetRectangle().IntersectsWith(listPlaneEnemy[j].GetRectangle()))
                    {
                        //如果条件成立则说明发生了碰撞。玩家子弹打中敌人飞机
                        //打中之后敌人的生命值减小 生命值减去子弹威力
                        listPlaneEnemy[j].Life -= listHearoZiDan[i].Power;

                        //生命值减小后判断敌人是否死亡
                        listPlaneEnemy[j].IsOver();

                        //玩家子弹打中后，应该被销毁
                        listHearoZiDan.Remove(listHearoZiDan[i]);//从子弹的集合中去除
                        break;
                    }
                }
            }
            #endregion

            #region 判断敌人子弹是否击中玩家飞机
            for (int i = 0; i < listEnemyZiDan.Count; i++) //遍历敌人子弹
            {
                if (listEnemyZiDan[i].GetRectangle().IntersectsWith(this.PH.GetRectangle()))
                {
                    //满足条件就意味着敌人打中了玩家飞机
                    this.PH.IsOver();
                    break;
                }
            }
            #endregion

            #region 判断玩家飞机是否和敌机飞机相撞
            for (int i = 0; i < listPlaneEnemy.Count; i++)
            {
                if (listPlaneEnemy[i].GetRectangle().IntersectsWith(this.PH.GetRectangle()))
                {
                    //满足条件就意味着玩家飞机和敌机相撞
                    listPlaneEnemy[i].Life = 0;//敌机直接死亡
                    listPlaneEnemy[i].IsOver();
                    break;
                }
            }
            #endregion

            #region 判断玩家子弹是否和敌人子弹相撞
            for (int i = 0; i < listHearoZiDan.Count; i++)
            {
                for (int j = 0; j < listEnemyZiDan.Count; j++)
                {
                    if (listHearoZiDan[i].GetRectangle().IntersectsWith(listEnemyZiDan[j].GetRectangle()))
                    {
                        //满足条件则玩家和敌人子弹移除
                        listHearoZiDan.Remove(listHearoZiDan[i]);
                        listEnemyZiDan.Remove(listEnemyZiDan[j]);
                        break;
                    }
                }
            }

            #endregion

        }
    }
}
