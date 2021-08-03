using System;
using System.Collections.Generic;

namespace Links.Models
{
    internal class StringSaver : IStringSaver
    {
        public bool IsEmpty => Source.Count == 0;
        public string Current => Source.Count == 0 ? null : Source[_index];

        public List<string> Source { get; }
        public bool CircleMode { get; set; }

        private int _index;
        public int Index
        {
            get => _index;
            set
            {
                if (Source == null)
                    throw new Exception("The Source is not initialized.");

                if (value < 0 || value >= Source.Count)
                    throw new IndexOutOfRangeException();

                _index = value;
            }
        }

        public StringSaver(IEnumerable<string> source = null, int index = -1)
        {
            Source = new List<string>();

            if (source != null)
                Source.AddRange(source);

            if (index == -1)
                _index = index;
            else
                Index = index;
        }

        public override string ToString()
        {
            return Current;
        }

        public void Add(object value)
        {
            Add(value.ToString());
        }

        public void Add(string value)
        {
            if (_index == Source.Count - 1)
                _index++;

            Source.Add(value);
        }

        public void Remove(int index)
        {
            if (index <= _index)
                _index--;

            Source.RemoveAt(index);
        }

        public string Back()
        {
            if (Source.Count == 0)
                return string.Empty;

            DecreaseIndex();
            return Current;
        }

        public string Next()
        {
            if (Source.Count == 0)
                return string.Empty;

            IncreaseIndex();
            return Current;
        }

        public void DecreaseIndex()
        {
            int backIndex = _index - 1;

            if (backIndex < 0)
                backIndex = CircleMode ? Source.Count - 1 : _index;

            _index = backIndex;
        }

        public void IncreaseIndex()
        {
            int nextIndex = _index + 1;

            if (nextIndex >= Source.Count)
                nextIndex = CircleMode ? 0 : _index;

            _index = nextIndex;
        }
    }
}
