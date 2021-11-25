using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Utilities.Utils
{
    public static class Util
    {
        /// <summary>
        /// Convert Object to String
        /// </summary>
        /// <param name="obj">Object</param>
        /// <returns>String value</returns>
        public static string CnvObjToString(object obj)
        {
            try
            {
                return Convert.ToString(obj);
            }
            catch (Exception)
            {
                return String.Empty;
            }
        }

        /// <summary>
        /// Convert Object to Int16
        /// </summary>
        /// <param name="obj">Object</param>
        /// <param name="defaulValue">Defaul Value = 0</param>
        /// <returns>Int16 Value</returns>
        public static Int16 CnvObjToInt16(object obj, Int16 defaulValue = 0)
        {
            try
            {
                return Convert.ToInt16(obj);
            }
            catch (Exception)
            {
                return defaulValue;
            }
        }

        /// <summary>
        /// Convert Object to Int32
        /// </summary>
        /// <param name="obj">Object</param>
        /// <param name="defaulValue">Defaul Value = 0</param>
        /// <returns>Int32 Value</returns>
        public static Int32 CnvObjToInt32(object obj, Int32 defaulValue = 0)
        {
            try
            {
                return Convert.ToInt32(obj);
            }
            catch (Exception)
            {
                return defaulValue;
            }
        }

        /// <summary>
        /// Convert Object to Int64
        /// </summary>
        /// <param name="obj">Object</param>
        /// <param name="defaulValue">Defaul Value = 0</param>
        /// <returns>Int64 Value</returns>
        public static Int64 CnvObjToInt64(object obj, Int64 defaulValue = 0)
        {
            try
            {
                return Convert.ToInt64(obj);
            }
            catch (Exception)
            {
                return defaulValue;
            }
        }

        /// <summary>
        /// Convert Object to Decimal
        /// </summary>
        /// <param name="obj">Object</param>
        /// <param name="dcmDefault">Defaul Value = 0</param>
        /// <returns>Decimal value</returns>
        public static decimal CnvObjToDecimal(object obj, decimal defaulValue = 0)
        {
            try
            {
                return Convert.ToDecimal(obj);
            }
            catch (Exception)
            {
                return defaulValue;
            }
        }

        /// <summary>
        /// Convert Object to Double
        /// </summary>
        /// <param name="obj">Object</param>
        /// <param name="defaulValue">Defaul Value = 0</param>
        /// <returns>Double value</returns>
        public static double CnvObjToDouble(object obj, double defaulValue = 0)
        {
            try
            {
                return Convert.ToDouble(obj);
            }
            catch (Exception)
            {
                return defaulValue;
            }
        }

        public static bool cnvIntToBoolean(int intValue)
        {
            if (intValue == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Convert Object to Datetime
        /// </summary>
        /// <param name="obj">Object</param>
        /// <param name="strDataFormat">string format datetime</param>
        /// <returns>Datetime value</returns>
        public static DateTime CnvObjToDatetime(object obj, string strDataFormat = "MM-dd-yyyy")
        {
            DateTime dtmResult = default(DateTime);
            string strDateTime = CnvObjToString(obj);
            try
            {
                if (!string.IsNullOrEmpty(strDateTime))
                {
                    return Convert.ToDateTime(strDateTime);
                }
                else
                {
                    return DateTime.ParseExact(strDateTime, strDataFormat, CultureInfo.CurrentCulture);
                }
            }
            catch (Exception ex)
            {
                return dtmResult;
            }
        }

        /// <summary>
        /// Check object IS Date ?
        /// </summary>
        /// <param name="obj">Object</param>
        /// <returns>Return True OR False</returns>
        public static bool IsDate(object obj)
        {
            string strDate = CnvObjToString(obj);
            try
            {
                DateTime dt = DateTime.Parse(strDate);
                if (dt.Day >= 1 || dt.Day <= 31)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool IsNullOrEmpty(object obj)
        {
            if (obj is null) return false;

            if (Convert.IsDBNull(obj)) return false;

            if (string.IsNullOrWhiteSpace(obj.ToString())) return false;

            return true;
        }
    }
}
