namespace Client.Shared.Buttons;

public static class ButtonStyleExtensions
{
    public static string GetStyleCSS(this ButtonStyle button) => button switch
    {
        ButtonStyle.Primary => "button-primary",
        ButtonStyle.Secondary => "button-secondary",
        ButtonStyle.Success => "button-success",
        ButtonStyle.Danger => "button-danger",
        ButtonStyle.Warning => "button-warning",
        ButtonStyle.Info => "button-info",
        ButtonStyle.Light => "button-light",
        ButtonStyle.Dark => "button-dark",
        ButtonStyle.Link => "button-Link",
        _ => throw new ArgumentOutOfRangeException(nameof(button), $"Not expected Style value: {button}"),
    };

    public static string GetOutlineCSS(this ButtonOutline button) => button switch
    {
        ButtonOutline.Primary => "button-outline-primary",
        ButtonOutline.Secondary => "button-outline-secondary",
        ButtonOutline.Success => "button-outline-success",
        ButtonOutline.Danger => "button-outline-danger",
        ButtonOutline.Warning => "button-outline-warning",
        ButtonOutline.Info => "button-outline-info",
        ButtonOutline.Light => "button-outline-light",
        ButtonOutline.Dark => "button-outline-dark",
        _ => throw new ArgumentOutOfRangeException(nameof(button), $"Not expected Style value: {button}"),
    };
    public static string GeRoundedCSS(this ButtonRounded button) => button switch
    {
        ButtonRounded.Primary => "button-rounded-primary",
        ButtonRounded.Secondary => "button-rounded-secondary",
        ButtonRounded.Success => "button-rounded-success",
        ButtonRounded.Danger => "button-rounded-danger",
        ButtonRounded.Warning => "button-rounded-warning",
        ButtonRounded.Info => "button-rounded-info",
        ButtonRounded.Light => "button-rounded-light",
        ButtonRounded.Dark => "button-rounded-dark",
        _ => throw new ArgumentOutOfRangeException(nameof(button), $"Not expected Style value: {button}"),
    };

    public static string GeRoundedOutlineCSS(this ButtonRoundedOutline button) => button switch
    {
        ButtonRoundedOutline.Primary => "button-rounded-outline-primary",
        ButtonRoundedOutline.Secondary => "button-rounded-outline-secondary",
        ButtonRoundedOutline.Success => "button-rounded-outline-success",
        ButtonRoundedOutline.Danger => "button-rounded-outline-danger",
        ButtonRoundedOutline.Warning => "button-rounded-outline-warning",
        ButtonRoundedOutline.Info => "button-rounded-outline-info",
        ButtonRoundedOutline.Light => "button-rounded-outline-light",
        ButtonRoundedOutline.Dark => "button-rounded-outline-dark",
        _ => throw new ArgumentOutOfRangeException(nameof(button), $"Not expected Style value: {button}"),
    };
}
