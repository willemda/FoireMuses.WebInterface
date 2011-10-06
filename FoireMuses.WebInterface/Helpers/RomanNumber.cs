using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoireMuses.WebInterface.Helpers
{
	public static class RomanNumber
	{
		private static readonly char[] chiffre1 = new char[] { 'I', 'X', 'C', 'M', '?' };
		private static readonly char[] chiffre5 = new char[] { 'V', 'L', 'D', '?', '?' };

		private static int ChiffreRomainToArabe(char aChar)
		{
			switch (aChar)
			{
				case 'C':
					return 100;

				case 'D':
					return 500;

				case 'I':
					return 1;

				case 'L':
					return 50;

				case 'M':
					return 1000;

				case 'V':
					return 5;

				case 'X':
					return 10;
			}
			return 0;
		}

		private static string Puissance10ArabeToRomain(int aNumber, int aPuissance10)
		{
			switch (aNumber)
			{
				case 0:
					return "";

				case 1:
					return chiffre1[aPuissance10].ToString();

				case 2:
					return String.Format("{0}{1}", chiffre1[aPuissance10], chiffre1[aPuissance10]);

				case 3:
					return String.Format("{0}{1}{2}", chiffre1[aPuissance10], chiffre1[aPuissance10], chiffre1[aPuissance10]);

				case 4:
					return String.Format("{0}{1}", chiffre1[aPuissance10], chiffre5[aPuissance10]);

				case 5:
					return chiffre5[aPuissance10].ToString();

				case 6:
					return String.Format("{0}{1}", chiffre5[aPuissance10], chiffre1[aPuissance10]);

				case 7:
					return String.Format("{0}{1}{2}", chiffre5[aPuissance10], chiffre1[aPuissance10], chiffre1[aPuissance10]);

				case 8:
					return String.Format("{0}{1}{2}{3}", chiffre5[aPuissance10], chiffre1[aPuissance10], chiffre1[aPuissance10], chiffre1[aPuissance10]);

				case 9:
					return String.Format("{0}{1}", chiffre1[aPuissance10], chiffre1[aPuissance10 + 1]);
			}
			return "???";
		}

		public static int ToArabe(string aRomanNumber)
		{
			int resultat = 0;
			for (int i = 0; i < aRomanNumber.Length; i++)
			{
				int n = ChiffreRomainToArabe(aRomanNumber[i]);
				if (((i + 1) < aRomanNumber.Length) && (n < ChiffreRomainToArabe(aRomanNumber[i + 1])))
				{
					resultat -= n;
				}
				else
				{
					resultat += n;
				}
			}
			return resultat;
		}

		public static string ToRoman(int aNumber)
		{
			if ((aNumber < 0) || (aNumber > 0xf9f))
			{
				return "?";
			}
			string result = "";
			for (int puissance10 = 0; aNumber > 0; puissance10++)
			{
				result = Puissance10ArabeToRomain(aNumber % 10, puissance10) + result;
				aNumber /= 10;
			}
			return result;
		}
	}
}