namespace SanShop.Api
{
    public static class Helper
    {
        public static string GetId()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }
    }
}
