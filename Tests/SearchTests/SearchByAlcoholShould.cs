using BLL.Services;
using FluentAssertions;
using Xunit;

namespace Tests.SearchTests
{
    public class SearchByAlcoholShould : IClassFixture<TestWithSqlite>
    {
        private readonly DrinkSearchService _sut;

        public SearchByAlcoholShould(TestWithSqlite fixture)
        {
            _sut = new DrinkSearchService(fixture.Repository);
        }

        [Fact]
        public void Get_all_drinks_from_the_database_if_we_tick_all_three_alcohol_options()
        {
            var filteredDrinks = _sut.SearchByAlcoholContent(true, true, true);
            filteredDrinks.Should().HaveCount(42);
        }

        [Fact]
        public void Get_correct_number_of_drinks_for_alcohol_true_option_only()
        {
            var filteredDrinks = _sut.SearchByAlcoholContent(true, false, false);
            filteredDrinks.Should().HaveCount(37);
        }

        [Fact]
        public void Get_correct_number_of_drinks_for_nonalcohol_true_option_only()
        {
            var filteredDrinks = _sut.SearchByAlcoholContent(false, true, false);
            filteredDrinks.Should().HaveCount(4);
        }

        [Fact]
        public void Get_correct_number_of_drinks_for_alcohol_optional_true_option_only()
        {
            var filteredDrinks = _sut.SearchByAlcoholContent(false, false, true);
            filteredDrinks.Should().HaveCount(1);
        }

        [Fact]
        public void Get_no_drinks_if_all_options_are_unticked()
        {
            var filteredDrinks = _sut.SearchByAlcoholContent(false, false, false);
            filteredDrinks.Should().BeEmpty();
        }
    }
}
