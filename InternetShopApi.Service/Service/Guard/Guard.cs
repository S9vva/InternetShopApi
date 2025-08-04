namespace InternetShopApi.Service
{
    public static class Guard
    {
        public static void AgainsNull<T>(T entity, string paramName)
        {
            if(entity == null)
            {
                throw new ArgumentNullException(paramName);
            }
        }

        public static void AgainstEmpty(string value, string paramName)
        {
            if(string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException($"{paramName} can't be empty");
            }
        }
    }
}
