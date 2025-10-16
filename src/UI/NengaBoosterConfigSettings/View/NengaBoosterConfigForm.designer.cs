
namespace IsTama.NengaBooster.UI.NengaBoosterConfigSettings.View
{
    partial class NengaBoosterConfigForm
    {
        /// <summary>
        /// Designer variable used to keep track of non-visual components.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageBehivor;
        private System.Windows.Forms.TabPage tabPageCooperation;
        private System.Windows.Forms.PropertyGrid PropertyGridBehavior;
        private System.Windows.Forms.PropertyGrid PropertyGridApplication;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnUpdate;
        
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
        private void InitializeComponent()
        {
            this.btnClose = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.tabPageCooperation = new System.Windows.Forms.TabPage();
            this.PropertyGridApplication = new System.Windows.Forms.PropertyGrid();
            this.tabPageBehivor = new System.Windows.Forms.TabPage();
            this.PropertyGridBehavior = new System.Windows.Forms.PropertyGrid();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageCooperation.SuspendLayout();
            this.tabPageBehivor.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(639, 424);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(87, 25);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "閉じる";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpdate.Location = new System.Drawing.Point(545, 424);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(87, 25);
            this.btnUpdate.TabIndex = 2;
            this.btnUpdate.Text = "更新";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.BtnUpdate_Click);
            // 
            // tabPageCooperation
            // 
            this.tabPageCooperation.Controls.Add(this.PropertyGridApplication);
            this.tabPageCooperation.Location = new System.Drawing.Point(4, 22);
            this.tabPageCooperation.Name = "tabPageCooperation";
            this.tabPageCooperation.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCooperation.Size = new System.Drawing.Size(734, 391);
            this.tabPageCooperation.TabIndex = 1;
            this.tabPageCooperation.Text = "年賀アプリ連携設定";
            this.tabPageCooperation.UseVisualStyleBackColor = true;
            // 
            // PropertyGridCooperation
            // 
            this.PropertyGridApplication.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PropertyGridApplication.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.PropertyGridApplication.Location = new System.Drawing.Point(3, 3);
            this.PropertyGridApplication.Name = "PropertyGridCooperation";
            this.PropertyGridApplication.Size = new System.Drawing.Size(728, 385);
            this.PropertyGridApplication.TabIndex = 0;
            // 
            // tabPageBehivor
            // 
            this.tabPageBehivor.Controls.Add(this.PropertyGridBehavior);
            this.tabPageBehivor.Location = new System.Drawing.Point(4, 22);
            this.tabPageBehivor.Name = "tabPageBehivor";
            this.tabPageBehivor.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageBehivor.Size = new System.Drawing.Size(734, 391);
            this.tabPageBehivor.TabIndex = 0;
            this.tabPageBehivor.Text = "動作設定";
            this.tabPageBehivor.UseVisualStyleBackColor = true;
            // 
            // PropertyGridBehavior
            // 
            this.PropertyGridBehavior.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PropertyGridBehavior.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.PropertyGridBehavior.Location = new System.Drawing.Point(3, 3);
            this.PropertyGridBehavior.Name = "PropertyGridBehavior";
            this.PropertyGridBehavior.Size = new System.Drawing.Size(726, 382);
            this.PropertyGridBehavior.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPageBehivor);
            this.tabControl1.Controls.Add(this.tabPageCooperation);
            this.tabControl1.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(742, 417);
            this.tabControl1.TabIndex = 0;
            // 
            // PropertySettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(741, 458);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Name = "PropertySettingsForm";
            this.Text = "プロパティ";
            this.Load += new System.EventHandler(this.NengaBoosterConfigForm_Load);
            this.tabPageCooperation.ResumeLayout(false);
            this.tabPageBehivor.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}
