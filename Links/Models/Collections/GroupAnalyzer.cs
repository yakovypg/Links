using Links.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Links.Models.Collections
{
    internal class GroupAnalyzer
    {
        public bool ContainsLink(IEnumerable<Group> groups, LinkInfo link)
        {
            if (link == null || groups == null)
                return false;

            foreach (var group in groups)
            {
                if (group.Contains(link))
                    return true;
            }

            return false;
        }

        public bool ContainsLink(IEnumerable<Group> groups, LinkInfo link, IEqualityComparer<LinkInfo> comparer)
        {
            if (link == null || groups == null)
                return false;

            foreach (var group in groups)
            {
                if (group.Contains(link, comparer))
                    return true;
            }

            return false;
        }

        public IEnumerable<LinkInfo> GetAllLinks(IEnumerable<Group> groups)
        {
            var links = new List<LinkInfo>();

            if (groups == null)
                return links;

            foreach (var group in groups)
            {
                if (group.Count > 0)
                    links.AddRange(group.Links);
            }

            return links;
        }

        public IEnumerable<LinkInfo> GetAllSelectedLinks(IEnumerable<Group> groups)
        {
            var links = new List<LinkInfo>();

            if (groups == null)
                return links;

            foreach (var group in groups)
            {
                if (group.Count == 0)
                    continue;

                var selectedLinks = group.Links.Where(t => t.IsSelected)?.ToArray();

                if (!selectedLinks.IsNullOrEmpty())
                    links.AddRange(selectedLinks);
            }

            return links;
        }
    }
}
