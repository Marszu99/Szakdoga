namespace TimeSheet.Resource
{
    public static class ResourceHandler
    {
        public static bool isEnglish;

        public static string GetResourceString(string key)
        {
            if (isEnglish)
            {
                return ResourceENG.ResourceManager.GetString(key);
            }
            else
            {
                return ResourceHUN.ResourceManager.GetString(key);
            }
        }
    }
}
