namespace DevNotepad.Dialogs.TPSubstringDlg
{
    partial class v_TPSubstringDlg
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txtLimit = new TextBox();
            lblLimit = new Label();
            lblType = new Label();
            rbSkipStart = new RadioButton();
            rbSkipEnd = new RadioButton();
            rbTakeStart = new RadioButton();
            rbTakeEnd = new RadioButton();
            rbTakeBefore = new RadioButton();
            rbTakeAfter = new RadioButton();
            rbBySelection = new RadioButton();
            SuspendLayout();
            // 
            // btnOK
            // 
            btnOK.Location = new Point(109, 223);
            btnOK.Margin = new Padding(6, 5, 6, 5);
            btnOK.Size = new Size(146, 45);
            btnOK.TabIndex = 7;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(263, 223);
            btnCancel.Margin = new Padding(6, 5, 6, 5);
            btnCancel.Size = new Size(146, 45);
            btnCancel.TabIndex = 8;
            // 
            // txtLimit
            // 
            txtLimit.Location = new Point(88, 8);
            txtLimit.Name = "txtLimit";
            txtLimit.Size = new Size(320, 32);
            txtLimit.TabIndex = 1;
            // 
            // lblLimit
            // 
            lblLimit.AutoSize = true;
            lblLimit.Location = new Point(8, 11);
            lblLimit.Name = "lblLimit";
            lblLimit.Size = new Size(57, 25);
            lblLimit.TabIndex = 0;
            lblLimit.Text = "&Limit:";
            // 
            // lblType
            // 
            lblType.AutoSize = true;
            lblType.Location = new Point(8, 56);
            lblType.Name = "lblType";
            lblType.Size = new Size(55, 25);
            lblType.TabIndex = 2;
            lblType.Text = "Type:";
            // 
            // rbSkipStart
            // 
            rbSkipStart.AutoSize = true;
            rbSkipStart.Location = new Point(88, 56);
            rbSkipStart.Name = "rbSkipStart";
            rbSkipStart.Size = new Size(134, 29);
            rbSkipStart.TabIndex = 3;
            rbSkipStart.TabStop = true;
            rbSkipStart.Text = "Skip start (&1)";
            rbSkipStart.UseVisualStyleBackColor = true;
            // 
            // rbSkipEnd
            // 
            rbSkipEnd.AutoSize = true;
            rbSkipEnd.Location = new Point(264, 56);
            rbSkipEnd.Name = "rbSkipEnd";
            rbSkipEnd.Size = new Size(129, 29);
            rbSkipEnd.TabIndex = 4;
            rbSkipEnd.TabStop = true;
            rbSkipEnd.Text = "Skip end (&2)";
            rbSkipEnd.UseVisualStyleBackColor = true;
            // 
            // rbTakeStart
            // 
            rbTakeStart.AutoSize = true;
            rbTakeStart.Location = new Point(88, 96);
            rbTakeStart.Name = "rbTakeStart";
            rbTakeStart.Size = new Size(136, 29);
            rbTakeStart.TabIndex = 5;
            rbTakeStart.TabStop = true;
            rbTakeStart.Text = "Take start (&3)";
            rbTakeStart.UseVisualStyleBackColor = true;
            // 
            // rbTakeEnd
            // 
            rbTakeEnd.AutoSize = true;
            rbTakeEnd.Location = new Point(264, 96);
            rbTakeEnd.Name = "rbTakeEnd";
            rbTakeEnd.Size = new Size(131, 29);
            rbTakeEnd.TabIndex = 6;
            rbTakeEnd.TabStop = true;
            rbTakeEnd.Text = "Take end (&4)";
            rbTakeEnd.UseVisualStyleBackColor = true;
            // 
            // rbTakeBefore
            // 
            rbTakeBefore.AutoSize = true;
            rbTakeBefore.Location = new Point(88, 136);
            rbTakeBefore.Name = "rbTakeBefore";
            rbTakeBefore.Size = new Size(154, 29);
            rbTakeBefore.TabIndex = 9;
            rbTakeBefore.TabStop = true;
            rbTakeBefore.Text = "Take before (&5)";
            rbTakeBefore.UseVisualStyleBackColor = true;
            // 
            // rbTakeAfter
            // 
            rbTakeAfter.AutoSize = true;
            rbTakeAfter.Location = new Point(264, 136);
            rbTakeAfter.Name = "rbTakeAfter";
            rbTakeAfter.Size = new Size(138, 29);
            rbTakeAfter.TabIndex = 10;
            rbTakeAfter.TabStop = true;
            rbTakeAfter.Text = "Take after (&6)";
            rbTakeAfter.UseVisualStyleBackColor = true;
            // 
            // rbBySelection
            // 
            rbBySelection.AutoSize = true;
            rbBySelection.Location = new Point(88, 176);
            rbBySelection.Name = "rbBySelection";
            rbBySelection.Size = new Size(157, 29);
            rbBySelection.TabIndex = 11;
            rbBySelection.TabStop = true;
            rbBySelection.Text = "By selection (&7)";
            rbBySelection.UseVisualStyleBackColor = true;
            // 
            // v_TPSubstringDlg
            // 
            AutoScaleDimensions = new SizeF(11F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(417, 276);
            Controls.Add(rbBySelection);
            Controls.Add(rbTakeAfter);
            Controls.Add(rbTakeBefore);
            Controls.Add(rbTakeEnd);
            Controls.Add(rbTakeStart);
            Controls.Add(rbSkipEnd);
            Controls.Add(rbSkipStart);
            Controls.Add(lblType);
            Controls.Add(lblLimit);
            Controls.Add(txtLimit);
            Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            Name = "v_TPSubstringDlg";
            Text = "v_TPSubstringDlg";
            Load += v_TPSubstringDlg_Load;
            Controls.SetChildIndex(btnOK, 0);
            Controls.SetChildIndex(btnCancel, 0);
            Controls.SetChildIndex(txtLimit, 0);
            Controls.SetChildIndex(lblLimit, 0);
            Controls.SetChildIndex(lblType, 0);
            Controls.SetChildIndex(rbSkipStart, 0);
            Controls.SetChildIndex(rbSkipEnd, 0);
            Controls.SetChildIndex(rbTakeStart, 0);
            Controls.SetChildIndex(rbTakeEnd, 0);
            Controls.SetChildIndex(rbTakeBefore, 0);
            Controls.SetChildIndex(rbTakeAfter, 0);
            Controls.SetChildIndex(rbBySelection, 0);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        internal TextBox txtLimit;
        private Label lblLimit;
        private Label lblType;
        internal RadioButton rbSkipStart;
        internal RadioButton rbSkipEnd;
        internal RadioButton rbTakeStart;
        internal RadioButton rbTakeEnd;
        internal RadioButton rbTakeBefore;
        internal RadioButton rbTakeAfter;
        internal RadioButton rbBySelection;
    }
}
