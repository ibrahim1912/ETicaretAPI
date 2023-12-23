using Microsoft.Extensions.Configuration;

namespace ETicaretAPI.Persistence
{
    static class Configuration
    {
        static public string ConnectionString
        {
            get
            {
                ConfigurationManager configurationManager = new();
                configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../Presentation/ETicaretAPI.API"));
                configurationManager.AddJsonFile("appsettings.json");

                //configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), @"D:\\connection-config"));
                //configurationManager.AddJsonFile("config.json");

                return configurationManager.GetConnectionString("MsSqlConnection");
            }
        }
    }
}
