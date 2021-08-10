using Links.Models.Collections.Comparers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Links.Models.Collections
{
    internal class GroupAnalyzer
    {
        public GroupAnalyzer()
        {
        }

        public bool ContainsLink(IEnumerable<Group> groups, LinkInfo link)
        {
            if (link == null || groups == null)
                return false;

            foreach (Group group in groups)
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

            foreach (Group group in groups)
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

            foreach (Group group in groups)
            {
                if (group.Count > 0)
                    links.AddRange(group.Links);
            }

            return links;
        }

        /// <summary>
        /// Removes links from the source groups that already exist in the destination groups.
        /// </summary>
        /// <param name="source">The source groups.</param>
        /// <param name="destination">The destination groups.</param>
        /// <param name="removeEmptyGroups">Indicates whether to delete a group from the collection of source groups if it has become empty.</param>
        /// <returns>The source collection of groups (in which empty groups can be deleted if the removeEmptyGroups parameter is 'true') 
        /// in which the links existed in the destination collection were deleted.</returns>
        public IEnumerable<Group> RemoveExistingLinks(IEnumerable<Group> source, IEnumerable<Group> destination, bool removeEmptyGroups = true)
        {
            if (source == null || source.Count() == 0)
                return source;

            foreach (Group group in source)
            {
                if (group.Links == null)
                    continue;

                var linksToRemove = new List<LinkInfo>();

                foreach (LinkInfo link in group.Links)
                {
                    if (ContainsLink(destination, link))
                        linksToRemove.Add(link);
                }

                _ = group.RemoveRange(linksToRemove);
            }

            return removeEmptyGroups
                ? source.Where(t => t.Count > 0)
                : source;
        }

        public IEnumerable<Group> DistributeLinks(IEnumerable<LinkInfo> links)
        {
            var groups = new List<Group>();
            var comparer = new GroupDesignEqualityComparer();

            if (links == null || links.Count() == 0)
                return groups;

            foreach (LinkInfo link in links)
            {
                Group group = groups.FirstOrDefault(t => comparer.Equals(t, link.ParentGroup));

                if (group == null)
                    group = link.ParentGroup.CopyDesign();

                link.ParentGroup = group;
                group.Add(link);

                groups.Add(group);
            }

            return groups;
        }
    }
}
