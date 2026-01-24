public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;

    public BookService(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<int> CreateBookAsync(CreateBookRequest request)
    {
        var book = new Book
        {
            Title = request.Title
        };
        await _bookRepository.AddAsync(book);
        return book.Id;
    }

    public async Task<BookDto?> GetBookById(int bookId)
    {
        var book = await _bookRepository.GetBookByBookIdAsync(bookId);
        if (book != null)
        {
            var newBook = new BookDto
            {
                Title = book.Title
            };
            return newBook;
        }
        return null;
    }
    
    public async Task<IEnumerable<BookDto>> GetBooksAsync() 
    {
        var books = await _bookRepository.GetBooksAsync();
        return books.Select(b => new BookDto
        {
            Id = b.Id,
            Title= b.Title
        }).ToList();
    }

}