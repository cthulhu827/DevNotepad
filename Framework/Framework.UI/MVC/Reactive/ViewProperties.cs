using System;
using System.Windows.Forms;

namespace Framework.MVC
{
    public class ViewProperties<T> : ModelProperties<T>
    {
        public ViewProperties(object owner)
            : base(owner)
        {
            suppressRaiseChangedInSetter = true;
        }

        public ViewProperties<T> RaiseChangedOn(T change, params TextBox[] textBoxes)
        {
            foreach (var textBox in textBoxes)
            {
                textBox.TextChanged += (s, e) => RaiseChanged(change);
            }

            return this;
        }

        public ViewProperties<T> RaiseChangedOn(T change, params CheckBox[] checkBoxes)
        {
            foreach (var checkBox in checkBoxes)
            {
                checkBox.CheckedChanged += (s, e) => RaiseChanged(change);
            }

            return this;
        }

        public ViewProperties<T> RaiseChangedOn(T change, params RadioButton[] radioButtons)
        {
            foreach (var radioButton in radioButtons)
            {
                radioButton.CheckedChanged += (s, e) => RaiseChanged(change);
            }

            return this;
        }

        public ViewProperties<T> RaiseChangedOn(T change, params ComboBox[] comboBoxes)
        {
            foreach (var comboBox in comboBoxes)
            {
                comboBox.SelectedIndexChanged += (s, e) => RaiseChanged(change);
            }

            return this;
        }

        public ViewProperties<T> RaiseChangedOn(T change, params DateTimePicker[] dateTimePickers)
        {
            foreach (var dateTimePicker in dateTimePickers)
            {
                dateTimePicker.ValueChanged += (s, e) => RaiseChanged(change);
            }

            return this;
        }
    }
}