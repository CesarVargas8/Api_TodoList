namespace IntroBackend
{
    public class Repository {
        List<Brewery> breweries = new List<Brewery>{
            new Brewery{Id=1, Name="Minerva"},
            new Brewery{Id=2, Name="Cerveceria de Colima"},
            new Brewery{Id=3, Name="Cerveceria de Baja California"},
        };

        public List<Brewery> GetBreweries() {
            return breweries;
        }

        public Brewery? GetBrewery(int id) => breweries.Find(r => r.Id == id);
    }

    public class Brewery {
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}