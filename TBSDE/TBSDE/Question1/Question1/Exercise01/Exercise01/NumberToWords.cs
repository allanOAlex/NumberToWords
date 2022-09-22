using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Exercise01
{
    public static class NumberToWordsExtensionMethod
    {
        public static string Towards(this BigInteger num)
        {

            var numberToWords = new NumberToWords();
            return numberToWords.Towards(num);
        }

    }

    public class NumberToWords
    {
        private Dictionary<string, string> TextString = new Dictionary<string, string>();
        private Dictionary<string, string> Scale = new Dictionary<string, string>();
        private int integer;
        private ulong longInt;
        private BigInteger bigInteger;
        private string prefix = "negative ";
        private string and = "and ";

        private StringBuilder builder;

        public NumberToWords()
        {
            Initialize();
        }

        public string Towards(BigInteger num)
        {
            builder = new StringBuilder();

            if (num == 0)
            {
                builder.Append(TextString[num.ToString()]);
                return builder.ToString();
            }

            if (BigInteger.TryParse(num.ToString(), out bigInteger))
            {
                if (num < 0)
                {
                    num *= (-1);

                    foreach (var scale in Scale)
                    {
                        string value = Convert.ToString(num);
                        string newNum = value;
                        newNum = Append(num.ToString(), scale.Key);
                    }

                    AppendValues(num.ToString());

                    return prefix + builder.ToString().Trim();

                }

            }

            foreach (var scale in Scale)
            {
                if (BigInteger.Parse(scale.Key) > num)
                {
                    continue;
                }
                switch (num)
                {
                    case var expression when num >= BigInteger.Parse(scale.Key):
                        AppendValues(num.ToString());
                        return builder.ToString().Trim();

                    default:
                        break;
                }

                //string outOfScaleValue = Convert.ToString(num);
                //string outOfScaleNumber = outOfScaleValue;
                //outOfScaleNumber = Append(num.ToString(), scale.Key);
            }



            AppendValues(num.ToString());

            return builder.ToString().Trim();
        }

        private string Append(string num, string scale)
        {
            if (num.Contains(","))
            {
                num = num.Replace(",", "");
            }

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
                builder.AppendFormat("{0} ", TextString[num]);
            }
        }

        private string AppendTens(string num)
        {
            if (Convert.ToUInt64(num) >= 10)
            {
                var tens = (Convert.ToUInt64(num) / 10) * 10;

                bool keyExists = TextString.ContainsKey(tens.ToString());

                if (keyExists)
                {
                    builder.AppendFormat("{0} ", TextString[tens.ToString()]);
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

                bool keyExists = TextString.ContainsKey(hundreds.ToString());

                if (keyExists)
                {
                    builder.AppendFormat("{0} hundred ", TextString[hundreds.ToString()]);
                    num = (Convert.ToUInt64(num) - (hundreds * 100)).ToString();

                    if (Convert.ToUInt64(num) > 0 && Convert.ToUInt64(num) < 100)
                    {
                        builder.AppendFormat($"{and}", TextString[hundreds.ToString()]);
                    }
                }
                else
                {
                    AppendValues(hundreds.ToString());
                    builder.AppendFormat("{0} hundred ", TextString[hundreds.ToString()]);
                    num = (Convert.ToUInt64(num) - (hundreds * 100)).ToString();

                    if (Convert.ToUInt64(num) > 0 && Convert.ToUInt64(num) > 100)
                    {
                        builder.AppendFormat($", ", TextString[hundreds.ToString()]);
                    }

                    if (Convert.ToUInt64(num) > 0 && Convert.ToUInt64(num) < 100)
                    {
                        builder.AppendFormat($"{and}", TextString[hundreds.ToString()]);
                    }
                }

            }
            return num;
        }

        private string AppendThousands(string num)
        {
            if (Convert.ToUInt64(num) >= 1000)
            {
                var thousands = Convert.ToUInt64(num) / 1000;

                bool keyExists = TextString.ContainsKey(thousands.ToString());

                if (keyExists)
                {
                    builder.AppendFormat("{0} thousand ", TextString[thousands.ToString()]);
                }
                else
                {
                    TextString.Add(thousands.ToString(), string.Empty.Trim());
                    AppendValues(thousands.ToString());
                    builder.AppendFormat("{0} thousand ", TextString[thousands.ToString()]).Replace("  ", " ");

                }

                num = (Convert.ToUInt64(num) - (thousands * 1000)).ToString();

                if (Convert.ToUInt64(num) > 0 && Convert.ToUInt64(num) > 100)
                {
                    builder.AppendFormat($", ", TextString[thousands.ToString()]);
                }

                if (Convert.ToUInt64(num) > 0 && Convert.ToUInt64(num) < 100)
                {
                    builder.AppendFormat($"{and}", TextString[thousands.ToString()]);
                }
            }

            return num;
        }

        private string AppendMillions(string num)
        {
            if (Convert.ToUInt64(num) >= 1000000)
            {
                var millions = Convert.ToUInt64(num) / 1000000;

                bool keyExists = TextString.ContainsKey(millions.ToString());

                if (keyExists)
                {
                    builder.AppendFormat("{0} million ", TextString[millions.ToString()]);
                }
                else
                {
                    TextString.Add(millions.ToString(), string.Empty.Trim());
                    AppendValues(millions.ToString());
                    builder.AppendFormat("{0} million ", TextString[millions.ToString()]).Replace("  ", " ");

                }

                num = (Convert.ToUInt64(num) - (millions * 1000000)).ToString();

                if (Convert.ToUInt64(num) > 0 && Convert.ToUInt64(num) > 100)
                {
                    builder.AppendFormat($", ", TextString[millions.ToString()]);
                }

                if (Convert.ToUInt64(num) > 0 && Convert.ToUInt64(num) < 100)
                {
                    builder.AppendFormat($"{and}", TextString[millions.ToString()]);
                }

            }
            return num;
        }

        private string AppendBillions(string num)
        {
            if (Convert.ToUInt64(num) >= 1000000000)
            {
                var billions = Convert.ToUInt64(num) / 1000000000;

                bool keyExists = TextString.ContainsKey(billions.ToString());

                if (keyExists)
                {
                    builder.AppendFormat("{0} billion ", TextString[billions.ToString()]);
                }
                else
                {
                    TextString.Add(billions.ToString(), string.Empty.Trim());
                    AppendValues(billions.ToString());
                    builder.AppendFormat("{0} billion ", TextString[billions.ToString()]).Replace("  ", " ");

                }

                num = (Convert.ToUInt64(num) - (billions * 1000000000)).ToString();

                if (Convert.ToUInt64(num) > 0 && Convert.ToUInt64(num) > 100)
                {
                    builder.AppendFormat($", ", TextString[billions.ToString()]);
                }


                if (Convert.ToUInt64(num) > 0 && Convert.ToUInt64(num) < 100)
                {
                    builder.AppendFormat($"{and}", TextString[billions.ToString()]);
                }

            }
            return num;
        }

        private string AppendTrillions(string num)
        {
            if (Convert.ToUInt64(num) >= 1000000000000)
            {
                var trillions = Convert.ToUInt64(num) / 1000000000000;

                bool keyExists = TextString.ContainsKey(trillions.ToString());

                if (keyExists)
                {
                    builder.AppendFormat("{0} trillion ", TextString[trillions.ToString()]);
                }
                else
                {
                    TextString.Add(trillions.ToString(), string.Empty.Trim());
                    AppendValues(trillions.ToString());
                    builder.AppendFormat("{0} trillion ", TextString[trillions.ToString()]).Replace("  ", " ");

                }

                num = (Convert.ToUInt64(num) - (trillions * 1000000000000)).ToString();

                if (Convert.ToUInt64(num) > 0 && Convert.ToUInt64(num) > 100)
                {
                    builder.AppendFormat($", ", TextString[trillions.ToString()]);
                }

                if (Convert.ToUInt64(num) > 0 && Convert.ToUInt64(num) < 100)
                {
                    builder.AppendFormat($"{and}", TextString[trillions.ToString()]);
                }

            }
            return num;
        }

        private string AppendQuadrillions(string num)
        {
            if (Convert.ToUInt64(num) >= 1000000000000000)
            {
                var quadrillions = Convert.ToUInt64(num) / 1000000000000000;

                bool keyExists = TextString.ContainsKey(quadrillions.ToString());

                if (keyExists)
                {
                    builder.AppendFormat("{0} quadrillion ", TextString[quadrillions.ToString()]);
                }
                else
                {
                    TextString.Add(quadrillions.ToString(), string.Empty.Trim());
                    AppendValues(quadrillions.ToString());
                    builder.AppendFormat("{0} quadrillion ", TextString[quadrillions.ToString()]).Replace("  ", " ");

                }

                num = (Convert.ToUInt64(num) - (quadrillions * 1000000000000000)).ToString();

                if (Convert.ToUInt64(num) > 0 && Convert.ToUInt64(num) > 100)
                {
                    builder.AppendFormat($", ", TextString[quadrillions.ToString()]);
                }

                if (Convert.ToUInt64(num) > 0 && Convert.ToUInt64(num) < 100)
                {
                    builder.AppendFormat($"{and}", TextString[quadrillions.ToString()]);
                }

            }
            return num;
        }

        private string AppendQuintillions(string num)
        {
            BigInteger convertedString = BigInteger.Parse(num);

            if (convertedString >= 1000000000000000000)
            {
                var quintillions = convertedString / 1000000000000000000;

                bool keyExists = TextString.ContainsKey(quintillions.ToString());

                if (keyExists)
                {
                    builder.AppendFormat("{0} quintillion ", TextString[quintillions.ToString()]);
                }
                else
                {
                    TextString.Add(quintillions.ToString(), string.Empty.Trim());
                    AppendValues(quintillions.ToString());
                    builder.AppendFormat("{0} quintillion ", TextString[quintillions.ToString()]).Replace("  ", " ");

                }

                BigInteger bitInt = convertedString - quintillions * 1000000000000000000;

                num = bitInt.ToString();

                if (BigInteger.Parse(num) > 0 && BigInteger.Parse(num) > 100)
                {
                    builder.AppendFormat($", ", TextString[quintillions.ToString()]);
                }

                if (Convert.ToUInt64(num) > 0 && Convert.ToUInt64(num) < 100)
                {
                    builder.AppendFormat($"{and}", TextString[quintillions.ToString()]);
                }
            }
            return num;
        }


        private void Initialize()
        {
            TextString.Add("0", "Zero");
            TextString.Add("1", "One");
            TextString.Add("2", "Two");
            TextString.Add("3", "Three");
            TextString.Add("4", "Four");
            TextString.Add("5", "Five");
            TextString.Add("6", "Six");
            TextString.Add("7", "Seven");
            TextString.Add("8", "Eight");
            TextString.Add("9", "Nine");
            TextString.Add("10", "Ten");
            TextString.Add("11", "Eleven");
            TextString.Add("12", "Twelve");
            TextString.Add("13", "Thirteen");
            TextString.Add("14", "Fourteen");
            TextString.Add("15", "Fifteen");
            TextString.Add("16", "Sixteen");
            TextString.Add("17", "Seventeen");
            TextString.Add("18", "Eighteen");
            TextString.Add("19", "Nineteen");
            TextString.Add("20", "Twenty");
            TextString.Add("30", "Thirty");
            TextString.Add("40", "Forty");
            TextString.Add("50", "Fifty");
            TextString.Add("60", "Sixty");
            TextString.Add("70", "Seventy");
            TextString.Add("80", "Eighty");
            TextString.Add("90", "Ninety");
            TextString.Add("100", "One Hundred");

            Scale.Add("1000000000000000000", "Quintillion");
            Scale.Add("1000000000000000", "Quadrillion");
            Scale.Add("1000000000000", "Trillion");
            Scale.Add("1000000000", "Billion");
            Scale.Add("1000000", "Million");
            Scale.Add("1000", "Thousand");
            Scale.Add("100", "Hundred");

        }

    }


}
