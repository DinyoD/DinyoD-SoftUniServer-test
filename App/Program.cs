using System.IO;
using System.Text;
using System.Threading.Tasks;

using AppTest;
using SUS.HTTP;
using SUS.MvcFramework;

namespace App
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            await Host.CreateHostAsync(new Startup(), 80);
        }
    }
}
