using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace LC.RA.LocationService.Services.Extensions
{
    public static class FormatterExtension
    {
        public static byte[] Serialize<T>(T objectGraph)
        {
            var formatter = new BinaryFormatter();

            using (var stream = new MemoryStream())
            {
                formatter.Serialize(stream, objectGraph);

                return stream.ToArray();
            }
        }
    }
}