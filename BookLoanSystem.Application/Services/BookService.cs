public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;
    private readonly ICacheService _cacheService;

    public BookService(IBookRepository bookRepository, ICacheService cacheService)
    {
        _bookRepository = bookRepository;
        _cacheService = cacheService;
    }

    public async Task<int> CreateBookAsync(CreateBookRequest request)
    {
        var book = new Book
        {
            Title = request.Title
        };
        await _bookRepository.AddAsync(book);
        _cacheService.Remove("books_all");
        return book.Id;
    }

    public async Task<BookDto?> GetBookById(int bookId)
    {
        string cacheKey = $"book_{bookId}";
        var cachedBook = _cacheService.Get<BookDto>(cacheKey);
        if (cachedBook != null)
        {
            return cachedBook;
        }

        var book = await _bookRepository.GetBookByBookIdAsync(bookId);
        if (book != null)
        {
            var newBook = new BookDto
            {
                Title = book.Title
            };
            _cacheService.Set(cacheKey, newBook, TimeSpan.FromMinutes(5));
            return newBook;
        }
        return null;
    }

    public async Task<IEnumerable<BookDto>> GetBooksAsync()
    {
        string cacheKey = "books_all";
        var cachedBooks = _cacheService.Get<List<BookDto>>(cacheKey);
        if (cachedBooks != null)
        {
            return cachedBooks;
        }

        var books = await _bookRepository.GetBooksAsync();
        var bookDtos = books.Select(b => new BookDto
        {
            Id = b.Id,
            Title = b.Title
        }).ToList();

        _cacheService.Set(cacheKey, bookDtos, TimeSpan.FromMinutes(5));
        return bookDtos;
    }
}