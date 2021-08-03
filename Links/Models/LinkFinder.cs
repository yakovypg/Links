using Links.Models.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Links.Models
{
    internal class LinkFinder : ILinkFinder
    {
        public bool AddByTitleFlag { get; private set; }
        public bool AddByLinkFlag { get; private set; }
        public bool AddByDateFlag { get; private set; }

        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }

        public string Filter { get; }
        public string Flags { get; }

        public LinkFinder(string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                Filter = string.Empty;
                Flags = null;

                ParseFlags(null);
                return;
            }

            (string filter, string flags) = ParseData(data);

            Filter = filter;
            Flags = flags;
        }

        public LinkFinder(string filter, string flags = null)
        {
            Filter = filter;
            Flags = flags;

            ParseFlags(flags);
        }

        public override string ToString()
        {
            return $"{Filter}\t{Flags}";
        }

        public IEnumerable<LinkInfo> FindAmong(IEnumerable<Group> groups)
        {
            var foundLinks = new List<LinkInfo>();

            if (groups == null)
                return foundLinks;

            Func<LinkInfo, bool> addByTitle = t => t.Title.Contains(Filter, StringComparison.OrdinalIgnoreCase);
            Func<LinkInfo, bool> addByLink = t => t.Link.Contains(Filter, StringComparison.OrdinalIgnoreCase);
            Func<LinkInfo, bool> addByDate = t => t.DateCreation >= StartDate && t.DateCreation <= EndDate;

            foreach (Group group in groups)
            {
                var currItems = new List<LinkInfo>();

                if (AddByTitleFlag)
                    currItems.AddRange(group.Links.Where(addByTitle));

                if (AddByLinkFlag)
                    currItems.AddRange(group.Links.Where(addByLink));

                if (AddByDateFlag)
                    currItems.AddRange(group.Links.Where(addByDate));

                foundLinks.AddRange(currItems.Distinct());
            }

            return foundLinks;
        }

        private void ParseFlags(string flags)
        {
            AddByTitleFlag = flags?.Contains("-t") ?? true;
            AddByLinkFlag = flags?.Contains("-l") ?? true;
            AddByDateFlag = flags?.Contains("-d") ?? false;

            if (AddByDateFlag)
            {
                try
                {
                    int index = flags.LastIndexOf("-d");
                    string flagData = flags.Substring(index + 2);
                    string[] parameters = flagData.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

                    string startDate = null;
                    string endDate = null;

                    bool isStartDateFull = parameters.Length >= 3 && parameters[1].Contains(":");

                    bool isEndDateFull = (parameters.Length >= 3 && parameters[2].Contains(":")) ||
                                         (parameters.Length >= 4 && parameters[3].Contains(":"));

                    if (parameters.Length <= 2)
                    {
                        startDate = parameters[0];
                        endDate = parameters[1];
                    }
                    else
                    {
                        if (isStartDateFull)
                        {
                            startDate = $"{parameters[0]} {parameters[1]}";
                            endDate = isEndDateFull ? $"{parameters[2]} {parameters[3]}" : parameters[2];
                        }
                        else
                        {
                            startDate = parameters[0];
                            endDate = isEndDateFull ? $"{parameters[1]} {parameters[2]}" : parameters[1];
                        }
                    }

                    StartDate = DateTime.Parse(startDate);
                    EndDate = DateTime.Parse(endDate);
                }
                catch
                {
                    AddByDateFlag = false;
                }
            }
        }

        private (string filter, string flags) ParseData(string data)
        {
            string filter, flags;
            int tabIndex = data.LastIndexOf("\t");

            if (tabIndex >= 0 && tabIndex < data.Length - 1)
            {
                filter = data.Remove(tabIndex);
                flags = data.Substring(tabIndex + 1);
            }
            else
            {
                filter = data;
                flags = null;
            }

            ParseFlags(flags);

            return (filter, flags);
        }
    }
}
