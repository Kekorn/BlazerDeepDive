namespace ServerManagement.Models
{
    public class ServersRepository
    {
        private static List<Server> servers = new List<Server>
        {
            new Server { ServerId = 1, Name = "Server 1", City = "New York" },
            new Server { ServerId = 2, Name = "Server 2", City = "Los Angeles" },
            new Server { ServerId = 3, Name = "Server 3", City = "Chicago" },
            new Server { ServerId = 4, Name = "Server 4", City = "Houston" },
            new Server { ServerId = 5, Name = "Server 5", City = "Phoenix" },
            new Server { ServerId = 6, Name = "Server 6", City = "Philadelphia" },
            new Server { ServerId = 7, Name = "Server 7", City = "San Antonio" },
            new Server { ServerId = 8, Name = "Server 8", City = "San Diego" },
            new Server { ServerId = 9, Name = "Server 9", City = "Dallas" },
            new Server { ServerId = 10, Name = "Server 10", City = "San Jose" },
            new Server { ServerId = 11, Name = "Server 11", City = "Austin" },
            new Server { ServerId = 12, Name = "Server 12", City = "Jacksonville" },
            new Server { ServerId = 13, Name = "Server 13", City = "Fort Worth" },
            new Server { ServerId = 14, Name = "Server 14", City = "Columbus" },
            new Server { ServerId = 15, Name = "Server 15", City = "Charlotte" }
        };

        public static void AddServer(Server server)
        {
            var maxId = servers.Max(s => s.ServerId);
            server.ServerId = maxId + 1;
            servers.Add(server);
        }

        public static List<Server> GetAllServers() => servers;

        public static List<Server> GetOnlineServers() => servers.Where(s => s.IsOnline).ToList();

        public static List<Server> GetOfflineServers() => servers.Where(s => !s.IsOnline).ToList();

        public static List<Server> GetServersByCity(string city)
        {
            return servers.Where(s => s.City?.Equals(city, StringComparison.OrdinalIgnoreCase) == true).ToList();
        }
        public static Server? GetServerById(int id)
        {
            var server = servers.FirstOrDefault(s => s.ServerId == id);
            if (server != null)
            {
                return new Server
                {
                    ServerId = server.ServerId,
                    Name = server.Name,
                    City = server.City,
                    IsOnline = (new Random().Next(0, 2)) == 0 ? false : true
                };
            }
            return null;
        }
    }
}
