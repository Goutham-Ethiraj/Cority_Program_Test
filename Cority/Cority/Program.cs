using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cority
{
    class Program
    {
        static int Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Input File Name not provided");
                return 1;
            }
            string str_FileName = args[0].ToString();

            iSplitBill obj_SplitBook;
            try
            {
                obj_SplitBook = SplitBook.GetObject();
                obj_SplitBook.fnComputeBill(str_FileName);
            }
            catch (Exception e)
            {
                Console.WriteLine("An Error Occured " + e.Message);
            }

            return 0;
        }
    }
}
