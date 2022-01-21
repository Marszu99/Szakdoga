namespace TimeSheet.Resource
{
    public static class ResourceHandler
    {
        public static bool isEnglish;

        public static string GetResourceString(string key)
        {
            if (isEnglish)
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
