using Microsoft.EntityFrameworkCore;
using SamuraiAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiAPI.Data.DAL
{
    public class SwordDAL : ISword
    {
        private readonly SamuraiDbContext _context;
        public SwordDAL(SamuraiDbContext context)
        {
            _context = context;
        }

        public async Task<Sword> AddSwordWithType(Sword sword)
        {
            _context.Swords.Add(sword);
            await _context.SaveChangesAsync();
            return sword;
        }

        public async Task Delete(int id)
        {
            try
            {
                var deleteSword = await _context.Swords.FirstOrDefaultAsync(s => s.Id == id);
                if (deleteSword == null)
                    throw new Exception($"Data sword dengan id {id} tidak ditemukan");
                _context.Swords.Remove(deleteSword);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<IEnumerable<Sword>> GetAll()
        {
            var results = await _context.Swords.OrderBy(s => s.Weight).ToListAsync();
            return results;
        }

        public async Task<Sword> GetById(int id)
        {
            var result = await _context.Swords.FirstOrDefaultAsync(s => s.Id == id);
            if (result == null) throw new Exception($"Data sword dengan id {id} tidak ditemukan");
            return result;
        }

        public async Task<IEnumerable<Sword>> GetByName(string name)
        {
            var swords = await _context.Swords.Where(s => s.SwordName.Contains(name))
               .OrderBy(s => s.SwordName).ToListAsync();
            return swords;
        }

        public async Task<Sword> Insert(Sword obj)
        {
            try
            {
                _context.Swords.AddRange(obj);
                await _context.SaveChangesAsync();
                return obj;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<Sword> Update(Sword obj)
        {
            try
            {
                var updateSamurai = await _context.Swords.FirstOrDefaultAsync(s => s.Id == obj.Id);
                if (updateSamurai == null)
                    throw new Exception($"Data sword dengan id {obj.Id} tidak ditemukan");

                updateSamurai.SwordName = obj.SwordName;
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

