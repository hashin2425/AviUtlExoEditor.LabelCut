using System;
using System.Collections.Generic;
using System.Linq;
using AviUtlExoEditor.LabelCut.Models.Item;
using AviUtlExoEditor.LabelCut.Models.Item.Filter;

namespace AviUtlExoEditor.LabelCut.Models
{
    internal static class AutoCutEditLogic
    {
        #region Method

        public static ExoItem AutoCutEdit(ExoItem orgExoItem, LabelItem[] labelItems)
        {
            ExoItem newExoItem = new();
            newExoItem.Width = orgExoItem.Width;
            newExoItem.Height = orgExoItem.Height;
            newExoItem.Rate = orgExoItem.Rate;
            newExoItem.Scale = orgExoItem.Scale;
            newExoItem.AudioRate = orgExoItem.AudioRate;
            newExoItem.AudioChannel = orgExoItem.AudioChannel;

            List<ObjectItem> objectItems = new();
            foreach (ObjectItem objectItem in orgExoItem.ObjectItems)
            {
                int currentFrame = 1;
                foreach (LabelItem item in labelItems)
                {
                    int startFrame = (int)Math.Ceiling(item.Start * newExoItem.Rate / newExoItem.Scale);
                    int endFrame = (int)Math.Floor(item.End * newExoItem.Rate / newExoItem.Scale);

                    ObjectItem newObjectItem = new();
                    newObjectItem.Start = currentFrame;
                    newObjectItem.End = currentFrame + endFrame - startFrame;
                    newObjectItem.Layer = objectItem.Layer;
                    newObjectItem.Overlay = objectItem.Overlay;
                    newObjectItem.Audio = objectItem.Audio;
                    newObjectItem.Camera = objectItem.Camera;
                    List<AbstractFilterItem> abstractFilterItems = new();
                    foreach (AbstractFilterItem filterItem in objectItem.Filters)
                    {
                        abstractFilterItems.Add(filterItem.Clone());
                    }
                    newObjectItem.Filters = abstractFilterItems.ToArray();

                    AudioFile audioFileFilter = (AudioFile)newObjectItem.Filters.FirstOrDefault(f => f is AudioFile);
                    if (audioFileFilter != null)
                    {
                        audioFileFilter.Position = item.Start;
                    }

                    MovieFile movieFileFilter = (MovieFile)newObjectItem.Filters.FirstOrDefault(f => f is MovieFile);
                    if (movieFileFilter != null)
                    {
                        movieFileFilter.Position = startFrame;
                    }

                    objectItems.Add(newObjectItem);
                    currentFrame = newObjectItem.End + 1;
                }
            }

            newExoItem.ObjectItems = objectItems.ToArray();
            newExoItem.Length = objectItems.Max(i => i.End);
            return newExoItem;
        }

        #endregion
    }
}
