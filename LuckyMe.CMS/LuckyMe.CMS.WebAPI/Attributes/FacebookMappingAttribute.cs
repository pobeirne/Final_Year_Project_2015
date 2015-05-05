using System;

namespace LuckyMe.CMS.WebAPI.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class FacebookMapping : Attribute
    {
        public string Name;

        public string Parent;

        public FacebookMapping(string name)
        {
            Name = name;
            Parent = string.Empty;
        }

        public string GetName()
        {
            return Name;
        }
    }
}