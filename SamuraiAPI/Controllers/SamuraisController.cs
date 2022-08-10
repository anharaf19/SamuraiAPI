using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SamuraiAPI.Data.DAL;
using SamuraiAPI.Domain;
using SamuraiAPI.DTO;
using SamuraiAPI.Helpers;

namespace SamuraiAPI.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    
    public class SamuraisController : ControllerBase
    {   
        private readonly ISamurai _samuraiDAL;
        private readonly IMapper _mapper;
        public SamuraisController(ISamurai samuraiDAL, IMapper mapper)
        {
            _samuraiDAL = samuraiDAL;
            _mapper = mapper;
        }

        
        [HttpGet]
        public async Task<IEnumerable<DefaultSamuraiDTO>> Get()
        {
            // List<SamuraiDTO> samuraiDTO = new List<SamuraiDTO>();
            // var results = await _samuraiDAL.GetAll();
            // foreach (var result in results)
            //{
            //    samuraiDTO.Add(new SamuraiDTO
            //    {
            //        Id = result.Id,
            //        Name = result.Name
            //    });
            //}
            var results = await _samuraiDAL.GetAll();
            var samuraiDTO = _mapper.Map<IEnumerable<DefaultSamuraiDTO>>(results);

            return samuraiDTO;
        }
        [HttpGet("{id}")]
        public async Task<DefaultSamuraiDTO> Get(int id)
        {
            /*SamuraiReadDTO samuraiDTO = new SamuraiReadDTO();
            samuraiDTO.Id = result.Id;
            samuraiDTO.Name = result.Name;*/
            var result = await _samuraiDAL.GetById(id);
            if (result == null) throw new Exception($"data {id} tidak ditemukan");
            var samuraiDTO = _mapper.Map<DefaultSamuraiDTO>(result);

            return samuraiDTO;
        }

        [HttpPost]
        public async Task<ActionResult> Post(DefaultSamuraiDTO samuraiDto)
        {
            try
            {
                var newSamurai = _mapper.Map<Samurai>(samuraiDto);
                var result = await _samuraiDAL.Insert(newSamurai);
                var samuraiReadDto = _mapper.Map<DefaultSamuraiDTO>(result);

                return CreatedAtAction("Get", new { id = result.Id }, samuraiDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult> Put(DefaultSamuraiDTO samuraiDto)
        {
            try
            {
                var updateSamurai = new Samurai
                {
                    Id = samuraiDto.Id,
                    Name = samuraiDto.Name
                };
                var result = await _samuraiDAL.Update(updateSamurai);
                return Ok(samuraiDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _samuraiDAL.Delete(id);
                return Ok($"Data samurai dengan id {id} berhasil didelete");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("ByName")]
        public async Task<IEnumerable<DefaultSamuraiDTO>> GetByName(string name)
        {
            List<DefaultSamuraiDTO> samuraiDtos = new List<DefaultSamuraiDTO>();
            var results = await _samuraiDAL.GetByName(name);
            foreach (var result in results)
            {
                samuraiDtos.Add(new DefaultSamuraiDTO
                {
                    Id = result.Id,
                    Name = result.Name
                });
            }
            return samuraiDtos;
        }

        [HttpPost("AddSamuraiWithSword")]
        public async Task<ActionResult> PostWithSword(SamuraiCreateWithSwordDTO samuraiCreateWithSwordDTO)
        {
            try
            {
                var samuraiSword = _mapper.Map<Samurai>(samuraiCreateWithSwordDTO);
                var result = await _samuraiDAL.AddSamuraiWithSword(samuraiSword);
                var DTO = _mapper.Map<SamuraiReadWithSwordDTO>(result);
                return Ok(DTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[HttpPost("AddSamuraiWithSword")]

        //public async Task<ActionResult> AddSamuraiWithSword(SamuraiCreateWithSwordDTO samuraiCreateWithSwordDTO)
        //{
        //    try
        //    {
        //        var newSamurai = new Samurai();
        //        newSamurai.Name = samuraiCreateWithSwordDTO.Name;


        //        foreach(var sword in samuraiCreateWithSwordDTO.Swords)
        //        {

        //            Sword s = new Sword();
        //            s.SwordName = sword.SwordName;
        //            s.Weight = sword.Weight;

        //            newSamurai.Swords.Add(s);

        //        }
        //        var result = await _samuraiDAL.Insert(newSamurai);
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        [HttpGet("AllSamurai")]
        public async Task<IEnumerable<Samurai>> GetAllSamurai()
        {
            var results = await _samuraiDAL.GetAllSamurai();
            var samuraiDTO = _mapper.Map<IEnumerable<Samurai>>(results);

            return samuraiDTO;
        }

    }
}
