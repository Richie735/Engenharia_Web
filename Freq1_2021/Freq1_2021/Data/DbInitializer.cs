using Db_al73254.Data;
using Freq1_2021.Models;

namespace Freq1_2021.Data
{
    public class DbInitializer
    {
        private readonly Context _context;
        public DbInitializer(Context context) { _context = context; }

        public void Run()
        {
            _context.Database.EnsureCreated();

            if (_context.Contact.Any())
            {
                return;
            }

            var categories = new Contact[]
            {
                new Contact {Email="ex01@gmail.com", NickName="ex01", Name="Ex 01", Amigo=true },
                new Contact {Email="ex02@gmail.com", NickName="ex02", Name="Ex 02", Amigo=false },
                new Contact {Email="ex03@gmail.com", NickName="ex03", Name="Ex 02", Amigo=true },
            };

            //_context.Category.AddRange(categories);
            foreach (var c in categories)
            {
                _context.Contact.Add(c);
            };
            _context.SaveChanges();
        }
    }
}