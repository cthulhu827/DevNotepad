namespace DevNotepad.Core.Utils
{
    /// <summary>
    /// Типы конвенций именования переменных
    /// </summary>
    public enum ConventionType
    {
        /// <summary>
        /// Конвенция не определена
        /// </summary>
        None,
        
        /// <summary>
        /// camelCase - первое слово с маленькой буквы, остальные с большой
        /// </summary>
        CamelCase,
        
        /// <summary>
        /// PascalCase - все слова с большой буквы
        /// </summary>
        PascalCase,
        
        /// <summary>
        /// snake_case - слова разделены подчеркиванием, все буквы маленькие
        /// </summary>
        SnakeCase,
        
        /// <summary>
        /// SCREAMING_SNAKE_CASE - слова разделены подчеркиванием, все буквы большие
        /// </summary>
        ScreamingSnakeCase,
        
        /// <summary>
        /// kebab-case - слова разделены дефисом, все буквы маленькие
        /// </summary>
        KebabCase
    }
}
