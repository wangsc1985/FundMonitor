using FundMonitor.helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace FundMonitor
{
    public partial class Form1 : Form
    {
        private List<Position> positions = new List<Position>();
        public Form1()
        {
            InitializeComponent();

            //positions.Add(new Position("002673", "西部证券", 10.158, 4900));
            //positions.Add(new Position("002839", "张家港行", 6.242, 8000));
            //positions.Add(new Position("601319", "中国人保", 7.224, 7000));
            //positions.Add(new Position("601319", "中国人保", 7.108, 7000));

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("positions.xml");
            XmlNodeList nodeList = xmlDoc.SelectSingleNode("positions").ChildNodes;
            foreach (XmlNode xn in nodeList)
            {
                XmlElement xe = (XmlElement)xn;
                string code = xe.GetAttribute("code");
                string name = xe.GetAttribute("name");
                string cost = xe.GetAttribute("cost");
                string amount = xe.GetAttribute("amount");
                positions.Add(new Position(code, name,Double.Parse(cost), int.Parse(amount)));
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            string url = "https://hq.sinajs.cn/list=sh000001";
            string body = HttpHelper.GetHttp(url);

            string[] result = body.Substring(body.IndexOf("\"")).Replace("\"", "").Split(',');


            double open = Double.Parse(result[2]);
            double price = Double.Parse(result[3]);
            //double increase = (price - open) / open;
            labelTime.Text = result[31];
            labelSz.Text = string.Format("{0:N2}", open);
            labelSzIncrease.Text = string.Format("{0:N2}", (price-open)/open*100);

            if (price > open)
            {
                labelSz.ForeColor = Color.Red;
                labelSzIncrease.ForeColor = Color.Red;
            }
            else if (price == open)
            {
                labelSz.ForeColor = Color.White;
                labelSzIncrease.ForeColor = Color.White;
            }
            else
            {
                labelSz.ForeColor = Color.Cyan;
                labelSzIncrease.ForeColor = Color.Cyan;
            }


                Stopwatch watch = new Stopwatch();
                watch.Start();
            double increase = 0;
            foreach (var p in positions)
            {
                 url = "https://hq.sinajs.cn/list=" + p.exchange + p.code;
                 body = HttpHelper.GetHttp(url);

                result = body.Substring(body.IndexOf("\"")).Replace("\"", "").Split(',');


                open = Double.Parse(result[2]);
                price = Double.Parse(result[3]);
                //double increase = (price - open) / open;
                labelTime.Text = result[31];
                p.increase = (price - p.cost) / p.cost;

                increase += p.increase;
            }

                watch.Stop();

            //label1.Text = watch.ElapsedMilliseconds+"";
            labelFunIncrease.Text = string.Format("{0:N2}", increase / positions.Count * 100);
            if (increase > 0)
            {
                labelFunIncrease.ForeColor = Color.Red;
            }
            else if (increase == 0)
            {
                labelFunIncrease.ForeColor = Color.White;
            }
            else
            {
                labelFunIncrease.ForeColor = Color.Cyan;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.timer1.Stop();
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.move)
            {
                this.Left += System.Windows.Forms.Control.MousePosition.X - this.MouseX;
                this.Top += System.Windows.Forms.Control.MousePosition.Y - this.MouseY;
            }
            this.MouseX = System.Windows.Forms.Control.MousePosition.X;
            this.MouseY = System.Windows.Forms.Control.MousePosition.Y;
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Default;
            this.move = false;
        }

        private bool move = false;
        private int MouseX, MouseY;


        private void labelFunIncrease_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                this.Cursor = Cursors.SizeAll;
                this.move = true;
            }
        }

        private void labelFunIncrease_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.move)
            {
                this.Left += System.Windows.Forms.Control.MousePosition.X - this.MouseX;
                this.Top += System.Windows.Forms.Control.MousePosition.Y - this.MouseY;
            }
            this.MouseX = System.Windows.Forms.Control.MousePosition.X;
            this.MouseY = System.Windows.Forms.Control.MousePosition.Y;
        }

        private void labelFunIncrease_MouseUp(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Default;
            this.move = false;
        }

        private void labelFunIncrease_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.Close();
            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                this.Cursor = Cursors.SizeAll;
                this.move = true;
            }
        }
    }
    class Position
    {
        public string code;
        public string name;
        public double cost;
        public int amount;
        public string exchange;


        public double increase;

        public Position(string code, string name, double cost, int amount)
        {
            this.code = code;
            this.name = name;
            this.cost = cost;
            this.amount = amount;
            this.exchange = code.Substring(0, 1).Equals("6") ? "sh" : "sz";
        }
    }
    public class StockInfo
    {
        public string time;
        public string name;
        public string code;
        public double cost;
        public double price;
        public double increase;
        public string exchange;
        public double amount;
    }
}
