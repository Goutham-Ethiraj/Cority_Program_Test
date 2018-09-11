using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Cority
{
    public class SplitBook : iSplitBill
    {
        static SplitBook obj_SplitBook;
        private SplitBook()
        {
            //Avoid creating objects for the class
        }
        public static SplitBook GetObject()
        {
            if (obj_SplitBook == null)
                obj_SplitBook = new SplitBook();
            return obj_SplitBook;
        }
        private string fnComputeRelativePath(string strFileName, Boolean IsInputFile, Boolean IsLogFile = false)
        {
            if (!IsLogFile)
            {
                if (IsInputFile)
                    return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\" + strFileName;
                else
                    return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\" + strFileName + ".out";
            }
            else
                return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\" + strFileName;
        }
        private void fnWriteLog(string logText)
        {
            string str_LogFilePath = fnComputeRelativePath("log.txt", false, true);

            try
            {
                using (StreamWriter writetext = File.AppendText(str_LogFilePath))
                {
                    writetext.WriteLine(logText + " " + DateTime.Now);
                    writetext.WriteLine("\n");
                }
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("The file not found " + e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred  " + e.Message);
            }
        }

        private string[] fnCalculateProfitOrLoss(decimal[][] dIndividualTotal, decimal total)
        {
            if (dIndividualTotal != null)
            {
                string[] strIndividualContribution = new string[dIndividualTotal.Length];
                decimal dInvidualShare = (total / dIndividualTotal.Length);
                for (int i = 0; i < dIndividualTotal.Length; i++)
                {
                    if (dInvidualShare > dIndividualTotal[i][0])
                        strIndividualContribution[i] = "$" + Math.Abs(Math.Round((dInvidualShare - dIndividualTotal[i][0]), 2, MidpointRounding.AwayFromZero));
                    else
                        strIndividualContribution[i] = "($" + Math.Abs(Math.Round((dInvidualShare - dIndividualTotal[i][0]), 2, MidpointRounding.AwayFromZero)) + ")";
                }
                return strIndividualContribution;
            }
            return null;
        }

        public string[] fnfnCalculateProfitOrLoss_Adapter(decimal[][] dIndividualTotal, decimal total)
        {
            return fnCalculateProfitOrLoss(dIndividualTotal, total);
        }

        private void fnWriteFile(string[] result, string OutputFileName)
        {
            string strOutputFilePath = fnComputeRelativePath(OutputFileName, false);

            if (result != null)
            {
                try
                {
                    using (StreamWriter writetext = File.AppendText(strOutputFilePath))
                    {
                        for (int i = 0; i < result.Length; i++)
                        {
                            writetext.WriteLine(result[i]);
                        }
                        writetext.WriteLine("\n");
                    }
                }
                catch (FileNotFoundException e)
                {
                    fnWriteLog("The file not found " + e.Message);
                }
                catch (Exception e)
                {
                    fnWriteLog("An error occurred  " + e.Message);
                }
            }
        }

        private void fnCreateOutputFile(string fileName)
        {
            string strOutputFilePath = fnComputeRelativePath(fileName, false);
            try
            {
                File.Create(strOutputFilePath).Close();
            }
            catch (Exception e)
            {
                fnWriteLog("Error in initializing output File");
            }
        }

        public void fnComputeBill(string fileName)
        {
            string strInputFilePath = fnComputeRelativePath(fileName, true);
            try
            {
                var fileStream = new FileStream(strInputFilePath, FileMode.Open, FileAccess.Read);
                using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
                {
                    string line;
                    int iNoOfParticipant, iNoOfReceipt;
                    decimal itotal, dIndividualTotal;
                    fnCreateOutputFile(fileName);
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        if (Convert.ToInt32(line) == 0)
                            break;
                        iNoOfParticipant = Convert.ToInt32(line);
                        decimal[][] data = new decimal[iNoOfParticipant][];
                        itotal = 0;
                        for (int i = 0; i < iNoOfParticipant; i++)
                        {
                            if ((line = streamReader.ReadLine()) != null)
                            {
                                iNoOfReceipt = Convert.ToInt32(line);
                                //data[i] = 
                                dIndividualTotal = 0;
                                for (int j = 0; j < iNoOfReceipt; j++)
                                {
                                    if ((line = streamReader.ReadLine()) != null)
                                    {
                                        dIndividualTotal += Math.Round(Convert.ToDecimal(line), 2);
                                    }
                                }
                                data[i] = new decimal[] { dIndividualTotal };
                                itotal += dIndividualTotal;
                            }
                        }
                        string[] strResult = fnCalculateProfitOrLoss(data, itotal);
                        fnWriteFile(strResult, fileName);
                    }

                }
            }
            catch (FileNotFoundException e)
            {
                fnWriteLog("The file not found " + e.Message);
            }
            catch (FormatException e)
            {
                fnWriteLog("Invalid format " + e.Message);
            }
            catch (Exception e)
            {
                fnWriteLog("An error occurred  " + e.Message);
            }
        }

    }


}
