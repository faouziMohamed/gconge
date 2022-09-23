using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace GConge.Models.Utils;

static public class Utils
{
  static public TSettings GetConfig<TSettings>(string configFile)
  {
    return new ConfigurationBuilder()
      .AddJsonFile(configFile)
      .Build()
      .GetRequiredSection(typeof(TSettings).Name)
      .Get<TSettings>();
  }

  static public TSettings GetConfig<TSettings>(bool isDevelopment, string configFile = "appsettings.json")
  {
    string file = isDevelopment ?
      $"{Path.GetFileNameWithoutExtension(configFile)}.Development.json"
      :
      configFile;

    return GetConfig<TSettings>(file);
  }
  static public byte[] HashPassword(string password, out byte[] salt)
  {
    using var hmac = new HMACSHA512();

    salt = hmac.Key;
    return hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
  }
  static public bool VerifyHashedPassword(string password, byte[] hashedPassword, byte[] salt)
  {
    using var hmac = new HMACSHA512(salt);
    byte[] computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
    return computedHash.SequenceEqual(hashedPassword);
  }
}
