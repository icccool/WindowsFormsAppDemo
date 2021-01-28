using CefSharp;
using CefSharp.WinForms;
using H3POS.Protocol.DocPrint;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsAppDemo.Api;
using WindowsFormsAppDemo.cef;
using WindowsFormsAppDemo.http;
using WindowsFormsAppDemo.Print;
using WindowsFormsAppDemo.Time;

namespace WindowsFormsAppDemo
{
    public partial class Form1 : Form
    {
        ChromiumWebBrowser browser;
        //IpconfigHelper ip = new IpconfigHelper();

        public Form1()
        {
            InitializeComponent();
            InitBrowser();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
        }

        public void InitBrowser()
        {
            Console.WriteLine(Cef.ChromiumVersion);
            //Cef.Initialize(new CefSettings());
            browser = new ChromiumWebBrowser("www.baidu.com")
            {
                KeyboardHandler = new CEFKeyBoardHander()
            };
            //this.panel1_Paint.Controls.Add(browser);
            browser.Dock = DockStyle.Fill;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("     力东上元路(二店)  \n");
            sb.Append("*************************************\n");
            sb.Append("进场时间：" + DateTime.Now.ToString() + "\n");
            sb.Append("出场时间：" + DateTime.Now.AddHours(2).ToString() + "\n");
            sb.Append("停车时长：   2   小时\n");
            sb.Append("停车收费：   5     元\n");
            sb.Append("*************************************\n");
            new PosPrint().Print(sb.ToString());
        }

        /// <summary>
        /// 开钱箱
        /// </summary>
        private void button2_Click(object sender, EventArgs e)
        {
            RawPrinterHelper.OpenCashDrawer();
        }


        /// <summary>
        /// 模拟携带cookie http请求
        /// </summary>
        private void button3_Click(object sender, EventArgs e)
        {
            string msg = HttpUtil.requestWithCookie(new Newtonsoft.Json.Linq.JObject());
            MessageBox.Show(msg);
        }

        /// <summary>
        /// 模拟下载文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void button4_Click(object sender, EventArgs e)
        {
            string url = "http://192.168.10.58/hwimg/static/upload/default/2019-09-05-07-06-002/template.txt";
            string path = System.AppDomain.CurrentDomain.BaseDirectory + "template.txt";
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            bool bl = HttpUtil.HttpDownload(url, path);
            MessageBox.Show("下载路径" + path + (bl ? "成功" : "失败"));
        }

        /// <summary>
        /// 按键事件
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                MessageBox.Show("按下了Delete");
            }
        }

        /// <summary>
        /// 打印条码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            PrintPreviewDialog pa = new PrintPreviewDialog();//打印对话框

            PrintDocument printDocument1 = new PrintDocument();
            printDocument1.DefaultPageSettings.PaperSize = new PaperSize("aa", 180, 160);
            printDocument1.PrintPage += new PrintPageEventHandler(PrintPageEvent);
            // printDocument1.Print();
            pa.Document = printDocument1;
            pa.ShowDialog();//预览
        }


        //打印的页面
        void PrintPageEvent(object sender, PrintPageEventArgs ev)
        {
            Code128 c = new Code128();
            string s = "1000706120101452112222";
            if (!((s.Length & 1) == 0))
            {
                Bitmap bmp = c.GetCodeImage(s, Code128.Encode.Code128B, ev.Graphics);
            }
            else
            {
                Bitmap bmp = c.GetCodeImage(s, Code128.Encode.Code128C, ev.Graphics);
            }
        }

        /// <summary>
        /// d
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_Click(object sender, EventArgs e)
        {
            ArrayList values = JsonConvert.DeserializeObject<ArrayList>("[]");
            PrintDocument pd = new PrintDocument();//new能够被打印机使用的对线
            PrintPreviewDialog pa = new PrintPreviewDialog();//打印对话框
            //pd.PrinterSettings.PrinterName = ip.PrinterName;//打印机名称
            pd.PrintController = new StandardPrintController();//获取打印机进程的控制器
            pd.PrintPage += DrawPage;
            pd.DefaultPageSettings.PaperSize = new PaperSize("票据", 150, 80);//设置纸张的大小
            pa.Document = pd;
            pd.Print();
            //pa.ShowDialog();//预览
        }

        /// <summary>
        /// 画取号票
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void DrawPage(object sender, PrintPageEventArgs e)
        {

            float x, y;
            float leftMargin = 0, topMargin = 0;

            //画标题
            Font font = new Font("黑体", 20);
            x = leftMargin;
            y = topMargin;

            //条码
            String sourceText = "A10007016201";
            Image image;
            BarcodeLib.Barcode b = new BarcodeLib.Barcode();
            b.BackColor = System.Drawing.Color.White;//设置图片背景
            b.ForeColor = System.Drawing.Color.Black;//设置字体颜色
            b.ImageFormat = System.Drawing.Imaging.ImageFormat.Jpeg;
            font = new Font("宋体", 1);
            b.LabelFont = font;
            b.Height = 50;
            b.Width = 131;
            image = b.Encode(BarcodeLib.TYPE.CODE128, sourceText);
            e.Graphics.DrawImage(image, 155, 70, 100, 65);
            e.Graphics.DrawString(sourceText, new Font("宋体", 10f), new SolidBrush(Color.Black), new PointF(155, 74 + image.Width));
        }



        /// <summary>
        /// 打印58mm收费小票
        /// </summary>
        /// <param name="printStr"></param>
        public void Print58(string printStr)
        {
            try
            {
                var printList = printStr.Split('\r', '\n').ToList();
                var rows = printList.Count(o => !string.IsNullOrEmpty(o));
                //打印预览
                // var previewDialog = new PrintPreviewDialog();
                var printDocument = new PrintDocument();
                //设置边距
                var margin = new Margins(1, 1, 10, 10);
                printDocument.DefaultPageSettings.Margins = margin;
                var height = (rows + 1) * 19;//计算高度(行数*行高)
                var pageSize = new PaperSize("First custom size", (int)(58 * 100 / 25.4), height);//58mm
                printDocument.DefaultPageSettings.PaperSize = pageSize;
                //打印事件设置
                printDocument.PrintPage += (a, b) =>
                {
                    var font = new Font("Arial", 8, FontStyle.Regular);//字体设置
                    var yLocation = b.MarginBounds.Y; //Y轴打印位置
                    foreach (var print in printList)
                    {
                        float heightStep = 0;
                        if (!string.IsNullOrEmpty(print))
                        {
                            var size = b.Graphics.MeasureString(print, font);
                            heightStep = size.Height * 1.3f;//1.3倍行间距
                            b.Graphics.DrawString(print, font, Brushes.Black, 0, yLocation);
                        }
                        yLocation += Convert.ToInt32(heightStep);
                    }
                    b.Graphics.DrawString(".", font, Brushes.Black, 0, yLocation); //多打一行,因为汉印打印机不打空白行
                };
                printDocument.Print();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "打印收据失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 打印58小票button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            //
            //取得当前系统时间
            //DateTime t = DateTime.Now;
            //在当前时间上加上一周
            //t = t.AddDays(7);

            DateTime netDate = GetInternetDate();
            //转换System.DateTime到SYSTEMTIME
            SYSTEMTIME st = new SYSTEMTIME();
            st.FromDateTime(netDate);
            //调用Win32 API设置系统时间
            Win32API.SetLocalTime(ref st);
            //显示当前时间
            MessageBox.Show(DateTime.Now.ToString());
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        public DateTime GetInternetDate()
        {
            try
            {
                var myHttpWebRequest = (HttpWebRequest)WebRequest.Create("https://www.baidu.com/");
                myHttpWebRequest.Timeout = 3000;
                var response = myHttpWebRequest.GetResponse();
                string todaysDates = response.Headers["date"];
                DateTime dt = DateTime.ParseExact(todaysDates,
                                           "ddd, dd MMM yyyy HH:mm:ss 'GMT'",
                                           CultureInfo.InvariantCulture.DateTimeFormat,
                                           DateTimeStyles.AssumeUniversal);
                response.Close();
                return dt;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return DateTime.Now;
        }
    }
}