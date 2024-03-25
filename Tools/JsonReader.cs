using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GeoDetectorMvcApp.Tools;

public class JsonReader
{
    public JArray ReadJsonByFilePath(string filePath)
    {
        using (StreamReader file = File.OpenText(filePath))
        using (JsonTextReader reader = new(file))
        {
            JArray outPut = JToken.ReadFrom(reader) as JArray;
            return outPut;
        }
    }
}

