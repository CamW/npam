using System;

namespace Npam.Example.User
{
    public class Program
    {
        private const string ServiceName = "passwd";

        public static void Main(string[] args)
        {
            Console.Write("Username: ");
            var user = Console.ReadLine();
            Console.Write("Password: ");
            var password = AcceptInputNoEcho();

            if (NpamUser.Authenticate(ServiceName, user, password))
            {
                Console.WriteLine("AUTHENTICATION - SUCCESS!");
                Console.WriteLine("\rUSER GROUPS:");
                foreach (var userGroup in NpamUser.GetGroups(user))
                {
                    Console.WriteLine("\t{0}", userGroup);
                }

                Console.WriteLine("\rUSER INFO:");
                Console.WriteLine("\t{0}", NpamUser.GetAccountInfo(user));
            }
            else
            {
                Console.WriteLine("AUTHENTICATION - FAILURE!");
            }
        }

        private static string AcceptInputNoEcho()
        {
            var password = "";
            while (true)
            {
                var i = Console.ReadKey(true);
                if (i.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine();
                    break;
                }
                else if (i.Key == ConsoleKey.Backspace)
                {
                    if (password.Length > 0)
                    {
                        password = password.Substring(0, password.Length - 1);
                    }
                }
                else
                {
                    password += (i.KeyChar);
                }
            }

            return password;
        }
    }
}