using Client.Containers.State;
using Microsoft.AspNetCore.Components.Forms;

namespace Client.Pages.Card.ViewModels;
public interface ICardVM : IViewModel
{    
    string Title { get; set; }
    int CategoryId { get; set; }
    int Id { get; set; }
    string Image { get; set; }
    bool InMyCollection { get; set; }
    bool IsLoading { get; set; }
    string Name { get; set; }
    int Number { get; set; }

    Task UpsertCard();
    Task LoadCard();
    Task HandleImageUpload(InputFileChangeEventArgs e);
}