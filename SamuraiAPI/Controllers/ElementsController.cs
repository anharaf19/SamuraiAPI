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
    public class ElementsController : ControllerBase
    {
        private readonly IElement _elementDAL;
        private readonly IMapper _mapper;
        public ElementsController(IElement elementDAL, IMapper mapper)
        {
            _elementDAL = elementDAL;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<DefaultElementDTO>> Get()
        {

            var results = await _elementDAL.GetAll();
            var elementDTO = _mapper.Map<IEnumerable<DefaultElementDTO>>(results);

            return elementDTO;
        }
        [HttpGet("ByName")]
        public async Task<IEnumerable<DefaultElementDTO>> GetByName(string name)
        {
            List<DefaultElementDTO> samuraiDtos = new List<DefaultElementDTO>();
            var results = await _elementDAL.GetByName(name);
            foreach (var result in results)
            {
                samuraiDtos.Add(new DefaultElementDTO
                {
                    Id = result.Id,
                    ElementName = result.ElementName
                });
            }
            return samuraiDtos;
        }
        [HttpGet("{id}")]
        public async Task<DefaultElementDTO> Get(int id)
        {

            var result = await _elementDAL.GetById(id);
            if (result == null) throw new Exception($"data {id} tidak ditemukan");
            var elementDTO = _mapper.Map<DefaultElementDTO>(result);

            return elementDTO;
        }
        [HttpPost]
        public async Task<ActionResult> Post(DefaultElementDTO elementDTO)
        {
            try
            {
                var newElement = _mapper.Map<Element>(elementDTO);
                var result = await _elementDAL.Insert(newElement);
                var swordReadDTO = _mapper.Map<DefaultElementDTO>(result);

                return CreatedAtAction("Get", new { id = result.Id }, elementDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        public async Task<ActionResult> Put(DefaultElementDTO swordDto)
        {
            try
            {
                var updateElement = new Element
                {
                    Id = swordDto.Id,
                    ElementName = swordDto.ElementName,
                    
                };
                var result = await _elementDAL.Update(updateElement);
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
                await _elementDAL.Delete(id);
                return Ok($"Data sword dengan id {id} berhasil didelete");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("AddSwordToExistingElement")]

        public async Task<ActionResult> AddSwordToExistingElement(SwordElementDTO swordElementDTO)
        {
            try
            {
                var swordelement = _mapper.Map<SwordElement>(swordElementDTO);
                var result = await _elementDAL.AddSwordToExistingElement(swordelement);
                var DTO = _mapper.Map<SwordElementDTO>(result);
                return Ok(DTO);



            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
