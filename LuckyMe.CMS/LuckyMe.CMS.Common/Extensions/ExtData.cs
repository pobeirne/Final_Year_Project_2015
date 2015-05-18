using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;

namespace LuckyMe.CMS.Common.Extensions
{
    public class ExtData<T>
    {
        public static List<T> GetData(string str)
        {
            if (string.IsNullOrEmpty(str))
                return new List<T>();

            var strData = new JavaScriptSerializer().Deserialize<T[]>(str);
            return strData.ToList();
        }
    }
}
