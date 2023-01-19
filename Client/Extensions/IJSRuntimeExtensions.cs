using Microsoft.JSInterop;

namespace Client.Extensions;

public static class IJSRuntimeExtensions
{
    public static async ValueTask ToastrSuccess(this IJSRuntime js, string message)
    {
        await js.InvokeVoidAsync("ShowToastr", "success", message);
    }

    public static async ValueTask ToastrError(this IJSRuntime js, string message)
    {
        await js.InvokeVoidAsync("ShowToastr", "error", message);
    }

    public static async ValueTask SweetAlertSuccess(this IJSRuntime js, string title, string message)
    {
        await js.InvokeVoidAsync("SweetAlert", "success", title, message);
    }

    public static async ValueTask SweetAlertError(this IJSRuntime js, string title, string message)
    {
        await js.InvokeVoidAsync("SweetAlert", "error", title, message);
    }
}
