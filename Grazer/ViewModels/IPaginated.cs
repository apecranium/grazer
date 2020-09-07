namespace Grazer.ViewModels
{
    public interface IPaginated
    {
        int Page { get; set; }
        int PageCount { get; }
    }
}
