using Newtonsoft.Json;

namespace HotelApp.BLL.Libraries.ObjectCloner
{
    public class ObjectCloner<T> where T : class
    {
        public T Clone(T source)
        {
            var serialized = JsonConvert.SerializeObject(source);
            return JsonConvert.DeserializeObject<T>(serialized);
        }
    }
}
