using Class04.Models;

namespace Class04.Data
{
    public class DbInitializer
    {
        private readonly Context _context;
        public DbInitializer(Context context) { _context = context; }

        public void Run()
        {
            _context.Database.EnsureCreated();

            if (_context.Category.Any())
            {
                return;
            }

            var categories = new Category[]
            {
                new Category {Name="Programing", Description="Algoritms and programing area course", State=true, Date=DateTime.Now},
                new Category {Name="Administration", Description="Public administration business management courses", State=true, Date=DateTime.Now},
                new Category {Name="Comunication", Description="Business and institucional comunication courses", State=true, Date=DateTime.Now},
            };

            //_context.Category.AddRange(categories);
            foreach(var c in categories) 
            {
                _context.Category.Add(c);
            };
            _context.SaveChanges();
        }
    }
}
