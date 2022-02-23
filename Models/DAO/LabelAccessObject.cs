using System.Collections.Generic;
using System.IO;
using System.Text;
using AviUtlExoEditor.LabelCut.Models.Item;

namespace AviUtlExoEditor.LabelCut.Models.DAO
{
    internal static class LabelAccessObject
    {
        #region Method

        public static LabelItem[] Deserialize(string filePath)
        {
            if (!File.Exists(filePath)) return null;

            List<LabelItem> items = new();

            foreach (string line in File.ReadLines(filePath, Encoding.UTF8))
            {
                LabelItem item = new();

                var splittedLine = line.Split('\t');

                item.Start = float.Parse(splittedLine[0]);
                item.End = float.Parse(splittedLine[1]);
                item.Name = splittedLine[2];

                items.Add(item);
            }

            return items.ToArray();
        }

        #endregion
    }
}
