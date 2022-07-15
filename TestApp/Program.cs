using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TestApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Assembly path
            var assembly = Assembly.LoadFrom(@" C:\Users\DevranErogul\source\repos\Addon.TCKNCheck\Addon.TCKNCheck\bin\Debug\Addon.TCKNCheck.dll");
            // Namespace ve Class
            var type = assembly.GetType("Addon.TCKNCheck.TCKNCheck");
            // Metod
            var method = type.GetMethod("Validate");
            var paramss = new object[1];
            var paramlist = new List<Dictionary<string, object>>();
            
            // Parametreleri ekliyorum.
            paramlist.Add(new Dictionary<string, object>
            {
                {"tckn", "111111111111111" },
            });
            paramlist.Add(new Dictionary<string, object>
            {
                {"ad", "Alim Devran" },
            });
            paramlist.Add(new Dictionary<string, object>
            {
                {"soyad", "Eroğul" },
            });
            paramlist.Add(new Dictionary<string, object>
            {
                {"yıl", "1980" },
            });

            
            paramss[0] = paramlist;

            var obj = Activator.CreateInstance(type);

            // Metodu cagiriyoruz
            var rs = method.Invoke(obj, paramss) as Dictionary<string, object>;

            if (!string.IsNullOrEmpty(rs["Error"].ToString()))
                Console.WriteLine(rs["Error"].ToString());
            else
            {
                List<Dictionary<string, object>> x = (List<Dictionary<string, object>>)rs["List"];

                Console.WriteLine(x[0]["Result"].ToString());

            }



            Console.ReadKey();
        }
    }
}
