using Newtonsoft.Json;

namespace SouthXchange.Model
{
    public class SxcModel
    {
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}