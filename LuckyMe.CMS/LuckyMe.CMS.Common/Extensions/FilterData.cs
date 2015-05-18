using System;
using System.Collections.Generic;
using System.Text;

namespace LuckyMe.CMS.Common.Extensions
{
    [Serializable]
    public class FilterData : ExtData<FilterData>
    {
   

        public string Type { get; set; }
        public string Field { get; set; }
        public object Value { get; set; }
        public string Comparison { get; set; }

        public override bool Equals(object obj)
        {
            //comparing an instance to a null should return false

            //if object can't be cast to this type - return false;
            var fd = obj as FilterData;
            if (fd == null) return false;

            return AreEqual(Type, fd.Type) &&
                   AreEqual(Field, fd.Field) &&
                   AreEqual(Comparison, fd.Comparison) &&
                   AreEqual(Value, fd.Value);
        }

        public override int GetHashCode()
        {
            return Type.GetHashCode() ^ Field.GetHashCode() ^ Comparison.GetHashCode() ^ Value.GetHashCode();
        }

        public bool AreEqual(object val1, object val2)
        {
            if (val1 == null && val2 == null)
            {
                return true;
            }
            if (val1 == null || val2 == null)
            {
                return false;
            }
            var oldStringValue = val1.ToString();
            var newStringValue = val2.ToString();
            return oldStringValue.Equals(newStringValue);
        }


        public static string GetWhereCriteria(List<FilterData> filters)
        {
            var strfilter = new StringBuilder("1 = 1");

            filters.ForEach(f =>
            {
                if (f.Type == "string" || f.Type == "datetime")
                {
                    strfilter.Append(" and " + f.Field + " = " + "\"" + f.Value + "\"");
                }
                else
                {
                    strfilter.Append(" and " + f.Field + " = " + f.Value);
                }
            });

            return strfilter.ToString();
        }
    }
}
