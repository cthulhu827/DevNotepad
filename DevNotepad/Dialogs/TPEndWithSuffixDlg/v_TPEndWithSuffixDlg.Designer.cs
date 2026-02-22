namespace DevNotepad.Dialogs.TPEndWithSuffixDlg
{
    partial class v_TPEndWithSuffixDlg
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
            txtSuffix = new TextBox();
            chkExceptLastLine = new CheckBox();
            lblSuffix = new Label();
            SuspendLayout();
            // 
            // btnOK
            // 
            btnOK.Location = new Point(79, 103);
            btnOK.Margin = new Padding(6, 5, 6, 5);
            btnOK.Size = new Size(146, 45);
            btnOK.TabIndex = 3;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(233, 103);
            btnCancel.Margin = new Padding(6, 5, 6, 5);
            btnCancel.Size = new Size(146, 45);
            btnCancel.TabIndex = 4;
            // 
            // txtSuffix
            // 
            txtSuffix.Location = new Point(80, 8);
            txtSuffix.Margin = new Padding(5);
            txtSuffix.Name = "txtSuffix";
            txtSuffix.Size = new Size(298, 32);
            txtSuffix.TabIndex = 1;
            // 
            // chkExceptLastLine
            // 
            chkExceptLastLine.AutoSize = true;
            chkExceptLastLine.Location = new Point(80, 56);
            chkExceptLastLine.Margin = new Padding(5);
            chkExceptLastLine.Name = "chkExceptLastLine";
            chkExceptLastLine.Size = new Size(156, 29);
            chkExceptLastLine.TabIndex = 2;
            chkExceptLastLine.Text = "&Except last line";
            chkExceptLastLine.UseVisualStyleBackColor = true;
            // 
            // lblSuffix
            // 
            lblSuffix.AutoSize = true;
            lblSuffix.Location = new Point(8, 11);
            lblSuffix.Name = "lblSuffix";
            lblSuffix.Size = new Size(63, 25);
            lblSuffix.TabIndex = 0;
            lblSuffix.Text = "&Suffix:";
            // 
            // v_TPEndWithSuffixDlg
            // 
            AutoScaleDimensions = new SizeF(11F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(387, 156);
            Controls.Add(lblSuffix);
            Controls.Add(chkExceptLastLine);
            Controls.Add(txtSuffix);
            Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            Margin = new Padding(6, 5, 6, 5);
            Name = "v_TPEndWithSuffixDlg";
            Text = "v_TPEndWithSuffixDlg";
            Load += v_TPEndWithSuffixDlg_Load;
            Controls.SetChildIndex(btnOK, 0);
            Controls.SetChildIndex(btnCancel, 0);
            Controls.SetChildIndex(txtSuffix, 0);
            Controls.SetChildIndex(chkExceptLastLine, 0);
            Controls.SetChildIndex(lblSuffix, 0);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        internal TextBox txtSuffix;
        internal CheckBox chkExceptLastLine;
        private Label lblSuffix;
    }
}