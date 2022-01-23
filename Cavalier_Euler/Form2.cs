using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cavalier_Euler
{
    public partial class Form2 : Form
    {

        Button[] grille;
        Button[] copieGrille;
        Image cavalier;
        List<int> coups;
        List<int> coupsFutur;
        int compteur;
        int compteurAnnulation;
        bool stateInit;
        bool trigger;
        bool cheatCode;
        int position;
        int fuiteMin;
        int fuiteMinIndex;

        public Form2()
        {
            coups = new List<int>();
            coupsFutur = new List<int>();
            cavalier = Image.FromFile("img\\cavalierEchec.jpg");
            compteur = 0; //nombre de coups
            compteurAnnulation = 10;
            stateInit = false;
            position = 0; // ou est mon cavalier
            trigger = false;
            cheatCode = false;
            InitializeComponent();
            int x = 2;
            int y = 2;
            this.grille = new Button[64];
            for (int i = 0; i < grille.Length; ++i)
            {
                grille[i] = new Button();
                grille[i].Text = "";
                grille[i].Location = new System.Drawing.Point(x, y);
                grille[i].Name = "button" + (i + 1);
                grille[i].Size = new System.Drawing.Size(60, 60);
                grille[i].TabIndex = i;
                grille[i].UseVisualStyleBackColor = true;
                grille[i].Click += new System.EventHandler(this.button1_Click);
                grille[i].Visible = true;
                this.Controls.Add(grille[i]);
                x += 60;
                if ((i + 1) % 8 == 0 && i != 0)
                {
                    y += 60;
                    x = 2;
                }
            }
            fuiteMin = 0;
            fuiteMinIndex = 0;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Button myButton = (Button)sender;
            if(compteur == 0)
            {
                int myIndice = getIndiceButton(myButton);
                compteur++;
                position = myIndice;
                coups.Add(position);
                myButton.Text = compteur.ToString();
                myButton.BackgroundImage = cavalier;
                myButton.BackgroundImageLayout = ImageLayout.Stretch;
                myButton.BackColor = Color.Yellow;
                myButton.Enabled = false;
                stateInit = true;
                label1.Text = "Veuillez choisir le mode de simulation";
                listBox1.Visible = true;
                listBox2.Visible = true;
                testPositionAvailable();
                coups.Add(position);

                Button buttonGo = new Button();
                buttonGo.Text = "Go";
                buttonGo.Location = new System.Drawing.Point(600, 300);
                buttonGo.Name = "buttonGo";
                buttonGo.Size = new System.Drawing.Size(60, 60);
                buttonGo.UseVisualStyleBackColor = true;
                buttonGo.Click += new System.EventHandler(this.buttonGo_Click);
                buttonGo.Visible = true;
                this.Controls.Add(buttonGo);
            }
            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            label1.Text = "Veuillez cliquer sur la case ou vous souhaitez commencer";
            foreach (Button b in grille)
            {
                b.Enabled = true;
            }
            button1.Visible = false;
            button2.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Button myButton = (Button)sender;

            Random r = new Random();
            int rd = r.Next(0, 63);

            myButton.Visible = false;
            foreach (Button b in grille)
            {
                b.Enabled = true;
            }
            stateInit = true;
            compteur++;
            label1.Text = "Veuillez choisir le mode de simulation";
            position = rd;
            coups.Add(position);
            grille[position].Text = compteur.ToString();
            grille[position].BackColor = Color.Yellow;
            grille[position].BackgroundImage = cavalier;
            grille[position].BackgroundImageLayout = ImageLayout.Stretch;
            grille[position].Enabled = false;
            button1.Visible = false;
            button2.Visible = false;
            listBox1.Visible = true;
            listBox2.Visible = true;

            Button buttonGo = new Button();
            buttonGo.Text = "Go";
            buttonGo.Location = new System.Drawing.Point(600, 300);
            buttonGo.Name = "buttonGo";
            buttonGo.Size = new System.Drawing.Size(60, 60);
            buttonGo.UseVisualStyleBackColor = true;
            buttonGo.Click += new System.EventHandler(this.buttonGo_Click);
            buttonGo.Visible = true;
            this.Controls.Add(buttonGo);

        }

        private void buttonGo_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem == null || listBox2.SelectedItem == null)
                return;
            label1.Text = "sa marche !!!!!";
            string type = listBox1.SelectedItem.ToString();
            string time = listBox2.SelectedItem.ToString();

            if(type == "pas-a-pas")
            {
                Task.Delay(20000000);
                label1.Text = "omggggggggggggg";
                algoEuler();
            }
        }


        private int getIndiceButton(Button bp)
        {
            for (int i = 0; i < grille.Length; ++i)
            {
                if (bp.Name == grille[i].Name)
                {
                    return i;
                }
            }
            return 0;
        }

        private int testPositionAvailable()
        {
            int nbPos = 0;
            coupsFutur.Clear();
            for (int i = 0; i < 64; ++i)
            {
                if ((testCoup(i) == 1) && (grille[i].Enabled == true))
                {
                    //Console.WriteLine("i = " + i);
                    nbPos++;
                    coupsFutur.Add(i);
                }
                if (cheatCode == true)
                    fcheatCode();

            }
            return nbPos;

        }

        private void fcheatCode()
        {
            // on reset les precedentes cases d'aide
            for (int i = 0; i < grille.Length; ++i)
            {
                // colorié en bleu les autres cases
                if (grille[i].Text == "")
                    grille[i].BackColor = default;
                // colorié en jaune les cases déja joué
                if (coups.Contains(grille[i].TabIndex))
                    grille[i].BackColor = Color.Yellow;
            }
            // on marque les cases ou on peut se deplacer
            for (int i = 0; i < coupsFutur.Count; ++i)
            {
                grille[coupsFutur[i]].BackColor = Color.Red;
            }
        }

        private int testCoup(int indice)
        {
            // position de mon coups actuel dans la grille
            int x = indice % 8;
            int y = indice / 8;

            // position de mon cavalier 
            int px = position % 8;
            int py = position / 8;

            // algo qui check le déplacement du cavalier
            if ((x == px + 2 && y == py + 1) || (x == px + 2 && y == py - 1) || (x == px - 2 && y == py + 1) || (x == px - 2 && y == py - 1) || (x == px + 1 && y == py + 2) || (x == px + 1 && y == py - 2) || (x == px - 1 && y == py + 2) || (x == px - 1 && y == py - 2))
            {
                return 1;
            }
            //label2.Visible = true;
            //label2.Text = "x : " + x + " y : " + y;
            return 0;
        }

        private void algoEuler()
        {
            testPositionAvailable();
            if(compteur == 64)
            {
                label1.Text = "c'est gagné !!";
                return;
            }
            if(coupsFutur.Count == 0)
            {
                label1.Text = "gros bug, c'est bizarre";
                return;
            }

            List<int> N = new List<int>(coupsFutur); //liste des numéro de mes cases possible à l'étape N ## (car sinon je vais les perdre en rappelant la fonction testPosition)

            fuiteMin = testPositionAvailableEuler(N[0]);
            fuiteMinIndex = 0;

            // pour chacun d'entre eux, je dois regarder quelles cases sont disponible
            for (int i =0; i < N.Count; ++i)
            {
                if (testPositionAvailableEuler(N[i]) < fuiteMin)    // la dans coupsFutur j'ai donc mes cases à l'étape n+1;
                {
                    
                    fuiteMin = testPositionAvailableEuler(N[i]);
                    fuiteMinIndex = i;
                }
            }
            jouerButton(N[fuiteMinIndex]);
            
        }

        private int testPositionAvailableEuler(int N)
        {
            int temp = position;
            position = N;

            int nbPos = 0;
            coupsFutur.Clear();
            for (int i = 0; i < 64; ++i)
            {
                if ((testCoup(i) == 1) && (grille[i].Enabled == true))
                {
                    //Console.WriteLine("i = " + i);
                    nbPos++;
                    coupsFutur.Add(i);
                }
                if (cheatCode == true)
                    fcheatCode();

            }
            position = temp;

            return nbPos;

        }

        private void jouerButton(int buttonP)
        {
            compteur++;
            grille[position].BackgroundImage = null;
            grille[buttonP].BackgroundImage = cavalier;
            grille[buttonP].BackgroundImageLayout = ImageLayout.Stretch;
            position = buttonP;
            grille[buttonP].Text = compteur.ToString();
            grille[buttonP].BackgroundImage = cavalier;
            grille[buttonP].BackColor = Color.Yellow;
            grille[buttonP].Enabled = false;
            coups.Add(buttonP);

            return;

        }
    }

}
