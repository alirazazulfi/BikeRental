using System.ComponentModel;

namespace BikeRental.DTO.Enums
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
            return attribute == null ? value.ToString() : attribute.Description;
        }
        public static string GetTitle(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attribute = Attribute.GetCustomAttribute(field, typeof(TitleAttribute)) as TitleAttribute;
            return attribute == null ? value.ToString() : attribute.Title;
        }
    }

    #region ----- Custom Attributes -----

    /// <summary>
    /// Value Attribute For Enum. 
    /// </summary>
    public class TitleAttribute : Attribute
    {
        public string Title;
        public TitleAttribute(string title) { Title = title; }
    }
    #endregion
}
