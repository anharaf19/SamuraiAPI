﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SamuraiAPI.Data.DAL;
using SamuraiAPI.Domain;
using SamuraiAPI.DTO;
using SamuraiAPI.Helpers;

namespace SamuraiAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SwordsController : ControllerBase
    {
        private readonly ISword _swordDAL;
        private readonly IMapper _mapper;
        public SwordsController(ISword swordDAL, IMapper mapper)
        {
            _swordDAL = swordDAL;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<DefaultSwordDTO>> Get()
        {
        
            var results = await _swordDAL.GetAll();
            var swordDTO = _mapper.Map<IEnumerable<DefaultSwordDTO>>(results);

            return swordDTO;
        }
        [HttpGet("{id}")]
        public async Task<DefaultSwordDTO> Get(int id)
        {
            
            var result = await _swordDAL.GetById(id);
            if (result == null) throw new Exception($"data {id} tidak ditemukan");
            var swordDTO = _mapper.Map<DefaultSwordDTO>(result);

            return swordDTO;
        }
        [HttpPost]
        public async Task<ActionResult> Post(DefaultSwordDTO swordDTO)
        {
            try
            {
                var newSword = _mapper.Map<Sword>(swordDTO);
                var result = await _swordDAL.Insert(newSword);
                var swordReadDTO = _mapper.Map<DefaultSwordDTO>(result);

                return CreatedAtAction("Get", new { id = result.Id }, swordDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        public async Task<ActionResult> Put(DefaultSwordDTO swordDto)
        {
            try
            {
                var updateSword = new Sword
                {
                    Id = swordDto.Id,
                    SwordName = swordDto.SwordName,
                    SamuraiId = swordDto.SamuraiId,
                    Weight = swordDto.Weight
                };
                var result = await _swordDAL.Update(updateSword);
                return Ok(swordDto);
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
                await _swordDAL.Delete(id);
                return Ok($"Data sword dengan id {id} berhasil didelete");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddSwordWithType")]

        public async Task<ActionResult> AddSwordWithType(SwordCreateWithTypeDTO swordCreateWithTypeDTO)
        {
            try
            {
                var swordType = _mapper.Map<Sword>(swordCreateWithTypeDTO);
                var result = await _swordDAL.AddSwordWithType(swordType);
                var DTO = _mapper.Map<SwordReadWithTypeDTO>(result);
                return Ok(DTO);



            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpGet("GetSwordWithType")]
        public async Task<IEnumerable<SwordReadWithTypeDTO>> GetSwordWithType(int pageNumber)
        {

            var results = await _swordDAL.GetSwordWithType(pageNumber);
            var swordDTO = _mapper.Map<IEnumerable<SwordReadWithTypeDTO>>(results);

            return swordDTO;
        }

        [HttpPost("AddElementToExistingSword")]

        public async Task<ActionResult> AddElementToExistingSword(SwordElementDTO swordElementDTO)
        {
            try
            {
                   

                    var swordelement = _mapper.Map<SwordElement>(swordElementDTO);
                    var result = await _swordDAL.AddElementToExistingSword(swordelement);
                    var DTO = _mapper.Map<SwordElementDTO>(result);
                    return Ok(DTO);


            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpDelete("DeleteElementFromSword/{id}")]
        public async Task<ActionResult> DeleteElementFromSword(int id)
        {
            try
            {
                await _swordDAL.DeleteElementFromSword(id);
                return Ok($"Data sword dengan id {id} berhasil didelete");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("ByName")]
        public async Task<IEnumerable<DefaultSwordDTO>> GetByName(string name)
        {
            List<DefaultSwordDTO> samuraiDtos = new List<DefaultSwordDTO>();
            var results = await _swordDAL.GetByName(name);
            foreach (var result in results)
            {
                samuraiDtos.Add(new DefaultSwordDTO
                {
                    Id = result.Id,
                    SwordName = result.SwordName,
                    Weight = result.Weight
                    
                });
            }
            return samuraiDtos;
        }
    }
}
