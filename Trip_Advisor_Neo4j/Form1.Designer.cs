namespace Trip_Advisor_Neo4j
{
    partial class Form1
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
            this.create_data = new System.Windows.Forms.Button();
            this.add_place_pic = new System.Windows.Forms.Button();
            this.placeId = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // create_data
            // 
            this.create_data.Font = new System.Drawing.Font("Rockwell", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.create_data.ForeColor = System.Drawing.Color.DarkRed;
            this.create_data.Location = new System.Drawing.Point(63, 62);
            this.create_data.Name = "create_data";
            this.create_data.Size = new System.Drawing.Size(237, 194);
            this.create_data.TabIndex = 0;
            this.create_data.Text = "CreateData";
            this.create_data.UseVisualStyleBackColor = true;
            this.create_data.Click += new System.EventHandler(this.create_data_Click);
            // 
            // add_place_pic
            // 
            this.add_place_pic.ForeColor = System.Drawing.Color.DarkRed;
            this.add_place_pic.Location = new System.Drawing.Point(591, 62);
            this.add_place_pic.Name = "add_place_pic";
            this.add_place_pic.Size = new System.Drawing.Size(130, 20);
            this.add_place_pic.TabIndex = 1;
            this.add_place_pic.Text = "AddPlacePicture";
            this.add_place_pic.UseVisualStyleBackColor = true;
            this.add_place_pic.Click += new System.EventHandler(this.add_place_pic_Click);
            // 
            // placeId
            // 
            this.placeId.Location = new System.Drawing.Point(532, 62);
            this.placeId.Name = "placeId";
            this.placeId.Size = new System.Drawing.Size(53, 20);
            this.placeId.TabIndex = 2;
            this.placeId.Text = "PlaceId";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(813, 319);
            this.Controls.Add(this.placeId);
            this.Controls.Add(this.add_place_pic);
            this.Controls.Add(this.create_data);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button create_data;
        private System.Windows.Forms.Button add_place_pic;
        private System.Windows.Forms.TextBox placeId;
    }
}

