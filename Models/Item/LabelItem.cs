using Livet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AviUtlExoEditor.LabelCut.Models.Item
{
    public class LabelItem : NotificationObject
    {
        public float Start { get; set; }
        public float End { get; set; }
        public string Name { get; set; }
    }
}
