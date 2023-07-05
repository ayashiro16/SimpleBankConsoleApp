namespace SimpleBankConsoleApp.Utils;

public static class TextValidation
{
    public static bool MenuSelection(string? input)
    {
        return (input is not null && Int32.TryParse(input, out int num) && num is >= 1 and <= 6);
    }

    public static bool Name(string? input)
    {
        return (input is not null && input.All(Char.IsLetter));
    }

    public static bool MoneyAmount(string? input)
    {
        return input is not null && Decimal.TryParse(input, out Decimal amount) && amount > 0;
    }
}