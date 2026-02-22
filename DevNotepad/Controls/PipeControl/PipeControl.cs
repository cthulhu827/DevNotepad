using System.ComponentModel;
using DevNotepad.Core.TextTransformers;
using Framework.AppInfrastructure;
using Framework.MVC;

namespace DevNotepad.Controls.PipeControl
{
    public partial class PipeControl : UserControl
    {
        private int selectedIndex = -1;

        private IDataSource<VM_PipeItem> dataSource = DataSourceFactory.CreateNull<VM_PipeItem>();

        private Control? prevFocusedControl;

        public PipeControl()
        {
            InitializeComponent();
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IDataSource<VM_PipeItem>? DataSource
        {
            get => dataSource;
            set
            {
                StopListeningDataSource();
                dataSource = value ?? DataSourceFactory.CreateNull<VM_PipeItem>();
                StartListeningDataSource();
                SelectedIndex = dataSource.Count - 1;
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectedIndex
        {
            get => selectedIndex;
            set
            {
                selectedIndex = value;
                Invalidate();
                OnSelectedIndexChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public event EventHandler? OnSelectedIndexChanged;

        public event EventHandler<PipeInsertItemEventArgs>? OnInsertItem;

        public event EventHandler<PipeEditItemEventArgs>? OnEditItem;

        public event EventHandler? OnDeleteItem;

        public void Edit(Control? focusedControl)
        {
            if (Focused) return;

            prevFocusedControl = focusedControl;
            Focus();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            var g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            /*var txt = "The very long ii";
            var txtWidth = (int)g.MeasureString(txt, UI.Font14, new PointF(0,0), StringFormat.GenericTypographic).Width;
            g.DrawString($"{txt} {txtWidth}", UI.Font14, Brushes.White, 10, 10, StringFormat.GenericTypographic);
            g.FillRectangle(Brushes.White, new Rectangle(10, 40, txtWidth, 10));

            return;*/

            int paddingY = 2;
            int paddingX = 4;
            int itemHeight = PipeItemColors.IconSize + 2 + paddingY * 2;
            int rightArrowWidth = itemHeight / 2;

            int x = paddingX * 2;
            int y = (Height - itemHeight) / 2;

            for (int i = 0; i < dataSource.Count; i++)
            {
                bool isFirst = i == 0;
                int leftArrowWidth = isFirst ? 0 : rightArrowWidth;

                var item = dataSource[i];

                var colors = i switch
                {
                    _ when i == selectedIndex => Focused ? PipeItemColors.Selected : PipeItemColors.Active,
                    _ when i < selectedIndex => PipeItemColors.Active,
                    _ => PipeItemColors.Inactive
                };

                var itemText = item.Text;
                int iconCount = 0;
                var parametrized = false;
                if (item.Transformer is IParametrizedTextTransformer parmetrizedTransformer)
                {
                    iconCount++;
                    parametrized = true;
                }

                var combined = false;
                if (item.Transformer is ICombinedTextTransformer)
                {
                    iconCount++;
                    combined = true;
                }


                var textSize = g.MeasureString(itemText, Font, new PointF(0, 0), StringFormat.GenericTypographic);
                var textWidth = (int)textSize.Width;
                int rectangleWidth = textWidth + 2 * paddingX + iconCount * (paddingX + PipeItemColors.IconSize);
                // Если рисуем иконки, то после них padding добавлять не нужно, т.к. у самих иконок справа тоже
                // есть пустое место, которое сыграет роль padding'а
                if (iconCount != 0) rectangleWidth -= paddingX;

                var points = isFirst
                    ? new Point[]
                    {
                        new(x, y),
                        new(x + rectangleWidth, y),
                        new(x + rectangleWidth + rightArrowWidth, y + itemHeight / 2),
                        new(x + rectangleWidth, y + itemHeight),
                        new(x, y + itemHeight)
                    }
                    : new Point[]
                    {
                        new(x, y),
                        new(x + leftArrowWidth + rectangleWidth, y),
                        new(x + leftArrowWidth + rectangleWidth + rightArrowWidth, y + itemHeight / 2),
                        new(x + leftArrowWidth + rectangleWidth, y + itemHeight),
                        new(x, y + itemHeight),
                        new(x + leftArrowWidth, y + itemHeight / 2)
                    };

                g.FillPolygon(colors.Bg, points);
                g.DrawPolygon(Focused ? PipeItemColors.PenEdit : PipeItemColors.PenView, points);

                var textX = x + leftArrowWidth + paddingX;
                var textY = y + (itemHeight - textSize.Height) / 2 - 2; // без -2 текст уезжает слишком вниз относительно иконки

                g.DrawString(itemText, Font, colors.Fore, textX, textY, StringFormat.GenericTypographic);

                int iconX = textX + textWidth + paddingX;

                // Рисуем иконку перед текстом, если есть ImageList
                if (parametrized)
                {
                    int iconY = y + (itemHeight - PipeItemColors.IconSize) / 2;
                    g.DrawImage(colors.ParamsIcon, new Point(iconX, iconY));
                    iconX += PipeItemColors.IconSize + paddingX;
                }

                if (combined)
                {
                    int iconY = y + (itemHeight - PipeItemColors.IconSize) / 2;
                    g.DrawImage(colors.ComposeIcon, new Point(iconX, iconY));
                }

                x += leftArrowWidth + rectangleWidth;
            }
        }

        protected override bool IsInputKey(Keys keyData)
        {
            return keyData switch
            {
                Keys.Left => true,
                Keys.Right => true,
                _ => base.IsInputKey(keyData)
            };
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            Invalidate();
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            Invalidate();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            switch (e)
            {
                case { KeyCode: Keys.Left, Modifiers: Keys.None }:
                {
                    var newPos = selectedIndex - 1;
                    if (newPos < 0) newPos = 0;
                    SelectedIndex = newPos;
                    break;
                }
                case { KeyCode: Keys.Right, Modifiers: Keys.None }:
                {
                    var newPos = selectedIndex + 1;
                    if (newPos > dataSource.Count - 1) newPos = dataSource.Count - 1;
                    SelectedIndex = newPos;
                    break;
                }
                case { KeyCode: Keys.Home, Modifiers: Keys.None }:
                    SelectedIndex = 0;
                    break;
                case { KeyCode: Keys.End, Modifiers: Keys.None }:
                    SelectedIndex = dataSource.Count - 1;
                    break;
                case { KeyCode: Keys.Left, Control: true }:
                    OnInsertItem?.Invoke(this, new PipeInsertItemEventArgs(true));
                    break;
                case { KeyCode: Keys.Right, Control: true }:
                    OnInsertItem?.Invoke(this, new PipeInsertItemEventArgs(false));
                    break;
                case { KeyCode: Keys.Return, Modifiers: Keys.None }:
                    FocusOut();
                    break;
                case { KeyCode: Keys.Return, Modifiers: Keys.Alt }:
                    OnEditItem?.Invoke(this, new PipeEditItemEventArgs(false));
                    break;
                case { KeyCode: Keys.Return, Modifiers: Keys.Alt | Keys.Shift }:
                    OnEditItem?.Invoke(this, new PipeEditItemEventArgs(true));
                    break;
                case { KeyCode: Keys.Delete }:
                    OnDeleteItem?.Invoke(this, EventArgs.Empty);
                    break;
            }

            base.OnKeyDown(e);
        }

        private void FocusOut()
        {
            if (prevFocusedControl == null)
                Parent?.SelectNextControl(this, false, true, true, true);
            else
            {
                prevFocusedControl.Focus();
                prevFocusedControl = null;
            }
        }

        private void StartListeningDataSource()
        {
            dataSource.MessageBus.Sign<NEListChanged>(On_NEListChanged);
            dataSource.MessageBus.Sign<NEListChangedContents>(On_NEListChangedContents);
            dataSource.MessageBus.Sign<NEListClear>(On_NEListClear);
            dataSource.MessageBus.Sign<NEListItemHighlight>(On_NEListItemHighlight);
        }

        private void StopListeningDataSource()
        {
            dataSource.MessageBus.UnsignObject(this);
        }

        private void On_NEListChanged(NEListChanged evnt)
        {
            Invalidate();
        }

        private void On_NEListChangedContents(NEListChangedContents evnt)
        {
            // do nothing
        }

        private void On_NEListClear(NEListClear evnt)
        {
            // do nothing
        }

        private void On_NEListItemHighlight(NEListItemHighlight evnt)
        {
            // do nothing
        }
    }
}