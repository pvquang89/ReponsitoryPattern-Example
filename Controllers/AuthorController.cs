using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReponsitoryPatternExample.Data;
using ReponsitoryPatternExample.Model;
using ReponsitoryPatternExample.Reponsitory;

namespace ReponsitoryPatternExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {

        public readonly IReponsitory<Author> _authorRepository;
        private readonly IMapper _mapper;

        public AuthorController(IReponsitory<Author> authorReponsitory, IMapper mapper)
        {
            _authorRepository = authorReponsitory;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllAuthor()
        {
            var a = await _authorRepository.GetAll();
            return Ok(a.OrderBy(a=>a.Id));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthorById(int id)
        {
            var a = await _authorRepository.GetEntityById(id);
            return a == null ? NotFound() : Ok(a);
        }

        [HttpPost]
        public async Task<IActionResult> AddAuthor(AuthorModel authorModel)
        {
            var a = _mapper.Map<Author>(authorModel);
            await _authorRepository.AddEntity(a);
            return CreatedAtAction(nameof(GetAuthorById), new {id=authorModel.Id}, authorModel);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor(int id, AuthorModel authorModel)
        {
            var authorExist = await _authorRepository.GetEntityById(id);
            if(authorExist == null)
            {
                return NotFound($"Not found author with ID : {id}");
            }
            if(id!=authorModel.Id)
            {
                return BadRequest("The id entered does not match the id in the json file");
            }
            //var a = _mapper.Map<Author>(authorModel);
            _mapper.Map(authorModel, authorExist);
            await _authorRepository.UpdateEntity(authorExist);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookById(int id)
        {
            await _authorRepository.DeleteEntityById(id);
            return NoContent();
        }
    }
}
