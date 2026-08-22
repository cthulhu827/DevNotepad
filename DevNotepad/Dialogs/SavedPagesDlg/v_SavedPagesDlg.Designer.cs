namespace DevNotepad.Dialogs.SavedPagesDlg
{
    partial class v_SavedPagesDlg
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
            txtSearch = new TextBox();
            lbSavedPages = new lb_SavedPages();
            SuspendLayout();
            // 
            // btnOK
            // 
            btnOK.Location = new Point(391, 447);
            btnOK.Margin = new Padding(6, 5, 6, 5);
            btnOK.Size = new Size(146, 45);
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(545, 447);
            btnCancel.Margin = new Padding(6, 5, 6, 5);
            btnCancel.Size = new Size(146, 45);
            // 
            // txtSearch
            // 
            txtSearch.BorderStyle = BorderStyle.FixedSingle;
            txtSearch.Location = new Point(8, 8);
            txtSearch.Margin = new Padding(5);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(682, 32);
            txtSearch.TabIndex = 0;
            // 
            // lbSavedPages
            // 
            lbSavedPages.BorderStyle = BorderStyle.FixedSingle;
            lbSavedPages.DrawMode = DrawMode.OwnerDrawFixed;
            lbSavedPages.FormattingEnabled = true;
            lbSavedPages.ItemHeight = 25;
            lbSavedPages.Location = new Point(8, 48);
            lbSavedPages.Margin = new Padding(5);
            lbSavedPages.Name = "lbSavedPages";
            lbSavedPages.Size = new Size(682, 377);
            lbSavedPages.TabIndex = 2;
            // 
            // v_SavedPagesDlg
            // 
            AutoScaleDimensions = new SizeF(11F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(699, 500);
            Controls.Add(lbSavedPages);
            Controls.Add(txtSearch);
            Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            Margin = new Padding(6, 5, 6, 5);
            Name = "v_SavedPagesDlg";
            Text = "Open saved page";
            Load += v_SavedPagesDlg_Load;
            Controls.SetChildIndex(txtSearch, 0);
            Controls.SetChildIndex(btnOK, 0);
            Controls.SetChildIndex(btnCancel, 0);
            Controls.SetChildIndex(lbSavedPages, 0);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        internal TextBox txtSearch;
        internal lb_SavedPages lbSavedPages;
    }
}