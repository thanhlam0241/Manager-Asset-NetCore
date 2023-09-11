using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web062023.AMIS.Application.Extension
{
    public class ConvertValueExtension
    {
        public static int ConvertNullableValue(int? value)
        {
            if (value == null)
            {
                return 0;
            }
            else
            {
                return (int)value;
            }
        }
        public static float ConvertNullableValue(float? value)
        {
            if (value == null)
            {
                return 0;
            }
            else
            {
                return (float)value;
            }
        }
        public static decimal ConvertNullableValue(decimal? value)
        {
            if (value == null)
            {
                return 0;
            }
            else
            {
                return (decimal)value;
            }
        }
        public static string ConvertNullableValue(string? value)
        {
            if (value == null)
            {
                return "";
            }
            else
            {
                return value;
            }
        }
    }
}
