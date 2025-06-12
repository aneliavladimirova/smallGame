using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace smallGame
{
    public partial class Form3 : Form
    {
        int br = 10;
        Random r = new Random();
        int targetNumber;

        int playerWins = 0;
        int computerWins = 0;

        Button firstClicked = null;
        Button secondClicked = null;
        Random rng = new Random();
        List<int> cardValues = new List<int>();

        public Form3()
        {
            InitializeComponent();
            targetNumber = r.Next(1, 10);

            panel1.Visible = false; // Скриваме панела по подразбиране

            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;

            comboBox1.SelectedIndex = 0;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 1)
            {
                panel1.Visible = true;
                InitMemoryGame();
            }
            else
            {
                panel1.Visible = false;
            }

            // Изчистваме label4 при избор на "Познай числото" или "Камък ножица хартия"
            if (comboBox1.SelectedIndex == 0 || comboBox1.SelectedIndex == 2)
            {
                label4.Text = "";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                label4.Text = "";
                int guess;
                if (!int.TryParse(textBox1.Text, out guess))
                {
                    MessageBox.Show("Невалидно число");
                    return;
                }

                if (guess > targetNumber)
                {
                    label2.Text = "Надолу";
                }
                else if (guess < targetNumber)
                {
                    label2.Text = "Нагоре";
                }
                else
                {
                    label4.Text = "Позна числото!";
                    return;
                }

                br--;
                label3.Text = br.ToString();

                if (br == 0)
                {
                    label4.Text = "Играта приключи. Числото беше: " + targetNumber;
                    MessageBox.Show("Числото беше: " + targetNumber);
                }
            }
            else if (comboBox1.SelectedIndex == 2)
            {
                label4.Text = "";
                string playerChoice = textBox1.Text.ToLower(); // камък, ножица, хартия
                string[] options = { "камък", "ножица", "хартия" };
                string computerChoice = options[r.Next(0, 3)];

                label2.Text = $"Ти: {playerChoice} | Компютър: {computerChoice}";

                if (playerChoice == computerChoice)
                {
                    label4.Text = $"Равенство!\nТи: {playerWins} | Компютър: {computerWins}";
                }
                else if (
                    (playerChoice == "камък" && computerChoice == "ножица") ||
                    (playerChoice == "ножица" && computerChoice == "хартия") ||
                    (playerChoice == "хартия" && computerChoice == "камък")
                )
                {
                    playerWins++;
                    label4.Text = $"Ти спечели рунда!\nТи: {playerWins} | Компютър: {computerWins}";
                }
                else
                {
                    computerWins++;
                    label4.Text = $"Компютърът спечели рунда!\nТи: {playerWins} | Компютър: {computerWins}";
                }

                if (playerWins == 3)
                {
                    MessageBox.Show("Ти спечели играта!");
                    label4.Text = "ТИ СПЕЧЕЛИ ИГРАТА!!!";
                    playerWins = 0;
                    computerWins = 0;
                }
                else if (computerWins == 3)
                {
                    MessageBox.Show("КОМПЮТЪРЪТ СПЕЧЕЛИ ИГРАТА!");
                    label4.Text = "КОМПЮТЪРЪТ СПЕЧЕЛИ ИГРАТА!!!";
                    playerWins = 0;
                    computerWins = 0;
                }
            }
        }

        private void InitMemoryGame()
        {
            cardValues = new List<int> { 1, 1, 2, 2, 3, 3 };
            cardValues = cardValues.OrderBy(x => rng.Next()).ToList();

            int i = 0;

            foreach (Control c in panel1.Controls) // Взимаме бутоните вътре в panel1!
            {
                if (c is Button && c != button1)
                {
                    c.Text = "";
                    c.Tag = cardValues[i];
                    c.Click -= Card_Click; // предпазваме се от дублирани event-и
                    c.Click += Card_Click;
                    i++;
                }
            }
        }

        private void Card_Click(object sender, EventArgs e)
        {
            if (firstClicked != null && secondClicked != null)
                return;

            Button clickedButton = sender as Button;
            if (clickedButton == null || clickedButton.Text != "")
                return;

            clickedButton.Text = clickedButton.Tag.ToString();

            if (firstClicked == null)
            {
                firstClicked = clickedButton;
                return;
            }

            secondClicked = clickedButton;

            if (firstClicked.Tag.ToString() == secondClicked.Tag.ToString())
            {
                firstClicked = null;
                secondClicked = null;
            }
            else
            {
                Timer t = new Timer();
                t.Interval = 1000;
                t.Tick += (s, args) =>
                {
                    firstClicked.Text = "";
                    secondClicked.Text = "";
                    firstClicked = null;
                    secondClicked = null;
                    t.Stop();
                };
                t.Start();
            }
            
        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            
                // Изчистваме всички основни label-и при смяна на играта
                label2.Text = "";
                label3.Text = "";
                label4.Text = "";
            textBox1.Text = "";
            if (comboBox1.SelectedIndex == 1)
                {
                    panel1.Visible = true;
                panel2.Visible = false ;
                InitMemoryGame();
                }
                else
                {
                    panel1.Visible = false;
                panel2.Visible = true;
                }
            

        }
    }
}
