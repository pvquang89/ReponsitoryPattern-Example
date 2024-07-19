using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReponsitoryPatternExample.Data;
using ReponsitoryPatternExample.Model;
using ReponsitoryPatternExample.Reponsitory;

namespace ReponsitoryPatternExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {

        public readonly IReponsitory<Book> _bookRepository;
        private readonly IMapper _mapper;

        public BooksController(IReponsitory<Book> bookReponsitory, IMapper mapper)
        {
            _bookRepository = bookReponsitory;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            var books = await _bookRepository.GetAll();
            return Ok(books.OrderBy(b=>b.Id));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            var book = await _bookRepository.GetEntityById(id);
            return book == null ? NotFound() : Ok(book);
        }

        [HttpPost]
        public async Task<IActionResult> AddBook(BookModel bookModel)
        {
            //chuyển từ Book sang BookModel để không phải nhập field Author
            var book = _mapper.Map<Book>(bookModel);
            await _bookRepository.AddEntity(book);
            return CreatedAtAction(nameof(GetBookById), new { id = book.Id }, book);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(BookModel bookModel, int id)
        {

            var bookExist = await _bookRepository.GetEntityById(id);
            if (bookExist == null)
                return NotFound($"Not found book with ID : {id}");

            if (id != bookExist.Id)
                return BadRequest("The id entered does not match the id in the json file");

            //var book = _mapper.Map<Book>(bookModel);
            //mapping từ bookModel sang bookExist
            _mapper.Map(bookModel,bookExist);
            await _bookRepository.UpdateEntity(bookExist);
            return Ok("Success Update");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookById(int id)
        {
            await _bookRepository.DeleteEntityById(id);
            return NoContent();
        }
    }
}