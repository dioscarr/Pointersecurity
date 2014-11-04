using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        
        static void Main(string[] args)
        {
            string option = "";
            Console.WriteLine("Select from the following List");
            Console.WriteLine("press 1 -Anagram Game");
            Console.WriteLine("press 2 for Anagram Array");
           option = Console.ReadLine();
            
            switch (option)
            {
                case "1":
                    anagram();
                    break;

                case "2":
                    Console.WriteLine("not yet implemented");
                    break;



            }

            
           
    }

        public static void anagram()
        {
            Console.WriteLine("Hello World");
            Console.WriteLine("Enter First word");
            string s1 = Console.ReadLine();

            Console.WriteLine("Enter Second word word");
            string s2 = Console.ReadLine();
            do
            {

                if (s1.Length == s2.Length)
                {
                    string count = "";
                    foreach (char c in s2)
                    {

                        int x1 = s1.IndexOf(c);
                        if (x1 >= 0)
                        {
                            s1 = s1.Remove(x1, 1);
                            count = count + "x";
                        }

                    }
                    if (s2.Length == count.Length)
                    {
                        Console.WriteLine("The same Characters");
                        Console.ReadLine();
                    }
                    else
                    {
                        Console.WriteLine("Different Characters");
                        Console.ReadLine();
                    }
                }
                else
                {
                    Console.WriteLine("Different Length");
                    Console.ReadLine();
                }

                Console.WriteLine("Enter First word");
                s1 = Console.ReadLine();

                Console.WriteLine("Enter Second word word");
                s2 = Console.ReadLine();
            } while (s1 != "-1" || s2 != "-1");




        }
    }

}
