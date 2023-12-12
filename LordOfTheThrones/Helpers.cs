using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LordOfTheThrones
{
	public class Helpers
	{
		public static bool NameChecker(string name)
		{
			if (name.Length >= 16)
			{
				return false;
			}
			return true;
		}
			           
	}
}
