using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace SnippitsBusinessLogic
{
    public class HairdresserLoaderstylist
    {
        public static List<Stylist> LoadFromCSV(string pFilename, out string pError)
        {
            pError = "";

            List<Stylist> Stylists = new List<Stylist>();       // Creates a list for stylist to be inputted into the list box

            StreamReader reader = null;

            try
            {
                reader = new StreamReader(pFilename);

                while (!reader.EndOfStream)
                {                                                   //while the file is not at the end transfers the information from the file into relevant variables
                    string line = reader.ReadLine();
                    string[] values = line.Split(',');
                    if (values.Length == 5)
                    {
                        string fname = values[0].Trim();
                        string surname = values[1].Trim();
                        string email = values[2].Trim();
                        string phone = values[3].Trim();
                        string wage = values[4].Trim();

                        try
                        {
                            Stylists.Add(new Stylist(fname, surname, email, phone, wage));   //Adds each variable to the stylist list ready to be shown on the list box
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
                return Stylists;
            }
            return null;
        }
        public static bool SaveToCSV(string pFilename, List<Stylist> pStylists, out string pError)
        {
            pError = "";

            if (!File.Exists(pFilename))    
            {                                                   // if file is not found then it throws an error message
                pError = "could not find file " + pFilename;
                return false;
            }

            StreamWriter writer = new StreamWriter(pFilename, false);

            foreach (Stylist stylist in pStylists)
            {                                                                   //writes stylists variables into relevant file to save for later use

                writer.WriteLine(stylist.Fname + "," + stylist.Sname + "," + stylist.Email + "," + stylist.Phone + "," + stylist.Wage);
            }
            writer.Close();

            return true;
        }

    }
}


