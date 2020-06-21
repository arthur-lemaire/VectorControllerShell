using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VectorControllerShell.Model;

namespace VectorControllerShell
{
    class Program
    {
        static async Task Main(string[] args)
        {
            if (args == null || args.Length == 0)
            {
                var listeVector = new Vector().CreateAllVector();
                Console.WriteLine("Begin Vector connection ...");
                if (listeVector != null)
                {
                    Console.WriteLine("Vector fetching ...");
                    foreach (var vector in listeVector)
                    {
                        Console.WriteLine($"Begin vector {vector.Name} ");
                        await vector.UpdateVectorStatusAsync();
                        Console.WriteLine($"Vector {vector.Name} is updated");
                        await vector.GoToSleepAsync();
                        if (vector.Sleep)
                        {
                            Console.WriteLine("Vector is sleeping");
                        }
                    }
                }
            }
        }
    }
}
