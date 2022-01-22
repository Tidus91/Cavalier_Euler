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

        static int[,] echec = new int[12, 12];

        static int[] depi = new int[] { 2, 1, -1, -2, -2, -1, 1, 2 };
        static int[] depj = new int[] { 1, 2, 2, 1, -1, -2, -2, -1 };

        public Form2()
        {
            InitializeComponent();
            //codeProf();
        }

        private void codeProf()
        {
            int nb_fuite, min_fuite, lmin_fuite = 0;
            int i, j, k, l, ii, jj;

            Random random = new Random();
            ii = random.Next(1, 8);
            jj = random.Next(1, 8);
            // ii et jj evoluent de 1 à 8 !
            Console.WriteLine("Case de départ: " + ii + "  " + jj);

            for (i = 0; i < 12; i++)
                for (j = 0; j < 12; j++)
                    echec[i, j] = ((i < 2 | i > 9 | j < 2 | j > 9) ? -1 : 0);





            i = ii + 1; j = jj + 1;
            echec[i, j] = 1;

            for (k = 2; k <= 64; k++)
            {
                for (l = 0, min_fuite = 11; l < 8; l++)
                {
                    ii = i + depi[l]; jj = j + depj[l];

                    nb_fuite = ((echec[ii, jj] != 0) ? 10 : fuite(ii, jj));

                    if (nb_fuite < min_fuite)
                    {
                        min_fuite = nb_fuite; lmin_fuite = l;
                    }
                }
                if (min_fuite == 9 & k != 64)
                {
                    Console.WriteLine("***   IMPASSE   ***");
                    break;
                }
                i += depi[lmin_fuite]; j += depj[lmin_fuite];
                echec[i, j] = k;
            }
            for (i = 2; i < 10; i++)
            {
                for (j = 2; j < 10; j++)
                {
                    Console.Write(echec[i, j] + " ");
                }
                Console.WriteLine();
            }
            Console.ReadKey();
        }

        static int fuite(int i, int j)
        {
            int n, l;

            for (l = 0, n = 8; l < 8; l++)
                if (echec[i + depi[l], j + depj[l]] != 0) n--;

            return (n == 0) ? 9 : n;
        }
    }
}
