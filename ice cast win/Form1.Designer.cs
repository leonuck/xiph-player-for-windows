namespace ice_cast_win
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private CustomScrollableListBox listBoxRadios;
        private CustomScrollableListBox listBoxFavorites;
        private System.Windows.Forms.Button buttonPlay;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.Button buttonAddToFavorites;
        private System.Windows.Forms.TextBox textBoxSearch;
        private System.Windows.Forms.Button buttonSearch;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.listBoxRadios = new CustomScrollableListBox();
            this.listBoxFavorites = new CustomScrollableListBox();
            this.buttonPlay = new System.Windows.Forms.Button();
            this.buttonStop = new System.Windows.Forms.Button(); // Botão Stop
            this.buttonAddToFavorites = new System.Windows.Forms.Button();
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listBoxRadios
            // 
            this.listBoxRadios.Location = new System.Drawing.Point(12, 41);
            this.listBoxRadios.Name = "listBoxRadios";
            this.listBoxRadios.Size = new System.Drawing.Size(260, 199);
            this.listBoxRadios.TabIndex = 0;
            // 
            // listBoxFavorites
            // 
            this.listBoxFavorites.Location = new System.Drawing.Point(290, 41);
            this.listBoxFavorites.Name = "listBoxFavorites";
            this.listBoxFavorites.Size = new System.Drawing.Size(260, 199);
            this.listBoxFavorites.TabIndex = 1;
            // 
            // buttonPlay
            // 
            this.buttonPlay.Location = new System.Drawing.Point(12, 246);
            this.buttonPlay.Name = "buttonPlay";
            this.buttonPlay.Size = new System.Drawing.Size(75, 23);
            this.buttonPlay.TabIndex = 2;
            this.buttonPlay.Text = "Play";
            this.buttonPlay.UseVisualStyleBackColor = true;
            this.buttonPlay.Click += new System.EventHandler(this.buttonPlay_Click);
            // 
            // buttonStop
            // 
            this.buttonStop.Location = new System.Drawing.Point(93, 246);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(75, 23);
            this.buttonStop.TabIndex = 6;
            this.buttonStop.Text = "Stop";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // buttonAddToFavorites
            // 
            this.buttonAddToFavorites.Location = new System.Drawing.Point(290, 246);
            this.buttonAddToFavorites.Name = "buttonAddToFavorites";
            this.buttonAddToFavorites.Size = new System.Drawing.Size(120, 23);
            this.buttonAddToFavorites.TabIndex = 3;
            this.buttonAddToFavorites.Text = "Add to Favorites";
            this.buttonAddToFavorites.UseVisualStyleBackColor = true;
            this.buttonAddToFavorites.Click += new System.EventHandler(this.buttonAddToFavorites_Click);
            // 
            // textBoxSearch
            // 
            this.textBoxSearch.Location = new System.Drawing.Point(12, 12);
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(179, 20);
            this.textBoxSearch.TabIndex = 4;
            // 
            // buttonSearch
            // 
            this.buttonSearch.Location = new System.Drawing.Point(197, 10);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(75, 23);
            this.buttonSearch.TabIndex = 5;
            this.buttonSearch.Text = "Search";
            this.buttonSearch.UseVisualStyleBackColor = true;
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(564, 281);
            this.Controls.Add(this.buttonSearch);
            this.Controls.Add(this.textBoxSearch);
            this.Controls.Add(this.buttonAddToFavorites);
            this.Controls.Add(this.buttonStop); // Adiciona o botão Stop
            this.Controls.Add(this.buttonPlay);
            this.Controls.Add(this.listBoxFavorites);
            this.Controls.Add(this.listBoxRadios);
            this.Name = "Form1";
            this.Text = "Radio Player";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}