using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Memory_Game
{
    public partial class Form1 : Form
    {
        // This sets all the objects and icons to random for each of the squares
        Random random = new Random();

        // Each string represents an icon
        // In addition, each string is shown 4 times because there needs to be 4 matching icons
        // We will have a total of 14 different icons
        List<string> icons = new List<string>()
    {
        "2", "2", "3", "3", "4", "4", "5", "5",
        "6", "6", "7", "7", "8", "8", "9", "9",
        "10", "10", "11", "11", "12", "12", "13", "13",
        "14", "14", "15", "15",
        "2", "2", "3", "3", "4", "4", "5", "5",
        "6", "6", "7", "7", "8", "8", "9", "9",
        "10", "10", "11", "11", "12", "12", "13", "13",
        "14", "14", "15", "15"

        };


        /// <summary>
        /// This randomizes the icons into each of the squares
        /// </summary>
        private void AssignIconsToSquares()
        {
            // The table consists of the 52 labels and the icon list consists of 14 distinct icons
            // This will randomize the icons in each of the labels in the table
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;
                if (iconLabel != null)
                {
                    int randomNumber = random.Next(icons.Count);
                    iconLabel.Text = icons[randomNumber];

                    iconLabel.ForeColor = iconLabel.BackColor;
                    icons.RemoveAt(randomNumber);
                }
            }
        }

        // firstClicked points to the first Label control that the player clicks, but it will be null if the player hasn't clicked a label yet
        Label firstClicked = null;

        // secondClicked points to the second Label control that the player clicks
        Label secondClicked = null;


        public Form1()
        {
            InitializeComponent();

            AssignIconsToSquares();
        }


        private void label_Click(object sender, EventArgs e)
        {
            // If the  The timer is only on after two non-matching icons have been shown to the player, so ignore any clicks if the timer is running
            if (timer1.Enabled == true)
                return;

            Label clickedLabel = sender as Label;

            if (clickedLabel != null)
            {
                // If there is an icon that has already appeared, and the player clicks it, then the program will ignore that click. It will not consider it as a first click or second click.
                if (clickedLabel.ForeColor == Color.Black)
                    return;

                // If firstClicked is available for the player to choose then the program knows this is the first click and the icon will appear in black color
                if (firstClicked == null)
                {
                    firstClicked = clickedLabel;
                    firstClicked.ForeColor = Color.Black;

                    return;
                }

                // Following the previous if statement, If the secondClicked is available for the player to choose then the program knows this is the second click and the icon will appear in black color
                secondClicked = clickedLabel;
                secondClicked.ForeColor = Color.Black;

                // Check to see if the player won by seeing if the icons match
                CheckForWinner();

                // If the player clicked 2 icons that are matching, the program will keep the icons appearred in black color and it will reset firstClicked and secondClicked for the next round to start properly. 
                if (firstClicked.Text == secondClicked.Text)
                {
                    firstClicked = null;
                    secondClicked = null;
                    return;
                }

                // If the player chooses 2 icons that do not match, a timer will start which will wait 3 quarters of a second, then hide the 2 icons for the player to try again.
                timer1.Start();
            }
        }

        
            /// <summary>
            /// This timer starts once a player clicks 2 icons that do not match, which will then make the icons dissappear
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void timer1_Tick(object sender, EventArgs e)
            {
                // Stop the timer
                timer1.Stop();

                // Hide both icons
                firstClicked.ForeColor = firstClicked.BackColor;
                secondClicked.ForeColor = secondClicked.BackColor;

                // Reset firstClicked and secondClicked so the next time a label is clicked, the program knows it's the first click and it can rerun the if statements again and check for winner
                firstClicked = null;
                secondClicked = null;
            }

        
        
        /// <summary>
        /// To check that the play finished the game, check that every icon is already appeared from the previous code and that every icon's foreground color is the same as the background color.
        /// In addition, if the game is fully finished, a message box will appear to notify the player that they won!
        /// </summary>
        private void CheckForWinner()
        {
            // The program will check all the icons to make sure the foreground color and background color is the same, so it knows that all the icons are matched.
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;

                if (iconLabel != null)
                {
                    if (iconLabel.ForeColor == iconLabel.BackColor)
                        return;
                }
            }

            // If the player wins the game, show a message box to notify them that they won!
            MessageBox.Show("You have successfully matched all the icons in the game!", "Congratulations!");
            Close();
        }

        
    }
}
