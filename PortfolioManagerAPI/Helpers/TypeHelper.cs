namespace PortfolioManagerAPI.Helpers
{
    public class TypeHelper
    {
        public static Object EmptyStringPropertiesToNull(Object obj)
        {
            obj.GetType().GetProperties()
            .Where(p => p.PropertyType == typeof(string))
            .ToList()
            .ForEach(p =>
            {
                var value = (string)p.GetValue(obj);
                if (string.IsNullOrWhiteSpace(value))
                {
                    p.SetValue(obj, null);
                }
            });
            return obj;
        }
    }
}
