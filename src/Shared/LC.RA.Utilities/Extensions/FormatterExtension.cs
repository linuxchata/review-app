using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace LC.RA.Utilities.Extensions
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

        public static T Deserialize<T>(byte[] array)
        {
            var formatter = new BinaryFormatter();

            using (var stream = new MemoryStream(array))
            {
                var objectGraph = (T)formatter.Deserialize(stream);

                return objectGraph;
            }
        }
    }
}