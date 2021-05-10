using FundMonitor.helper;
using LitJson;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Speech.Synthesis;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

namespace FundMonitor
{
    public partial class Form1 : Form
    {

        private string env = "yipinshangdu-4wk7z";
        private string appid = "wxbdf065bdeba96196";
        private string secret = "d2834f10c0d81728e73a4fe4012c0a5d";


        private bool isSpeake = false;
        private delegate void FormControlInvoker();
        private List<Position> positionList = new List<Position>();
        private decimal preIncrease = 0m, preIncrease1 = 0m, sustainedIncrease, sustainedCount;
        string preTime = "";
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



            //XmlDocument xmlDoc = new XmlDocument();
            //xmlDoc.Load("data.xml");

            //XmlNodeList nodeList = xmlDoc.SelectNodes("//position");
            //foreach (XmlNode xn in nodeList)
            //{
            //    XmlElement xe = (XmlElement)xn;
            //    string code = xe.GetAttribute("code");
            //    string name = xe.GetAttribute("name");
            //    string cost = xe.GetAttribute("cost");
            //    string amount = xe.GetAttribute("amount");
            //    positions.Add(new Position(code, name, Double.Parse(cost), int.Parse(amount)));
            //}



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
            var speake = this.getValue("speake");
            if (speake != null)
            {
                this.isSpeake = Convert.ToBoolean(speake);
                if (isSpeake)
                {
                    this.pictureBoxVolume.Image = global::FundMonitor.Properties.Resources.volume_on;
                    //this.labelTime.ForeColor = System.Drawing.Color.White;
                }
                else
                {
                    this.pictureBoxVolume.Image = global::FundMonitor.Properties.Resources.volume_off;
                    //this.labelTime.ForeColor = System.Drawing.Color.Silver;
                }
            }

            var pwd = this.getValue("pwd");
            if (pwd != null)
            {
                positionList = getPositions(pwd);
            }
            else
            {
                MessageBox.Show("配置pwd");
            }
        }

        private List<Position> getPositions(string pwd)
        {
            List<Position> result = new List<Position>();
            
            string html = HttpHelper.GetHttp($"https://sahacloudmanager.azurewebsites.net/home/token/{appid}/{secret}");

            var data = html.Split(':');
            string access_token = data[0];


            string url = $"https://api.weixin.qq.com/tcb/invokecloudfunction?access_token={access_token}&env={env}&name=getPositions";
            string body = "{\"pwd\":\"" + pwd + "\"}";
            html = HttpHelper.PostHttpByJson(url, body);
            JsonData jsonData = JsonMapper.ToObject(html);
            if (jsonData["errcode"].ToString().Equals("0"))
            {
                string resp_data = jsonData["resp_data"].ToString();
                jsonData = JsonMapper.ToObject(resp_data);
                JsonData list = jsonData["data"];

                foreach (JsonData position in list)
                {
                    string name = position["name"].ToString();
                    string code = position["code"].ToString();
                    int amount = Convert.ToInt32(position["amount"].ToString());
                    decimal cost = Convert.ToDecimal(position["cost"].ToString());
                    string exchange = position["exchange"].ToString();
                    if (amount > 0)
                        result.Add(new Position(code, name, cost, amount,exchange));
                }
            }


            return result;

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private bool isFinished = true;
        private void timer1_Tick(object sender, EventArgs e)
        {

            new Thread(new ThreadStart(() =>
            {
                if (isFinished == false)
                    return;
                isFinished = false;
                string url = "https://hq.sinajs.cn/list=sh000001";
                string body = HttpHelper.GetHttp(url);

                try
                {

                    string[] result = body.Substring(body.IndexOf("\"")).Replace("\"", "").Split(',');

                    double open = Double.Parse(result[2]);
                    double price = Double.Parse(result[3]);
                    //double increase = (price - open) / open;

                    //labelTime.Text = result[31];
                    labelSz.Text = string.Format("{0:N2}", price);
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
                    isFinished = true;
                }


                Stopwatch watch = new Stopwatch();
                watch.Start();
                decimal totalProfit = 0, totalFund = 0, totalDiff = 0;
                DateTime now = DateTime.Now;
                string time = "";
                try
                {
                    string str = "";
                    foreach (var position in positionList)
                    {
                        url = "https://hq.sinajs.cn/list=" + position.exchange + position.code;
                        body = HttpHelper.GetHttp(url);

                        var result = body.Substring(body.IndexOf("\"")).Replace("\"", "").Split(',');


                        decimal yesPrice = Decimal.Parse(result[2]);
                        decimal price = Decimal.Parse(result[3]);
                        if (price == 0)
                        {
                            price = yesPrice;
                        }
                        time = result[31];


                        var fee =TradeUtils.commission(price, position.amount) + TradeUtils.tax(-1, price, position.amount) + TradeUtils.transferFee(price, position.amount);
                        //position.increase = ((price - position.cost) * position.amount * 100 - fee) / (position.cost * position.amount * 100);
                        str += $"{position.name}\t{price}\t";

                        totalProfit += (price - position.cost) * position.amount * 100 - fee;
                        totalFund += position.cost * position.amount * 100;
                        totalDiff += (price - yesPrice) * position.amount * 100;

                        //var tt = result[31].Split(':');
                        //var tradeTime = new DateTime(now.Year, now.Month, now.Day, int.Parse(tt[0]), int.Parse(tt[1]), int.Parse(tt[2]));

                        //increase += position.increase;

                        //label1.Text = string.Format("{0:F0}", (now - tradeTime).TotalSeconds) + "";
                        //label1.Text = now.ToString("HH:mm:ss");
                    }

                    if (!preTime.Equals(time))
                    {
                        var write = File.AppendText("stock.txt");
                        str = $"{time}\t" + str;
                        write.WriteLine(str);
                        write.Close();
                        preTime = time;
                    }

                    labelTime.Text = time;
                    var increase = totalProfit*100 / totalFund;
                    var difInc = totalDiff * 100 / totalFund;


                    decimal span = increase - preIncrease1;
                    if (sustainedIncrease * span >= 0)
                    {
                        // 符号相同
                        sustainedIncrease += span;
                        if (span != 0)
                        {
                            sustainedCount++;
                        }
                    }
                    else
                    {
                        // 符号相反
                        sustainedIncrease = span;
                        sustainedCount = 1;
                    }
                    if (sustainedIncrease == increase)
                    {
                        sustainedIncrease = 0;
                        sustainedCount = 0;
                    }
                    label1.Text = string.Format("{0:F2}", sustainedIncrease) + " (" + sustainedCount + ")";
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
                    preIncrease1 = increase;



                    if (isSpeake && Math.Abs(increase - preIncrease) > (1.0m / positionList.Count))
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
                    labelDiff.Text = string.Format("{0:F2}", difInc);
                    if (difInc > 0)
                    {
                        labelDiff.ForeColor = Color.Red;
                    }
                    else if (difInc == 0)
                    {
                        labelDiff.ForeColor = Color.White;
                    }
                    else
                    {
                        labelDiff.ForeColor = Color.Cyan;
                    }

                    notifyIcon1.Text = string.Format("{0:F2}", increase);
                }
                catch (Exception)
                {
                    labelFunIncrease.Text = "-";
                    isFinished = true;
                }

                isFinished = true;
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

        private void pictureBoxVolume_MouseClick(object sender, MouseEventArgs e)
        {
            isSpeake = !isSpeake;
            if (isSpeake)
            {
                this.pictureBoxVolume.Image = global::FundMonitor.Properties.Resources.volume_on;
                //this.labelTime.ForeColor = System.Drawing.Color.Silver;
            }
            else
            {
                this.pictureBoxVolume.Image = global::FundMonitor.Properties.Resources.volume_off;
                //this.labelTime.ForeColor = System.Drawing.Color.White;
            }
            this.setValue("speake", isSpeake);
        }

        private void labelTime_MouseClick(object sender, MouseEventArgs e)
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
        public decimal cost;
        public int amount;
        public string exchange;

        public decimal increase;

        public Position(string code, string name, decimal cost, int amount,string exchange)
        {
            this.code = code;
            this.name = name;
            this.cost = cost;
            this.amount = amount;
            this.exchange = exchange;
        }
    }
}
