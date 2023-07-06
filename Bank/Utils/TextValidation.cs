namespace SimpleBankConsoleApp.Utils;

public static class TextValidation
{
    public static bool MenuSelection(string? input) => !string.IsNullOrEmpty(input) && int.TryParse(input, out var num) && num is >= 1 and <= 6;

    public static bool Name(string? input) => !string.IsNullOrEmpty(input) && input.All(char.IsLetter);
    
}