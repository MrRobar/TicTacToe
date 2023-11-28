public static class NumberToSymbol
{
    public static string GetSymbol(int digit)
    {
        return digit == 1 ? "X" : "O";
    }
}