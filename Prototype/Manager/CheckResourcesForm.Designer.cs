
namespace Prototype
{
    partial class CheckResourcesForm
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
            this.dgv = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.btnAcceptResource = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDenyResource = new System.Windows.Forms.Button();
            this.loaderImage = new System.Windows.Forms.PictureBox();
            this.labelResourcesNotFound = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.loaderImage)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.AllowUserToResizeColumns = false;
            this.dgv.AllowUserToResizeRows = false;
            this.dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Location = new System.Drawing.Point(12, 12);
            this.dgv.MultiSelect = false;
            this.dgv.Name = "dgv";
            this.dgv.ReadOnly = true;
            this.dgv.RowHeadersVisible = false;
            this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv.Size = new System.Drawing.Size(651, 432);
            this.dgv.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 493);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(126, 37);
            this.button1.TabIndex = 7;
            this.button1.Text = "Назад";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnAcceptResource
            // 
            this.btnAcceptResource.Location = new System.Drawing.Point(506, 450);
            this.btnAcceptResource.Name = "btnAcceptResource";
            this.btnAcceptResource.Size = new System.Drawing.Size(157, 37);
            this.btnAcceptResource.TabIndex = 12;
            this.btnAcceptResource.Text = "Принять";
            this.btnAcceptResource.UseVisualStyleBackColor = true;
            this.btnAcceptResource.Click += new System.EventHandler(this.buttonAcceptResource_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(343, 493);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(157, 37);
            this.btnEdit.TabIndex = 13;
            this.btnEdit.Text = "Редактировать";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnDenyResource
            // 
            this.btnDenyResource.Location = new System.Drawing.Point(506, 493);
            this.btnDenyResource.Name = "btnDenyResource";
            this.btnDenyResource.Size = new System.Drawing.Size(157, 37);
            this.btnDenyResource.TabIndex = 14;
            this.btnDenyResource.Text = "Отклонить";
            this.btnDenyResource.UseVisualStyleBackColor = true;
            this.btnDenyResource.Click += new System.EventHandler(this.btnDenyResource_Click);
            // 
            // loaderImage
            // 
            this.loaderImage.ImageLocation = "";
            this.loaderImage.Location = new System.Drawing.Point(295, 184);
            this.loaderImage.Name = "loaderImage";
            this.loaderImage.Size = new System.Drawing.Size(64, 57);
            this.loaderImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.loaderImage.TabIndex = 16;
            this.loaderImage.TabStop = false;
            // 
            // labelResourcesNotFound
            // 
            this.labelResourcesNotFound.AutoSize = true;
            this.labelResourcesNotFound.Location = new System.Drawing.Point(280, 161);
            this.labelResourcesNotFound.Name = "labelResourcesNotFound";
            this.labelResourcesNotFound.Size = new System.Drawing.Size(111, 20);
            this.labelResourcesNotFound.TabIndex = 19;
            this.labelResourcesNotFound.Text = "Нет ресурсов";
            this.labelResourcesNotFound.Visible = false;
            // 
            // CheckResourcesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(675, 540);
            this.Controls.Add(this.labelResourcesNotFound);
            this.Controls.Add(this.loaderImage);
            this.Controls.Add(this.btnDenyResource);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnAcceptResource);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dgv);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CheckResourcesForm";
            this.Text = "Ресурсы на рассмотрении";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CheckResourcesForm_FormClosing);
            this.Load += new System.EventHandler(this.CheckResourcesForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.loaderImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnAcceptResource;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDenyResource;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.PictureBox loaderImage;
        private System.Windows.Forms.Label labelResourcesNotFound;
    }
}