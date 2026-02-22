using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.Domain
{
    public static class RulesBuildSyntax
    {
        private static IRule<T> RuleFromRuleSet<T>(IRuleSet<T> ruleSet)
        {
            var rule = ruleSet.LastRule();

            if (rule == null) // если RuleSet пустой, то устанавливаем свойство самого RuleSet'а...
            {
                return ruleSet;
            }
            else // ...иначе - свойство последнего элемента в RuleSet'е
            {
                return rule;
            }
        }

        public static IRuleSet<T> InvalidMessage<T>(this IRuleSet<T> ruleSet, string invalidMessage)
        {
            var rule = RuleFromRuleSet(ruleSet);
            rule.InvalidMessage = invalidMessage;
            return ruleSet;
        }

        public static IRuleSet<T> Id<T>(this IRuleSet<T> ruleSet, int id)
        {
            var rule = RuleFromRuleSet(ruleSet);
            rule.Id = id;
            return ruleSet;
        }

        public static IRuleSet<T> IsAssignedId<T>(this IRuleSet<T> ruleSet, Func<T, int> getValueDelegate)
        {
            new Rule_Check_Value<T, int>(ruleSet, getValueDelegate,
                                         id => EntityUtils.IsAssigned(id) ? RuleState.Valid : RuleState.Invalid);
            return ruleSet;
        }

        public static IRuleSet<T> IsAssignedOrNullId<T>(this IRuleSet<T> ruleSet, Func<T, int> getValueDelegate)
        {
            new Rule_Check_Value<T, int>(ruleSet, getValueDelegate,
                                         id => EntityUtils.IsAssignedOrNull(id) ? RuleState.Valid : RuleState.Invalid);
            return ruleSet;
        }

        public static IRuleSet<T> IsAssignedEntity<T>(this IRuleSet<T> ruleSet, Func<T, Entity> getValueDelegate)
        {
            new Rule_Check_Value<T, Entity>(ruleSet, getValueDelegate,
                                            e => (e == null || EntityUtils.IsUnassigned(e.Id)) ? RuleState.Invalid : RuleState.Valid);
            return ruleSet;
        }

        public static IRuleSet<T> IsNotNull<T>(this IRuleSet<T> ruleSet, Func<T, object> getValueDelegate)
        {
            new Rule_Check_Value<T, object>(ruleSet, getValueDelegate,
                                            e => e == null ? RuleState.Invalid : RuleState.Valid);
            return ruleSet;
        }

        public static IRuleSet<T> IsTrue<T>(this IRuleSet<T> ruleSet, Func<T, bool> getValueDelegate)
        {
            new Rule_Check_Value<T, bool>(ruleSet, getValueDelegate,
                                          res => res ? RuleState.Valid : RuleState.Invalid);
            return ruleSet;
        }

        public static IRuleSet<T> IsFalse<T>(this IRuleSet<T> ruleSet, Func<T, bool> getValueDelegate)
        {
            new Rule_Check_Value<T, bool>(ruleSet, getValueDelegate,
                                          res => res ? RuleState.Invalid : RuleState.Valid);
            return ruleSet;
        }

        public static IRuleSet<T> IsNotZero<T>(this IRuleSet<T> ruleSet, Func<T, int> getValueDelegate)
        {
            new Rule_Check_Value<T, int>(ruleSet, getValueDelegate,
                                         i => i == 0 ? RuleState.Invalid : RuleState.Valid);
            return ruleSet;
        }

        public static IRuleSet<T> IsNotEmptyString<T>(this IRuleSet<T> ruleSet, Func<T, string> getValueDelegate)
        {
            new Rule_Check_Value<T, string>(ruleSet, getValueDelegate,
                                            s => string.IsNullOrEmpty(s) ? RuleState.Invalid : RuleState.Valid);
            return ruleSet;
        }

        public static IRuleSet<T> End_Container<T>(this IRuleSet<T> ruleSet)
        {
            return ruleSet.Owner;
        }

        public static IRuleSet<T> Container_OR<T>(this IRuleSet<T> ruleSet)
        {
            var result = new Rule_Container_OR<T>(ruleSet);
            return result;
        }

        public static IRuleSet<T> Container_AND<T>(this IRuleSet<T> ruleSet)
        {
            var result = new Rule_Container_AND<T>(ruleSet);
            return result;
        }

        private static RuleState CheckEnumerableHasItems<TItem>(IEnumerable<TItem> enumerable)
        {
            if (enumerable == null)
            {
                return RuleState.Invalid;
            }

            if (enumerable.Any())
            {
                return RuleState.Valid;
            }

            return RuleState.Invalid;
        }

        public static IRuleSet<T> HasItems<T, TItem>(this IRuleSet<T> ruleSet, Func<T, IEnumerable<TItem>> getEnumerableDelegate)
        {
            new Rule_Check_Value<T, IEnumerable<TItem>>(ruleSet, getEnumerableDelegate, CheckEnumerableHasItems);
            return ruleSet;
        }

        public static IRuleSet<T> EnumWithRule<T, TItem>(this IRuleSet<T> ruleSet, Func<T, IEnumerable<TItem>> getEnumerableDelegate,
          Action<IRuleSet<TItem>> bindItemRules, bool stopEnumerateOnFail = true)
        {
            new Rule_Check_Enumerable<T, TItem>(ruleSet, getEnumerableDelegate,
                                                bindItemRules, stopEnumerateOnFail);
            return ruleSet;
        }

        public static IRuleSet<T> EnumWithDelegate<T, TItem>(this IRuleSet<T> ruleSet, Func<T, IEnumerable<TItem>> getEnumerableDelegate,
          Func<TItem, RuleState> validateItemDelegate, bool stopEnumerateOnFail = true)
        {
            new Rule_Check_Enumerable<T, TItem>(ruleSet, getEnumerableDelegate,
                                                validateItemDelegate, stopEnumerateOnFail);
            return ruleSet;
        }

        public static IRuleSet<T> EnumWithDelegate<T, TItem>(this IRuleSet<T> ruleSet, Func<T, IEnumerable<TItem>> getEnumerableDelegate,
          Func<T, TItem, RuleState> validateItemDelegate, bool stopEnumerateOnFail = true)
        {
            new Rule_Check_Enumerable<T, TItem>(ruleSet, getEnumerableDelegate,
                                                validateItemDelegate, stopEnumerateOnFail);
            return ruleSet;
        }

        public static IRuleSet<T> CheckEnumerableR<T, TItem>(this IRuleSet<T> ruleSet, Func<T, IEnumerable<TItem>> getEnumerableDelegate,
          Action<IRuleSet<TItem>> bindItemRules, bool stopEnumerateOnFail = true)
        {
            return ruleSet.Container_AND()
                .HasItems(getEnumerableDelegate)
                .EnumWithRule(getEnumerableDelegate, bindItemRules, stopEnumerateOnFail)
              .End_Container();
        }

        public static IRuleSet<T> CheckEnumerableD<T, TItem>(this IRuleSet<T> ruleSet, Func<T, IEnumerable<TItem>> getEnumerableDelegate,
          Func<TItem, RuleState> validateItemDelegate, bool stopEnumerateOnFail = true)
        {
            return ruleSet.Container_AND()
                .HasItems(getEnumerableDelegate)
                .EnumWithDelegate(getEnumerableDelegate, validateItemDelegate, stopEnumerateOnFail)
              .End_Container();
        }

        public static IRuleSet<T> CheckEnumerableD<T, TItem>(this IRuleSet<T> ruleSet, Func<T, IEnumerable<TItem>> getEnumerableDelegate,
          Func<T, TItem, RuleState> validateItemDelegate, bool stopEnumerateOnFail = true)
        {
            return ruleSet.Container_AND()
                .HasItems(getEnumerableDelegate)
                .EnumWithDelegate(getEnumerableDelegate, validateItemDelegate, stopEnumerateOnFail)
              .End_Container();
        }
    }
}
