using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DevNotepad.Core.Utils
{
    /// <summary>
    /// Класс для парсинга и построения имен переменных в различных конвенциях
    /// </summary>
    public class VariableNameParser
    {
        /// <summary>
        /// Определяет тип конвенции именования переменной
        /// </summary>
        /// <param name="variableName">Имя переменной</param>
        /// <returns>Тип конвенции</returns>
        public ConventionType Determine(string variableName)
        {
            if (string.IsNullOrWhiteSpace(variableName))
                return ConventionType.None;

            // Проверка на snake_case или SCREAMING_SNAKE_CASE
            if (variableName.Contains('_'))
            {
                // Если все буквы в верхнем регистре - SCREAMING_SNAKE_CASE
                if (variableName.All(c => !char.IsLetter(c) || char.IsUpper(c)))
                    return ConventionType.ScreamingSnakeCase;
                
                // Если все буквы в нижнем регистре - snake_case
                if (variableName.All(c => !char.IsLetter(c) || char.IsLower(c)))
                    return ConventionType.SnakeCase;
                
                return ConventionType.None;
            }

            // Проверка на kebab-case
            if (variableName.Contains('-'))
            {
                if (variableName.All(c => !char.IsLetter(c) || char.IsLower(c)))
                    return ConventionType.KebabCase;
                
                return ConventionType.None;
            }

            // Проверка на PascalCase или camelCase
            if (Regex.IsMatch(variableName, @"^[a-zA-Z][a-zA-Z0-9]*$"))
            {
                // Если первая буква заглавная - PascalCase
                if (char.IsUpper(variableName[0]))
                    return ConventionType.PascalCase;
                
                // Если первая буква строчная - camelCase
                if (char.IsLower(variableName[0]))
                    return ConventionType.CamelCase;
            }

            return ConventionType.None;
        }

        /// <summary>
        /// Разбивает имя переменной на токены
        /// </summary>
        /// <param name="variableName">Имя переменной</param>
        /// <param name="conventionType">Тип конвенции (если не указан, будет определен автоматически)</param>
        /// <returns>Массив токенов в нижнем регистре</returns>
        public string[] Parse(string variableName, ConventionType? conventionType = null)
        {
            if (string.IsNullOrWhiteSpace(variableName))
                return Array.Empty<string>();

            var convention = conventionType ?? Determine(variableName);

            switch (convention)
            {
                case ConventionType.SnakeCase:
                case ConventionType.ScreamingSnakeCase:
                    return variableName.Split('_')
                        .Where(s => !string.IsNullOrWhiteSpace(s))
                        .Select(s => s.ToLower())
                        .ToArray();

                case ConventionType.KebabCase:
                    return variableName.Split('-')
                        .Where(s => !string.IsNullOrWhiteSpace(s))
                        .Select(s => s.ToLower())
                        .ToArray();

                case ConventionType.CamelCase:
                case ConventionType.PascalCase:
                    return SplitCamelCase(variableName)
                        .Where(s => !string.IsNullOrWhiteSpace(s))
                        .Select(s => s.ToLower())
                        .ToArray();

                case ConventionType.None:
                default:
                    // Если конвенция не определена, возвращаем как один токен
                    return new[] { variableName.ToLower() };
            }
        }

        /// <summary>
        /// Строит имя переменной из токенов в соответствии с конвенцией
        /// </summary>
        /// <param name="tokens">Массив токенов</param>
        /// <param name="conventionType">Тип конвенции</param>
        /// <returns>Имя переменной</returns>
        public string Build(string[] tokens, ConventionType conventionType)
        {
            if (tokens == null || tokens.Length == 0)
                return string.Empty;

            switch (conventionType)
            {
                case ConventionType.CamelCase:
                    return BuildCamelCase(tokens);

                case ConventionType.PascalCase:
                    return BuildPascalCase(tokens);

                case ConventionType.SnakeCase:
                    return string.Join("_", tokens.Select(t => t.ToLower()));

                case ConventionType.ScreamingSnakeCase:
                    return string.Join("_", tokens.Select(t => t.ToUpper()));

                case ConventionType.KebabCase:
                    return string.Join("-", tokens.Select(t => t.ToLower()));

                case ConventionType.None:
                default:
                    return string.Join("", tokens.Select(t => t.ToLower()));
            }
        }

        /// <summary>
        /// Разбивает строку в camelCase/PascalCase на токены
        /// </summary>
        private string[] SplitCamelCase(string input)
        {
            var tokens = new List<string>();
            var currentToken = new StringBuilder();

            for (int i = 0; i < input.Length; i++)
            {
                char c = input[i];

                // Если встретили заглавную букву и это не первый символ
                if (char.IsUpper(c) && currentToken.Length > 0)
                {
                    // Сохраняем текущий токен
                    tokens.Add(currentToken.ToString());
                    currentToken.Clear();
                }

                currentToken.Append(c);
            }

            // Добавляем последний токен
            if (currentToken.Length > 0)
            {
                tokens.Add(currentToken.ToString());
            }

            return tokens.ToArray();
        }

        /// <summary>
        /// Строит строку в camelCase из токенов
        /// </summary>
        private string BuildCamelCase(string[] tokens)
        {
            var result = new StringBuilder();
            
            for (int i = 0; i < tokens.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(tokens[i]))
                    continue;

                if (i == 0)
                {
                    // Первый токен полностью в нижнем регистре
                    result.Append(tokens[i].ToLower());
                }
                else
                {
                    // Остальные токены с заглавной первой буквы
                    result.Append(Capitalize(tokens[i]));
                }
            }

            return result.ToString();
        }

        /// <summary>
        /// Строит строку в PascalCase из токенов
        /// </summary>
        private string BuildPascalCase(string[] tokens)
        {
            var result = new StringBuilder();
            
            foreach (var token in tokens)
            {
                if (string.IsNullOrWhiteSpace(token))
                    continue;

                // Все токены с заглавной первой буквы
                result.Append(Capitalize(token));
            }

            return result.ToString();
        }

        /// <summary>
        /// Делает первую букву заглавной, остальные - строчными
        /// </summary>
        private string Capitalize(string word)
        {
            if (string.IsNullOrWhiteSpace(word))
                return word;

            word = word.ToLower();
            return char.ToUpper(word[0]) + word.Substring(1);
        }
    }
}
