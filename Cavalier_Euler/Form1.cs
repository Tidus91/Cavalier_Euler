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
    public partial class Form1 : Form
    {
        Button[] grille;
        Image cavalier;
        List<int> coups;
        List<int> coupsFutur;
        int compteur;
        int compteurAnnulation;
        bool stateInit;
        bool trigger;
        bool cheatCode;
        int position;
        Form form2;

        public Form1()
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
            for (int i=0; i < grille.Length; ++i)
            {
                grille[i] = new Button();
                grille[i].Text = "";
                grille[i].Location = new System.Drawing.Point(x, y);
                grille[i].Name = "button" + (i + 1);
                grille[i].Size = new System.Drawing.Size(60, 60);
                grille[i].TabIndex = i;
                grille[i].UseVisualStyleBackColor = true;
                grille[i].Click += new System.EventHandler(this.button1_Click);
                grille[i].Visible = false;
                this.Controls.Add(grille[i]);
                x += 60;
                if ((i + 1) % 8 == 0 && i != 0)
                {
                    y += 60;
                    x = 2;
                }
            }
            form2 = new Form2();
            
        }

        private void fcheatCode()
        {
            // on reset les precedentes cases d'aide
            for (int i = 0; i < grille.Length; ++i)
            {
                // colorié en bleu les autres cases
                if(grille[i].Text == "")
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

        private void Form1_Load(object sender, EventArgs e)
        {
            label1.Text = "Bonjour ! \n Souhaitez vous jouer une partie ? ou faire une Simulation ?";
            button101.Text = "Jouer une partie !";
            button102.Text = "Faire une simulation !";
            label2.Text = "Voulez vous choisir ou commencer ou aléatoire ?";
            label2.Visible = false;
            button103.Text = "Choisir manuellement";
            button103.Visible = false;
            button104.Text = "Choisir aléatoirement";
            button104.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Button myButton = (Button)sender;
            if (!stateInit)
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
                label2.Text = "Ou voulez vous jouer ensuite ?  :  " + compteur + "ème coups !";
                button104.Enabled = false;
                testPositionAvailable();
                coups.Add(position);
            }
            else
            {
                int myIndice = getIndiceButton(myButton); 
                if(myIndice < 0 || myIndice > 63)
                {
                    label2.Text = "gros bug, mais c'est impossbile";
                }
                if (testCoup(myIndice) == 0)
                {
                    //faire quoi si le deplacement est pas bon
                    label2.Text = "le deplacement n'est pas valide !";
                    return;
                }
                compteur++;
                label2.Text = "Ou voulez vous jouer ensuite ?  :  " + compteur + "ème coups !";
                grille[position].BackgroundImage = null;
                myButton.BackgroundImage = cavalier;
                myButton.BackgroundImageLayout = ImageLayout.Stretch;
                position = myIndice;
                myButton.Text = compteur.ToString();
                myButton.BackColor = Color.Yellow;
                myButton.Enabled = false;
                coups.Add(position);
                if (testPositionAvailable() == 0)
                    label2.Text = "Fin du game, vous avez perdu en " + compteur + " essais !";
                //finDuGame();
                
            }

        }

        private void finDuGame()
        {
            coups = new List<int>();
            compteur = 0; //nombre de coups
            stateInit = false;
            position = 0; // ou est mon cavalier
            trigger = false;
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
                grille[i].Visible = false;
                this.Controls.Add(grille[i]);
                x += 60;
                if ((i + 1) % 8 == 0 && i != 0)
                {
                    y += 60;
                    x = 2;
                }
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

        private int testCoup(int indice)
        {
            // position de mon coups actuel dans la grille
            int x = indice % 8;
            int y = indice / 8;

            // position de mon cavalier 
            int px = position % 8;
            int py = position / 8;

            // algo qui check le déplacement du cavalier
            if( (x == px+2 && y == py+1) || (x == px+2 && y == py-1) || (x == px-2 && y == py+1) || (x == px-2 && y == py-1) || (x == px+1 && y == py+2) || (x == px+1 && y == py-2) || (x == px-1 && y == py+2) || (x == px-1 && y == py-2) )
            {
                return 1;
            }
            //label2.Visible = true;
            //label2.Text = "x : " + x + " y : " + y;
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
                
            button104.Visible = true;
            button104.Text = nbPos.ToString() + " coups dispo";
            return nbPos;

        }

        private bool testLoose()
        {
            int px = position % 8;
            int py = position / 8;

            //-17; -15; -10; -6; +6 +10 +15 +17

            return true;
        }

        private void button102_Click(object sender, EventArgs e)
        {
            Button myButton = (Button)sender;
            if (myButton.Name == "button102")
            {
                myButton.Text = "je suis cliquer !";
                form2.Show();
                form2.Size = new Size(1124, 965);
                form2.BackColor = Color.FromArgb(162,105,80);

            }
               

            else if (myButton.Name == "button101")
            {
                myButton.Visible = false;
                foreach(Button b in grille){
                    b.Visible = true;
                    b.Enabled = false;
                }
                label1.Visible = false;
                button102.Visible = false;
                label2.Visible = true;
                button103.Visible = true;
                button104.Visible = true;
            }
                
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {

            if (compteur > 1 && compteurAnnulation > 0)
            {
                int lastI = (coups.Count - 1);

                grille[coups[lastI]].Enabled = true;
                grille[coups[lastI]].Text = "";
                grille[coups[lastI]].BackColor = default;
                grille[coups[lastI]].BackgroundImage = null;
                //grille[lastI].BackgroundImageLayout = ImageLayout.Stretch;
                grille[coups[lastI - 1]].BackgroundImage = cavalier;


                coups.RemoveAt(lastI);
                position = grille[coups[lastI - 1]].TabIndex;
                compteur--;
                compteurAnnulation--;
                testPositionAvailable();
                Control [] myLabel = this.Controls.Find("labelCancel",false);
                myLabel[0].Text = "Il vous reste : " + compteurAnnulation + " retour possible";

            }
        }

        private void buttonCheat_Click(object sender, EventArgs e)
        {
            Button myButton = (Button)sender;
            if (!cheatCode)
            {
                cheatCode = true;
                testPositionAvailable();
                Control[] myLabel = this.Controls.Find("labelCheat", false);
                myLabel[0].Text = "l'aide est activé !";
                return;
            }
            else
            {
                cheatCode = false;
                for(int i = 0; i < grille.Length; ++i)
                {
                    if (!(coups.Contains(grille[i].TabIndex)))
                        grille[i].BackColor = default;
                }
                Control[] myLabel = this.Controls.Find("labelCheat", false);
                myLabel[0].Text = "l'aide n'est activé.";
            }
            
            //myButton.Visible = false;
        }

        private void lancerSimulation()
        {

        }

        private void button103_Click(object sender, EventArgs e)
        {
            Button myButton = (Button)sender;
            if (myButton.Name == "button103")
            {
                label2.Text = "Veuillez cliquer sur la case ou vous souhaitez commencer";
                foreach (Button b in grille)
                {
                    b.Enabled = true;
                }

            }
            else if (myButton.Name == "button104")
            {
                Random r = new Random();
                int rd = r.Next(0, 63);
                
                myButton.Visible = false;
                foreach (Button b in grille)
                {
                    b.Enabled = true;
                }
                stateInit = true;
                compteur++;
                label2.Text = "Ou voulez vous jouer ensuite ?  :  " + compteur + "ème coups !";
                position = rd;
                coups.Add(position);
                grille[position].Text = compteur.ToString();
                grille[position].BackColor = Color.Yellow;
                grille[position].BackgroundImage = cavalier;
                grille[position].BackgroundImageLayout = ImageLayout.Stretch;
                grille[position].Enabled = false;
                

                
            }
            creationButtonsCheatAndCancel();
            button103.Visible = false;
            button104.Visible = false;
            button104.Enabled = false;
        }

        private void creationButtonsCheatAndCancel()
        {
            Button buttonCheat = new Button();
            buttonCheat.Text = "Activé/désactivé cheat";
            buttonCheat.Location = new Point(600, 250);
            buttonCheat.Name = "buttonCheat";
            buttonCheat.Size = new Size(194, 23);
            buttonCheat.UseVisualStyleBackColor = true;
            buttonCheat.Click += new System.EventHandler(this.buttonCheat_Click);
            buttonCheat.Visible = true;
            this.Controls.Add(buttonCheat);

            Label labelCheat = new Label();
            labelCheat.Text = "l'aide n'est pas activé";
            labelCheat.Location = new Point(600, 200);
            labelCheat.Name = "LabelCheat";
            labelCheat.Size = new Size(194, 23);
            labelCheat.Visible = true;
            this.Controls.Add(labelCheat);



            Button buttonCancel = new Button();
            buttonCancel.Text = "annulé coup précédent";
            buttonCancel.Location = new Point(800, 250);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(194, 23);
            buttonCancel.UseVisualStyleBackColor = true;
            buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            buttonCancel.Visible = true;
            this.Controls.Add(buttonCancel);

            Label labelCancel = new Label();
            labelCancel.Text = "Il vous reste : " + compteurAnnulation + " retour possible";
            labelCancel.Location = new Point(800, 200);
            labelCancel.Name = "labelCancel";
            labelCancel.Size = new Size(194, 23);
            labelCancel.Visible = true;
            this.Controls.Add(labelCancel);
        }

    }

}
