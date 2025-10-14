using System;

namespace Core.Lib
{
    //[MenuItem("Auto/Gen/Update Enum Values")]
    // Атрибут для маркировки enum-ов, которые нужно обновить
    [AttributeUsage(AttributeTargets.Enum)]
    public class GenEnumValuesAttribute : Attribute
    {
    }
}