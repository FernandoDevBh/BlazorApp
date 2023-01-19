﻿using Client.Containers.State;
using Microsoft.AspNetCore.Components.Forms;

namespace Client.Components.Category.ViewModels;

public interface ICategoryViewModel : IViewModel
{
    int Id { get; set; }
    bool IsLoading { get; set; }
    string Name { get; set; }
    string Symbol { get; }
    string Title { get; set; }

    Task LoadCategory();
    Task UpsertCategory();
    Task HandleImageUpload(InputFileChangeEventArgs e);
}