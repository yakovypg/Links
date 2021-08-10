using Newtonsoft.Json;
using System.Windows.Media.Imaging;

namespace Links.Models.Collections.Serialization
{
    internal class BitmapImageSerializer : IBitmapImageSerializer
    {
        public BitmapImage Deserialize(string data)
        {
            byte[] bytes = JsonConvert.DeserializeObject<byte[]>(data);
            return Data.ImageTransformer.ToBitmapImage(bytes);
        }

        public string Serialize(BitmapImage image)
        {
            byte[] bytes = Data.ImageTransformer.GetBytes(image);
            return JsonConvert.SerializeObject(bytes);
        }
    }
}
