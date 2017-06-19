using System.Collections.Generic;

namespace Snake.HighscoreManagment
{
    class HighscoreItemComparer :  IComparer<HighscoreItem>
    {
        private SortMethod _sortMethod;

        public HighscoreItemComparer(SortMethod sortMethod)
        {
            _sortMethod = sortMethod;
        }

        public int Compare(HighscoreItem x, HighscoreItem y)
        {            
            return x.CompareTo(y, _sortMethod);
        }
    }
}
