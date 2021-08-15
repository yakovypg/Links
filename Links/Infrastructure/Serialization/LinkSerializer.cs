using Links.Infrastructure.Extensions;
using Links.Infrastructure.Serialization.Base;
using Links.Models.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Links.Infrastructure.Serialization
{
    internal class LinkSerializer : Serializer<LinkInfo>, IMultiSerializer<LinkInfo>
    {
        public LinkSerializer()
        {
        }

        public override LinkInfo Deserialize(string data)
        {
            var item = new SerializeDataParser().ParseData(data).SerializationItem;

            if (item == null)
                return null;

            string link = item.GetValue("Link");
            string title = item.GetValue("Title");

            string backgroundImageData = item.GetValue("BackgroundImage").Extract(1, 1);
            var backgroundImage = new BitmapImageSerializer().Deserialize(backgroundImageData);

            return new LinkInfo(link, title, null, backgroundImage);
        }

        public IEnumerable<LinkInfo> DeserializeMany(string data)
        {
            var item = new SerializeDataParser().ParseData(data).SerializationItem;

            if (item == null)
                return null;

            var linksPresenters = item.Children[0]?.Children;

            if (linksPresenters == null || linksPresenters.Count == 0)
                return null;

            var links = new List<LinkInfo>();

            for (int i = 0; i < linksPresenters.Count; ++i)
            {
                string linkData = linksPresenters[i]?.GetValuesDataString();

                if (!string.IsNullOrEmpty(linkData))
                {
                    var link = Deserialize(linkData);

                    if (link != null)
                        links.Add(link);
                }
            }

            return links;
        }

        public override string Serialize(LinkInfo link)
        {
            return Serialize(link, true);
        }

        public string Serialize(LinkInfo link, bool addInfo)
        {
            if (link == null)
                return GenerateNullValueDataString();

            string backgroundImageData = new BitmapImageSerializer().Serialize(link.BackgroundImage);

            var dict = new Dictionary<string, object>()
            {
                { "Link", link.Link },
                { "Title", link.Title },
                { "BackgroundImage", backgroundImageData.Surround(START_COMPLEX_TYPE, END_COMPLEX_TYPE) },
            };

            string data = ConvertToDataString(dict);
            return GenerateFullDataString(data, addInfo);
        }

        public string SerializeMany(IEnumerable<LinkInfo> links)
        {
            if (links.IsNullOrEmpty())
                return GenerateNullValueDataString();

            int currPos = 0;
            int linksCount = links.Count();
            var dataBuilder = new StringBuilder(START_COMPLEX_TYPE + "");

            foreach (LinkInfo link in links)
            {
                currPos++;
                string currData = Serialize(link, false).Surround(START_COMPLEX_TYPE, END_COMPLEX_TYPE);

                dataBuilder.Append(currData);

                if (currPos < linksCount)
                    dataBuilder.Append(DELIMITER);
            }

            string data = dataBuilder.Append(END_COMPLEX_TYPE).ToString();
            return GenerateFullDataString(data);
        }
    }
}
