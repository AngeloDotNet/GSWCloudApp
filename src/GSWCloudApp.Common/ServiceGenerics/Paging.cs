namespace GSWCloudApp.Common.ServiceGenerics;

/// <summary>
/// Represents a paginated list of items.
/// </summary>
/// <typeparam name="T">The type of the items.</typeparam>
public class Paging<T>
{
    /// <summary>
    /// Gets or sets the total number of items.
    /// </summary>
    public int TotalItems { get; set; }

    /// <summary>
    /// Gets or sets the size of the page.
    /// </summary>
    public int PageSize { get; set; }

    /// <summary>
    /// Gets or sets the current page number.
    /// </summary>
    public int CurrentPage { get; set; }

    /// <summary>
    /// Gets the total number of pages.
    /// </summary>
    public int TotalPages => (int)Math.Ceiling(TotalItems / (double)PageSize);

    /// <summary>
    /// Gets or sets the list of items.
    /// </summary>
    public List<T> Items { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Paging{T}"/> class.
    /// </summary>
    public Paging()
    {
        Items = [];
    }
}
