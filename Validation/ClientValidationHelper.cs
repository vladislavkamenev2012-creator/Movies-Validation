namespace MVC_1.Validation
{
    internal static class ClientValidationHelper
    {
        public static void MergeAttribute(IDictionary<string, string> attributes, string key, string value)
        {
            if (!attributes.ContainsKey(key))
                attributes.Add(key, value);
        }
    }
}
