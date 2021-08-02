﻿using System.Collections.ObjectModel;

namespace Links.Models.Collections
{
    internal interface IGroup
    {
        string Name { get; set; }
        GroupIcon Icon { get; set; }
        ObservableCollection<LinkInfo> Links { get; }
    }
}