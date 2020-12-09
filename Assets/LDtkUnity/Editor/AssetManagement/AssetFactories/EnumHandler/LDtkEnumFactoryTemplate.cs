﻿namespace LDtkUnity.Editor
{
    public readonly struct LDtkEnumFactoryTemplate
    {
        public LDtkEnumFactoryTemplate(string enumType, string[] values)
        {
            EnumType = enumType;
            Values = values;
        }

        public readonly string EnumType;
        public readonly string[] Values;
    }
}