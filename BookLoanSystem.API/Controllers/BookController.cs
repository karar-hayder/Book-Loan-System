using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("[controller]")]
[Authorize]
public class BookController : ControllerBase
{
    private readonly IBookService _bookService;

    public BookController(IBookService bookService)
    {
        _bookService = bookService;
    }


    [HttpGet()]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<BookDto>>> Get()
    {
        var books = await _bookService.GetBooksAsync();
        return Ok(books);
    }

    [HttpPost()]
    [Authorize(Policy = "AdminOnly")]
    public async Task<ActionResult<BookDto>> CreateBook([FromBody] CreateBookRequest request)
    {
        await _bookService.CreateBookAsync(request);
        return Created();
    }

    [HttpGet("{bookId:int}")]
    [AllowAnonymous]
    public async Task<ActionResult<BookDto>> GetBookById(int bookId)
    {
        var book = await _bookService.GetBookById(bookId);
        if (book == null)
        {
            return NotFound();
        }
        return Ok(book);
    }
}