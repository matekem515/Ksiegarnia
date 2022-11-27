using System;
using Xunit;
namespace Ksiegarnia.tests
{
	public class TypeTests
	{
		int counter = 0;
		public delegate string WriteMessage(string message);

		[Fact]
		public void WriteMessageDelegateCanPointToMethod()
		{
			
			WriteMessage del;

			del = ReturnMessage;
			del += ReturnMessage;
			del += ReturnMessage2;

			var result = del("Hello");
			Assert.Equal(3, counter);
		}

		string ReturnMessage(string message)
		{
			counter++;
			return message;
		}
		string ReturnMessage2(string message)
		{
			counter++;
			return message.ToUpper() ;
		}
		[Fact]
		public void GetEmployeeReturnsDifferentsObjects()
		{
			var emp1 = GetTitle("LOTR");
			var emp2 = GetTitle("Harry Potter");
			

			Assert.NotSame(emp1, emp2);
			Assert.False(object.ReferenceEquals(emp1, emp2));
		}
		[Fact]
		public void GetEmployeeReturnsSameobjects()
		{
			var emp1 = GetTitle("Diuna");
			var emp2 = emp1;
			Assert.Same(emp1, emp2);
			Assert.True(object.ReferenceEquals(emp1, emp2));
		}

		[Fact]
		public void CScharCanPassByReference()
		{
			var emp1 = GetTitle("Diuna");
			GetLibrarySetTitile(out emp1, "NewTitle");
			Assert.Equal("NewTitle", emp1.Title);
		}

		private void GetLibrarySetTitile(out Library lib,string title)
		{
			lib = new Library(title);
		}
		[Fact]
		public void CanSetNameFromReference()
		{
			var emp1 = GetTitle("Diuna");
			this.SetTitle(emp1, "NewTitle");
			Assert.Equal("NewTitle", emp1.Title);
		}
		
		private Library GetTitle(string title)
		{
			return new Library(title);
		}

		private void SetTitle(Library library,string title)
		{
			library.Title = title;
		}
		[Fact]
		public void ChangeTitleFromLibrary()
		{
			var emp1 = GetTitle("Diuna");
			this.SetTitle(emp1, "NewTitle");
			Assert.Equal("NewTitle", emp1.Title);
		}

	}
}
