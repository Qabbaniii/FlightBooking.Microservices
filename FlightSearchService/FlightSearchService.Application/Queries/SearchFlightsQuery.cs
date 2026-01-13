public record SearchFlightsQuery(

    string From,
    string To,
    DateTime Date,
    string CabinClass,
    int PassengersCount

);
