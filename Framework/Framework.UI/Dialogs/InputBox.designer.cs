namespace Framework.UI
{
  partial class InputBox
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
      this.lblPrompt = new System.Windows.Forms.Label();
      this.txtValue = new System.Windows.Forms.TextBox();
      this.SuspendLayout();
      // 
      // btnOK
      // 
      this.btnOK.Location = new System.Drawing.Point(168, 56);
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(256, 56);
      // 
      // lblPrompt
      // 
      this.lblPrompt.AutoSize = true;
      this.lblPrompt.Location = new System.Drawing.Point(8, 8);
      this.lblPrompt.Name = "lblPrompt";
      this.lblPrompt.Size = new System.Drawing.Size(35, 13);
      this.lblPrompt.TabIndex = 2;
      this.lblPrompt.Text = "label1";
      // 
      // txtValue
      // 
      this.txtValue.Location = new System.Drawing.Point(8, 24);
      this.txtValue.Name = "txtValue";
      this.txtValue.Size = new System.Drawing.Size(328, 20);
      this.txtValue.TabIndex = 3;
      this.txtValue.TextChanged += new System.EventHandler(this.txtValue_TextChanged);
      // 
      // TfrmInputBoxDialog
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(344, 87);
      this.Controls.Add(this.lblPrompt);
      this.Controls.Add(this.txtValue);
      this.Name = "TfrmInputBoxDialog";
      this.Text = "InputBoxDialog";
      this.Shown += new System.EventHandler(this.TfrmInputBoxDialog_Shown);
      this.Controls.SetChildIndex(this.txtValue, 0);
      this.Controls.SetChildIndex(this.lblPrompt, 0);
      this.Controls.SetChildIndex(this.btnOK, 0);
      this.Controls.SetChildIndex(this.btnCancel, 0);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label lblPrompt;
    private System.Windows.Forms.TextBox txtValue;
  }
}