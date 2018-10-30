using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MobileAppAPI.Models
{
    public class commonEncryption
    {
        //This function accepts a string as argument and retruns the string after encrypting it.
        public String Encrypt(String strName)
        {
            String EncryptedText;
            int intseed;
            int ictr;
            Char strTemp;
            int iAscii;
            int intlen;
            int intseedval;
            String strAscii;
            EncryptedText = "";
            try
            {
                strName = strName.Trim();
                intlen = strName.Length;
                intseedval = Power(intlen, 2);
                for (ictr = 1; ictr <= intlen; ictr++)
                {
                    intseed = Power(ictr, 2);
                    strTemp = Convert.ToChar(strName.Substring(ictr - 1, 1));
                    iAscii = (int)strTemp;
                    iAscii = iAscii + 2 * intseed + intseedval;
                    strAscii = Convert.ToString(iAscii);
                    if (strAscii.Length < 3)
                    {
                        if (strAscii.Length < 2)
                        {
                            strAscii = "00" + strAscii;
                        }
                        else
                        {
                            strAscii = "0" + strAscii;
                        }
                    }
                    EncryptedText = EncryptedText + strAscii;
                }
                intseedval = 2 * intseedval;
                EncryptedText = EncryptedText + "." + intseedval;

                return EncryptedText;
            }
            catch
            {

            }
            return EncryptedText;
        }

        #region Decryption
        public string Decrypt(string strName)
        {
            string DecryptedText = null;
            int intseed = 0;
            int ictr = 0;
            string strTemp = null;
            int iAscii = 0;
            int intlen = 0;
            int icounter = 0;
            int intseedval = 0;

            DecryptedText = "";
            try
            {
                strName = strName.Trim();
                intseedval = Convert.ToInt32(strName.Substring(strName.IndexOf(".") + 1));
                intseedval = intseedval / 2;
                strName = strName.Substring(0, strName.IndexOf("."));
                intlen = strName.Length;
                icounter = 1;
                for (ictr = 1; ictr <= intlen;)
                {
                    intseed = Power(icounter, 2);
                    strTemp = strName.Substring(ictr - 1, 3);
                    iAscii = Convert.ToInt32(strTemp);
                    iAscii = iAscii - 2 * intseed - intseedval;
                    DecryptedText = DecryptedText + (char)iAscii;
                    icounter = icounter + 1;
                    ictr += 3;
                }
                return DecryptedText;
            }
            catch (Exception ex)
            {
            }
            return DecryptedText;
        }
        #endregion
        private int Power(int intNumber, int intPower)
        {
            int intresult;
            int ictr;
            intresult = 1;
            try
            {

                for (ictr = 1; ictr <= intPower; ictr++)
                {
                    intresult = intresult * intNumber;
                }

                return intresult;
            }
            catch
            {

            }
            return intresult;
        }
    }
}