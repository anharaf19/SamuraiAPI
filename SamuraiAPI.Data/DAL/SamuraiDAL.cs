using Microsoft.EntityFrameworkCore;
using SamuraiAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiAPI.Data.DAL
{
    public class SamuraiDAL : ISamurai
    {

        private readonly SamuraiDbContext _context;
        public SamuraiDAL(SamuraiDbContext context)
        {
            _context = context;
        }

        public async Task<Samurai> AddSamuraiWithSword(Samurai obj)
        {
            foreach (var sword in obj.Swords)
            {
                sword.SamuraiId = obj.Id;
            }
            _context.Samurais.Add(obj);
            _context.Swords.AddRange(obj.Swords);
            await _context.SaveChangesAsync();
            return obj;
        }

        public async Task Delete(int id)
        {
            try
            {
                var deleteSamurai = await _context.Samurais.FirstOrDefaultAsync(s => s.Id == id);
                if (deleteSamurai == null)
                    throw new Exception($"Data samurai dengan id {id} tidak ditemukan");
                _context.Samurais.Remove(deleteSamurai);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<IEnumerable<Samurai>> GetAll()
        {
            var results = await _context.Samurais.OrderBy(s => s.Name).ToListAsync();
            return results;
        }

        public async Task<IEnumerable<Samurai>> GetAllSamurai()
        {
            var results = await _context.Samurais.Include(s => s.Swords)
              .ThenInclude(e => e.Elements).Include(t=>t.Swords).ThenInclude(el=>el.SwordType)
              .OrderBy(s => s.Name).AsNoTracking().ToListAsync();


            
            return results;

            //var results = await (from s in _context.Samurais 
            //                     join sw in _context.Swords on s.Id equals sw.SamuraiId
            //                     join st in _context.SwordTypes on sw.Id equals st.SwordId
            //                     join se in _context.SwordElements on sw.Id equals se.SwordsId
            //                     join e in _context.Elements on se.ElementsId equals e.Id orderby s.Name select new {s,sw,st,se,e}).ToListAsync();

            //return results;

        }

        public async Task<Samurai> GetById(int id)
        {
            var result = await _context.Samurais.FirstOrDefaultAsync(s => s.Id == id);
            if (result == null) throw new Exception($"Data samurai dengan id {id} tidak ditemukan");
            return result;
        }

        public async Task<IEnumerable<Samurai>> GetByName(string name)
        {
            var samurais = await _context.Samurais.Where(s => s.Name.Contains(name))
               .OrderBy(s => s.Name).ToListAsync();
            return samurais;
        }

        public async Task<Samurai> Insert(Samurai obj)
        {
            try
            {
                _context.Samurais.Add(obj);
                await _context.SaveChangesAsync();
                return obj;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<Samurai> Update(Samurai obj)
        {
            try
            {
                var updateSamurai = await _context.Samurais.FirstOrDefaultAsync(s => s.Id == obj.Id);
                if (updateSamurai == null)
                    throw new Exception($"Data samurai dengan id {obj.Id} tidak ditemukan");

                updateSamurai.Name = obj.Name;
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
