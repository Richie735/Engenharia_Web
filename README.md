# Web_Engineering

Notes from Web Engineering, a curricular unit, 1st semester of the 3rd year of computer engineering

# Frequência 1

1. Criar Modelo de Dados
	Models → Add → Class 
	→ Adicionar Biblioteca
		using System.ComponentModel.DataAnnotations;
	→ Adicionar Atributos
		- Se é valor único → [Key]
		- Se é obrigatório → [Required(ErrorMessage = "Required field")] (Boolean não precisaom de ErrorMessage)
		- Tipo de Ficheiro (ex. PDF) → [RegularExpression(@"^.+\.([pP][dD][fF])$”)]
		- Extenção Maxima → [StringLength(256, ErrorMessage = "{0} length can not exceed {1} characters")]
		- Entenção Entre 2 Valores → [StringLength(50, MinimumLength = 3, ErrorMessage = "{0} length must be between {2} and {1}")]
		- So pode ter letras → [RegularExpression(@"[a-zA-Z\d]{3,}", ErrorMessage="Not Valid")]

2. Criar Controlador
	Controllers → Add → Controller 
	→ MVC Controller with views, using Entity Framework (caso Db)
		Model Class = Model criado antes
		Data context class = + → mudar nome (NomeDb.Data.Context)

3. Mudar a Home Pag
	Program.cs → +/-linha 30 → alterar o pattern (pattern: "{controller=ControllerName}/{action=ControllerViewName}/{id?}");)

4. Preparar Data Base
	Package Manager Console → "add-migration Name" → Esperar o fim da execução → "update-database"

5. Inicializar Data Base
	Data → Add → Class 
	→ Rename "DbInitializer.cs" → 
	```C#
	using DbName.Data;
	using ProjectName.Models;
	
	namespace ProjectName.Data
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
	
	            var categories = new ModelName[]
	            {
	                new ModelName {/*Inicializar os Atributos*/},
	                //Repetir algumas vezes
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
	```
	→ Ir ao Program.cs → Adicionar este codigo
	```C#
	using Microsoft.EntityFrameworkCore;
	using Microsoft.Extensions.DependencyInjection;
	using DbName.Data;       //Verificar se tem
	using ProjectName.Data;  //Estas 2 bibliotecas
	//(...)
	// Add services to the container.
	builder.Services.AddControllersWithViews();
	
	builder.Services.AddTransient<DbInitializer>();
	
	var app = builder.Build();
	
	using var scope = app.Services.CreateScope();
	var services = scope.ServiceProvider;
	
	var initializer = services.GetRequiredService<DbInitializer>();
	
	initializer.Run();
	
	// Configure the HTTP request pipeline.
	if (!app.Environment.IsDevelopment())
	```

6. Executar

## Extras
- Redirecionamento
	```C#
	app.MapControllerRoute(
    name: "Filtered",
    pattern: "PaginaX", //localhost:XXXX/PaginaX leva para a ControllerName/ControllerView
    defaults: new { Controller = "ControllerName", Action = "ControllerView" }
    );
	```

- Adicionar Novas Funções ao Controller
	- Se o método servir para alterar apenas um atributo (ex comprar) não colocamos HttpPost
	```C#
	public async Task<IActionResult>Comprar(int? id)
	{
		if (id == null || _context.Carro == null)
		{ return NotFound(); }
	
		var carro = await _context.Carro.FirstOrDefaultAsync(x => x.Id == id);
	
		if (carro == null)
		{ return NotFound(); }
	
		carro.Vendido = true;
	
		_context.Update(carro);
		await _context.SaveChangesAsync();
	
		return RedirectToAction("Index");
	}
	```

- Ordenar a View
	```C#
	View(await _context.NomeDaClasse.OrderByDescending(x => x.ParametroDeOrdenaçao).ToListAsync());
	```

- Inserir Imagens sem criar pasta
	```C#
	try
	{
		var destination = Path.Combine(_he.ContentRootPath, "wwwroot/Fotos", Path.GetFileName(Foto.FileName));
		FileStream fs = new FileStream(destination, FileMode.Create);
		await Foto.CopyToAsync(fs);
		fs.Close();
		carro.Foto = Foto.FileName;
		carro.Vendido = false;
		_context.Add(carro);
		await _context.SaveChangesAsync();
		return RedirectToAction("Index");
	}
	catch
	{
	return View(carro);
	}
	```

- Inserir Imagens e criar nova pasta
	```C#
	public async Task<IActionResult> Inserir(Class class, IFormCollection Files)
	{
		try
		{
			string destination = Path.Combine(_he.ContentRootPath, "wwwroot\\", class.atributoKey);
			
			if (!Directory.Exists(destination))
			{
				Directory.CreateDirectory(destination);
			}
			
			foreach (var f in Files.Files)
			{
				string path = destination + "\\" + f.FileName;
				
				FileStream fs = new FileStream(path, FileMode.Create);
				
				f.CopyTo(fs);
				
				class.desig=f.Name;
				fs.Close();
			}
			
			_context.Add(class);
			await _context.SaveChangesAsync();
			return RedirectToAction("Lista");
		}
		catch
		{
			return View(class);
		}
	}
	```

- Inserir mensagens de sucesso
	```C#
	//Controlador
	string Inserted = Mensagem;
	TempData["texto"] = Inserted;
	
	//View
	<p>
	<label>@TempData["Texto"]</label>
	</p>
	```

- Inserir dados na base de dados
	SQL server Object → SQL Server → localdb → Databases → Escolher Base de dados → Tables → Escolher Tabela → View Data → Adiconar dados nas linhas