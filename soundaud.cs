using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace Cyanohydrins
{
    internal class soundaud
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool Beep(uint dwFreq, uint dwDuration);

        public static Random r = new Random();

        public static void Audio()
        {
            MemoryStream[] sounds =
            {
                new MemoryStream(Properties.Resources.glitch)
            };
            int[] d =
            {
               126000,
            };

            while (true)
            {
                int sound = r.Next(0, 1);
                sounds[sound].Position = 0;
                SoundPlayer player = new SoundPlayer(sounds[sound]);
                player.Play();
                Thread.Sleep(d[sound]);
            }
        }

        public static void Beeps()
        {
            while (true)
            {
                Beep((uint)r.Next(38, 126000), (uint)r.Next(1, 1000));
                Thread.Sleep(r.Next(1, 4000));
            }
        }
    }
}