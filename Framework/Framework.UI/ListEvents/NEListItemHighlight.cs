using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Domain;

namespace Framework.AppInfrastructure
{
    public class NEListItemHighlight : NotifyEvent
    {
        public const int HighlightId = 1;
        public const int HighlightIndex = 2;

        public int Item;
        public int HighlightType;

        public bool HighlightById
        {
            get { return HighlightType == HighlightId; }
        }

        public bool HighlightByIndex
        {
            get { return HighlightType == HighlightIndex; }
        }
    }
}
