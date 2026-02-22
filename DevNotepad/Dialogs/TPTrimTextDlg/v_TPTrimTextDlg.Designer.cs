namespace DevNotepad.Dialogs.TPTrimTextDlg
{
    partial class v_TPTrimTextDlg
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
            txtPrefix = new TextBox();
            chkAutoPrefix = new CheckBox();
            chkAutoSuffix = new CheckBox();
            lblPrefix = new Label();
            lblSuffix = new Label();
            txtSuffix = new TextBox();
            SuspendLayout();
            // 
            // btnOK
            // 
            btnOK.Location = new Point(229, 183);
            btnOK.Margin = new Padding(6, 5, 6, 5);
            btnOK.Size = new Size(146, 45);
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(383, 183);
            btnCancel.Margin = new Padding(6, 5, 6, 5);
            btnCancel.Size = new Size(146, 45);
            // 
            // txtPrefix
            // 
            txtPrefix.Location = new Point(120, 38);
            txtPrefix.Margin = new Padding(5);
            txtPrefix.Name = "txtPrefix";
            txtPrefix.Size = new Size(408, 32);
            txtPrefix.TabIndex = 2;
            // 
            // chkAutoPrefix
            // 
            chkAutoPrefix.AutoSize = true;
            chkAutoPrefix.Location = new Point(16, 40);
            chkAutoPrefix.Margin = new Padding(5);
            chkAutoPrefix.Name = "chkAutoPrefix";
            chkAutoPrefix.Size = new Size(99, 29);
            chkAutoPrefix.TabIndex = 4;
            chkAutoPrefix.Text = "Auto (&p)";
            chkAutoPrefix.UseVisualStyleBackColor = true;
            // 
            // chkAutoSuffix
            // 
            chkAutoSuffix.AutoSize = true;
            chkAutoSuffix.Location = new Point(16, 128);
            chkAutoSuffix.Margin = new Padding(5);
            chkAutoSuffix.Name = "chkAutoSuffix";
            chkAutoSuffix.Size = new Size(96, 29);
            chkAutoSuffix.TabIndex = 5;
            chkAutoSuffix.Text = "Auto (&s)";
            chkAutoSuffix.UseVisualStyleBackColor = true;
            // 
            // lblPrefix
            // 
            lblPrefix.AutoSize = true;
            lblPrefix.Location = new Point(8, 8);
            lblPrefix.Margin = new Padding(5, 0, 5, 0);
            lblPrefix.Name = "lblPrefix";
            lblPrefix.Size = new Size(125, 25);
            lblPrefix.TabIndex = 6;
            lblPrefix.Text = "Prefix to trim:";
            // 
            // lblSuffix
            // 
            lblSuffix.AutoSize = true;
            lblSuffix.Location = new Point(8, 96);
            lblSuffix.Margin = new Padding(5, 0, 5, 0);
            lblSuffix.Name = "lblSuffix";
            lblSuffix.Size = new Size(124, 25);
            lblSuffix.TabIndex = 7;
            lblSuffix.Text = "Suffix to trim:";
            // 
            // txtSuffix
            // 
            txtSuffix.Location = new Point(120, 126);
            txtSuffix.Margin = new Padding(5);
            txtSuffix.Name = "txtSuffix";
            txtSuffix.Size = new Size(408, 32);
            txtSuffix.TabIndex = 8;
            // 
            // v_TPTrimTextDlg
            // 
            AutoScaleDimensions = new SizeF(11F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(537, 236);
            Controls.Add(txtSuffix);
            Controls.Add(lblSuffix);
            Controls.Add(lblPrefix);
            Controls.Add(chkAutoSuffix);
            Controls.Add(chkAutoPrefix);
            Controls.Add(txtPrefix);
            Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            Margin = new Padding(6, 5, 6, 5);
            Name = "v_TPTrimTextDlg";
            Text = "v_TPTrimTextDlg";
            Load += v_TPTrimTextDlg_Load;
            Controls.SetChildIndex(btnOK, 0);
            Controls.SetChildIndex(btnCancel, 0);
            Controls.SetChildIndex(txtPrefix, 0);
            Controls.SetChildIndex(chkAutoPrefix, 0);
            Controls.SetChildIndex(chkAutoSuffix, 0);
            Controls.SetChildIndex(lblPrefix, 0);
            Controls.SetChildIndex(lblSuffix, 0);
            Controls.SetChildIndex(txtSuffix, 0);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        internal TextBox txtPrefix;
        private Label lblPrefix;
        private Label lblSuffix;
        internal TextBox txtSuffix;
        internal CheckBox chkAutoPrefix;
        internal CheckBox chkAutoSuffix;
    }
}