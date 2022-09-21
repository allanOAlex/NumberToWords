using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Exercice01.Core
{
    public static class NumberToTextExtensionMethod
    {
        public static string ToWords(this string num)
        {
            var numberToText = new NumberToText();
            return numberToText.ToWords(num);
        }

    }

    public class NumberToText
    {
        private Dictionary<string, string> textString = new Dictionary<string, string>();
        private Dictionary<string, string> scale = new Dictionary<string, string>();

        private StringBuilder builder;

        public NumberToText()
        {
            Initialize();
        }

        public string ToWords(string num)
        {
            builder = new StringBuilder();

            if (num == "0")
            {
                builder.Append(textString[num]);
                return builder.ToString();
            }

            foreach (var scale in scale)
                num = Append(num, scale.Key);

            AppendValues(num);

            return builder.ToString().Trim();
        }
        private string Append(string num, string scale)
        {
            num = num.Replace(",","");

            if (BigInteger.Parse(num) > BigInteger.Parse(scale) - 1)
            {
                var baseScale = num;
                AppendValues(baseScale);
                num = (BigInteger.Parse(num) - BigInteger.Parse(baseScale)).ToString();
            }
            return num;
        }
        private string AppendValues(string num)
        {
            num = AppendQuintillions(num);
            num = AppendQuadrillions(num);
            num = AppendTrillions(num);
            num = AppendBillions(num);
            num = AppendMillions(num);
            num = AppendThousands(num);
            num = AppendHundreds(num);
            num = AppendTens(num);
            AppendUnits(num);

            return num;
        }


        private void AppendUnits(string num)
        {
            if (Convert.ToUInt64(num) > 0 && Convert.ToUInt64(num) <= 9)
            {
                builder.AppendFormat("{0} ", textString[num]);
            }
        }

        private string AppendTens(string num)
        {
            if (Convert.ToUInt64(num) >= 10)
            {
                var tens = (Convert.ToUInt64(num) / 10) * 10;

                bool keyExists = textString.ContainsKey(tens.ToString());

                if (keyExists)
                {
                    builder.AppendFormat("{0} ", textString[tens.ToString()]);
                    num = (Convert.ToUInt64(num) - tens).ToString();
                }
                else
                {
                    AppendValues(tens.ToString());
                    num = (Convert.ToUInt64(num) - tens).ToString();
                }

            }
            return num;
        }

        private string AppendHundreds(string num)
        {
            if (Convert.ToUInt64(num) >= 100)
            {
                var hundreds = Convert.ToUInt64(num) / 100;

                bool keyExists = textString.ContainsKey(hundreds.ToString());

                if (keyExists)
                {
                    builder.AppendFormat("{0} hundred ", textString[hundreds.ToString()]);
                    num = (Convert.ToUInt64(num) - (hundreds * 100)).ToString();
                }
                else
                {
                    AppendValues(hundreds.ToString());
                    num = (Convert.ToUInt64(num) - (hundreds * 100)).ToString();
                }

            }
            return num;
        }

        private string AppendThousands(string num)
        {
            if (Convert.ToUInt64(num) >= 1000)
            {
                var thousands = Convert.ToUInt64(num) / 1000;

                bool keyExists = textString.ContainsKey(thousands.ToString());

                if (keyExists)
                {
                    builder.AppendFormat("{0} thousand ", textString[thousands.ToString()]);
                }
                else
                {
                    textString.Add(thousands.ToString(), string.Empty.Trim());
                    AppendValues(thousands.ToString());
                    builder.AppendFormat("{0} thousand ", textString[thousands.ToString()]).Replace("  ", " ");

                }

                num = (Convert.ToUInt64(num) - (thousands * 1000)).ToString();
            }

            return num;
        }

        private string AppendMillions(string num)
        {
            if (Convert.ToUInt64(num) >= 1000000)
            {
                var millions = Convert.ToUInt64(num) / 1000000;

                bool keyExists = textString.ContainsKey(millions.ToString());

                if (keyExists)
                {
                    builder.AppendFormat("{0} million ", textString[millions.ToString()]);
                }
                else
                {
                    textString.Add(millions.ToString(), string.Empty.Trim());
                    AppendValues(millions.ToString());
                    builder.AppendFormat("{0} million ", textString[millions.ToString()]).Replace("  ", " ");

                }

                num = (Convert.ToUInt64(num) - (millions * 1000000)).ToString();

            }
            return num;
        }


        private string AppendBillions(string num)
        {
            if (Convert.ToUInt64(num) >= 1000000000)
            {
                var billions = Convert.ToUInt64(num) / 1000000000;

                bool keyExists = textString.ContainsKey(billions.ToString());

                if (keyExists)
                {
                    builder.AppendFormat("{0} billion ", textString[billions.ToString()]);
                }
                else
                {
                    textString.Add(billions.ToString(), string.Empty.Trim());
                    AppendValues(billions.ToString());
                    builder.AppendFormat("{0} billion ", textString[billions.ToString()]).Replace("  ", " ");

                }

                num = (Convert.ToUInt64(num) - (billions * 1000000000)).ToString();

            }
            return num;
        }

        private string AppendTrillions(string num)
        {
            if (Convert.ToUInt64(num) >= 1000000000000)
            {
                var trillions = Convert.ToUInt64(num) / 1000000000000;

                bool keyExists = textString.ContainsKey(trillions.ToString());

                if (keyExists)
                {
                    builder.AppendFormat("{0} trillion ", textString[trillions.ToString()]);
                }
                else
                {
                    textString.Add(trillions.ToString(), string.Empty.Trim());
                    AppendValues(trillions.ToString());
                    builder.AppendFormat("{0} trillion ", textString[trillions.ToString()]).Replace("  ", " ");

                }

                num = (Convert.ToUInt64(num) - (trillions * 1000000000000)).ToString();

            }
            return num;
        }

        private string AppendQuadrillions(string num)
        {
            num = num.Replace(",", "");

            if (Convert.ToUInt64(num) >= 1000000000000000)
            {
                var quadrillions = Convert.ToUInt64(num) / 1000000000000000;

                bool keyExists = textString.ContainsKey(quadrillions.ToString());

                if (keyExists)
                {
                    builder.AppendFormat("{0} quadrillion ", textString[quadrillions.ToString()]);
                }
                else
                {
                    textString.Add(quadrillions.ToString(), string.Empty.Trim());
                    AppendValues(quadrillions.ToString());
                    builder.AppendFormat("{0} quadrillion ", textString[quadrillions.ToString()]).Replace("  ", " ");

                }

                num = (Convert.ToUInt64(num) - (quadrillions * 1000000000000000)).ToString();

            }
            return num;
        }

        private string AppendQuintillions(string num)
        {
			BigInteger convertedString = BigInteger.Parse(num);
            num = string.Format("{0:N0}", convertedString);
            string testValue = string.Format("{0:N0}", 1000000000000000000);

            if (convertedString >= 1000000000000000000)
            {
                var quintillions = convertedString / 1000000000000000000;

                bool keyExists = textString.ContainsKey(quintillions.ToString());

                if (keyExists)
                {
                    builder.AppendFormat("{0} quintillion ", textString[quintillions.ToString()]);
                }
                else
                {
                    textString.Add(quintillions.ToString(), string.Empty.Trim());
                    AppendValues(quintillions.ToString());
                    builder.AppendFormat("{0} quintillion ", textString[quintillions.ToString()]).Replace("  ", " ");

                }

                BigInteger bitInt = convertedString - quintillions * 1000000000000000000;

                num = bitInt.ToString();
            }
            return num;
        }


        private void Initialize()
        {
            textString.Add("0", "Zero");
            textString.Add("1", "One");
            textString.Add("2", "Two");
            textString.Add("3", "Three");
            textString.Add("4", "Four");
            textString.Add("5", "Five");
            textString.Add("6", "Six");
            textString.Add("7", "Seven");
            textString.Add("8", "Eight");
            textString.Add("9", "Nine");
            textString.Add("10", "Ten");
            textString.Add("11", "Eleven");
            textString.Add("12", "Twelve");
            textString.Add("13", "Thirteen");
            textString.Add("14", "Fourteen");
            textString.Add("15", "Fifteen");
            textString.Add("16", "Sixteen");
            textString.Add("17", "Seventeen");
            textString.Add("18", "Eighteen");
            textString.Add("19", "Nineteen");
            textString.Add("20", "Twenty");
            textString.Add("30", "Thirty");
            textString.Add("40", "Forty");
            textString.Add("50", "Fifty");
            textString.Add("60", "Sixty");
            textString.Add("70", "Seventy");
            textString.Add("80", "Eighty");
            textString.Add("90", "Ninety");
            textString.Add("100", "Hundred");

            scale.Add("1000", "Thousand");
            scale.Add("1000000", "Million");
            scale.Add("1000000000", "Billion");
            scale.Add("1000000000000", "Trillion");
            scale.Add("1000000000000000", "Quadrillion");
            scale.Add("1000000000000000000", "Quintillion");

        }
    }
}
