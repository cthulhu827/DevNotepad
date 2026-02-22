using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.Domain
{
    class Rule_Check_Enumerable<T, TItem> : AbstractRule<T>
    {
        private readonly Func<T, IEnumerable<TItem>> getEnumerableDelegate;
        private readonly Action<IRuleSet<TItem>> bindItemRules;
        private readonly Func<TItem, RuleState> validateItemDelegate;
        private readonly Func<T, TItem, RuleState> validateItemDelegate2;
        private readonly bool stopEnumerateOnFail;

        public Rule_Check_Enumerable(IRuleSet<T> owner, Func<T, IEnumerable<TItem>> getEnumerableDelegate,
          Action<IRuleSet<TItem>> bindItemRules, bool stopEnumerateOnFail)
            : base(owner)
        {
            this.getEnumerableDelegate = getEnumerableDelegate;
            this.bindItemRules = bindItemRules;
            this.stopEnumerateOnFail = stopEnumerateOnFail;
        }

        public Rule_Check_Enumerable(IRuleSet<T> owner, Func<T, IEnumerable<TItem>> getEnumerableDelegate,
          Func<TItem, RuleState> validateItemDelegate, bool stopEnumerateOnFail)
            : base(owner)
        {
            this.getEnumerableDelegate = getEnumerableDelegate;
            this.validateItemDelegate = validateItemDelegate;
            this.stopEnumerateOnFail = stopEnumerateOnFail;
        }

        public Rule_Check_Enumerable(IRuleSet<T> owner, Func<T, IEnumerable<TItem>> getEnumerableDelegate,
          Func<T, TItem, RuleState> validateItemDelegate, bool stopEnumerateOnFail)
            : base(owner)
        {
            this.getEnumerableDelegate = getEnumerableDelegate;
            validateItemDelegate2 = validateItemDelegate;
            this.stopEnumerateOnFail = stopEnumerateOnFail;
        }

        public override RuleState Validate(T objectToValidate, IRuleSet<T> brokenRules)
        {
            var lst = getEnumerableDelegate(objectToValidate);

            var itemRules = Rules.CreateRuleList<TItem>();
            if (bindItemRules != null)
                bindItemRules(itemRules);
            else
                if (validateItemDelegate != null)
                    itemRules.IsTrue(item => validateItemDelegate(item).IsValid());
                else
                    if (validateItemDelegate2 != null)
                        itemRules.IsTrue(item => validateItemDelegate2(objectToValidate, item).IsValid());

            var result = RuleState.Valid;
            var dummy = Rules.CreateBrokenRules<TItem>();
            foreach (var item in lst)
            {
                itemRules.Validate(item, dummy);
                if (itemRules.State < result) result = itemRules.State;

                if (stopEnumerateOnFail && result.IsNotValid()) break;
            }

            State = result;
            if ((brokenRules != null) && (State.IsNotValid())) brokenRules.Add(this);

            return State;
        }
    }
}
