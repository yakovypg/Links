using Links.Data.Imaging;
using Newtonsoft.Json;
using System.Windows.Media.Imaging;

namespace Links.Infrastructure.Serialization
{
    internal class BitmapImageSerializer : IBitmapImageSerializer
    {
        public BitmapImage Deserialize(string data)
        {
            byte[] bytes = JsonConvert.DeserializeObject<byte[]>(data);
            return ImageTransformer.ToBitmapImage(bytes);
        }

        public string Serialize(BitmapImage image)
        {
            byte[] bytes = ImageTransformer.GetBytes(image);
            return JsonConvert.SerializeObject(bytes);
        }
    }
}
