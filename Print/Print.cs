using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsAppDemo.Print.bean;

namespace WindowsFormsAppDemo.Print
{
    public class Print
    {
        Margins margin;
        PaperSize pageSize;
        string printerName;
        string docName;
        TicketTemplate template;
        float positonX = 0;//打印坐标x
        float positonY = 0;//打印坐标y
        //new 
        public Print(string DocName, TicketTemplate ticketTemplate)
        {
            this.printerName = ticketTemplate.printerName;
            this.docName = DocName;
            this.margin = new Margins(0, 0, 5, 5);
            var ticketWidth = (int)getYc(ticketTemplate.ticketWidth);
            this.pageSize = new PaperSize("", ticketWidth, 10826);//189, 826
            this.template = ticketTemplate;
        }


        PrintDocument GetPrintDocument()
        {
            PrintDocument doc = new PrintDocument();
            //设置边距
            doc.DefaultPageSettings.Margins = margin;
            ////纸张设置默认
            doc.DefaultPageSettings.PaperSize = pageSize;

            doc.PrintPage += new PrintPageEventHandler(PrintPageEvent);
            if (!string.IsNullOrEmpty(printerName))
            {
                doc.PrinterSettings.PrinterName = printerName;
            }

            return doc;
        }

        private float getYc(double cm)
        {
            return (float)(100 * cm / 25.4);
        }

        void PrintPageEvent(object sender, PrintPageEventArgs ev) //TODO 文字截取
        {
            #region 测试数据
            //TemplateGroup group1;
            //TemplateRow row1;
            //TemplateCol col1;
            //template = new TicketTemplate();
            //template.groups = new List<TemplateGroup>();
            ////头
            //group1 = new TemplateGroup();
            //group1.rows = new List<TemplateRow>();
            //row1 = new TemplateRow();
            //row1.cols = new List<TemplateCol>();
            //col1 = new TemplateCol();
            //col1.type = "text";
            //col1.label = "标题";
            //col1.fontSize = 12;
            //col1.bold = true;
            //col1.width = 30;
            //row1.cols.Add(col1);
            //group1.rows.Add(row1);

            //row1 = new TemplateRow();
            //group1.rows.Add(row1);

            //row1 = new TemplateRow();
            //row1.type = "normal";
            //row1.cols = new List<TemplateCol>();
            //col1 = new TemplateCol();
            //col1.type = "normal";
            //col1.label = "字段1:";
            //col1.value = "字段1的值字段1的值";
            //col1.width = 20;
            //row1.cols.Add(col1);
            //col1 = new TemplateCol();
            //col1.type = "normal";
            //col1.label = "字段2：";
            //col1.value = "字段2的值字段2的值";
            //col1.width = 20;
            //row1.cols.Add(col1);
            //group1.rows.Add(row1);

            //row1 = new TemplateRow();
            //group1.rows.Add(row1);

            //row1 = new TemplateRow();
            //row1.type = "normal";
            //row1.cols = new List<TemplateCol>();
            //col1 = new TemplateCol();
            //col1.type = "text";
            //col1.label = "地址:";
            //col1.value = "浦东金桥华宏创新园35号楼浦东金桥华宏创新园35号楼";
            //col1.width = 50;
            //row1.cols.Add(col1);
            //group1.rows.Add(row1);

            //row1 = new TemplateRow();
            //row1.type = "line";
            //group1.rows.Add(row1);

            //row1 = new TemplateRow();
            //row1.type = "normal";
            //row1.cols = new List<TemplateCol>();
            //col1 = new TemplateCol();
            //col1.type = "text";
            //col1.label = "地址:";
            //col1.value = "浦东金桥华宏创新园35号楼浦东金桥华宏创新园35号楼";
            //col1.width = 50;
            //row1.cols.Add(col1);
            //group1.rows.Add(row1);
            //template.groups.Add(group1);

            ////身
            //group1 = new TemplateGroup();
            //group1.type = "body";
            //group1.rows = new List<TemplateRow>();
            //row1 = new TemplateRow();
            //row1.cols = new List<TemplateCol>();
            //col1 = new TemplateCol();
            //col1.type = "normal";
            //col1.label = "品名";
            //col1.fontSize = 8;
            //col1.bold = true;
            //col1.width = 15;
            //row1.cols.Add(col1);
            //col1 = new TemplateCol();
            //col1.type = "normal";
            //col1.label = "规格";
            //col1.fontSize = 8;
            //col1.bold = true;
            //col1.width = 15;
            //row1.cols.Add(col1);
            //col1 = new TemplateCol();
            //col1.type = "normal";
            //col1.label = "材质";
            //col1.fontSize = 8;
            //col1.bold = true;
            //col1.width = 15;
            //row1.cols.Add(col1);
            //group1.rows.Add(row1);

            //row1 = new TemplateRow();
            //row1.cols = new List<TemplateCol>();
            //col1 = new TemplateCol();
            //col1.type = "normal";
            //col1.value = "阿莫西林颗粒";
            //col1.fontSize = 8;
            //col1.width = 15;
            //row1.cols.Add(col1);
            //col1 = new TemplateCol();
            //col1.type = "normal";
            //col1.value = "20片/盒";
            //col1.fontSize = 8;
            //col1.width = 15;
            //row1.cols.Add(col1);
            //col1 = new TemplateCol();
            //col1.type = "normal";
            //col1.value = "对乙酰氨基酚";
            //col1.fontSize = 8;
            //col1.width = 15;
            //row1.cols.Add(col1);
            //group1.rows.Add(row1);

            //row1 = new TemplateRow();
            //row1.cols = new List<TemplateCol>();
            //col1 = new TemplateCol();
            //col1.type = "normal";
            //col1.value = "安乃近安乃近";
            //col1.fontSize = 8;
            //col1.bold = true;
            //col1.width = 15;
            //row1.cols.Add(col1);
            //col1 = new TemplateCol();
            //col1.type = "normal";
            //col1.value = "20片/盒";
            //col1.fontSize = 8;
            //col1.bold = true;
            //col1.width = 15;
            //col1 = new TemplateCol();
            //col1.type = "normal";
            //col1.value = "对乙酰氨基酚";
            //col1.fontSize = 8;
            //col1.bold = true;
            //col1.width = 15;
            //row1.cols.Add(col1);
            //group1.rows.Add(row1);

            //template.groups.Add(group1);
            #endregion
            try
            {
                positonX = ev.MarginBounds.Left;
                positonY = ev.MarginBounds.Top;
                bool hasMember = false;
                template.groups.ForEach(group =>
                {
                    group.rows.ForEach(row =>
                    {
                        if (row.type == "line")//分割线
                        {
                            var templateCol = new TemplateCol()
                            {
                                label = "----------------------------------------------------------",
                                width = ev.PageBounds.Width
                            };
                            DrawLine(ev, templateCol);
                            positonY += 20;
                        }
                        else
                        {
                            //非空行
                            if (row.cols != null && row.cols.Count > 0)
                            {
                                var strLine = string.Empty;
                                if (row.cols.Count == 1)
                                {
                                    //单行布局
                                    var curCol = row.cols[0];
                                    curCol.isCenter = true;

                                    if (curCol.key == "coupons" && !string.IsNullOrEmpty(curCol.value))
                                    {
                                        //打印券码
                                        DrawBarCode(ev, curCol);
                                    }
                                    else
                                    {
                                        DrawLine(ev, curCol);
                                    }
                                    positonY += curCol.rectangleHeight;
                                }
                                else
                                {
                                    if (row.cols.Count == 1)//单行单字段允许换行
                                    {
                                        var curCol = row.cols[0];
                                        DrawLine(ev, curCol);
                                        positonY += curCol.rectangleHeight;
                                    }
                                    else//多个字段矩形截断
                                    {
                                        //是否包含会员
                                        for (int i = 0; i < row.cols.Count; i++)
                                        {
                                            var curCol = row.cols[i];
                                            if ((curCol.key.IndexOf("memberCardNumber") > -1 ||
                                            curCol.key.IndexOf("memberName") > -1) &&
                                            !string.IsNullOrEmpty(curCol.value))
                                            {
                                                hasMember = true;
                                                break;
                                            }
                                        }

                                        bool isBlankLine = false;
                                        row.cols.ForEach(col =>
                                        {
                                            if (hasMember)
                                            {
                                                //如果有会员打印所有信息
                                                DrawLine(ev, col);
                                            }
                                            else
                                            {
                                                if (col.key == "integralBalance"
                                                || col.key == "receiveCredits"
                                                || col.key == "totalCredits"
                                                || col.key.IndexOf("memberCardNumber") > -1
                                                || col.key.IndexOf("memberName") > -1
                                                || col.key == "memberCardName")
                                                {
                                                    //没有会员的情况： 不打印会员相关信息
                                                    isBlankLine = true;
                                                }
                                                else
                                                {
                                                    DrawLine(ev, col);
                                                }
                                            }
                                        });

                                        //以最大的矩形作为行高
                                        float lineHeight = 0;
                                        row.cols.ForEach(col =>
                                        {
                                            if (col.rectangleHeight > lineHeight)
                                            {
                                                lineHeight = isBlankLine ? 0 : col.rectangleHeight;
                                            }
                                        });
                                        positonY += lineHeight;
                                    }
                                }
                            }
                            else//空行
                            {
                                positonY += 15;
                            }
                        }
                        positonX = ev.MarginBounds.Left;
                    });
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 打印二维码
        /// </summary>
        /// <param name="e"></param>
        /// <param name="templateCol"></param>
        public void DrawBarCode(PrintPageEventArgs e, TemplateCol templateCol)
        {
            var printableArea = e.PageSettings.PrintableArea;
            if (templateCol == null)
            {
                return;
            }
            float offsetWidth = positonX;//水平偏移量
            float offsetHeight = positonY;//垂直偏移量
            float rectangleHeight = (float)(30);//条码高度
            float rectangleWidth = getYc(templateCol.width);//矩形宽度
            ArrayList values = JsonConvert.DeserializeObject<ArrayList>(templateCol.value);
            Code128 c = new Code128();
            c.DefaultX = (int)offsetWidth + 50;
            c.DefaultY = (int)offsetHeight;
            if (values.Count > 0)
            {
                for (int i = 0; i < values.Count; i++)
                {
                    JObject jObject = (JObject)values[i];
                    //面额
                    string couponAmount = jObject["couponAmount"].ToString();

                    //e.Graphics.DrawString("优惠券：" + couponAmount + "元", new Font("宋体", 10f), new SolidBrush(Color.Black), new PointF(c.DefaultX, 20 + c.DefaultY));
                    //c.DefaultY += 20;
                    //positonY += 20;

                    //打印券码
                    string couponCode = jObject["couponCode"].ToString();
                    if (couponCode.IndexOf("P") > -1)
                    {
                        couponCode = couponCode.Replace("P", "");
                    }
                    Code128.Encode enCode = Code128.Encode.Code128C;
                    if (!((couponCode.Length & 1) == 0))
                    {
                        enCode = Code128.Encode.Code128B;
                    }
                    Bitmap bmp = c.GetCodeImage(couponCode, enCode, e.Graphics);
                    c.DefaultY += (bmp.Height + 10);
                    positonY += (bmp.Height + 10);


                    //分割线
                    //DrawLine(e, new TemplateCol()
                    //{
                    //    label = "----------------------------------------------------------",
                    //    width = templateCol.width
                    //});
                    //c.DefaultY += 20;
                    //positonY += 20;

                }
            }
            //记录移动到的点
            templateCol.rectangleHeight = rectangleHeight;
        }

        //StringFormat sf = new StringFormat();
        //sf.Trimming = StringTrimming.EllipsisCharacter;//省略号
        //字体颜色
        //Brush brush = new SolidBrush(Color.FromArgb(51, 51, 51, 1));
        //换行处理 https://www.cnblogs.com/ANPY/archive/2015/10/19/4891962.html
        public void DrawLine(PrintPageEventArgs e, TemplateCol templateCol)
        {
            var printableArea = e.PageSettings.PrintableArea;
            if (templateCol == null)
            {
                return;
            }
            FontStyle style = FontStyle.Regular;
            float fontsize = templateCol.fontSize > 0 ? templateCol.fontSize : 8;
            float offsetWidth = positonX;//水平偏移量
            float offsetHeight = positonY;//垂直偏移量
            string val = templateCol.value;
            //前缀
            if (!String.IsNullOrEmpty(templateCol.valuePrefix) && !String.IsNullOrEmpty(templateCol.value))
            {
                val = templateCol.valuePrefix + templateCol.value;
            }
            //后缀
            if (!String.IsNullOrEmpty(templateCol.valueSuffix) && !String.IsNullOrEmpty(templateCol.value))
            {
                val = (val + templateCol.valueSuffix);
            }
            string content = templateCol.label + val;

            var pageWidth = e.PageBounds.Width;
            if (offsetWidth > pageWidth) return;//偏移量大于小票宽度，不打印

            if (templateCol.bold)//加粗
            {
                style = FontStyle.Bold;
            }
            if (templateCol.italic)//斜体
            {
                style = FontStyle.Italic;
            }
            Font font = new Font("微软雅黑", fontsize, style);
            SizeF sizeText = e.Graphics.MeasureString(content, font);//宽高单位：像素
            float rectangleHeight = (float)(sizeText.Height);////文字高度 or 矩形高度,1,2增加间距
            float rectangleWidth = getYc(templateCol.width);//矩形宽度
            //格式
            StringFormat fmt = new StringFormat();
            fmt.LineAlignment = StringAlignment.Near;//左对齐
            fmt.FormatFlags = StringFormatFlags.LineLimit;//自动换行
            //居中
            if (templateCol.isCenter)
            {
                offsetWidth = (e.PageBounds.Width - sizeText.Width) / 2;//偏移量//228-92.7=67.6
                if (offsetWidth < 0)
                {
                    offsetWidth = 0;
                }
            }

            //允许换行，1.内容宽度>设置宽度  2.实际宽度大于小票宽度再换行   templateCol.isWrap 
            if (sizeText.Width > rectangleWidth || (offsetWidth + sizeText.Width > pageWidth))
            {
                //根据实际字符计算行数
                int lines = (int)(sizeText.Width / rectangleWidth) + 1;
                //默认展示两行
                //int lines = 2; 
                rectangleHeight = sizeText.Height * lines;
            }

            RectangleF descRect = new RectangleF(offsetWidth, offsetHeight, rectangleWidth, rectangleHeight);

            e.Graphics.DrawString(content, font, Brushes.Black, descRect, fmt);

            //记录移动到的点
            positonX = offsetWidth + rectangleWidth;
            templateCol.rectangleHeight = rectangleHeight;
        }

        public void print()
        {
            GetPrintDocument().Print();
        }

    }
}
