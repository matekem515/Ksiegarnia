

using System.Collections.Generic;

namespace Ksiegarnia
{
	public abstract class LibraryBase : Book, ILibrary
	{
		public LibraryBase()
		{

		}
		public abstract event GradeAddedDelegate GradeAdded;
		public abstract event TitleChangeDelegate TitleChange;
		public abstract event StatisticAddView StatisticView;

		public abstract double AddGrade(string grade);

		public abstract void AddPrice(double price);

		public abstract void ChangeTitle(List<Book> books,string title);

		public abstract Statistics GetStatistics();

		public abstract void SendMemory(List<Book> returnBook);

		public abstract void SendTxt(List<Book> returnBook);
	}
}
