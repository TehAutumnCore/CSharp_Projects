using Blog.Models;

namespace Blog.ViewModels;

public class ProjectIndexViewModel
{
    public List<Project> Projects { get; set; } = new();
    public int CurrentPage { get; set; } //current page number
    public int TotalPages { get; set; } //total page numbers 

    public bool HasPreviousPage => CurrentPage > 1; //cant be on first page
    public bool HasNextPage => CurrentPage < TotalPages; //cant be on the last page
}