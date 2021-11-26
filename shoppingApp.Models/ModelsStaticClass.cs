namespace ShoppingApp.Models
{
    using System;
    using System.IO;
    using System.Reflection;

    public static class ModelsStaticClass
    {
        public static string modelsXmlPath = Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml");
    }
}
