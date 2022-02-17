namespace TimeSheet.Resource
{
    public static class ResourceHandler
    {
        public static string GetResourceString(string key)
        {
            return Resources.ResourceManager.GetString(key);
        }
    }
}
