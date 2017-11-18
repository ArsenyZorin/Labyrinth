namespace TestTaskMitsar
{
    partial class Labyrinth
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Labyrinth));
            this.SizeOfField_textBox = new System.Windows.Forms.TextBox();
            this.FieldWPF = new System.Windows.Forms.Integration.ElementHost();
            this.AmountOfObstacles = new System.Windows.Forms.TextBox();
            this.CreateObstaclesButton = new System.Windows.Forms.Button();
            this.FindPath = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ClearAll = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // SizeOfField_textBox
            // 
            this.SizeOfField_textBox.Location = new System.Drawing.Point(13, 33);
            this.SizeOfField_textBox.Name = "SizeOfField_textBox";
            this.SizeOfField_textBox.Size = new System.Drawing.Size(100, 20);
            this.SizeOfField_textBox.TabIndex = 0;
            this.SizeOfField_textBox.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            this.SizeOfField_textBox.Enter += new System.EventHandler(this.SizeOfField_textBox_Enter);
            this.SizeOfField_textBox.Leave += new System.EventHandler(this.SizeOfField_textBox_Leave);
            // 
            // FieldWPF
            // 
            this.FieldWPF.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FieldWPF.Location = new System.Drawing.Point(13, 59);
            this.FieldWPF.Name = "FieldWPF";
            this.FieldWPF.Size = new System.Drawing.Size(387, 323);
            this.FieldWPF.TabIndex = 1;
            this.FieldWPF.Text = "elementHost1";
            this.FieldWPF.Child = null;
            // 
            // AmountOfObstacles
            // 
            this.AmountOfObstacles.Location = new System.Drawing.Point(110, 33);
            this.AmountOfObstacles.Name = "AmountOfObstacles";
            this.AmountOfObstacles.Size = new System.Drawing.Size(130, 20);
            this.AmountOfObstacles.TabIndex = 3;
            this.AmountOfObstacles.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // CreateObstaclesButton
            // 
            this.CreateObstaclesButton.Location = new System.Drawing.Point(246, 31);
            this.CreateObstaclesButton.Name = "CreateObstaclesButton";
            this.CreateObstaclesButton.Size = new System.Drawing.Size(85, 20);
            this.CreateObstaclesButton.TabIndex = 4;
            this.CreateObstaclesButton.Text = "Препятствия";
            this.CreateObstaclesButton.UseVisualStyleBackColor = true;
            this.CreateObstaclesButton.Click += new System.EventHandler(this.CreateObstaclesButton_Click);
            // 
            // FindPath
            // 
            this.FindPath.Location = new System.Drawing.Point(246, 6);
            this.FindPath.Name = "FindPath";
            this.FindPath.Size = new System.Drawing.Size(151, 21);
            this.FindPath.TabIndex = 5;
            this.FindPath.Text = "Поиск пути";
            this.FindPath.UseVisualStyleBackColor = true;
            this.FindPath.Click += new System.EventHandler(this.FindPath_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Размер поля";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(107, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(133, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Количество препятствий";
            // 
            // ClearAll
            // 
            this.ClearAll.Location = new System.Drawing.Point(331, 31);
            this.ClearAll.Name = "ClearAll";
            this.ClearAll.Size = new System.Drawing.Size(66, 20);
            this.ClearAll.TabIndex = 6;
            this.ClearAll.Text = "Очистить";
            this.ClearAll.UseVisualStyleBackColor = true;
            this.ClearAll.Click += new System.EventHandler(this.ClearAll_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(412, 394);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ClearAll);
            this.Controls.Add(this.FindPath);
            this.Controls.Add(this.CreateObstaclesButton);
            this.Controls.Add(this.AmountOfObstacles);
            this.Controls.Add(this.FieldWPF);
            this.Controls.Add(this.SizeOfField_textBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(428, 432);
            this.Name = "Form1";
            this.Text = "Labyrinth";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox SizeOfField_textBox;
        private System.Windows.Forms.Integration.ElementHost FieldWPF;
        private System.Windows.Forms.TextBox AmountOfObstacles;
        private System.Windows.Forms.Button CreateObstaclesButton;
        private System.Windows.Forms.Button FindPath;
        private System.Windows.Forms.Button ClearAll;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

