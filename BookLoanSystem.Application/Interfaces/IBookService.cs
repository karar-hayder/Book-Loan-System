public interface IBookService
{
    Task<int> CreateBookAsync(CreateBookRequest request);
    Task<BookDto?> GetBookById(int bookId);

    Task<IEnumerable<BookDto>> GetBooksAsync();
}