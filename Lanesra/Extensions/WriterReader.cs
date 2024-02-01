using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanesra.Extensions
{
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;
    using Newtonsoft.Json;

    public static class FileIO
    {
        public static async Task<List<T>> ReaderAsync<T>(string path)
        {
            var content = await File.ReadAllTextAsync(path);
            return JsonConvert.DeserializeObject<List<T>>(content);
        }

        public static async Task<bool> WriterAsync<T>(string path, List<T> values)
        {
            var json = JsonConvert.SerializeObject(values);
            await File.WriteAllTextAsync(path, json);
            return true;
        }
    }

}
