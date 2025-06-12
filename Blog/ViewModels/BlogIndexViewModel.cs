namespace Blog.ViewModels;

public class BlogIndexViewModel
{
    public List<Blog.Models.BlogPost> BlogPosts { get; set; } = new(); //creating a new list from the Blog database/models/BlogModel called BlogPosts
    public int CurrentPage { get; set; } //will track the current page
    public int TotalPages { get; set; } //tracks the total amount of pages

/*TL;DR Previous PageLogic
True if we're not on the first page, if we are, shouldnt show a "previous" button
If current page == 2 and TotalPages == 2
    should have a previous button
if current page == 1 and TotalPages ==2
    should NOT have a previous button
*/
    public bool HasPreviousPage => CurrentPage > 1;

/* TL;DR Next Page Logic
//True if we're not on the last page, if we are, shouldn't show a "next" button
if current page == 2 and TotalPages == 2
    Should NOT have a next button
if currentpage == 1 and TotalPages == 2
    should have a next button
*/
    public bool HasNextPage => CurrentPage < TotalPages; 

}