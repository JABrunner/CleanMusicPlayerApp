using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CleanMusicPlayerApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            /*disables Windows Media Player controls to only show Visualizer, option in properties isn't working for
            some reason*/
            axWindowsMediaPlayer1.uiMode = "none";
            trackVolume.Value = 50;
        }
        //create string to allow to select and load multiple files
        string[] paths, files;
        int flag = 1;
        //create flag to allow toggle in Play/Pause button
        Point lastPoint;

        private void btnExit_Click(object sender, EventArgs e)
        {
            //Code to exit application
            this.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void TrackList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Allow for selection of track in list and plays on click
            axWindowsMediaPlayer1.URL = paths[TrackList.SelectedIndex];
            axWindowsMediaPlayer1.Ctlcontrols.play();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            //Stop track
            axWindowsMediaPlayer1.Ctlcontrols.stop();
            progressBar1.Value = 0;
            //resets progress bar to zero when track is stopped
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            //Toggle between play and pause and switch the image to denote which toggle is active
            flag *= -1;
            
            if (flag == -1)
            {
                Bitmap b = new Bitmap(@"C:\Users\laser\OneDrive\Desktop\Coding Projects\C#\CleanMusicPlayerApp\CleanMusicPlayerApp\Resources\pause_48px.png");
                btnPlay.BackgroundImage = b;
            }
            else
            {
                Bitmap b2 = new Bitmap(@"C:\Users\laser\OneDrive\Desktop\Coding Projects\C#\CleanMusicPlayerApp\CleanMusicPlayerApp\Resources\play_60px.png");
                btnPlay.BackgroundImage = b2;
            }
            if (axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsPlaying)
            {
                axWindowsMediaPlayer1.Ctlcontrols.pause();
                
            }

            else
            {
                axWindowsMediaPlayer1.Ctlcontrols.play();
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            //back up one track
            if (TrackList.SelectedIndex > 0)
            {
                TrackList.SelectedIndex = TrackList.SelectedIndex - 1;
            }
        }

        private void btnForward_Click(object sender, EventArgs e)
        {
            //move forward one track
            if (TrackList.SelectedIndex<TrackList.Items.Count - 1)
            {
                TrackList.SelectedIndex = TrackList.SelectedIndex + 1;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //sync timer with progress bar to show how far into the track has played
            if(axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsPlaying)
            {
                progressBar1.Maximum = (int)axWindowsMediaPlayer1.Ctlcontrols.currentItem.duration;
                progressBar1.Value = (int)axWindowsMediaPlayer1.Ctlcontrols.currentPosition;
            }
        }

        private void trackVolume_Scroll(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.settings.volume = trackVolume.Value;
        }

        private void ProgressBarMouseDown(object sender, MouseEventArgs e)
        {
            //Allows progress bar to be clicked and move track relative to position of progress bar
            axWindowsMediaPlayer1.Ctlcontrols.currentPosition = axWindowsMediaPlayer1.currentMedia.duration * e.X / progressBar1.Width;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {

            lastPoint = new Point(e.X, e.Y);
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            //loads track into application opening File Explorer allowing use to pick file destination
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = true;
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                files = ofd.FileNames;
                paths = ofd.FileNames;
                for (int x = 0; x < files.Length; x++)
                {
                    TrackList.Items.Add(files[x]);
                }
            }
        }
    }
}



//Created by Jordan Brunner