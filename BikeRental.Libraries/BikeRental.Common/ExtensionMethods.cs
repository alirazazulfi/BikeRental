namespace BikeRental.Common
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// This is the extension method for string.
        /// The first parameter takes the "this" modifier
        /// and specifies the type for which the method is defined.
        /// This will check if string IsNullOrEmpty or IsNullOrWhiteSpace
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNullEmpty(this string value) => string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value) ? true : false;
    }
}
