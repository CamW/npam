using System;
using System.Collections.Generic;
using Npam.Interop;

namespace Npam.Example.Session
{
    public class Program
    {
        const string PamServiceName = "passwd";

        public static void Main(string[] args)
        {
            Console.Write("Username: ");
            var user = Console.ReadLine();

            using var mySession = new NpamSession(PamServiceName, user, ConvHandler, IntPtr.Zero);
            var retVal = mySession.Start();
            if (retVal == PamStatus.PamSuccess)
            {
                Console.WriteLine("START - SUCCESS!");
                retVal = mySession.Authenticate(0);
                if (retVal == PamStatus.PamSuccess)
                {
                    Console.WriteLine("AUTHENTICATION - SUCCESS!");
                    retVal = mySession.AccountManagement(0);
                    if (retVal == PamStatus.PamSuccess)
                    {
                        Console.WriteLine("ACCESS - SUCCESS!");
                    }
                    else
                    {
                        Console.WriteLine("ACCESS - Failure: {0}", retVal);
                    }
                }
                else
                {
                    Console.WriteLine("AUTHENTICATION - Failure: {0}", retVal);
                }
            }
            else
            {
                Console.WriteLine("START - Failure: {0}", retVal);
            }
        }

        private static IEnumerable<PamResponse> ConvHandler(IEnumerable<PamMessage> messages, IntPtr appData)
        {
            foreach (var message in messages)
            {
                var response = "";

                switch (message.MsgStyle)
                {
                    case MessageStyle.PamPromptEchoOn:
                        Console.Write(message.Message);
                        response = Console.ReadLine();
                        break;
                    case MessageStyle.PamPromptEchoOff:
                        Console.Write(message.Message);
                        response = AcceptInputNoEcho();
                        break;
                    case MessageStyle.PamErrorMsg:
                        Console.Error.WriteLine(message.Message);
                        break;
                    default:
                        Console.WriteLine(message.Message);
                        Console.WriteLine("Unsupported PAM message style \"{0}\".", message.MsgStyle);
                        break;
                }

                yield return new PamResponse(response);
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