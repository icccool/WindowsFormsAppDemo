using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsAppDemo.Print.bean
{
    public class ContentBlock
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
        public string fontSize { get; set; }
        /// <summary>
        /// 是否加粗(1加粗)
        /// </summary>
        public string bold { get; set; }
        /// <summary>
        /// 字段宽度
        /// </summary>
        public int width { get; set; }
        /// <summary>
        /// 字段高度
        /// </summary>
        public string height { get; set; }
        /// <summary>
        /// 字段位置（居左 居中  居右）
        /// </summary>
        public string align { get; set; }
        /// <summary>
        /// 距左边距离
        /// </summary>
        public int left { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int sortWeight { get; set; }
    }
}
