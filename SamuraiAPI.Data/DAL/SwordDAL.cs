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

        public async Task<SwordElement> AddElementToExistingSword(SwordElement swordElement)
        {
            var s = _context.Swords.FirstOrDefault(s => s.Id == swordElement.SwordsId);
            if (s != null) { 
            _context.SwordElements.Add(swordElement);
            await _context.SaveChangesAsync();
            }
            else
            {
                Console.WriteLine("Error");
            }
            return swordElement;
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

        public async Task DeleteElementFromSword(int id)
        {
            try
            {
                var deleteSword = await _context.SwordElements.Where(s => s.SwordsId == id).ToListAsync();
                if (deleteSword == null)
                    throw new Exception($"Data sword dengan id {id} tidak ditemukan");
                _context.SwordElements.RemoveRange(deleteSword);
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

      

        public async Task<IEnumerable<Sword>> GetSwordWithType(int pageNumber)
        {
            var s = await _context.Swords.Include(s => s.SwordType)
               .OrderBy(s => s.SwordName).AsNoTracking().ToListAsync();


            int numberOfObjectsPerPage = 10;
            var queryResultPage = s
              .Skip(numberOfObjectsPerPage * (pageNumber - 1))
              .Take(numberOfObjectsPerPage);
            return queryResultPage;
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

