using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;

namespace Links.Models.Collections
{
    internal class GroupIcon : IGroupIcon, INotifyPropertyChanged, ICloneable, IComparable, IComparable<GroupIcon>
    {
        public enum Colors
        {
            Black,
            Brown,
            White,
            Wheat,
            Silver,
            LightGray,
            Gray,
            Yellow,
            Gold,
            Orange,
            Lime,
            LightGreen,
            Green,
            Violet,
            Blue,
            LightBlue,
            Red,
            Pink,
            Purple
        }

        public string DisplayName => ColorName;
        public string ColorName => ForegroundColor.ToString();

        private Brush _foreground;
        public Brush Foreground
        {
            get => _foreground;
            private set
            {
                _foreground = value;
                OnPropertyChanged(nameof(Foreground));
            }
        }

        private Colors _foregroundColor;
        public Colors ForegroundColor
        {
            get => _foregroundColor;
            set
            {
                _foregroundColor = value;
                _colorIndex = (int)value;
                Foreground = GetBrushFromIndex((int)value);
            }
        }

        private int _colorIndex;
        public int ColorIndex
        {
            get => _colorIndex;
            set
            {
                _colorIndex = value;
                _foregroundColor = (Colors)value;
                Foreground = GetBrushFromIndex(value);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        public GroupIcon() : this(Colors.Gold)
        {
        }

        public GroupIcon(Colors color)
        {
            ForegroundColor = color;
        }

        public Brush GetBrushFromIndex(int index)
        {
            switch ((Colors)index)
            {
                case Colors.Black: return Brushes.Black;
                case Colors.Brown: return Brushes.Brown;
                case Colors.White: return Brushes.White;
                case Colors.Wheat: return Brushes.Wheat;
                case Colors.Silver: return Brushes.Silver;
                case Colors.LightGray: return Brushes.LightGray;
                case Colors.Gray: return Brushes.Gray;
                case Colors.Yellow: return Brushes.Yellow;
                case Colors.Gold: return Brushes.Gold;
                case Colors.Orange: return Brushes.Orange;
                case Colors.Lime: return Brushes.Lime;
                case Colors.LightGreen: return Brushes.LightGreen;
                case Colors.Green: return Brushes.Green;
                case Colors.Violet: return Brushes.Violet;
                case Colors.Blue: return Brushes.Blue;
                case Colors.LightBlue: return Brushes.LightBlue;
                case Colors.Red: return Brushes.Red;
                case Colors.Pink: return Brushes.Pink;
                case Colors.Purple: return Brushes.Purple;

                default: return Brushes.Gold;
            }
        }

        public int CompareTo(GroupIcon other)
        {
            return ColorName.CompareTo(other.ColorName);
        }

        public int CompareTo(object other)
        {
            return other is GroupIcon icon
                ? CompareTo(icon)
                : 1;
        }

        public object Clone()
        {
            return new GroupIcon(ForegroundColor);
        }

        public override string ToString()
        {
            return DisplayName;
        }

        public override bool Equals(object obj)
        {
            return obj is GroupIcon other && ColorIndex == other.ColorIndex;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(DisplayName, ColorName, _foreground, Foreground, _foregroundColor, ForegroundColor, _colorIndex, ColorIndex);
        }
    }
}
