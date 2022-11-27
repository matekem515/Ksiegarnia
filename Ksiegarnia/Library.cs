using System;
using System.Collections.Generic;
using System.IO;

namespace Ksiegarnia
{
	public delegate void GradeAddedDelegate(object sender, EventArgs args);
	public delegate void TitleChangeDelegate(object sender, EventArgs args);
	public delegate void StatisticAddView(object sender, EventArgs args);
	public class Library: LibraryBase
	{
		private const string FILENAME = "library.txt";
		private const string AUDIT = "audit.txt";

		private List<double> prices = new List<double>() {};

		DateTime dateTime = DateTime.UtcNow;
		public override event GradeAddedDelegate GradeAdded;
		public override event TitleChangeDelegate TitleChange;
		public override event StatisticAddView StatisticView;

		Delegates delegates = new Delegates();

		public Library()
		{
			

		}
		public override void AddPrice(double price)
		{
			prices.Add(price);

		}
		public override double AddGrade(string s)
		{
			if (double.TryParse(s, out double result))
			{
			
				if (result <3 && GradeAdded != null)
				{
					GradeAdded(this, new EventArgs());
				}
					
	
			}
			return result;
		}
		public override void ChangeTitle(List<Book> books, string title)
		{
			
			foreach (var item in books)
			{
				Console.WriteLine($"Book: { item.Title} ?");
			}
			foreach (var item in books)
			{
				Console.WriteLine("Which book title you want to change ?");
				title = Console.ReadLine();
				if (item.Title == title)
				{
					
					Console.WriteLine("Write down new book title");
					title = Console.ReadLine();
					item.Title = title;
					TitleChange(this, new EventArgs());
					break;
				}
				else
				{
					Console.WriteLine("No book found with this title ");
				}
			}
			
		}

		public override Statistics GetStatistics()
		{
			var result = new Statistics();
			result.priceHigh = double.MinValue;
			result.priceLow = double.MaxValue;
			result.average = 0.0;

			foreach (var price in prices )
			{
				result.priceLow = Math.Min(result.priceLow, price);
				result.priceHigh = Math.Max(result.priceHigh, price);

				result.average += price;
			}
			result.average /= prices.Count;
			StatisticView(this, new EventArgs());
			return result;
		}

		public override void SendMemory(List<Book> returnBook)
		{
			foreach (var item in returnBook)
			{
				Console.WriteLine($"Book: {item.Title}");
				Console.WriteLine($"Grade: {item.Grade}");
				Console.WriteLine($"Price: {item.Price}");
			}
			
		}

		public override void SendTxt(List<Book> returnBook)
		{
			using (var write = File.AppendText(FILENAME))
			{
				foreach (var item in returnBook)
				{
					write.WriteLine($"Book: {item.Title}");
					write.WriteLine($"Grade: {item.Grade}");
					write.WriteLine($"Price: {item.Price}");
				}
				
			}
			using (var write = File.AppendText(AUDIT))
			{
				foreach (var item in returnBook)
				{
					write.WriteLine($"Book: {item.Title}, Time: {dateTime}");
					write.WriteLine($"Grade: {item.Grade}, Time: {dateTime}");
					write.WriteLine($"Price: {item.Price}, Time: {dateTime}");
				}
				
			}
		}
		
		public void GradeAdd(string grade,Book book)
		{

				if ((grade.Contains("+") || (grade.Contains("-"))))
				{


					switch (grade)
					{
						case "-6" or "6-" or "5+" or "+5":

							{
								book.Grade = AddGrade("5,5");
								break;
							}

						case "-5" or "5-" or "4+" or "+4":
							{
								book.Grade = AddGrade("4,5");
								break;
							}
						case "-4" or "4-" or "3+" or "+3":
							{
								book.Grade = AddGrade("3,5");
								break;
							}
						case "-3" or "3-" or "2+" or "+2":
							{
								book.Grade = AddGrade("2,5");
								break;
							}
						case "-2" or "2-" or "1+" or "+1":
							{
								book.Grade = AddGrade("1,5");
								break;
							}

					}

				}
				else if ((int.Parse(grade) > 0 && int.Parse(grade) < 7))
				{
					book.Grade = AddGrade(grade);
				}
				else
				{
					Console.WriteLine("Choose the correct number");
				}
			
			
		}
		
		public List<Book> EnterInformations()
		{

			List<Book> returnBook = new List<Book>();
			string r;
			string title = null;
			string grade = null;
			double price = 0.0;

			while (true)
			{

				try
				{

					Book book = new Book();
					Console.WriteLine();
					Console.WriteLine("------------------------------");
					Console.WriteLine("Welcome in the library program");
					Console.WriteLine("-------------------------------");
					Console.WriteLine("If you want to add new book write 'D' letter to the console");
					Console.WriteLine("-------------------------------");
					Console.WriteLine("If you want to store the program results on the hard drive write T / if you want to store it in the memory write P");
					Console.WriteLine("-------------------------------");
					Console.WriteLine("To see the book prices MIN/Max and AVG write letter K");
					Console.WriteLine("-------------------------------");
					Console.WriteLine("If you want to quit the program write letter Q");
					
					
					if (returnBook.Count > 0)
					{
						Console.WriteLine("-------------------------------");
						Console.WriteLine("Do you want to change the title of the book ? If yes, write letter C");
						
					}

					switch (r = Console.ReadLine().ToLower())
					{

						case "d":
							Console.WriteLine("Give a title for the new book");
							title = Console.ReadLine();
							Console.WriteLine("Give a grade for the new book");
							grade = Console.ReadLine();
							Console.WriteLine("Give a price for the new book");
							try
							{
								price = double.Parse(Console.ReadLine());
							}
							catch (FormatException e)
							{

								Console.WriteLine($"{e.Message}");
								break;
							}
							


							book.Title = title;
							book.Price = price;
							try
							{
								GradeAdded += delegates.OnGradeAdded;
								GradeAdd(grade, book);
							}
							catch (FormatException e)
							{
								Console.WriteLine($"{e.Message}");
								break;
							}
							
							returnBook.Add(book);
							prices.Add(book.Price);
							continue;

						case "t":
							SendTxt(returnBook);
							continue;
						case "p":
							SendMemory(returnBook);
							continue;
						case "c":
							TitleChange += delegates.OnTitleChange;
							ChangeTitle(returnBook, title);
							continue;
						case "k":
							StatisticView += delegates.StatisticView;
							Console.WriteLine($"Avarage price:  {GetStatistics().average}");
							Console.WriteLine($"Minimal price:  {GetStatistics().priceLow}");
							Console.WriteLine($"Maximal price: {GetStatistics().priceHigh}");
							continue;
						case "q":
							break;

					}

				}
				catch (ArgumentException ex)
				{

					Console.WriteLine(ex.Message);
				}
				catch (IndexOutOfRangeException ex)
				{

					Console.WriteLine(ex.Message);
				}


			}


		}
	}
}
