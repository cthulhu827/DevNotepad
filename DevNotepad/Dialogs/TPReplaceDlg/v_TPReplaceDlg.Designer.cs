namespace DevNotepad.Dialogs.TPReplaceDlg
{
    partial class v_TPReplaceDlg
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
            lblOldValue = new Label();
            txtOldValue = new TextBox();
            lblNewValue = new Label();
            txtNewValue = new TextBox();
            SuspendLayout();
            // 
            // btnOK
            // 
            btnOK.Location = new Point(127, 111);
            btnOK.Margin = new Padding(6, 5, 6, 5);
            btnOK.Size = new Size(146, 45);
            btnOK.TabIndex = 4;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(281, 111);
            btnCancel.Margin = new Padding(6, 5, 6, 5);
            btnCancel.Size = new Size(146, 45);
            btnCancel.TabIndex = 5;
            // 
            // lblOldValue
            // 
            lblOldValue.AutoSize = true;
            lblOldValue.Location = new Point(8, 12);
            lblOldValue.Name = "lblOldValue";
            lblOldValue.Size = new Size(96, 25);
            lblOldValue.TabIndex = 0;
            lblOldValue.Text = "&Old value:";
            // 
            // txtOldValue
            // 
            txtOldValue.Location = new Point(128, 8);
            txtOldValue.Name = "txtOldValue";
            txtOldValue.Size = new Size(298, 32);
            txtOldValue.TabIndex = 1;
            // 
            // lblNewValue
            // 
            lblNewValue.AutoSize = true;
            lblNewValue.Location = new Point(8, 60);
            lblNewValue.Name = "lblNewValue";
            lblNewValue.Size = new Size(104, 25);
            lblNewValue.TabIndex = 2;
            lblNewValue.Text = "&New value:";
            // 
            // txtNewValue
            // 
            txtNewValue.Location = new Point(128, 56);
            txtNewValue.Name = "txtNewValue";
            txtNewValue.Size = new Size(298, 32);
            txtNewValue.TabIndex = 3;
            // 
            // v_TPReplaceDlg
            // 
            AutoScaleDimensions = new SizeF(11F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(435, 164);
            Controls.Add(txtNewValue);
            Controls.Add(lblNewValue);
            Controls.Add(txtOldValue);
            Controls.Add(lblOldValue);
            Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            Name = "v_TPReplaceDlg";
            Text = "v_TPReplaceDlg";
            Load += v_TPReplaceDlg_Load;
            Controls.SetChildIndex(btnOK, 0);
            Controls.SetChildIndex(btnCancel, 0);
            Controls.SetChildIndex(lblOldValue, 0);
            Controls.SetChildIndex(txtOldValue, 0);
            Controls.SetChildIndex(lblNewValue, 0);
            Controls.SetChildIndex(txtNewValue, 0);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblOldValue;
        internal TextBox txtOldValue;
        private Label lblNewValue;
        internal TextBox txtNewValue;
    }
}
