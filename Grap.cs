using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Cyanohydrins
{

    internal class Grap
    {
        [DllImport("user32.dll")]
        public static extern IntPtr GetDC(IntPtr hWnd);

        [DllImport("gdi32.dll")]
        public static extern IntPtr DeleteDC(IntPtr hdc);

        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateSolidBrush(uint color);

        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateCompatibleDC(IntPtr hdc);

        [DllImport("user32.dll")]
        private static extern int GetSystemMetrics(int nIndex);

        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateCompatibleBitmap(IntPtr hdc, int cx, int cy);

        [DllImport("gdi32.dll", SetLastError = true, EntryPoint = "GdiAlphaBlend")]
        public static extern bool AlphaBlend(IntPtr hdcDest, int xoriginDest, int yoriginDest, int wDest, int hDest, IntPtr hdcSrc, int xoriginSrc, int yoriginSrc, int wSrc, int hSrc, BLENDFUNCTION ftn);

        [DllImport("gdi32.dll", SetLastError = true)]
        public static extern bool BitBlt(IntPtr hdc, int x, int y, int cx, int cy, IntPtr hdcSrc, int x1, int y1, uint rop);

        [DllImport("user32.dll")]
        public static extern int ReleaseDC(IntPtr hwnd, IntPtr hdc);

        public const uint SRCCOPY = 0x00CC0020;
        public const uint SRCPAINT = 0x00EE0086;
        public const uint SRCAND = 0x008800C6;
        public const uint SRCINVERT = 0x00660046;
        public const uint SRCERASE = 0x00440328;
        public const uint NOTSRCCOPY = 0x00330008;
        public const uint NOTSRCERASE = 0x001100A6;
        public const uint MERGECOPY = 0x00C000CA;
        public const uint MERGEPAINT = 0x00BB0226;
        public const uint PATCOPY = 0x00F00021;
        public const uint PATPAINT = 0x00FB0A09;
        public const uint PATINVERT = 0x005A0049;
        public const uint DSTINVERT = 0x00550009;
        public const uint BLACKNESS = 0x00000042;
        public const uint WHITENESS = 0x00FF0062;
        public const uint CAPTUREBLT = 0x40000000;
        public const uint CUSTOM = 0x00100C85;
        private const byte AC_SRC_OVER = 0x00;
        private const int SM_CXSCREEN = 0;
        private const int SM_CYSCREEN = 1;

        [DllImport("gdi32.dll", SetLastError = true)]
        public static extern bool Rectangle(IntPtr hdc, int left, int top, int right, int bottom);

        [DllImport("gdi32.dll", SetLastError = true)]
        public static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);

        [DllImport("gdi32.dll", SetLastError = true)]
        public static extern IntPtr DeleteObject(IntPtr ho);

        [DllImport("gdi32.dll")]
        public static extern bool PatBlt(IntPtr hdc, int x, int y, int width, int height, uint rop);

        [DllImport("user32.dll")]
        public static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);
        public delegate bool EnumWindowsProc(IntPtr hwnd, IntPtr lParam);

        [DllImport("gdi32.dll")]
        public static extern bool StretchBlt(IntPtr hdcDest, int nXOriginDest, int nYOriginDest, int nWidthDest, int nHeightDest,
        IntPtr hdcSrc, int nXOriginSrc, int nYOriginSrc, int nWidthSrc, int nHeightSrc, uint dwRop);

        [DllImport("user32.dll")]
        public static extern IntPtr GetDesktopWindow();

        [DllImport("user32.dll")]
        public static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        [DllImport("gdi32.dll")]
        public static extern bool PlgBlt(IntPtr hdcDest, POINT[] lpPoint, IntPtr hdcSrc,
            int nXSrc, int nYSrc, int nWidth, int nHeight, IntPtr hbmMask, int xMask, int yMask);

        [DllImport("user32.dll")]
        public static extern bool RedrawWindow(IntPtr hWnd, IntPtr lprcUpdate, IntPtr hrgnUpdate, uint flags);

        [DllImport("user32.dll")]
        public static extern IntPtr LoadIcon(IntPtr hInstance, IntPtr lpIconName);

        [DllImport("user32.dll")]
        public static extern bool DrawIcon(IntPtr hdc, int x, int y, IntPtr hIcon);

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        private static uint x_seed = 123456789;
        public static void SeedXorshift32(uint seed)
        {
            if (seed != 0) x_seed = seed;
        }

        public static uint Xorshift32()
        {
            uint x = x_seed;
            x ^= x << 13;
            x ^= x >> 17;
            x ^= x << 5;
            return x_seed = x;
        }

        public static uint RGB(byte r, byte g, byte b)
        {
            return (uint)((b << 16) | (g << 8) | r);
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct BLENDFUNCTION
        {
            public byte BlendOp;
            public byte BlendFlags;
            public byte SourceConstantAlpha;
            public byte AlphaFormat;
        }

        [DllImport("user32.dll")]
        public static extern bool InvalidateRect(IntPtr hWnd, IntPtr lpRect, bool bErase);

        public static Random rand = new Random();

        public static uint GetRandomHEXColor()
        {
            byte b = (byte)rand.Next(255);
            byte g = (byte)rand.Next(255);
            byte r = (byte)rand.Next(255);
            return (uint)((b << 16) | (g << 8) | r);
        }
        public static void GDI1()
        {
            DateTime startTime = DateTime.Now;
            TimeSpan maxDuration = TimeSpan.FromSeconds(20);
            while (DateTime.Now - startTime < maxDuration)
            {
                Random rand = new Random();
                int sw = GetSystemMetrics(0);
                int sh = GetSystemMetrics(1);
                int rx;
                IntPtr hdc = GetDC(IntPtr.Zero);
                rx = rand.Next(sw);
                int ry = rand.Next(sh);
                BitBlt(hdc, 10, ry, sw, 96, hdc, 0, ry, SRCCOPY);
                BitBlt(hdc, -10, ry, sw, -96, hdc, 0, ry, SRCCOPY);
                ReleaseDC(IntPtr.Zero, hdc);
                if (rand.Next(10) == 0)
                {
                    InvalidateRect(IntPtr.Zero, IntPtr.Zero, true);
                }
            }
        }
        public static void GDI1or1()
        {
            DateTime startTime = DateTime.Now;
            TimeSpan maxDuration = TimeSpan.FromSeconds(10);
            int w = GetSystemMetrics(SM_CXSCREEN);
            int h = GetSystemMetrics(SM_CYSCREEN);
            while (DateTime.Now - startTime < maxDuration)
            {
                IntPtr hdc = GetDC(IntPtr.Zero);
                for (int i = 0; i < 5; i++)
                {
                    IntPtr hdcCopy = CreateCompatibleDC(hdc);
                    int nBlockSize = rand.Next(128, 257);
                    int nNewBlockSize = nBlockSize + rand.Next(16, 33);
                    int x = rand.Next(0, Math.Max(1, w - nBlockSize));
                    int y = rand.Next(0, Math.Max(1, h - nBlockSize));
                    uint rop = (rand.Next(2) == 0) ? SRCAND : SRCPAINT;
                    StretchBlt(hdc, x - (nNewBlockSize - nBlockSize) / 2, y - (nNewBlockSize - nBlockSize) / 2,
                               nNewBlockSize, nNewBlockSize, hdc, x, y, nBlockSize, nBlockSize, rop);
                    StretchBlt(hdcCopy, x - (nNewBlockSize - nBlockSize) / 2, y - (nNewBlockSize - nBlockSize) / 2,
                               nNewBlockSize, nNewBlockSize, hdc, x, y, nBlockSize, nBlockSize, WHITENESS);
                    BLENDFUNCTION blend = new BLENDFUNCTION
                    {
                        BlendOp = AC_SRC_OVER,
                        BlendFlags = 0,
                        SourceConstantAlpha = 50,
                        AlphaFormat = 0
                    };
                    AlphaBlend(hdc, 0, 0, w, h, hdcCopy, 0, 0, w, h, blend);
                    DeleteDC(hdcCopy);
                }
                ReleaseDC(IntPtr.Zero, hdc);
            }
        }

        public static void Zer1()
        {
            DateTime startTime = DateTime.Now;
            TimeSpan maxDuration = TimeSpan.FromSeconds(30);
            while (DateTime.Now - startTime < maxDuration)
            {
                SeedXorshift32((uint)Environment.TickCount);
                IntPtr desk = GetDC(IntPtr.Zero);

                RECT wRect;
                GetWindowRect(GetDesktopWindow(), out wRect);

                POINT[] wPt = new POINT[3];
                int c = 100;

                wPt[0].X = wRect.Left + (int)(Xorshift32() % 110) - 50;
                wPt[0].Y = wRect.Top + (int)(Xorshift32() % 210) - 100;
                wPt[1].X = wRect.Right + (int)(Xorshift32() % 210) - 100;
                wPt[1].Y = wRect.Top + (int)(Xorshift32() % 410) - 200;
                wPt[2].X = wRect.Left + c - (int)(Xorshift32() % 210) - c;
                wPt[2].Y = wRect.Bottom - c + (int)(Xorshift32() % 210) - c;

                PlgBlt(desk, wPt, desk, wRect.Left, wRect.Top, wRect.Right - wRect.Left, wRect.Bottom - wRect.Top, IntPtr.Zero, 0, 0);
                ReleaseDC(IntPtr.Zero, desk);
                if (Xorshift32() % 5 == 1)
                {
                    RedrawWindow(IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, 133);
                }
            }
        }

        public static void Zer2()
        {
            DateTime startTime = DateTime.Now;
            TimeSpan maxDuration = TimeSpan.FromSeconds(30);
            while (DateTime.Now - startTime < maxDuration)
            {
                SeedXorshift32((uint)Environment.TickCount);
                IntPtr desk = GetDC(IntPtr.Zero);

                RECT wRect;
                GetWindowRect(GetDesktopWindow(), out wRect);

                POINT[] wPt = new POINT[3];
                int c = 100;

                wPt[0].X = wRect.Left - (int)(Xorshift32() % 110) + 50;
                wPt[0].Y = wRect.Top - (int)(Xorshift32() % 210) + 100;
                wPt[1].X = wRect.Right - (int)(Xorshift32() % 210) + 100;
                wPt[1].Y = wRect.Top - (int)(Xorshift32() % 410) + 200;
                wPt[2].X = wRect.Left - c + (int)(Xorshift32() % 210) + c;
                wPt[2].Y = wRect.Bottom + c - (int)(Xorshift32() % 210) + c;

                PlgBlt(desk, wPt, desk, wRect.Left, wRect.Top, wRect.Right - wRect.Left, wRect.Bottom - wRect.Top, IntPtr.Zero, 0, 0);
                ReleaseDC(IntPtr.Zero, desk);
                if (Xorshift32() % 5 == 1)
                {
                    RedrawWindow(IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, 133);
                }
            }
        }

        public static void GDI2()
        {
            DateTime startTime = DateTime.Now;
            TimeSpan maxDuration = TimeSpan.FromSeconds(30);
            while (DateTime.Now - startTime < maxDuration)
            {
                SeedXorshift32((uint)Environment.TickCount);
                IntPtr desk = GetDC(IntPtr.Zero);
                RECT wRect;
                GetWindowRect(GetDesktopWindow(), out wRect);
                POINT[] wPt = new POINT[3];
                int c = 100;
                wPt[0].X = wRect.Left - (int)(Xorshift32() % 110) + 50;
                wPt[0].Y = wRect.Top - (int)(Xorshift32() % 210) + 100;
                wPt[1].X = wRect.Right - (int)(Xorshift32() % 210) + 100;
                wPt[1].Y = wRect.Top - (int)(Xorshift32() % 410) + 200;
                wPt[2].X = wRect.Left - c + (int)(Xorshift32() % 210) + c;
                wPt[2].Y = wRect.Bottom + c - (int)(Xorshift32() % 210) + c;
                wPt[0].X = wRect.Left + (int)(Xorshift32() % 110) - 50;
                wPt[0].Y = wRect.Top + (int)(Xorshift32() % 210) - 100;
                wPt[1].X = wRect.Right + (int)(Xorshift32() % 210) - 100;
                wPt[1].Y = wRect.Top + (int)(Xorshift32() % 410) - 200;
                wPt[2].X = wRect.Left + c - (int)(Xorshift32() % 210) - c;
                wPt[2].Y = wRect.Bottom - c + (int)(Xorshift32() % 210) - c;
                PlgBlt(desk, wPt, desk, wRect.Left, wRect.Top, wRect.Right - wRect.Left, wRect.Bottom - wRect.Top, IntPtr.Zero, 0, 0);
                ReleaseDC(IntPtr.Zero, desk);
                if (Xorshift32() % 5 == 1)
                {
                    RedrawWindow(IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, 133);
                }
            }
        }
        public static void GDI2or1()
        {
            Random rand = new Random();
            DateTime startTime = DateTime.Now;
            TimeSpan maxDuration = TimeSpan.FromSeconds(60);
            while (DateTime.Now - startTime < maxDuration)
            {
                IntPtr hdc = GetDC(IntPtr.Zero);
                int w = GetSystemMetrics(SM_CXSCREEN);
                int h = GetSystemMetrics(SM_CYSCREEN);
                IntPtr brush = CreateSolidBrush(RGB((byte)rand.Next(255), (byte)rand.Next(255), (byte)rand.Next(255)));
                IntPtr oldBrush = SelectObject(hdc, brush);
                PatBlt(hdc, 0, 0, w, h, PATINVERT);
                SelectObject(hdc, oldBrush);
                DeleteObject(brush);
                ReleaseDC(IntPtr.Zero, hdc);
            }
        }
        public static void GDI3()
        {
            while (true)
            {
                IntPtr desktopDC = GetDC(IntPtr.Zero);
                Graphics g = Graphics.FromHdc(desktopDC);
                g.SmoothingMode = SmoothingMode.AntiAlias;
                string[] texts = {
            "Cyanohydrin",
            "Cyanohydrin",
            "Cyanohydrin",
            "Cyanohydrin",
            "Cyanohydrin"
        };
                Color[] colors = {
            Color.Red,
            Color.Orange,
            Color.Yellow,
            Color.Green,
            Color.Blue
        };
                float[] angles = { -30f, 15f, 45f, -60f, 75f };
                int screenW = Screen.PrimaryScreen.Bounds.Width;
                int screenH = Screen.PrimaryScreen.Bounds.Height;
                while (true)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        using (Font font = new Font("Arial", 67, FontStyle.Bold | FontStyle.Underline))
                        using (Brush brush = new SolidBrush(colors[i]))
                        {
                            g.ResetTransform();
                            int x = rand.Next(0, screenW);
                            int y = rand.Next(0, screenH);
                            g.TranslateTransform(x, y);
                            g.RotateTransform(angles[i]);
                            g.ScaleTransform(2.0f, 1.0f);
                            g.DrawString(texts[i], font, brush, 0, 0);
                        }
                    }
                    Thread.Sleep(100);
                }
            }
        }
        public static void GDI3or1()
        {
            DateTime startTime = DateTime.Now;
            TimeSpan maxDuration = TimeSpan.FromSeconds(30);
            while (DateTime.Now - startTime < maxDuration)
            {
                int w = GetSystemMetrics(0);
                int h = GetSystemMetrics(1);
                int rw = rand.Next(w);
                int rh = rand.Next(h);
                IntPtr hdc = GetDC(IntPtr.Zero);
                BitBlt(hdc, rw, rand.Next(-12, 12), rand.Next(600), h, hdc, rw, rand.Next(-12, 12), SRCCOPY);
                BitBlt(hdc, rand.Next(-12, 12), rh, w, rand.Next(600), hdc, rand.Next(-12, 12), rh, SRCCOPY);
                DeleteDC(hdc);
            }
        }
        public static void GDI3or2()
        {
            DateTime startTime = DateTime.Now;
            TimeSpan maxDuration = TimeSpan.FromSeconds(30);
            int w = GetSystemMetrics(SM_CXSCREEN);
            int h = GetSystemMetrics(SM_CYSCREEN);
            while (DateTime.Now - startTime < maxDuration)
            {
                IntPtr hdc = GetDC(IntPtr.Zero);
                for (int i = 0; i < 5; i++)
                {
                    IntPtr hdcCopy = CreateCompatibleDC(hdc);
                    int nBlockSize = rand.Next(128, 257);
                    int nNewBlockSize = nBlockSize + rand.Next(16, 33);
                    int x = rand.Next(0, Math.Max(1, w - nBlockSize));
                    int y = rand.Next(0, Math.Max(1, h - nBlockSize));
                    uint rop = (rand.Next(2) == 0) ? SRCAND : SRCPAINT;
                    StretchBlt(hdc, x - (nNewBlockSize - nBlockSize) / 2, y - (nNewBlockSize - nBlockSize) / 2,
                               nNewBlockSize, nNewBlockSize, hdc, x, y, nBlockSize, nBlockSize, rop);
                    StretchBlt(hdcCopy, x - (nNewBlockSize - nBlockSize) / 2, y - (nNewBlockSize - nBlockSize) / 2,
                               nNewBlockSize, nNewBlockSize, hdc, x, y, nBlockSize, nBlockSize, WHITENESS);
                    BLENDFUNCTION blend = new BLENDFUNCTION
                    {
                        BlendOp = AC_SRC_OVER,
                        BlendFlags = 0,
                        SourceConstantAlpha = 50,
                        AlphaFormat = 0
                    };
                    AlphaBlend(hdc, 0, 0, w, h, hdcCopy, 0, 0, w, h, blend);
                    DeleteDC(hdcCopy);
                }
                ReleaseDC(IntPtr.Zero, hdc);
            }
        }
     public static void GDI4()
        {
            while (true)
            {
                int w = GetSystemMetrics(0);
                int h = GetSystemMetrics(1);

                int ry = rand.Next(-500, h);

                IntPtr hdc = GetDC(IntPtr.Zero);
                IntPtr brush = CreateSolidBrush(colors.Random());
                SelectObject(hdc, brush);

                BitBlt(hdc, 0, ry, w, rand.Next(h), hdc, 0, ry, PATINVERT);

                ry = rand.Next(-500, h);

                BitBlt(hdc, rand.Next(-10, 10), ry, w, rand.Next(600), hdc, rand.Next(-10, 10), ry, SRCCOPY);
                BitBlt(hdc, rand.Next(-10, 10), ry, w, rand.Next(600), hdc, rand.Next(-10, 10), ry, SRCCOPY);
                BitBlt(hdc, rand.Next(-10, 10), ry, w, rand.Next(600), hdc, rand.Next(-10, 10), ry, SRCCOPY);

                ry = rand.Next(-500, h);

                BitBlt(hdc, 0, ry, w, rand.Next(600), hdc, 0, ry, NOTSRCCOPY);

                Thread.Sleep(2);

                DeleteDC(hdc);
            }
        }
    }
}
