using System.Collections.Generic;

namespace Links.Models
{
    internal interface IStringSaver
    {
        bool IsEmpty { get; }
        string Current { get; }

        List<string> Source { get; }
        bool CircleMode { get; set; }
        int Index { get; set; }

        void Add(object value);
        void Add(string value);
        void Remove(int index);

        string Back();
        string Next();
    }
}
