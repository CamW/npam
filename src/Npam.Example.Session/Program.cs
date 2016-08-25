﻿using System;
using System.Collections.Generic;
using Npam.Interop;
using Npam;

namespace Npam.Example
{
    public class Program
    {
        const string PamServiceName = "npam-demo";

        public static void Main(string[] args){
            
            Console.Write("Username: ");
	        string user = Console.ReadLine();

            using (NpamSession mySession = new NpamSession(PamServiceName, user, ConvHandler, IntPtr.Zero)) {
                var retval = mySession.Start();
                if (retval == PamStatus.PAM_SUCCESS) {
                    Console.WriteLine("START - SUCCESS!");
                    retval = mySession.Authenticate(0);
                    if (retval == PamStatus.PAM_SUCCESS) {
                        Console.WriteLine("AUTHENTICATION - SUCCESS!");
                        retval = mySession.AccountManagement(0);
                        if (retval == PamStatus.PAM_SUCCESS) {
                            Console.WriteLine("ACCESS - SUCCESS!");
                        } else {
                            Console.WriteLine("ACCESS - Failure: {0}", retval);
                        }
                    }
                    else {
                        Console.WriteLine("AUTHENTICATION - Failure: {0}", retval);
                    }
                } else {
                    Console.WriteLine("START - Failure: {0}", retval);
                }
            }
        }

        private static IEnumerable<PamResponse> ConvHandler (IEnumerable<PamMessage> messages, IntPtr appData) {
            foreach (PamMessage message in messages)
            {
                string response = "";

                switch (message.MsgStyle) {
                    case MessageStyle.PAM_PROMPT_ECHO_ON :
                        Console.Write(message.Message);
                        response = Console.ReadLine();
                        break;
                    case MessageStyle.PAM_PROMPT_ECHO_OFF :
                        Console.Write(message.Message);
                        response  = AcceptInputNoEcho();
                        break;
                    case MessageStyle.PAM_ERROR_MSG :
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
            string password = "";
            while (true)
            {
                ConsoleKeyInfo i = Console.ReadKey(true);
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