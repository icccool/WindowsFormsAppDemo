using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsAppDemo.Print.bean
{

    public class PosTemplate
    {
        public List<ContentLine> HeadList;
        public List<ContentLine> BodyList;
        public List<ContentLine> DataList;
        public List<ContentLine> TailList;

        //public string showCoupon { get; set; }
    }
    public class TicketTemplate
    {
        /// <summary>
        /// 打印内容
        /// </summary>
        public List<TemplateGroup> groups;
        /// <summary>
        /// 小票宽度
        /// </summary>
        public int ticketWidth { get; set; }
        /// <summary>
        /// 打印机名称
        /// </summary>
        public string printerName { get; set; }
        /// <summary>
        /// 类型(1有驱 2无驱)
        /// </summary>
        public int printType { get; set; }
    }

    public class TemplateGroup
    {
        /// <summary>
        /// type ('line':分割线,row且cols为空为空行)
        /// </summary>
        public string type { get; set; }
        public int sortWeight { get; set; }

        public List<TemplateRow> rows;

    }


    public class TemplateRow
    {
        public string type { get; set; }
        public int sortWeight { get; set; }
        public List<TemplateCol> cols;
    }


    public class TemplateCol
    {
        /// <summary>
        /// 字段标题
        /// </summary>
        public string label { get; set; }
        /// <summary>
        /// 字段变量
        /// </summary>
        public string key { get; set; }
        /// <summary>
        /// 字段值
        /// </summary>
        public string value { get; set; }
        /// <summary>
        /// 字号
        /// </summary>
        public int fontSize { get; set; } //新加
        /// <summary>
        /// 无驱打印字体倍数
        /// </summary>
        public string escFontSize { get; set; }//新加
        /// <summary>
        /// 是否加粗(true斜体加粗)
        /// </summary>
        public bool bold { get; set; }
        /// <summary>
        /// 是否斜体(true斜体)
        /// </summary>
        public bool italic { get; set; }
        /// <summary>
        /// 字段宽度
        /// </summary>
        public int width { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int sortWeight { get; set; }

        /// <summary>
        /// 字段类型  单个text居中 
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// 值的前缀
        /// </summary>
        public string valuePrefix { get; set; }

        /// <summary>
        /// 值的后缀
        /// </summary>
        public string valueSuffix { get; set; }


        private bool _isCenter = false;


        /// <summary>
        /// 是否居中 (客户端增加字段)  有驱存值用
        /// </summary>
        public bool isCenter
        {
            get { return _isCenter; }
            set { _isCenter = value; }
        }

        private bool _isWrap = false;
        public TemplateCol()
        {

        }

        public TemplateCol(string label, string value)
        {
            this.label = label;
            this.value = value;
        }

        /// <summary>
        /// 是否换行 (客户端增加字段)  有驱存值用 (暂不用)
        /// </summary>
        public bool isWrap
        {
            get { return _isWrap; }
            set { _isWrap = value; }
        }
        public float nextX { get; set; }//有驱存值用
        public float nextY { get; set; }//有驱存值用
        public float rectangleHeight { get; set; }//有驱存值用

    }

}
