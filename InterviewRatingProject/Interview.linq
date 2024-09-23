<Query Kind="Program" />

void Main()
{
    const string csvFile = "restaurant-ratings.csv"; 

    if (File.Exists(csvFile))
    {
        string[] lines = File.ReadAllLines(csvFile);
        var ratings = new List<CityRating>();

        foreach (string line in lines.Skip(1)) 
        {
            string[] columns = line.Split(',');

            if (columns.Length == 4)
            {
                CityRating rating = new CityRating
                {
                    Id = int.Parse(columns[0]),
                    City = columns[1],
                    Cousine = columns[2],
                    Rating = double.Parse(columns[3])
                };
                ratings.Add(rating);
            }
        }

        var result = ratings
            .GroupBy(r => r.City)
            .Select(g => new { City = g.Key, AverageRating = g.Average(r => r.Rating) })
            .OrderByDescending(r => r.AverageRating)
            .First();

        $"{result.City} : {result.AverageRating}".Dump();
    }
    else
    {
        "File does not Exists.".Dump();
    }
}

public class CityRating
{
    public int Id { get; set; }
    public string City { get; set; }
    public string Cousine { get; set; }
    public double Rating { get; set; }
}
