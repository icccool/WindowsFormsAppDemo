﻿using H3POS.Protocol.DocPrint;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsAppDemo.Print
{
    /// <summary>
    /// 热敏打印测试
    /// </summary>
    class PosPrint
    {

        public static void OpenCashDrawer()
        {
            PrintDocument pdc = new PrintDocument();
            string printerName = pdc.PrinterSettings.PrinterName;            //
            //string send = "" + (char)(27) + (char)(64) + (char)(27) + 'J' + (char)(255);    //标准            
            string send = "" + (char)(27) + (char)(112) + (char)(0) + (char)(60) + (char)(255);//非标准            
            RawPrinterHelper.SendStringToPrinter(printerName, send);
        }

      

        //定义一个字符串流，用来接收所要打印的数据
        private StringReader printContent;
        //str要打印的数据
        public bool Print(string str)
        {

            string l_strDefaultPortName = "";
            string l_strDefaultBrand = "";
        
            //获取默认打印机的相关信息
            string l_strSQL = string.Format("SELECT * from Win32_Printer where default = true ");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(l_strSQL);
            ManagementObjectCollection printers = searcher.Get();
            foreach (ManagementObject print in printers)
            {
                l_strDefaultPortName = print["PortName"].ToString();
                l_strDefaultBrand = print["DriverName"].ToString();//驱动名称不能冲突 做为识别牌子的标准
            }
            if (string.IsNullOrEmpty(l_strDefaultPortName)) return false;

            //string l_strNetIP = serverCommon.ReadLocalSet("PrintSet", "NetIP", "192.168.0.31");
            //string l_strNetPort = serverCommon.ReadLocalSet("PrintSet", "NetPort", "9100");
            //string l_strBaudRate = serverCommon.ReadLocalSet("PrintSet", "BaudRate", "38400");
            //string l_strDataBits = serverCommon.ReadLocalSet("PrintSet", "DataBits", "128");
            //int l_intPortCOM = Int32.Parse(serverCommon.ReadLocalSet("PrintSet", "PortCOM", "2"));
            //int l_intBaudRateCOM = Int32.Parse(serverCommon.ReadLocalSet("PrintSet", "BaudRateCOM", "9600"));
            //int l_intDataBitsCOM = Int32.Parse(serverCommon.ReadLocalSet("PrintSet", "DataBitsCOM", "8"));


            bool result = true;
            try
            {
                printContent = new StringReader(str.ToString());
                PrintDocument pd = new PrintDocument();
                pd.PrintController = new System.Drawing.Printing.StandardPrintController();
                pd.DefaultPageSettings.Margins.Top = 2;
                pd.DefaultPageSettings.Margins.Left = 0;
                //pd.DefaultPageSettings.PaperSize.Width = 320;
                //pd.DefaultPageSettings.PaperSize.Height = 5150;
                //DL-581P
                pd.PrinterSettings.PrinterName = pd.DefaultPageSettings.PrinterSettings.PrinterName;//默认打印机
                //pd.PrinterSettings.PrinterName = "DL-581P";
                pd.PrintPage += new PrintPageEventHandler(this.pd_PrintPage);
                pd.Print();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Unkown Exception:" + ex);
                result = false;
            }
            finally
            {
                if (printContent != null)
                    printContent.Close();
            }
            return result;
        }



        private void pd_PrintPage(object sender, PrintPageEventArgs ev)
        {
            Font printFont = new Font("Arial", 9);//打印字体

            float linesPerPage = 0;
            float yPos = 0;
            int count = 0;
            float leftMargin = ev.MarginBounds.Left;
            float topMargin = ev.MarginBounds.Top;
            String line = "";

            linesPerPage = ev.MarginBounds.Height / printFont.GetHeight(ev.Graphics);

            StringFormat sdf = new StringFormat();

            while (count < linesPerPage && ((line = printContent.ReadLine()) != null))
            {
                yPos = topMargin + (count * printFont.GetHeight(ev.Graphics));
                ev.Graphics.DrawString(line, printFont, Brushes.Black,
                   leftMargin, yPos, sdf);
                count++;
            }

            // If more lines exist, print another page.
            if (line != null)
                ev.HasMorePages = true;
            else
                ev.HasMorePages = false;
        }


    }


}
