using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class BookController : ControllerBase
{
    private readonly IBookService _bookService;

    public BookController(IBookService bookService)
    {
        _bookService = bookService;
    }


    [HttpGet()]
    public async Task<ActionResult<IEnumerable<BookDto>>> Get()
    {
        var books = await _bookService.GetBooksAsync();
        return Ok(books);
    }

    [HttpPost()]
    public async Task<ActionResult<BookDto>> CreateBook([FromBody] CreateBookRequest request) 
    {
        await _bookService.CreateBookAsync(request);
        return Created();
    }

    [HttpGet("{bookId:int}")]
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