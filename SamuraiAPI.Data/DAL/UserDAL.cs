using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SamuraiAPI.Domain;
using SamuraiAPI.Data;
using SamuraiAPI.Data.DAL;

namespace UsersAPI.Data.DAL
{
    public class UserDAL : IUser
    {
        private readonly SamuraiDbContext _context;
        public UserDAL(SamuraiDbContext context)
        {
            _context = context;
        }
        public async Task Delete(int id)
        {
            try
            {
                var deleteUser = await _context.Users.FirstOrDefaultAsync(s => s.Id == id);
                if (deleteUser == null)
                    throw new Exception($"Data user dengan id {id} tidak ditemukan");
                _context.Users.Remove(deleteUser);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            var results = await _context.Users.OrderBy(s => s.FirstName).ToListAsync();
            return results;
        }

        public async Task<User> GetById(int id)
        {
            var result = await _context.Users.FirstOrDefaultAsync(s => s.Id == id);
            if (result == null) throw new Exception($"Data user dengan id {id} tidak ditemukan");
            return result;
        }

        public async Task<User> Insert(User obj)
        {
            try
            {
                _context.Users.Add(obj);
                await _context.SaveChangesAsync();
                return obj;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<User> Update(User obj)
        {
            try
            {
                var updateUsers = await _context.Users.FirstOrDefaultAsync(s => s.Id == obj.Id);
                if (updateUsers == null)
                    throw new Exception($"Data sword dengan id {obj.Id} tidak ditemukan");

                updateUsers.FirstName = obj.FirstName;
                updateUsers.LastName = obj.LastName;
                updateUsers.Username = obj.Username;
                updateUsers.Password = obj.Password;
                await _context.SaveChangesAsync();
                return obj;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
