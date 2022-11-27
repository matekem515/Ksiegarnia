using System;
using Xunit;
namespace Ksiegarnia.tests
{
	public class LibraryTest
	{
		[Fact]
		public void Test()
		{
			//arrange
			var lib = new Library();
			lib.AddPrice(10.4);
			lib.AddPrice(9.4);
			lib.AddPrice(11.4);
			//act
			var result = lib.GetStatistics();

			//assert
			Assert.Equal(10.4, result.average,1);
			Assert.Equal(11.4,result.priceHigh);
			Assert.Equal(9.4,result.priceLow);
		}
	}
}
