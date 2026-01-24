public interface IBookRepository
{
    Task AddAsync(Book book);
    Task<Book?> GetBookByBookIdAsync(int bookId);

    Task<IEnumerable<Book>> GetBooksAsync();
}