using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuroJackpotMostOftenNumbers
{
	public class EuroJackpotResponse
	{
		public Prize[] prizes { get; set; }
		public int[] starNumbers { get; set; }
		public string date { get; set; }
		public int[] winningNumbers { get; set; }

		public class Prize
		{
			public int id { get; set; }
			public string name { get; set; }
			public float amount { get; set; }
			public int numberOfWinners { get; set; }
		}
	}
}
