namespace Service.Utils;

public static class EnumValidator
{
    public static bool IsValidEnum(string target, Type enumType)
    {
        string[] types = Enum.GetNames(enumType);
        foreach (string type in types)
        {
            if (type.Equals(target))
                return true;
        }
        return false;
    }

    public static void EnsureValidEnum(string target, Type enumType)
    {
        if (IsValidEnum(target, enumType))
            return;
        string[] statues = Enum.GetNames(enumType);
        string text = "";
        for (int index = 0; index < statues.Length; ++index)
        {
            text += statues[index];
            if (index < statues.Length - 1)
                text += ", ";
        }
        throw new Exception($"Either {nameof(enumType)} is not a enum or {target} is not from {nameof(enumType)}");
    }

}

