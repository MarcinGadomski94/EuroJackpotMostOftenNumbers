using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuroJackpotMostOftenNumbers
{
	public class EuroJackpotRequest
	{
		private readonly string BaseAddress = @"https://danskespil.dk/dlo/scapi/danskespil/numbergames/eurojackpot/winningNumbers?";
		public DateTime Date { get; set; }

		public override string ToString()
		{
			return $"{BaseAddress}date={Date.ToString("yyyy-MM-dd")}";
		}
	}
}
