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
            SuspendLayout();
            // 
            // btnOK
            // 
            btnOK.Location = new Point(119, 183);
            btnOK.Margin = new Padding(6, 5, 6, 5);
            btnOK.Size = new Size(146, 45);
            btnOK.TabIndex = 5;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(273, 183);
            btnCancel.Margin = new Padding(6, 5, 6, 5);
            btnCancel.Size = new Size(146, 45);
            btnCancel.TabIndex = 6;
            // 
            // txtSearchText
            // 
            txtSearchText.Location = new Point(120, 8);
            txtSearchText.Name = "txtSearchText";
            txtSearchText.Size = new Size(298, 32);
            txtSearchText.TabIndex = 1;
            // 
            // chkExclude
            // 
            chkExclude.AutoSize = true;
            chkExclude.Location = new Point(120, 56);
            chkExclude.Name = "chkExclude";
            chkExclude.Size = new Size(96, 29);
            chkExclude.TabIndex = 2;
            chkExclude.Text = "&Exclude";
            chkExclude.UseVisualStyleBackColor = true;
            // 
            // chkCaseSensitive
            // 
            chkCaseSensitive.AutoSize = true;
            chkCaseSensitive.Location = new Point(120, 96);
            chkCaseSensitive.Name = "chkCaseSensitive";
            chkCaseSensitive.Size = new Size(148, 29);
            chkCaseSensitive.TabIndex = 3;
            chkCaseSensitive.Text = "&Case sensitive";
            chkCaseSensitive.UseVisualStyleBackColor = true;
            // 
            // chkRegEx
            // 
            chkRegEx.AutoSize = true;
            chkRegEx.Location = new Point(120, 136);
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
            // v_TPGrepDlg
            // 
            AutoScaleDimensions = new SizeF(11F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(427, 236);
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
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        internal TextBox txtSearchText;
        internal CheckBox chkExclude;
        internal CheckBox chkCaseSensitive;
        internal CheckBox chkRegEx;
        private Label lblSearchText;
    }
}