using System.Collections;

namespace code
{
    internal class Program
    {
        static void Main(string[] args)
        {
            UserHandler userHandler = new UserHandler();
            ProductHandler productHandler = new ProductHandler();

            ArrayList users = userHandler.SelectAll();
            foreach(User usr in users)
            {
                Console.WriteLine(usr);
            }

            ArrayList products = productHandler.SelectAll();
            foreach (Product product in products)
            {
                Console.WriteLine(product);
            }



        }
    }
}