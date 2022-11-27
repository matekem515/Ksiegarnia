using System.Collections.Generic;

namespace Ksiegarnia
{
	public interface ILibrary
	{
		string Title { get; }
		double AddGrade(string grade);

		void AddPrice(double price);

		void SendMemory(List<Book> returnBook);

		void ChangeTitle(List<Book> books , string title);

		Statistics GetStatistics();
		void SendTxt(List<Book> returnBook);

		event GradeAddedDelegate GradeAdded;

		event TitleChangeDelegate TitleChange;

		event StatisticAddView StatisticView;
	}
}
