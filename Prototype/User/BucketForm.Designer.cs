
namespace Prototype.User
{
    partial class BucketForm
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
            this.labelResourcesNotFound = new System.Windows.Forms.Label();
            this.totalPrice = new System.Windows.Forms.Label();
            this.btnPayment = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnClearBucket = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(223, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Корзина";
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
            this.dgv.Size = new System.Drawing.Size(523, 474);
            this.dgv.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 548);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(126, 37);
            this.button1.TabIndex = 7;
            this.button1.Text = "Назад";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.buttonBack_Click);
            // 
            // labelResourcesNotFound
            // 
            this.labelResourcesNotFound.AutoSize = true;
            this.labelResourcesNotFound.Location = new System.Drawing.Point(215, 214);
            this.labelResourcesNotFound.Name = "labelResourcesNotFound";
            this.labelResourcesNotFound.Size = new System.Drawing.Size(111, 20);
            this.labelResourcesNotFound.TabIndex = 18;
            this.labelResourcesNotFound.Text = "Нет ресурсов";
            this.labelResourcesNotFound.Visible = false;
            // 
            // totalPrice
            // 
            this.totalPrice.AutoSize = true;
            this.totalPrice.Location = new System.Drawing.Point(332, 509);
            this.totalPrice.Name = "totalPrice";
            this.totalPrice.Size = new System.Drawing.Size(121, 20);
            this.totalPrice.TabIndex = 19;
            this.totalPrice.Text = "### TOTAL ###";
            // 
            // btnPayment
            // 
            this.btnPayment.Location = new System.Drawing.Point(409, 547);
            this.btnPayment.Name = "btnPayment";
            this.btnPayment.Size = new System.Drawing.Size(126, 37);
            this.btnPayment.TabIndex = 20;
            this.btnPayment.Text = "Оплатить";
            this.btnPayment.UseVisualStyleBackColor = true;
            this.btnPayment.Click += new System.EventHandler(this.btnPayment_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(144, 548);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(126, 37);
            this.btnRemove.TabIndex = 21;
            this.btnRemove.Text = "Убрать";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnClearBucket
            // 
            this.btnClearBucket.Location = new System.Drawing.Point(277, 548);
            this.btnClearBucket.Name = "btnClearBucket";
            this.btnClearBucket.Size = new System.Drawing.Size(126, 37);
            this.btnClearBucket.TabIndex = 22;
            this.btnClearBucket.Text = "Убрать всё";
            this.btnClearBucket.UseVisualStyleBackColor = true;
            this.btnClearBucket.Click += new System.EventHandler(this.btnClearBucket_Click);
            // 
            // BucketForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(547, 597);
            this.Controls.Add(this.btnClearBucket);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnPayment);
            this.Controls.Add(this.totalPrice);
            this.Controls.Add(this.labelResourcesNotFound);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dgv);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BucketForm";
            this.Text = "Корзина";
            this.Load += new System.EventHandler(this.BucketForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.Label labelResourcesNotFound;
        private System.Windows.Forms.Label totalPrice;
        private System.Windows.Forms.Button btnPayment;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnClearBucket;
    }
}