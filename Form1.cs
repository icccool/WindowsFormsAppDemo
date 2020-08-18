using CefSharp;
using CefSharp.WinForms;
using H3POS.Protocol.DocPrint;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsAppDemo.http;
using WindowsFormsAppDemo.Print;

namespace WindowsFormsAppDemo
{
    public partial class Form1 : Form
    {

        ChromiumWebBrowser browser;

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
            browser = new ChromiumWebBrowser("www.baidu.com");
            this.panel1.Controls.Add(browser);
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
        /// 模拟http请求
        /// </summary>
        private void button3_Click(object sender, EventArgs e)
        {

            string msg = HttpUtil.requestWithCookie(new Newtonsoft.Json.Linq.JObject());
            MessageBox.Show(msg);

        }
    }
}
