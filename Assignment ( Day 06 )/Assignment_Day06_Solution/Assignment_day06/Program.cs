using Assignment_day06.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Courses}/{action=Index}/{id?}");

using (var scope = app.Services.CreateScope())
{
    var ctx = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    if (!ctx.Students.Any())
    {
        ctx.Students.Add(new Assignment_day06.Models.Student { FullName = "Ali Ahmed" });
        ctx.Students.Add(new Assignment_day06.Models.Student { FullName = "Mona Ali" });

        ctx.Courses.Add(new Assignment_day06.Models.Course { Name = "Math", Credits = 3, Description = "Basics" });
        ctx.Courses.Add(new Assignment_day06.Models.Course { Name = "Programming", Credits = 4, Description = "C# course" });

        ctx.SaveChanges();

        ctx.Enrollments.Add(new Assignment_day06.Models.Enrollment { StudentId = 1, CourseId = 1, Degree = 78 });
        ctx.Enrollments.Add(new Assignment_day06.Models.Enrollment { StudentId = 2, CourseId = 2, Degree = 45 });

        ctx.SaveChanges();
    }
}


app.Run();
