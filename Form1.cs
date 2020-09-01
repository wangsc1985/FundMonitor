using FundMonitor.helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Threading;
using System.Xml;

namespace FundMonitor
{
    public partial class Form1 : Form
    {
        private delegate void FormControlInvoker();
        private List<Position> positions = new List<Position>();
        private double preIncrease = 0,sustainedIncrease;
        private void setValue(string key, Object value)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("data.xml");
            XmlElement root = (XmlElement)xmlDoc.SelectSingleNode("app");//查找<bookstore>
            if (root == null)
                return;
            root.SetAttribute(key, value.ToString());
            xmlDoc.Save("data.xml");
        }
        private string getValue(string key)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("data.xml");
            XmlElement root = (XmlElement)xmlDoc.SelectSingleNode("app");//查找<bookstore>
            if (root == null)
                return null;
            if (root.HasAttribute(key))
                return root.GetAttribute(key);
            else
                return null;
        }
        public Form1()
        {
            InitializeComponent();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("data.xml");
            //XmlNodeList nodeList = xmlDoc.SelectSingleNode("positions").ChildNodes;
            XmlNodeList nodeList = xmlDoc.SelectNodes("//position");
            foreach (XmlNode xn in nodeList)
            {
                XmlElement xe = (XmlElement)xn;
                string code = xe.GetAttribute("code");
                string name = xe.GetAttribute("name");
                string cost = xe.GetAttribute("cost");
                string amount = xe.GetAttribute("amount");
                positions.Add(new Position(code, name, Double.Parse(cost), int.Parse(amount)));
            }


            var left = this.getValue("left");
            if (left != null)
            {
                this.Left = Convert.ToInt32(left);
            }
            var top = this.getValue("top");
            if (top != null)
            {
                this.Top = Convert.ToInt32(top);
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {

            new Thread(new ThreadStart(() =>
            {
                string url = "https://hq.sinajs.cn/list=sh000001";
                string body = HttpHelper.GetHttp(url);

                try
                {

                    string[] result = body.Substring(body.IndexOf("\"")).Replace("\"", "").Split(',');

                    double open = Double.Parse(result[2]);
                    double price = Double.Parse(result[3]);
                    //double increase = (price - open) / open;

                        //labelTime.Text = result[31];
                        labelSz.Text = string.Format("{0:N2}", open);
                        labelSzIncrease.Text = string.Format("{0:N2}", (price - open) / open * 100);

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
                }
                catch (Exception)
                {
                    labelTime.Text = "-";
                    labelSz.Text = "-";
                    labelSzIncrease.Text = "-";
                }


                Stopwatch watch = new Stopwatch();
                watch.Start();
                double increase = 0;
                DateTime now = DateTime.Now;
                string time = "";
                try
                {
                    foreach (var p in positions)
                    {
                        url = "https://hq.sinajs.cn/list=" + p.exchange + p.code;
                        body = HttpHelper.GetHttp(url);

                        var result = body.Substring(body.IndexOf("\"")).Replace("\"", "").Split(',');


                        double open = Double.Parse(result[2]);
                        double price = Double.Parse(result[3]);
                        time = result[31];
                        p.increase = (price - p.cost) / p.cost;

                        //var tt = result[31].Split(':');
                        //var tradeTime = new DateTime(now.Year, now.Month, now.Day, int.Parse(tt[0]), int.Parse(tt[1]), int.Parse(tt[2]));

                        increase += p.increase;
                        //label1.Text = string.Format("{0:F0}", (now - tradeTime).TotalSeconds) + "";
                        //label1.Text = now.ToString("HH:mm:ss");
                    }
                    labelTime.Text = time;
                    increase = increase / positions.Count * 100;


                    double span = increase - preIncrease;
                    if (sustainedIncrease * span >= 0)
                    {
                        // 符号相同
                        sustainedIncrease += span;
                    }
                    else
                    {
                        // 符号相反
                        sustainedIncrease = span;
                    }
                    if (sustainedIncrease > 0)
                    {
                        label1.ForeColor = Color.Red;
                    }
                    else if (sustainedIncrease == 0)
                    {
                        label1.ForeColor = Color.White;
                    }
                    else
                    {
                        label1.ForeColor = Color.Cyan;
                    }












                    if (Math.Abs(increase - preIncrease) > (1.0 / positions.Count))
                    {
                        if (increase < 0)
                        {
                            speake("负" + string.Format("{0:F2}", -increase), -5);
                        }
                        else
                        {
                            speake(string.Format("{0:F2}", increase));
                        }


                        preIncrease = increase;
                    }
                    watch.Stop();


                    //label1.Text = watch.ElapsedMilliseconds+"";
                    labelFunIncrease.Text = string.Format("{0:F2}", increase);
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
                catch (Exception)
                {
                    labelFunIncrease.Text = "-";
                }

            })).Start();

        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.timer1.Stop();

            this.setValue("left", this.Left);
            this.setValue("top", this.Top);
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

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
                this.Show();
        }

        private void labelFunIncrease_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                this.Cursor = Cursors.SizeAll;
                this.move = true;
            }
        }


        private void labelTime_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void labelFunIncrease_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.Close();
            }
        }

        private void speake(string msg)
        {
            SpeechSynthesizer speech = new SpeechSynthesizer();
            speech.Rate = 0;
            speech.Volume = 100;
            speech.SpeakAsync(msg);
        }
        private void speake(string msg, int rate)
        {
            SpeechSynthesizer speech = new SpeechSynthesizer();
            speech.Rate = rate;
            speech.Volume = 100;
            speech.SpeakAsync(msg);
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
            exchange = code.Substring(0, 1).Equals("6") ? "sh" : "sz";
        }
    }
}
