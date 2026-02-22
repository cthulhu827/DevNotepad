using System;
using System.Linq.Expressions;

namespace Framework.Utils
{
    public static class ExpressionHelper
    {
        /// <summary>
        /// Возвращает имя свойства из переданного лямбда-выражения вида "someObject => someObject.someProperty".
        /// </summary>
        /// <remarks>
        /// В некоторых случаях вызов свойства заворачивается в вызов Convert
        /// (https://devio.wordpress.com/2010/07/27/get-property-name-as-string-value/
        /// https://long2know.com/2015/07/eliminating-string-literals-in-net/),
        /// учитываем эти случаи.
        /// </remarks>
        public static string GetPropertyName<T, TResult>(Expression<Func<T, TResult>> expr)
        {
            var body = expr.Body as MemberExpression ?? (MemberExpression)((UnaryExpression)expr.Body).Operand;
            return body.Member.Name;
        }

        /// <summary>
        /// Метод для удобства вызова <see cref="GetPropertyName{T,TResult}"/>
        /// </summary>
        /// <remarks>
        /// Использование: объявить поле <see cref="IExpressionSyntax{T}"/> от нужного типа,
        /// (можно не инстанцировать), назвать "properties" и вызывать на нём "properties._(x => x.Name)"
        /// </remarks>
        public static string _<T>(this IExpressionSyntax<T> syntax, Expression<Func<T, object>> expression)
        {
            return GetPropertyName(expression);
        }
    }
}