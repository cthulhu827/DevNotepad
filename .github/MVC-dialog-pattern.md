# Паттерн MVC для диалогов редактирования трансформеров

В этом проекте диалоговые окна для редактирования параметров трансформеров, реализующих
`IParametrizedTextTransformer`, строятся по единому паттерну MVC. Примеры готовых диалогов
лежат в `DevNotepad/Dialogs/`:

- `TPGrepDlg`
- `TPTrimTextDlg`
- `TPSubstringDlg`
- `TPReplaceDlg`
- `TPPaddingDlg`

Ниже описан сам паттерн и пошаговый чек-лист для добавления нового диалога.

## 1. Составляющие паттерна

Каждый диалог состоит из четырёх частей (плюс файлы вьюхи, которые генерирует WinForms-дизайнер):

| Роль | Имя файла | Наследование / сущность |
| --- | --- | --- |
| Model | `m_TPXxxDlg.cs` | `m_TPBase<TTransformer, p_TPXxxDlg>` |
| View | `v_TPXxxDlg.cs` (+ `.Designer.cs`, `.resx`) | `ModalDialog` (из `Framework.UI`) |
| Controller | `c_TPXxxDlg.cs` | `c_TPBase<m_TPXxxDlg, v_TPXxxDlg>` |
| Presenter (коды изменений) | `p_TPXxxDlg.cs` | обычный `enum` |

Все классы кладутся в отдельную папку `DevNotepad/Dialogs/TPXxxDlg/`
с namespace `DevNotepad.Dialogs.TPXxxDlg`.

## 2. Базовые классы (папка `DevNotepad/Dialogs/TPBase/`)

- `ITPDlgModel` — интерфейс модели: `Init(...)` и `Transformer`.
- `ITPDlgController` — интерфейс контроллера: `bool Edit(...)`.
- `m_TPBase<TTransformer, TEnum>` — базовая модель:
  - хранит `transformer`, `source`, `editSession`;
  - предоставляет `protected readonly EventRaiser<TEnum> eventRaiser`;
  - в `ApplyChanges` вызывает `Transformer.Transform(Source)`, `NotifyChanged(...)` и `EditSession.Changed()`.
- `c_TPBase<TModel, TView>` — базовый контроллер:
  - реализует `Edit(...)` — создаёт модель, вызывает `Init(...)` и `ShowDialog(model)`.
- `TPEditorAttribute` — атрибут, связывающий контроллер с типом трансформера.

Регистрация контроллера происходит автоматически через рефлексию: `m_WorkArea.EditItemIfNeed`
ищет тип с атрибутом `TPEditorAttribute`, у которого `TransformerType` совпадает с типом
редактируемого трансформера (см. `DevNotepad/Controls/WorkArea/m_WorkArea.cs`).

## 3. Чек-лист для нового диалога `TPXxxDlg`

1. **Presenter** — файл `p_TPXxxDlg.cs`. Для каждого редактируемого свойства трансформера
   заводится значение `XxxChanged`:

   ```csharp
   namespace DevNotepad.Dialogs.TPXxxDlg;

   public enum p_TPXxxDlg
   {
       Param1Changed,
       Param2Changed,
   }
   ```

2. **Model** — файл `m_TPXxxDlg.cs`. Для каждого свойства трансформера делается
   свойство-обёртка, где `set` оборачивает присваивание в `eventRaiser.Raise(...)`:

   ```csharp
   using DevNotepad.Core.TextTransformers;
   using DevNotepad.Dialogs.TPBase;

   namespace DevNotepad.Dialogs.TPXxxDlg;

   public class m_TPXxxDlg : m_TPBase<XxxTransformer, p_TPXxxDlg>
   {
       public string Param1
       {
           get => Transformer.Param1;
           set => eventRaiser.Raise(() => Transformer.Param1 = value, p_TPXxxDlg.Param1Changed);
       }
   }
   ```

   Для числовых полей трансформера (`int`) значение в модели хранится строкой и парсится
   с защитой от некорректного ввода, как в `m_TPGrepDlg`:

   ```csharp
   public string GroupSize
   {
       get => Transformer.GroupSize.ToString();
       set => eventRaiser.Raise(
           () => Transformer.GroupSize = int.TryParse(value, out var intValue) ? intValue : 0,
           p_TPXxxDlg.GroupSizeChanged);
   }
   ```

3. **View** — три файла:
   - `v_TPXxxDlg.cs` — наследует `ModalDialog`, в обработчике `Load` задаёт цвета/шрифты
     из `UI.Clr*` / `UI.Font*`;
   - `v_TPXxxDlg.Designer.cs` — сгенерированный дизайнером код контролов (кнопки `btnOK`/
     `btnCancel` берутся из базового `ModalDialog` и лишь перепозиционируются);
   - `v_TPXxxDlg.resx` — стандартный ресурсный файл (можно скопировать из соседнего диалога).

   Доступные контроллеру контролы объявляются `internal`.

4. **Controller** — файл `c_TPXxxDlg.cs`:

   ```csharp
   using DevNotepad.Core.TextTransformers;
   using DevNotepad.Dialogs.TPBase;
   using static DevNotepad.Dialogs.TPXxxDlg.p_TPXxxDlg;

   namespace DevNotepad.Dialogs.TPXxxDlg;

   [TPEditor(typeof(XxxTransformer))]
   public class c_TPXxxDlg : c_TPBase<m_TPXxxDlg, v_TPXxxDlg>
   {
       protected override void DoConnectModel()
       {
           base.DoConnectModel();
           View.Text = Model.Transformer.Caption;

           View.txtParam1.TextChanged += txtParam1_TextChanged;
           // подписка на остальные контролы...

           ApplyModelChanges(null);
       }

       protected override void DoDisconnectModel()
       {
           View.txtParam1.TextChanged -= txtParam1_TextChanged;
           // отписка...

           base.DoDisconnectModel();
       }

       protected override void OnModelChanged(int[] changeCodes)
       {
           ApplyModelChanges(changeCodes.ToEnums<p_TPXxxDlg>());
       }

       private void ApplyModelChanges(p_TPXxxDlg[]? changes)
       {
           changes ??= Enums.Values<p_TPXxxDlg>();

           modelSuppressor.Exec(() =>
           {
               if (changes.Contains(Param1Changed))
                   View.txtParam1.Text = Model.Param1;
               // остальные поля...
           });
       }

       private void txtParam1_TextChanged(object? sender, EventArgs e)
       {
           if (modelSuppressor.Suppress) return;
           Model.Param1 = View.txtParam1.Text;
       }
   }
   ```

## 4. Ключевые правила

- **Направление данных.** Вьюха → модель: обработчики событий контролов пишут в свойства модели.
  Модель → вьюха: `OnModelChanged` → `ApplyModelChanges` обновляет контролы.
- **`modelSuppressor`.** Все записи в контролы внутри `ApplyModelChanges` должны выполняться
  внутри `modelSuppressor.Exec(...)`, чтобы не вызвать рекурсию «контрол → модель → контрол».
  В обработчиках событий контролов обязательна проверка `if (modelSuppressor.Suppress) return;`.
- **Немедленное применение.** Изменения применяются к трансформеру сразу при редактировании,
  поэтому при нажатии ОК ничего дополнительно сохранять не нужно (см. `m_WorkArea.EditItemIfNeed`).
- **`UI.UnfocusCheckBox`.** Если в диалоге есть чекбоксы/радиокнопки, после их изменения фокус
  возвращается на главный `TextBox`, чтобы не терялись горячие клавиши.
