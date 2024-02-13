namespace WorldGui
{
    partial class WorldGui
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            listErrors = new ListBox();
            dataGridView = new DataGridView();
            rdbtnCountry = new RadioButton();
            rdbtnCity = new RadioButton();
            rdbtnLanguage = new RadioButton();
            txtFilter = new TextBox();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridView).BeginInit();
            SuspendLayout();
            // 
            // listErrors
            // 
            listErrors.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            listErrors.BackColor = Color.FromArgb(255, 128, 128);
            listErrors.FormattingEnabled = true;
            listErrors.ItemHeight = 20;
            listErrors.Location = new Point(12, 467);
            listErrors.Name = "listErrors";
            listErrors.Size = new Size(1323, 224);
            listErrors.TabIndex = 0;
            // 
            // dataGridView
            // 
            dataGridView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView.Location = new Point(12, 44);
            dataGridView.Name = "dataGridView";
            dataGridView.RowHeadersWidth = 51;
            dataGridView.RowTemplate.Height = 29;
            dataGridView.Size = new Size(1323, 417);
            dataGridView.TabIndex = 1;
            dataGridView.CellClick += dataGridView_CellClick;
            // 
            // rdbtnCountry
            // 
            rdbtnCountry.AutoSize = true;
            rdbtnCountry.Location = new Point(13, 12);
            rdbtnCountry.Name = "rdbtnCountry";
            rdbtnCountry.Size = new Size(81, 24);
            rdbtnCountry.TabIndex = 2;
            rdbtnCountry.TabStop = true;
            rdbtnCountry.Text = "Country";
            rdbtnCountry.UseVisualStyleBackColor = true;
            rdbtnCountry.CheckedChanged += RdbtnCountry_CheckedChanged;
            // 
            // rdbtnCity
            // 
            rdbtnCity.AutoSize = true;
            rdbtnCity.Location = new Point(100, 12);
            rdbtnCity.Name = "rdbtnCity";
            rdbtnCity.Size = new Size(55, 24);
            rdbtnCity.TabIndex = 3;
            rdbtnCity.TabStop = true;
            rdbtnCity.Text = "City";
            rdbtnCity.UseVisualStyleBackColor = true;
            rdbtnCity.CheckedChanged += RdbtnCity_CheckedChanged;
            // 
            // rdbtnLanguage
            // 
            rdbtnLanguage.AutoSize = true;
            rdbtnLanguage.Location = new Point(161, 12);
            rdbtnLanguage.Name = "rdbtnLanguage";
            rdbtnLanguage.Size = new Size(150, 24);
            rdbtnLanguage.TabIndex = 4;
            rdbtnLanguage.TabStop = true;
            rdbtnLanguage.Text = "Country Language";
            rdbtnLanguage.UseVisualStyleBackColor = true;
            rdbtnLanguage.CheckedChanged += RdbtnLanguage_CheckedChanged;
            // 
            // txtFilter
            // 
            txtFilter.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txtFilter.Location = new Point(923, 11);
            txtFilter.Name = "txtFilter";
            txtFilter.Size = new Size(403, 27);
            txtFilter.TabIndex = 5;
            txtFilter.TextChanged += TextBox1_TextChanged;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Location = new Point(864, 14);
            label1.Name = "label1";
            label1.Size = new Size(42, 20);
            label1.TabIndex = 6;
            label1.Text = "Filter";
            // 
            // WorldGui
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1338, 702);
            Controls.Add(label1);
            Controls.Add(txtFilter);
            Controls.Add(rdbtnLanguage);
            Controls.Add(rdbtnCity);
            Controls.Add(rdbtnCountry);
            Controls.Add(dataGridView);
            Controls.Add(listErrors);
            Name = "WorldGui";
            Text = "World View Gui";
            ((System.ComponentModel.ISupportInitialize)dataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox listErrors;
        private DataGridView dataGridView;
        private RadioButton rdbtnCountry;
        private RadioButton rdbtnCity;
        private RadioButton rdbtnLanguage;
        private TextBox txtFilter;
        private Label label1;
    }
}