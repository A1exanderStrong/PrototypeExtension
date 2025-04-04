﻿
namespace Prototype
{
    partial class StuffForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.txtResourceName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ComboBoxCategories = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnAddToBucket = new System.Windows.Forms.Button();
            this.btnEditResource = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.loaderImage = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxSort = new System.Windows.Forms.ComboBox();
            this.labelResourcesNotFound = new System.Windows.Forms.Label();
            this.btnBucket = new System.Windows.Forms.Button();
            this.btnRemoveResource = new System.Windows.Forms.Button();
            this.btnIncPage = new System.Windows.Forms.Button();
            this.btnDecPage = new System.Windows.Forms.Button();
            this.lbPageNum = new System.Windows.Forms.Label();
            this.lbDisplayed = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.loaderImage)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(390, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Ресурсы";
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.AllowUserToResizeColumns = false;
            this.dgv.AllowUserToResizeRows = false;
            this.dgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Location = new System.Drawing.Point(12, 32);
            this.dgv.MultiSelect = false;
            this.dgv.Name = "dgv";
            this.dgv.ReadOnly = true;
            this.dgv.RowHeadersVisible = false;
            this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv.Size = new System.Drawing.Size(833, 474);
            this.dgv.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 679);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(126, 37);
            this.button1.TabIndex = 7;
            this.button1.Text = "Назад";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtResourceName
            // 
            this.txtResourceName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.txtResourceName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtResourceName.Location = new System.Drawing.Point(144, 517);
            this.txtResourceName.MaxLength = 150;
            this.txtResourceName.Name = "txtResourceName";
            this.txtResourceName.Size = new System.Drawing.Size(701, 26);
            this.txtResourceName.TabIndex = 8;
            this.txtResourceName.TextChanged += new System.EventHandler(this.txtResourceName_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 520);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(126, 20);
            this.label2.TabIndex = 9;
            this.label2.Text = "Наименование:";
            // 
            // ComboBoxCategories
            // 
            this.ComboBoxCategories.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxCategories.FormattingEnabled = true;
            this.ComboBoxCategories.Location = new System.Drawing.Point(144, 549);
            this.ComboBoxCategories.Name = "ComboBoxCategories";
            this.ComboBoxCategories.Size = new System.Drawing.Size(249, 28);
            this.ComboBoxCategories.TabIndex = 10;
            this.ComboBoxCategories.SelectedIndexChanged += new System.EventHandler(this.ComboBoxCategories_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 552);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 20);
            this.label3.TabIndex = 11;
            this.label3.Text = "Категория: ";
            // 
            // btnAddToBucket
            // 
            this.btnAddToBucket.Location = new System.Drawing.Point(688, 679);
            this.btnAddToBucket.Name = "btnAddToBucket";
            this.btnAddToBucket.Size = new System.Drawing.Size(157, 37);
            this.btnAddToBucket.TabIndex = 12;
            this.btnAddToBucket.Text = "В корзину";
            this.btnAddToBucket.UseVisualStyleBackColor = true;
            this.btnAddToBucket.Click += new System.EventHandler(this.buttonAddToBucket_Click);
            // 
            // btnEditResource
            // 
            this.btnEditResource.Location = new System.Drawing.Point(525, 679);
            this.btnEditResource.Name = "btnEditResource";
            this.btnEditResource.Size = new System.Drawing.Size(157, 37);
            this.btnEditResource.TabIndex = 13;
            this.btnEditResource.Text = "Редактировать";
            this.btnEditResource.UseVisualStyleBackColor = true;
            this.btnEditResource.Click += new System.EventHandler(this.btnEditResource_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 5);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(261, 24);
            this.progressBar1.TabIndex = 14;
            // 
            // loaderImage
            // 
            this.loaderImage.ImageLocation = "";
            this.loaderImage.Location = new System.Drawing.Point(384, 248);
            this.loaderImage.Name = "loaderImage";
            this.loaderImage.Size = new System.Drawing.Size(64, 57);
            this.loaderImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.loaderImage.TabIndex = 15;
            this.loaderImage.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(482, 552);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(108, 20);
            this.label4.TabIndex = 17;
            this.label4.Text = "Сортировка: ";
            // 
            // comboBoxSort
            // 
            this.comboBoxSort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSort.FormattingEnabled = true;
            this.comboBoxSort.Items.AddRange(new object[] {
            "Нет",
            "Сначала новые",
            "Сначала старые",
            "Сначала дорогие",
            "Сначала дешёвые"});
            this.comboBoxSort.Location = new System.Drawing.Point(596, 549);
            this.comboBoxSort.Name = "comboBoxSort";
            this.comboBoxSort.Size = new System.Drawing.Size(249, 28);
            this.comboBoxSort.TabIndex = 16;
            this.comboBoxSort.SelectedIndexChanged += new System.EventHandler(this.comboBoxSort_SelectedIndexChanged);
            // 
            // labelResourcesNotFound
            // 
            this.labelResourcesNotFound.AutoSize = true;
            this.labelResourcesNotFound.Location = new System.Drawing.Point(380, 225);
            this.labelResourcesNotFound.Name = "labelResourcesNotFound";
            this.labelResourcesNotFound.Size = new System.Drawing.Size(111, 20);
            this.labelResourcesNotFound.TabIndex = 18;
            this.labelResourcesNotFound.Text = "Нет ресурсов";
            this.labelResourcesNotFound.Visible = false;
            // 
            // btnBucket
            // 
            this.btnBucket.Location = new System.Drawing.Point(636, 3);
            this.btnBucket.Name = "btnBucket";
            this.btnBucket.Size = new System.Drawing.Size(90, 26);
            this.btnBucket.TabIndex = 19;
            this.btnBucket.Text = "Корзина";
            this.btnBucket.UseVisualStyleBackColor = true;
            this.btnBucket.Click += new System.EventHandler(this.btnBucket_Click);
            // 
            // btnRemoveResource
            // 
            this.btnRemoveResource.Location = new System.Drawing.Point(362, 679);
            this.btnRemoveResource.Name = "btnRemoveResource";
            this.btnRemoveResource.Size = new System.Drawing.Size(157, 37);
            this.btnRemoveResource.TabIndex = 20;
            this.btnRemoveResource.Text = "Удалить";
            this.btnRemoveResource.UseVisualStyleBackColor = true;
            this.btnRemoveResource.Click += new System.EventHandler(this.btnRemoveResource_Click);
            // 
            // btnIncPage
            // 
            this.btnIncPage.Location = new System.Drawing.Point(799, 586);
            this.btnIncPage.Name = "btnIncPage";
            this.btnIncPage.Size = new System.Drawing.Size(46, 37);
            this.btnIncPage.TabIndex = 21;
            this.btnIncPage.Text = ">";
            this.btnIncPage.UseVisualStyleBackColor = true;
            this.btnIncPage.Click += new System.EventHandler(this.btnIncPage_Click);
            // 
            // btnDecPage
            // 
            this.btnDecPage.Location = new System.Drawing.Point(747, 586);
            this.btnDecPage.Name = "btnDecPage";
            this.btnDecPage.Size = new System.Drawing.Size(46, 37);
            this.btnDecPage.TabIndex = 22;
            this.btnDecPage.Text = "<";
            this.btnDecPage.UseVisualStyleBackColor = true;
            this.btnDecPage.Click += new System.EventHandler(this.btnDecPage_Click);
            // 
            // lbPageNum
            // 
            this.lbPageNum.AutoSize = true;
            this.lbPageNum.Location = new System.Drawing.Point(12, 614);
            this.lbPageNum.Name = "lbPageNum";
            this.lbPageNum.Size = new System.Drawing.Size(128, 20);
            this.lbPageNum.TabIndex = 23;
            this.lbPageNum.Text = "PAGE NUMBER";
            // 
            // lbDisplayed
            // 
            this.lbDisplayed.AutoSize = true;
            this.lbDisplayed.Location = new System.Drawing.Point(12, 594);
            this.lbDisplayed.Name = "lbDisplayed";
            this.lbDisplayed.Size = new System.Drawing.Size(154, 20);
            this.lbDisplayed.TabIndex = 24;
            this.lbDisplayed.Text = "ITEMS DISPLAYED";
            // 
            // StuffForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(857, 728);
            this.Controls.Add(this.lbDisplayed);
            this.Controls.Add(this.lbPageNum);
            this.Controls.Add(this.btnDecPage);
            this.Controls.Add(this.btnIncPage);
            this.Controls.Add(this.btnRemoveResource);
            this.Controls.Add(this.btnBucket);
            this.Controls.Add(this.labelResourcesNotFound);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.comboBoxSort);
            this.Controls.Add(this.loaderImage);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.btnEditResource);
            this.Controls.Add(this.btnAddToBucket);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ComboBoxCategories);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtResourceName);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dgv);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "StuffForm";
            this.Text = "StuffForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.StuffForm_FormClosing);
            this.Load += new System.EventHandler(this.StuffForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.loaderImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtResourceName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox ComboBoxCategories;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnAddToBucket;
        private System.Windows.Forms.Button btnEditResource;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.PictureBox loaderImage;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBoxSort;
        private System.Windows.Forms.Label labelResourcesNotFound;
        private System.Windows.Forms.Button btnBucket;
        private System.Windows.Forms.Button btnRemoveResource;
        private System.Windows.Forms.Button btnIncPage;
        private System.Windows.Forms.Button btnDecPage;
        private System.Windows.Forms.Label lbPageNum;
        private System.Windows.Forms.Label lbDisplayed;
    }
}