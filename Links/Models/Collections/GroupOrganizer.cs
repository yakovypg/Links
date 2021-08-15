using Links.Infrastructure.Extensions;
using Links.Models.Collections.Comparers;
using System.Collections.Generic;
using System.Linq;

namespace Links.Models.Collections
{
    internal class GroupOrganizer
    {
        public void RedefineParentsForLinks(IEnumerable<Group> groups)
        {
            if (groups.IsNullOrEmpty())
                return;
            
            foreach (var group in groups)
            {
                if (group.Links.IsNullOrEmpty())
                    continue;

                foreach (var link in group.Links)
                {
                    link.ParentGroup = group;
                    link.ResetPrimaryGroup();
                }
            }
        }

        public IEnumerable<Group> DistributeLinks(IEnumerable<LinkInfo> links)
        {
            var groups = new List<Group>();
            var comparer = new GroupDesignEqualityComparer();

            if (links.IsNullOrEmpty())
                return groups;

            foreach (var link in links)
            {
                Group group = groups.FirstOrDefault(t => comparer.Equals(t, link.ParentGroup));

                if (group == null)
                {
                    group = link.ParentGroup.CopyDesign();
                    groups.Add(group);
                }

                link.ParentGroup = group;
                group.Add(link);
            }

            return groups;
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
            if (source.IsNullOrEmpty())
                return source;

            foreach (var group in source)
            {
                if (group.Links == null)
                    continue;

                var linksToRemove = new List<LinkInfo>();
                var groupAnalyzer = new GroupAnalyzer();

                foreach (var link in group.Links)
                {
                    if (groupAnalyzer.ContainsLink(destination, link))
                        linksToRemove.Add(link);
                }

                group.RemoveRange(linksToRemove);
            }

            return removeEmptyGroups
                ? source.Where(t => t.Count > 0)
                : source;
        }
    }
}
