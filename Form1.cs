using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 飞机大战_2020
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitialGame();//调用初始化函数
        }

        static Random r = new Random();//用于随机出现敌人飞机，用于限制飞机出现的几率

        // 初始化游戏
        public void InitialGame()
        {
            //首先初始化背景
            SingleObject.GetSingle().AddGameObject(new BackGround(0,-850,5));

            //初始化玩家飞机
            SingleObject.GetSingle().AddGameObject(new PlaneHero(160, 550, 5, 3, Direction.Up));

            //初始化敌人飞机，敌人飞机众多，所以单写一个函数来初始化
            InitialPlaneEnemy();
            
        }

        //初始化敌人飞机需要timer实时判断数量并重新初始化
        private void InitialPlaneEnemy()
        {
            for (int i = 0; i < 4; i++)//循环出现敌人飞机 4后面可以设置
            {
                SingleObject.GetSingle().AddGameObject(new PlaneEnemy(r.Next(0, this.Width), -400, r.Next(0, 2)));
            }
            //不应该每次都出现最大的敌人飞机，应该有一定的几率出现
            if(r.Next(0,100)> 80)
            {
                //百分之二十的几率出现大飞机
                SingleObject.GetSingle().AddGameObject(new PlaneEnemy(r.Next(0, this.Width), -400, 2));
            }
        }


        /// <summary>
        /// 绘制背景到窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //当窗体被重新绘制的时候会执行这个控件
            //窗体重新绘制的时候，就绘制自己的背景
            //拿到单例类对象，调用他的Draw函数,通过e传递进来
            SingleObject.GetSingle().Draw(e.Graphics);

            //绘制玩家的得分
            string score = SingleObject.GetSingle().Score.ToString();
            e.Graphics.DrawString(score, new Font("宋体", 20, FontStyle.Bold), Brushes.Red, new Point(0, 0));
        }

        /// <summary>
        /// 让窗体不停的重绘
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerBG_Tick(object sender, EventArgs e)
        {
            //每50毫秒让窗体发生重绘事件
            this.Invalidate();//不停的重绘背景
            //不停的绘制完成之后，但是绘制的背景在闪烁，需要解决闪烁的问题

            //不停的判断敌人飞机的数量
            int conut = SingleObject.GetSingle().listPlaneEnemy.Count;
            if (conut <= 1)
            {
                //再次对飞机初始化
                InitialPlaneEnemy();
            }

            //不停进行碰撞检测
            SingleObject.GetSingle().Collision();

        }

        /// <summary>
        /// 解决背景闪烁问题
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            //在窗体加载的时候解决窗体的闪烁问题
            //将图像绘制到缓冲区，以减少闪烁次数
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint, true);
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            //当鼠标在窗体移动的时候会触发这个事件，让飞机跟着鼠标的移动而移动
            //通过参数e可以得到鼠标的坐标，把鼠标坐标赋值给玩家飞机坐标即可
            SingleObject.GetSingle().PH.MouseMove(e);

        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            //判断玩家是否在窗体按下了鼠标左键
            //按下左键，则调用玩家飞机开炮的方法
            SingleObject.GetSingle().PH.Fire();

        }
    }
}
