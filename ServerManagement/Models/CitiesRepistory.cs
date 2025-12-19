namespace ServerManagement.Models
{
    public class CitiesRepistory
    {        
        private static List<string> cities= ServersRepository
            .GetAllServers()
            .Select(s => s.City)
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .Where(c => c is not null)
            .Cast<string>()
            .ToList();

        public static List<string> GetCities() => cities;
    }

}
