using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using NegocioGalRio_API.Contexts;
using NegocioGalRio_API.DTO;
using NegocioGalRio_API.Models;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        //coneccion a la base de datos
        var connectionString = builder.Configuration.GetConnectionString("connection");
        //se hace la inyeccion de dependencia para poder usarlo mas facil en los controller
        builder.Services.AddDbContext<NegocioContext>(options => options.UseSqlServer(connectionString));

        //var mapper = ConfigureMapper();
        //builder.Services.AddSingleton(mapper);
        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddAutoMapper(typeof(Program));
        //Registro de servicios y repositorios
        //builder.Services.AddScoped<RolRepository>();
        //builder.Services.AddScoped<RolService>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Producto, ProductoDTO>(); // Mapeo de Producto a ProductoDTO
        CreateMap<ProductoDTO, Producto>(); // Mapeo de ProductoDTO a Producto
        CreateMap<ProductoInsert, Producto>(); // Mapeo de ProductoDTO a Producto
        CreateMap<ProductoUpdate, Producto>();

        CreateMap<CategoriaDTO, Categoria>();
        CreateMap<Categoria, CategoriaDTO>();
        CreateMap<CategoriaInsert, Categoria>();

        CreateMap<Proveedor, ProveedorInsert>();
        CreateMap<ProveedorInsert, Proveedor>();

        CreateMap<Compra, CompraDTO>();
        CreateMap<CompraDTO, Compra>();
        
        CreateMap<DetalleCompraDTO, DetalleCompra>();
        CreateMap<DetalleCompra, DetalleCompraDTO>();

        CreateMap<CompraInsert, Compra>();
        CreateMap<Compra, CompraInsert>();

        CreateMap<DetalleCompraInsert, DetalleCompra>();

        CreateMap<Venta, VentaDTO>();
        CreateMap<VentaDTO, Venta>();

        CreateMap<DetalleVentaDTO, DetalleVenta>();
        CreateMap<DetalleVenta, DetalleVentaDTO>();

        CreateMap<VentaInsert, Venta>();
        CreateMap<Venta, VentaInsert>();

        CreateMap<DetalleVentaInsert, DetalleVenta>();

        // Si los nombres de propiedades difieren, usa .ForMember()
    }
}