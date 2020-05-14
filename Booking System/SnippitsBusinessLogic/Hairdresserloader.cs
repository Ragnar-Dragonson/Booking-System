using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnippitsBusinessLogic
{
    public static class Hairdresserloader
    {
        public static List<Customer> LoadFromCSV(string pFilename, out string pError)
        {
            pError = "";

            List<Customer> Customers = new List<Customer>();        

            StreamReader reader = null;  //Creates a reader for the needed file

            try
            {
                reader = new StreamReader(pFilename);

                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();            //until the end of the file it loads the information from the file into the relevant variables
                    string[] values = line.Split(',');
                    if (values.Length == 4)
                    {
                        string fname = values[0].Trim();
                        string surname = values[1].Trim();
                        string email = values[2].Trim();
                        string phone = values[3].Trim();

                        try
                        {
                            Customers.Add(new Customer(fname, surname, email, phone));          //adds the variables data into the customer list ready to be shown on the list box
                        }
                        catch (Exception ex)
                        {
                            pError = ex.Message;
                            return null;
                        }
                    } 
                    else
                    {
                        pError = "Line in file is incorrect:" + line;

                    }
                }
            }

            catch
            {
                pError = "Could not open file " + pFilename;
                return null;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }

            if (pError == "")
            {
                return Customers;
            }
            return null;
        }
        public static bool SaveToCSV(string pFilename, List<Customer> pCustomers, out string pError)
        {
            pError = "";

            if (!File.Exists(pFilename))
            {                                                   //returns error message if the file cannot be found
                pError = "could not find file " + pFilename;
                return false;
            }

            StreamWriter writer = new StreamWriter(pFilename, false);       //creates a writer to write the customers information into the necessary file

            foreach (Customer customer in pCustomers)
            {
               
                writer.WriteLine(customer.Fname + "," + customer.Sname + "," + customer.Email + "," + customer.Phone);
            }
            writer.Close();

            return true;
        }

    }
}
