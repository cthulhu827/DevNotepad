namespace DevNotepad.Dialogs.TPGrepDlg
{
    partial class v_TPGrepDlg
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
            txtSearchText = new TextBox();
            chkExclude = new CheckBox();
            chkCaseSensitive = new CheckBox();
            chkRegEx = new CheckBox();
            lblSearchText = new Label();
            txtLinesBefore = new TextBox();
            txtLinesAfter = new TextBox();
            lblLinesBefore = new Label();
            lblLinesAfter = new Label();
            chkDoNotSeparate = new CheckBox();
            SuspendLayout();
            // 
            // btnOK
            // 
            btnOK.Location = new Point(135, 311);
            btnOK.Margin = new Padding(6, 5, 6, 5);
            btnOK.Size = new Size(146, 45);
            btnOK.TabIndex = 9;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(289, 311);
            btnCancel.Margin = new Padding(6, 5, 6, 5);
            btnCancel.Size = new Size(146, 45);
            btnCancel.TabIndex = 10;
            // 
            // txtSearchText
            // 
            txtSearchText.Location = new Point(136, 8);
            txtSearchText.Name = "txtSearchText";
            txtSearchText.Size = new Size(296, 32);
            txtSearchText.TabIndex = 1;
            // 
            // chkExclude
            // 
            chkExclude.AutoSize = true;
            chkExclude.Location = new Point(136, 56);
            chkExclude.Name = "chkExclude";
            chkExclude.Size = new Size(96, 29);
            chkExclude.TabIndex = 2;
            chkExclude.Text = "&Exclude";
            chkExclude.UseVisualStyleBackColor = true;
            // 
            // chkCaseSensitive
            // 
            chkCaseSensitive.AutoSize = true;
            chkCaseSensitive.Location = new Point(136, 96);
            chkCaseSensitive.Name = "chkCaseSensitive";
            chkCaseSensitive.Size = new Size(148, 29);
            chkCaseSensitive.TabIndex = 3;
            chkCaseSensitive.Text = "&Case sensitive";
            chkCaseSensitive.UseVisualStyleBackColor = true;
            // 
            // chkRegEx
            // 
            chkRegEx.AutoSize = true;
            chkRegEx.Location = new Point(136, 136);
            chkRegEx.Name = "chkRegEx";
            chkRegEx.Size = new Size(190, 29);
            chkRegEx.TabIndex = 4;
            chkRegEx.Text = "&Regular expression";
            chkRegEx.UseVisualStyleBackColor = true;
            // 
            // lblSearchText
            // 
            lblSearchText.AutoSize = true;
            lblSearchText.Location = new Point(8, 11);
            lblSearchText.Name = "lblSearchText";
            lblSearchText.Size = new Size(109, 25);
            lblSearchText.TabIndex = 0;
            lblSearchText.Text = "&Search text:";
            // 
            // txtLinesBefore
            // 
            txtLinesBefore.Location = new Point(136, 184);
            txtLinesBefore.Name = "txtLinesBefore";
            txtLinesBefore.Size = new Size(96, 32);
            txtLinesBefore.TabIndex = 6;
            // 
            // txtLinesAfter
            // 
            txtLinesAfter.Location = new Point(136, 224);
            txtLinesAfter.Name = "txtLinesAfter";
            txtLinesAfter.Size = new Size(96, 32);
            txtLinesAfter.TabIndex = 8;
            // 
            // lblLinesBefore
            // 
            lblLinesBefore.AutoSize = true;
            lblLinesBefore.Location = new Point(8, 188);
            lblLinesBefore.Name = "lblLinesBefore";
            lblLinesBefore.Size = new Size(119, 25);
            lblLinesBefore.TabIndex = 5;
            lblLinesBefore.Text = "Lines &before:";
            // 
            // lblLinesAfter
            // 
            lblLinesAfter.AutoSize = true;
            lblLinesAfter.Location = new Point(8, 228);
            lblLinesAfter.Name = "lblLinesAfter";
            lblLinesAfter.Size = new Size(103, 25);
            lblLinesAfter.TabIndex = 7;
            lblLinesAfter.Text = "Lines &after:";
            // 
            // chkDoNotSeparate
            // 
            chkDoNotSeparate.AutoSize = true;
            chkDoNotSeparate.Location = new Point(136, 264);
            chkDoNotSeparate.Name = "chkDoNotSeparate";
            chkDoNotSeparate.Size = new Size(165, 29);
            chkDoNotSeparate.TabIndex = 11;
            chkDoNotSeparate.Text = "Do not &separate";
            chkDoNotSeparate.UseVisualStyleBackColor = true;
            // 
            // v_TPGrepDlg
            // 
            AutoScaleDimensions = new SizeF(11F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(443, 364);
            Controls.Add(chkDoNotSeparate);
            Controls.Add(lblLinesAfter);
            Controls.Add(lblLinesBefore);
            Controls.Add(txtLinesAfter);
            Controls.Add(txtLinesBefore);
            Controls.Add(lblSearchText);
            Controls.Add(chkRegEx);
            Controls.Add(chkCaseSensitive);
            Controls.Add(chkExclude);
            Controls.Add(txtSearchText);
            Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            Name = "v_TPGrepDlg";
            Text = "v_TPGrepDlg";
            Load += v_TPGrepDlg_Load;
            Controls.SetChildIndex(btnOK, 0);
            Controls.SetChildIndex(btnCancel, 0);
            Controls.SetChildIndex(txtSearchText, 0);
            Controls.SetChildIndex(chkExclude, 0);
            Controls.SetChildIndex(chkCaseSensitive, 0);
            Controls.SetChildIndex(chkRegEx, 0);
            Controls.SetChildIndex(lblSearchText, 0);
            Controls.SetChildIndex(txtLinesBefore, 0);
            Controls.SetChildIndex(txtLinesAfter, 0);
            Controls.SetChildIndex(lblLinesBefore, 0);
            Controls.SetChildIndex(lblLinesAfter, 0);
            Controls.SetChildIndex(chkDoNotSeparate, 0);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        internal TextBox txtSearchText;
        internal CheckBox chkExclude;
        internal CheckBox chkCaseSensitive;
        internal CheckBox chkRegEx;
        private Label lblSearchText;
        internal TextBox txtLinesBefore;
        internal TextBox txtLinesAfter;
        private Label lblLinesBefore;
        private Label lblLinesAfter;
        internal CheckBox chkDoNotSeparate;
    }
}