

namespace Ksiegarnia
{
	public class Book
	{
		private string title;
		private double grade;
		private double price;
		public Book()
		{
	
			
		}
		public string Title
		{
			get
			{
				return this.title;
			}
			set
			{
				title = value;
			}
		}
		public double Grade
		{
			get
			{
				return this.grade;
			}
			set
			{
				grade = value;
			}
		}
		public double Price
		{
			get
			{
				return this.price;
			}
			set
			{
				price = value;
			}
		}
	}
}
