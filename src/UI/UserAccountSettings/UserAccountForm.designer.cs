
namespace IsTama.NengaBooster.UI.UserAccountSettings.View
{
  partial class UserAccountForm
  {
    /// <summary>
    /// Designer variable used to keep track of non-visual components.
    /// </summary>
    private System.ComponentModel.IContainer components = null;
    
    /// <summary>
    /// Disposes resources used by the form.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing) {
        if (components != null) {
          components.Dispose();
        }
      }
      base.Dispose(disposing);
    }
    
    /// <summary>
    /// This method is required for Windows Forms designer support.
    /// Do not change the method contents inside the source code editor. The Forms designer might
    /// not be able to load this method if it was changed manually.
    /// </summary>
    private void InitializeComponent()
    {
            this.label1 = new System.Windows.Forms.Label();
            this.TxtBoxUserName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TxtBoxPassword = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "User";
            // 
            // TxtBoxUser
            // 
            this.TxtBoxUserName.BackColor = System.Drawing.SystemColors.Info;
            this.TxtBoxUserName.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TxtBoxUserName.Location = new System.Drawing.Point(13, 29);
            this.TxtBoxUserName.Name = "TxtBoxUser";
            this.TxtBoxUserName.Size = new System.Drawing.Size(212, 23);
            this.TxtBoxUserName.TabIndex = 1;
            this.TxtBoxUserName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtBoxKeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "Password";
            // 
            // TxtBoxPassword
            // 
            this.TxtBoxPassword.BackColor = System.Drawing.SystemColors.Info;
            this.TxtBoxPassword.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TxtBoxPassword.Location = new System.Drawing.Point(13, 75);
            this.TxtBoxPassword.Name = "TxtBoxPassword";
            this.TxtBoxPassword.Size = new System.Drawing.Size(212, 23);
            this.TxtBoxPassword.TabIndex = 3;
            this.TxtBoxPassword.UseSystemPasswordChar = true;
            this.TxtBoxPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtBoxKeyDown);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(13, 104);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(106, 23);
            this.btnOk.TabIndex = 4;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.BtnOkClick);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(126, 104);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(99, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancelClick);
            // 
            // UserInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(242, 146);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.TxtBoxPassword);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TxtBoxUserName);
            this.Controls.Add(this.label1);
            this.Name = "UserInfoForm";
            this.Text = "ユーザー情報";
            this.Load += new System.EventHandler(this.LoginFormLoad);
            this.ResumeLayout(false);
            this.PerformLayout();

    }
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Button btnOk;
    private System.Windows.Forms.TextBox TxtBoxPassword;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox TxtBoxUserName;
    private System.Windows.Forms.Label label1;
  }
}
