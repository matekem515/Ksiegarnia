using System;

namespace Ksiegarnia
{
	public class Delegates
	{
		public  void OnGradeAdded(object sender, EventArgs args)
		{
			Console.WriteLine($"Book receive low grade, we will give it on the lower shelf");
			
		}
		public  void OnTitleChange(object sender, EventArgs args)
		{
			Console.WriteLine($"Title was changed for this book");
			
		}
		public void StatisticView(object sender, EventArgs args)
		{
			Console.WriteLine($"Statistics was showed");
			
		}
	}
}