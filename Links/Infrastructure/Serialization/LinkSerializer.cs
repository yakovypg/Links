using Links.Models.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;

namespace Links.Infrastructure.Serialization
{
    internal class LinkSerializer : ILinkSerializer
    {
        public LinkSerializer()
        {
        }

        public LinkInfo Deserialize(string data)
        {
            if (string.IsNullOrEmpty(data))
                return null;

            var item = new SerializerItem(data);

            string link = item.GetValue("Link");
            string title = item.GetValue("Title");

            string imageBytes = item.GetValue("BackgroundImage");
            BitmapImage backgroundImage = null;

            try
            {
                var serializer = new BitmapImageSerializer();
                backgroundImage = serializer.Deserialize(imageBytes);
            }
            catch { }

            return new LinkInfo(link, title, null, backgroundImage);
        }

        public IEnumerable<LinkInfo> DeserializeMany(string data)
        {
            if (string.IsNullOrEmpty(data) || data.Length < 2)
                return null;

            data = data.Remove(data.Length - 1).Substring(1);
            string[] items = data.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

            if (items == null || items.Length == 0)
                return null;

            var links = new LinkInfo[items.Length];

            for (int i = 0; i < items.Length; ++i)
                links[i] = Deserialize(items[i]);

            return links;
        }

        public string Serialize(LinkInfo link)
        {
            if (link == null)
                return null;

            string imageBytes;

            try
            {
                var serializer = new BitmapImageSerializer();
                imageBytes = serializer.Serialize(link.BackgroundImage);
            }
            catch
            {
                imageBytes = "null";
            }

            return $"Link={link.Link} Title={link.Title} BackgroundImage={imageBytes}";
        }

        public string SerializeMany(IEnumerable<LinkInfo> links)
        {
            int linksCount = links.Count();

            if (links == null || linksCount == 0)
                return null;

            int currPos = 0;
            var dataBuilder = new StringBuilder("[");

            foreach (LinkInfo link in links)
            {
                currPos++;
                string data = Serialize(link);

                dataBuilder.Append(data);

                if (currPos < linksCount)
                    dataBuilder.Append(",");
            }

            return dataBuilder.Append("]").ToString();
        }
    }
}
