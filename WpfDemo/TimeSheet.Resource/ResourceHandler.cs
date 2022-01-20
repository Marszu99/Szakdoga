using System;
using System.Collections.Generic;
using System.Text;

namespace TimeSheet.Resource
{
    public static class ResourceHandler
    {
        public static bool isEnglish = true;

        public static string GetResourceString(string key)
        {
            if (isEnglish == true)
            {
                return ResourceEN.ResourceManager.GetString(key);
            }
            else
            {
                return ResourceHU.ResourceManager.GetString(key);
            }
        }
    }
}
