using FluentValidation;

public class SearchFlightsQueryValidator
    : AbstractValidator<SearchFlightsQuery>
{
    public SearchFlightsQueryValidator()
    {
        RuleFor(x => x.From).NotEmpty();
        RuleFor(x => x.To).NotEmpty();
        RuleFor(x => x.PassengersCount).GreaterThan(0);
        RuleFor(x => x.Date).GreaterThanOrEqualTo(DateTime.Today);
    }
}
