using Microsoft.EntityFrameworkCore;
using SamuraiAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiAPI.Data.DAL
{
    public class ElementDAL : IElement
    {
        private readonly SamuraiDbContext _context;
        public ElementDAL(SamuraiDbContext context)
        {
            _context = context;
        }

        public async Task<SwordElement> AddSwordToExistingElement(SwordElement swordElement)
        {
            _context.SwordElements.Add(swordElement);
            await _context.SaveChangesAsync();
            return swordElement;
        }

        public async Task Delete(int id)
        {
            try
            {
                var deleteSword = await _context.Elements.FirstOrDefaultAsync(s => s.Id == id);
                if (deleteSword == null)
                    throw new Exception($"Data element dengan id {id} tidak ditemukan");
                _context.Elements.Remove(deleteSword);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<IEnumerable<Element>> GetAll()
        {
            var results = await _context.Elements.OrderBy(s => s.ElementName).ToListAsync();
            return results;
        }

        public async Task<Element> GetById(int id)
        {
            var result = await _context.Elements.FirstOrDefaultAsync(s => s.Id == id);
            if (result == null) throw new Exception($"Data element dengan id {id} tidak ditemukan");
            return result;
        }

        public async Task<IEnumerable<Element>> GetByName(string name)
        {
            var elements = await _context.Elements.Where(s => s.ElementName.Contains(name))
              .OrderBy(s => s.ElementName).ToListAsync();
            return elements;
        }

        public async Task<Element> Insert(Element obj)
        {

            try
            {
                _context.Elements.Add(obj);
                await _context.SaveChangesAsync();
                return obj;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<Element> Update(Element obj)
        {
            try
            {
                var updateSamurai = await _context.Elements.FirstOrDefaultAsync(s => s.Id == obj.Id);
                if (updateSamurai == null)
                    throw new Exception($"Data element dengan id {obj.Id} tidak ditemukan");

                updateSamurai.ElementName = obj.ElementName;
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
