using Links.Data.Imaging;
using Links.Infrastructure.Serialization.Base;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Windows.Media.Imaging;

namespace Links.Infrastructure.Serialization
{
    internal class BitmapImageSerializer : Serializer<BitmapImage>
    {
        public override BitmapImage Deserialize(string data)
        {
            var item = new SerializeDataParser().ParseData(data).SerializationItem;

            if (item == null)
                return null;

            string bytesData = item.GetValue("Bytes");
            byte[] bytes = JsonConvert.DeserializeObject<byte[]>(bytesData);

            return ImageTransformer.ToBitmapImage(bytes);
        }

        public override string Serialize(BitmapImage image)
        {
            if (image == null)
                return GenerateNullValueDataString();

            byte[] bytes = ImageTransformer.GetBytes(image);
            string bytesData = JsonConvert.SerializeObject(bytes);

            var dict = new Dictionary<string, object>()
            {
                { "Bytes", bytesData }
            };

            string data = ConvertToDataString(dict);
            return GenerateFullDataString(data);
        }
    }
}
