using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace UpgradeCreator
{
    public partial class UpgradeCreator : Form
    {
        string filePath = (Application.StartupPath + "\\UpgradesFile.txt").ToString();

        public UpgradeCreator()
        {
            InitializeComponent();
        }

        private void createUpgradeButton_Click(object sender, EventArgs e)
        {
            CreateUpgrade();
        }

        void CreateUpgrade()
        {
            if(upgradeType.SelectedItem != null)
            {
                if(upgradeAmount.Text != string.Empty && int.Parse(upgradeAmount.Text) > 0)
                {
                    if(upgradeCost.Text != string.Empty && int.Parse(upgradeCost.Text) > 0)
                    {
                        outputLabel.Text = filePath;
                    }
                    else
                    {
                        outputLabel.Text = "You must enter an upgrade cost\n greater than 0";
                    }
                }
                else
                {
                    outputLabel.Text = "You must enter an upgrade value\ngreater than 0";
                }
            }
            else
            {
                outputLabel.Text = "You must select an upgrade Type.";
            }
        }
    }
}
