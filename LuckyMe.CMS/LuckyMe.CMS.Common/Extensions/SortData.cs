using System;

namespace LuckyMe.CMS.Common.Extensions
{
    [Serializable]
    public class SortData : ExtData<SortData>
    {
        public SortData()
        {
            
        }

        public string Property { get; set; }
        public string Direction { get; set; }

        public override bool Equals(object obj)
        {
            //comparing an instance to a null should return false

            //if object can't be cast to this type - return false;
            var fd = obj as SortData;
            if (fd == null) return false;

            return AreEqual(Property, fd.Property) &&
                   AreEqual(Direction, fd.Direction);
        }

        public override int GetHashCode()
        {
            return Property.GetHashCode() ^ Direction.GetHashCode() ;
        }

        private  bool AreEqual(object val1, object val2)
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
    }
}
