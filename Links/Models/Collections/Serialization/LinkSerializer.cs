using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Links.Models.Collections.Serialization
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

            string imageJson = item.GetValue("BackgroundImage");
            ImageSource backgroundImage = null;

            try
            {
                backgroundImage = JsonConvert.DeserializeObject<BitmapImage>(imageJson);
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

            string imageJson;

            try
            {
                imageJson = JsonConvert.SerializeObject(link.BackgroundImage);
            }
            catch
            {
                imageJson = "null";
            }

            return $"Link={link.Link} Title={link.Title} BackgroundImage={imageJson}";
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

                _ = dataBuilder.Append(data);

                if (currPos < linksCount)
                    _ = dataBuilder.Append(",");
            }

            return dataBuilder.Append("]").ToString();
        }
    }
}
