using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Helper
{
    public class RabbitMQHelper
    {
        public static byte[] MessageToBytes(object message)
        {
            string json = JsonConvert.SerializeObject(message);
            return Encoding.UTF8.GetBytes(json);
        }
        public static T MessageResolve<T>(ReadOnlyMemory<byte> body)
        {
            var message = Encoding.UTF8.GetString(body.ToArray());
            return JsonConvert.DeserializeObject<T>(message);
        }
    }
}
