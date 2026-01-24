using Microsoft.EntityFrameworkCore;

public class BookRepository : IBookRepository
{
    private readonly BookLoanSystemDbContext _context;

    public BookRepository(BookLoanSystemDbContext context) 
    {
        _context = context;
    }

    public async Task AddAsync(Book book)
    {
        _context.Books.Add(book);
        await _context.SaveChangesAsync();
    }

    public async Task<Book?> GetBookByBookIdAsync(int bookId)
    {
        return await _context.Books.FirstOrDefaultAsync(b => b.Id == bookId);
    }

    public async Task<IEnumerable<Book>> GetBooksAsync()
    {
        return await _context.Books.ToListAsync();
    }

}