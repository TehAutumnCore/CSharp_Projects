using Microsoft.EntityFrameworkCore; //to use DbContext
using ExpenseTracker.Models; // required to recognize Exprense

namespace ExpenseTracker.Data //references the directory its in
{
    public class ExpenseTrackerDbContext : DbContext //declares db context class, used to track changes, map models to db tables, perform CRUD operations, and connect to the db
    {
        public DbSet<Expense> Expenses { get; set; } //defines a table in the database, tells EF Core I wan t atable named Expenses, and each row maps to an Expense Object
        
        public ExpenseTrackerDbContext(DbContextOptions<ExpenseTrackerDbContext>options) //Constructor that passes config to the base DbContext to allow dependency Injection(DI) to supply the configuration when the app starts up
            :base(options)
        {
            
        }
    }
}