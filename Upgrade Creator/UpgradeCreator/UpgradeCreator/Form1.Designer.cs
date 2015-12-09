namespace UpgradeCreator
{
    partial class UpgradeCreator
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
            this.Title = new System.Windows.Forms.Label();
            this.upgradeType = new System.Windows.Forms.ComboBox();
            this.upgradeTypeLabel = new System.Windows.Forms.Label();
            this.upgradeAmount = new System.Windows.Forms.TextBox();
            this.upgradeAmountLabel = new System.Windows.Forms.Label();
            this.upgradeCost = new System.Windows.Forms.TextBox();
            this.upgradeCostLabel = new System.Windows.Forms.Label();
            this.outputLabel = new System.Windows.Forms.Label();
            this.createUpgradeButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Title
            // 
            this.Title.AutoSize = true;
            this.Title.Location = new System.Drawing.Point(85, 9);
            this.Title.Name = "Title";
            this.Title.Size = new System.Drawing.Size(114, 17);
            this.Title.TabIndex = 0;
            this.Title.Text = "Upgrade Creator";
            // 
            // upgradeType
            // 
            this.upgradeType.FormattingEnabled = true;
            this.upgradeType.Items.AddRange(new object[] {
            "ACCELERATION",
            "BRAKES",
            "MAXSPEED",
            "MAXBLACKOUT"});
            this.upgradeType.Location = new System.Drawing.Point(149, 50);
            this.upgradeType.Name = "upgradeType";
            this.upgradeType.Size = new System.Drawing.Size(121, 24);
            this.upgradeType.TabIndex = 1;
            // 
            // upgradeTypeLabel
            // 
            this.upgradeTypeLabel.AutoSize = true;
            this.upgradeTypeLabel.Location = new System.Drawing.Point(27, 56);
            this.upgradeTypeLabel.Name = "upgradeTypeLabel";
            this.upgradeTypeLabel.Size = new System.Drawing.Size(99, 17);
            this.upgradeTypeLabel.TabIndex = 2;
            this.upgradeTypeLabel.Text = "Upgrade Type";
            // 
            // upgradeAmount
            // 
            this.upgradeAmount.Location = new System.Drawing.Point(149, 81);
            this.upgradeAmount.Name = "upgradeAmount";
            this.upgradeAmount.Size = new System.Drawing.Size(100, 22);
            this.upgradeAmount.TabIndex = 3;
            // 
            // upgradeAmountLabel
            // 
            this.upgradeAmountLabel.AutoSize = true;
            this.upgradeAmountLabel.Location = new System.Drawing.Point(30, 85);
            this.upgradeAmountLabel.Name = "upgradeAmountLabel";
            this.upgradeAmountLabel.Size = new System.Drawing.Size(115, 17);
            this.upgradeAmountLabel.TabIndex = 4;
            this.upgradeAmountLabel.Text = "Upgrade Amount";
            // 
            // upgradeCost
            // 
            this.upgradeCost.Location = new System.Drawing.Point(149, 110);
            this.upgradeCost.Name = "upgradeCost";
            this.upgradeCost.Size = new System.Drawing.Size(100, 22);
            this.upgradeCost.TabIndex = 5;
            // 
            // upgradeCostLabel
            // 
            this.upgradeCostLabel.AutoSize = true;
            this.upgradeCostLabel.Location = new System.Drawing.Point(33, 114);
            this.upgradeCostLabel.Name = "upgradeCostLabel";
            this.upgradeCostLabel.Size = new System.Drawing.Size(95, 17);
            this.upgradeCostLabel.TabIndex = 6;
            this.upgradeCostLabel.Text = "Upgrade Cost";
            // 
            // outputLabel
            // 
            this.outputLabel.AutoSize = true;
            this.outputLabel.Location = new System.Drawing.Point(30, 147);
            this.outputLabel.Name = "outputLabel";
            this.outputLabel.Size = new System.Drawing.Size(0, 17);
            this.outputLabel.TabIndex = 7;
            // 
            // createUpgradeButton
            // 
            this.createUpgradeButton.Location = new System.Drawing.Point(69, 196);
            this.createUpgradeButton.Name = "createUpgradeButton";
            this.createUpgradeButton.Size = new System.Drawing.Size(148, 31);
            this.createUpgradeButton.TabIndex = 8;
            this.createUpgradeButton.Text = "Create Upgrade";
            this.createUpgradeButton.UseVisualStyleBackColor = true;
            this.createUpgradeButton.Click += new System.EventHandler(this.createUpgradeButton_Click);
            // 
            // UpgradeCreator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(282, 253);
            this.Controls.Add(this.createUpgradeButton);
            this.Controls.Add(this.outputLabel);
            this.Controls.Add(this.upgradeCostLabel);
            this.Controls.Add(this.upgradeCost);
            this.Controls.Add(this.upgradeAmountLabel);
            this.Controls.Add(this.upgradeAmount);
            this.Controls.Add(this.upgradeTypeLabel);
            this.Controls.Add(this.upgradeType);
            this.Controls.Add(this.Title);
            this.Name = "UpgradeCreator";
            this.Text = "Upgrade Creator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Title;
        private System.Windows.Forms.ComboBox upgradeType;
        private System.Windows.Forms.Label upgradeTypeLabel;
        private System.Windows.Forms.TextBox upgradeAmount;
        private System.Windows.Forms.Label upgradeAmountLabel;
        private System.Windows.Forms.TextBox upgradeCost;
        private System.Windows.Forms.Label upgradeCostLabel;
        private System.Windows.Forms.Label outputLabel;
        private System.Windows.Forms.Button createUpgradeButton;
    }
}

