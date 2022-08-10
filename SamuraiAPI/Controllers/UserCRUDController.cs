using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SamuraiAPI.Data.DAL;
using SamuraiAPI.Domain;
using SamuraiAPI.DTO;
using SamuraiAPI.Helpers;
using UsersAPI.Data.DAL;

namespace SamuraiAPI.Controllers
{

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserCRUDController : ControllerBase
    {
        private readonly IUser _userDAL;
        private readonly IMapper _mapper;
        public UserCRUDController(IUser userDAL, IMapper mapper)
        {
            _userDAL = userDAL;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IEnumerable<DefaultUserDTO>> Get()
        {

            var results = await _userDAL.GetAll();
            var userDTO = _mapper.Map<IEnumerable<DefaultUserDTO>>(results);

            return userDTO;
        }

        [HttpGet("{id}")]
        public async Task<DefaultUserDTO> Get(int id)
        {

            var result = await _userDAL.GetById(id);
            if (result == null) throw new Exception($"data {id} tidak ditemukan");
            var userDTO = _mapper.Map<DefaultUserDTO>(result);

            return userDTO;
        }

        [HttpPost]
        public async Task<ActionResult> Post(DefaultUserDTO userDto)
        {
            try
            {
                var newUser = _mapper.Map<User>(userDto);
                var result = await _userDAL.Insert(newUser);
                var samuraiReadDto = _mapper.Map<DefaultUserDTO>(result);

                return CreatedAtAction("Get", new { id = result.Id }, userDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        public async Task<ActionResult> Put(DefaultUserDTO userDto)
        {
            try
            {
                var updateUser = new User
                {
                    Id = userDto.Id,
                    FirstName = userDto.FirstName,
                    LastName = userDto.LastName,
                    Username = userDto.Username,
                    Password = userDto.Password,

                };
                var result = await _userDAL.Update(updateUser);
                return Ok(userDto);
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
                await _userDAL.Delete(id);
                return Ok($"Data user dengan id {id} berhasil didelete");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
