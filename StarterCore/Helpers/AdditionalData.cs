using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace StarterCore.Helpers
{
    public class AdditionalData
    {
        public static dynamic Info { get; set; }
        public static void Initialize(string path)
        {
            string json = File.ReadAllText(path + @"\Helpers\Files\additionalData.json");
            Info = JsonConvert.DeserializeObject<object>(json);
        }
    }
}
