namespace SimpleBankConsoleApp.Utils;

public static class TextValidation
{
    public static bool MenuSelection(string input)
    {
        return (int.TryParse(input, out var num) && num is >= 1 and <= 6);
    }

    public static bool Name(string input)
    {
        return (input.All(char.IsLetter));
    }
}