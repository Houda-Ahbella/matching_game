using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace matching_game
{
    public partial class Form1 : Form
    {
        
        Random random = new Random();

        // listes des icones
        List<string> icons = new List<string>()
        {
             "!", "!", "N", "N", ",", ",", "k", "k",
             "b", "b", "v", "v", "w", "w", "z", "z"
        };

        // firstClicked points to the first Label control 
        
        Label firstClicked = null;

        // secondClicked points to the second Label control 
       
        Label secondClicked = null;

        int timeLeft=10;

        private void AssignIconsToSquares()
        {
            // remplir les cases de tableLayoutPanel
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
        public Form1()
        {
            InitializeComponent();

            AssignIconsToSquares();
        }

        private void label_Click(object sender, EventArgs e)
        {
            //label cliquée
            Label clickedLabel = sender as Label;
           
            if (timer1.Enabled == true)
                return;
            

            if (clickedLabel != null)
            {
                // le joueur a cliqué sur un label déja cliqué 
                if (clickedLabel.ForeColor == Color.Black)
                    return;

                // si c'est la premier clique 
                if (firstClicked == null)
                {
                    firstClicked = clickedLabel;
                    firstClicked.ForeColor = Color.Black;
                    return;
                }

                // si c'est la deuxieme clique 
                secondClicked = clickedLabel;
                secondClicked.ForeColor = Color.Black;
                // tester si le joueur reussi d'afficher tous
               CheckForWinner();
                if (firstClicked.Text == secondClicked.Text)
                {
                    firstClicked = null;
                    secondClicked = null;
                    return;
                }

                timer1.Start();
            }
        }
        private void CheckForWinner()
        {
            // Parcourir toutes les labels du the TableLayoutPanel, 
            // tester si les icon is matched
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;

                if (iconLabel != null)
                {
                    if (iconLabel.ForeColor == iconLabel.BackColor)
                        return;
                }
            }

            // si  il y pas unmatched icons
            // le jeu est s'arrete et le joueur a gagné
            MessageBox.Show("You matched all the icons!", "Congratulations");
            Close();
        }
        private void timer1_click(object sender, EventArgs e)
        {
            // Stop the timer
            timer1.Stop();

            // Hide both icons
            firstClicked.ForeColor = firstClicked.BackColor;
            secondClicked.ForeColor = secondClicked.BackColor;

            
            // les deux labels retournent invisible  
            firstClicked = null;
            secondClicked = null;
        }

        
    }
}
