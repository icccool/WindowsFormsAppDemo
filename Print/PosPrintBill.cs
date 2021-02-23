using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsAppDemo.Enums;
using WindowsFormsAppDemo.Print.bean;

namespace WindowsFormsAppDemo.Print
{

    public class PosPrintBill
    {
        string json = string.Empty;

        TicketTemplate ticketTemplate = new TicketTemplate();

        OpenBoxType printType;

        public PosPrintBill(string jsonText)
        {
            json = jsonText;
        }
        
        public void print()
        {
            try
            {
                if (string.IsNullOrEmpty(json))
                {
                    MessageBox.Show("打印数据为空！");
                    return;
                };
                ticketTemplate = JsonConvert.DeserializeObject<TicketTemplate>(json);
                printType = ticketTemplate.printType == 1 ? OpenBoxType.Normal : OpenBoxType.Lpt;
                if (ticketTemplate == null || ticketTemplate.groups == null)
                {
                    MessageBox.Show("模板对象为空！");
                    return;
                }
                switch (printType)
                {
                    case OpenBoxType.Normal:
                        normalPrint();
                        break;
                    case OpenBoxType.Lpt:
                        lptPrint();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("打印失败！" + ex.Message);
            }
        }
        /// <summary>
        /// 串口打印方式
        /// </summary>
        private void normalPrint()
        {
            Print docPrint = new Print("pos小票", ticketTemplate);
            docPrint.print();
        }

        /// <summary>
        /// lpt打印方式
        /// </summary>
        private void lptPrint()
        {
            //PrintLPT docPrint = new PrintLPT("pos小票", ticketTemplate);
            //docPrint.print();
        }

    }
}
