using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EuroJackpotMostOftenNumbers
{
	class Program
	{
		private static Dictionary<int, int> _numbersCount;
		private static Dictionary<int, int> _numbersCountSorted;
		private static Dictionary<int, int> _starNumbersCount;
		private static Dictionary<int, int> _starNumbersCountSorted;

		private static readonly int MAX_NUMBER = 50;
		private static readonly int MAX_STAR_NUMBER = 10;

		static void Main(string[] args)
		{
			_numbersCount = new Dictionary<int, int>();
			_numbersCountSorted = new Dictionary<int, int>();
			_starNumbersCount = new Dictionary<int, int>();
			_starNumbersCountSorted = new Dictionary<int, int>();

			var httpClient = new HttpClient();
			httpClient.DefaultRequestHeaders.Accept.Clear();
			httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

			var drawDate = new DateTime(2015, 01, 02);
			var draws = 0;

			var numberToAdd = 1;
			while (numberToAdd <= MAX_NUMBER)
			{
				_numbersCount.Add(numberToAdd, 0);
				numberToAdd++;
			}

			var starNumberToAdd = 1;
			while (starNumberToAdd <= MAX_STAR_NUMBER)
			{
				_starNumbersCount.Add(starNumberToAdd, 0);
				starNumberToAdd++;
			}

			while (drawDate < DateTime.Now)
			{
				draws++;
				Console.WriteLine($"Checking result for {drawDate.ToString("yyyy-MM-dd")}");
				var externalRequest = new EuroJackpotRequest();
				externalRequest.Date = drawDate;
				var externalRequestString = externalRequest.ToString();
				var httpResponse = httpClient.GetAsync(externalRequestString).Result;
				var externalResponseString = httpResponse.Content.ReadAsStringAsync().Result;
				var externalResponse = JsonConvert.DeserializeObject<EuroJackpotResponse>(externalResponseString);

				foreach (var winningNumber in externalResponse.winningNumbers)
				{
					if (_numbersCount.ContainsKey(winningNumber))
						_numbersCount[winningNumber] = _numbersCount[winningNumber] + 1;
					else
						_numbersCount.Add(winningNumber, 1);
				}

				foreach (var starNumber in externalResponse.starNumbers)
				{
					if (_starNumbersCount.ContainsKey(starNumber))
						_starNumbersCount[starNumber] = _starNumbersCount[starNumber] + 1;
					else
						_starNumbersCount.Add(starNumber, 1);
				}

				drawDate = drawDate.AddDays(7);
			}

			foreach(var item in _numbersCount.OrderByDescending(a => a.Value))
				_numbersCountSorted.Add(item.Key, item.Value);

			foreach(var item in _starNumbersCount.OrderByDescending(a => a.Value))
				_starNumbersCountSorted.Add(item.Key, item.Value);

			Console.WriteLine(Environment.NewLine);

			foreach(var item in _numbersCountSorted)
				Console.WriteLine($"Number {item.Key} was chosen {item.Value} times in {draws} draws");

			Console.WriteLine(Environment.NewLine);

			foreach(var item in _starNumbersCountSorted)
				Console.WriteLine($"Star number {item.Key} was chosen {item.Value} times in {draws} draws");

			Console.ReadLine();
		}
	}
}
