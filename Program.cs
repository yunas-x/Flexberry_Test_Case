using System;
using System.Configuration;
using System.Net.NetworkInformation;

namespace FlexberryTestCase
{
    class Program
    {
        static void Main(string[] args)
        {
            Logger.LogInvalidEmails();
            ConfigDeserializer.GetAdresses();
        }
    }
}
