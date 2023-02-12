using Customer_Information.WebAPI;
using Customer_Information.WebAPI.Data;
using Customer_Information.WebAPI.Repository;
using Customer_Information.WebAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<CustomerDBContext>(option => {
    option.UseSqlServer(builder.Configuration.GetConnectionString("DataConnectionName"));
});
builder.Services.AddScoped<ICountryRepository, CountryRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomerAddressRepository, CustomerAddressRepository>();
builder.Services.AddAutoMapper(typeof(MappingConfig));//add auto mapper dependency
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
