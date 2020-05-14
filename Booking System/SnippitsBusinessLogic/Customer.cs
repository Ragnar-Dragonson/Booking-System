using SnippitsBusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnippitsBusinessLogic
{
    public class Customer
    {

        #region Private Member Variables
        private string c_fname;
        private string c_sname;
        private string c_email;
        private string c_phone;
        #endregion




        #region Getters
        public string Fname { get { return c_fname; } }
        public string Sname { get { return c_sname; } }
        public string Email { get { return c_email; } }
        public string Phone { get { return c_phone; } }
        #endregion

        // Getters and setters used to keep the values private
        #region Setters for member variables
        public bool SetFname(string pFname, out string pError)
        {
            pError = "";

            pFname = pFname.Trim();


            if (pFname.Contains(' '))
            {
                pError += "First name must not contain a space character\r\n";
            }

            if (pFname.Any(char.IsDigit))                       //validates the first name variable against empty spaces and number inputs
            {
                pError += "First name must not contain numbers\r\n";
            }

            if (pError == "")
            {
                c_fname = pFname;
                return true;
            }
            return false;
        }

        public bool SetSname(string pSname, out string pError)
        {
            pError = "";

            pSname = pSname.Trim();


            if (pSname.Contains(' '))
            {
                pError += "Surname must not contain a space character\r\n";
            }                                                                   //Validates the surname variable against empty spaces and number inputs

            if (pSname.Any(char.IsDigit))
            {
                pError += "Surname must not contain numbers\r\n";
            }

            if (pError == "")
            {
                c_sname = pSname;
                return true;
            }
            return false;
        }

        public bool SetEmail(string pEmail, out string pError)
        {
            pError = "";

            pEmail = pEmail.Trim();

            try
            {
                string[] splitat = pEmail.Split(new[] { '@' });                 //Splits the email firstly with the @ symbol
                string[] splitstop = splitat[1].Split(new[] { '.' });           //Splits the email again to ensure that a full stop is also included
                if (splitat.Count() != 2 || !IsNormalCharacter(splitat[0]) || !IsNormalCharacter(splitstop[0]) || !IsNormalCharacter(splitstop[1]))
                {
                    pError += "Please enter a valid email address";
                }
            }                                                   //returns errors for incorrect inputs
            catch (IndexOutOfRangeException)
            {
                pError += "Please enter a valid email address";
            }


            if (pEmail.Contains(' '))
            {                                                           //stops input that contains a space character as this is invalid for an email
                pError += "Email must not contain a space character\r\n";
            }



            if (pError == "")
            {
                c_email = pEmail;
                return true;
            }
            return false;
        }



        public bool SetPhone(string pPhone, out string pError)
        {
            pError = "";

            pPhone = pPhone.Trim();


            if (pPhone.Contains(' '))
            {
                pError += "Phone number must not contain a space character\r\n";
            }                                                                   

            if (pPhone.Length > 11 || pPhone.Length < 11)
            {                                                                                                       //Validates the phone number input against space characters, being too long or too short and against letters being included
                pError += "Phone number must be no less than or greater than 11 characters long\r\n";
            }

            if (pPhone.All(char.IsDigit) == false)
            {
                pError += "Phone number must only contain numbers\r\n";
            }

            if (pError == "")
            {
                c_phone = pPhone;
                return true;
            }
            return false;
        } 
        #endregion

        private bool IsNormalCharacter(string input)
        {
            bool result = true;
            if (input == "")
            {                                           
                result = false;
            }
            foreach (char character in input)   
            {
                if (!char.IsLetterOrDigit(character))   //checks each character to see if it is not a letter or a digit
                {
                    result = false;
                }
            }
            return result;
        }


        public Customer(string pFname, string pSname, string pEmail, string pPhone)
        {
            string allErrors = "";
            string error;

            SetFname(pFname, out error);
            allErrors += error;                 

            SetSname(pSname, out error);
            allErrors += error;             //Calls the setters to error check and retrieve the variable data

            SetEmail(pEmail, out error);
            allErrors += error;

            SetPhone(pPhone, out error);
            allErrors += error;

            

            if (allErrors != "")
            {
                throw new Exception(allErrors);
            }
        }
        public override string ToString()
        {
            return Fname + " " + Sname;     //returns an output for the list box so that both first name and surname are shown
        }

    }
}

