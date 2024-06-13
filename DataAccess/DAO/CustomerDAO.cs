using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DAO
{
    public class CustomerDAO
    {
        private static CustomerDAO? instance;
        private static readonly object instanceLock = new object();

        public CustomerDAO()
        {

        }

        public static CustomerDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new CustomerDAO();
                    }
                }
                return instance;
            }
        }

        public async Task<IEnumerable<Customer>> GetAll()
        {
            using var db = new FuminiHotelManagementContext();
            return await db.Customers.ToListAsync();
        }

        public Task<Customer?> GetCustomer(int id)
        {
            using var db = new FuminiHotelManagementContext();
            return db.Customers.FirstOrDefaultAsync(c => c.CustomerId == id);
        }

        public async Task<Customer> AddCustomer(Customer customer)
        {
            using var db = new FuminiHotelManagementContext();
            var newCustomer = await db.Customers.AddAsync(customer);
            await db.SaveChangesAsync();
            return newCustomer.Entity;
        }

        public bool DeleteCustomer(int id)
        {
            using var db = new FuminiHotelManagementContext();
            var user = db.Customers.FirstOrDefault(c => c.CustomerId == id);
            if (user != null)
            {
                db.Customers.Remove(user);
                return db.SaveChanges() > 0;
            }

            return false;
        }

        public Customer UpdateCustomer(Customer customer)
        {
            using var db = new FuminiHotelManagementContext();
            db.Customers.Update(customer);
            db.SaveChanges();
            return customer;
        }
    }
}
