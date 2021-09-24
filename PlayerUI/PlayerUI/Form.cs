using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace PlayerUI
{
    public partial class Form : System.Windows.Forms.Form
    {
        protected int firstplay = 0;
        protected int random = 0;
        protected int preindex;
        List<string> listSong = new List<string>();
        public Form()
        {
            InitializeComponent();
            hideSubMenu();
            playbtn.Image = Image.FromFile("img\\play0.png");
            randombtn.Image = Image.FromFile("img\\random.png");
        }

        private void hideSubMenu()
        {
            panelMediaSubMenu.Visible = false;
            panelPlaylistSubMenu.Visible = false;
            panelToolsSubMenu.Visible = false;
        }

        private void showSubMenu(Panel subMenu)
        {
            if (subMenu.Visible == false)
            {
                hideSubMenu();
                subMenu.Visible = true;
            }
            else
                subMenu.Visible = false;
        }

        private void btnMedia_Click(object sender, EventArgs e)
        {
            showSubMenu(panelMediaSubMenu);
        }

        #region MediaSubMenu
        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "請選擇檔案";
            ofd.Multiselect = true;
            
            ofd.InitialDirectory = Application.StartupPath+"\\music";
            ofd.Filter = "mp3檔案|*.mp3";
            ofd.ShowDialog();
            //獲得我們在資料夾中選擇所有檔案的全路徑
            string[] path = ofd.FileNames;
            for (int i = 0; i < path.Length; i++)
            {
                //將音樂檔案的檔名載入到ListBox中
                musiclist.Items.Add(Path.GetFileName(path[i]));
                listSong.Add(path[i]);
                //將音樂檔案的全路徑儲存到泛型集合中
            }
            //..
            //your codes
            //..
            hideSubMenu();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //..
            //your codes
            //..
            openChildForm(new Form2());
            hideSubMenu();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //..
            //your codes
            //..
            hideSubMenu();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //..
            //your codes
            //..
            hideSubMenu();
        }
        #endregion

        private void btnPlaylist_Click(object sender, EventArgs e)
        {
            showSubMenu(panelPlaylistSubMenu);
        }

        #region PlayListManagemetSubMenu
        private void button8_Click(object sender, EventArgs e)
        {
            //..
            //your codes
            //..
            hideSubMenu();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //..
            //your codes
            //..
            hideSubMenu();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //..
            //your codes
            //..
            hideSubMenu();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //..
            //your codes
            //..
            hideSubMenu();
        }
        #endregion

        private void btnTools_Click(object sender, EventArgs e)
        {
            showSubMenu(panelToolsSubMenu);
        }
        #region ToolsSubMenu
        private void button13_Click(object sender, EventArgs e)
        {
            //..
            //your codes
            //..
            hideSubMenu();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            //..
            //your codes
            //..
            hideSubMenu();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            //..
            //your codes
            //..
            hideSubMenu();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //..
            //your codes
            //..
            hideSubMenu();
        }
        #endregion

        private void btnEqualizer_Click(object sender, EventArgs e)
        {
            //..
            //your codes
            //..
            hideSubMenu();
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            //..
            //your codes
            //..
            hideSubMenu();
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private System.Windows.Forms.Form activeForm = null;
        private void openChildForm(System.Windows.Forms.Form childForm)
        {
            if (activeForm != null) activeForm.Close();
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelChildForm.Controls.Add(childForm);
            panelChildForm.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)//音量調整
        {
            axWindowsMediaPlayer1.settings.volume = trackBar1.Value;
            label3.Text = $"{trackBar1.Value}%";
        }

        private void playbtn_Click(object sender, EventArgs e)
        {
            try
            {
                if(firstplay == 0)
                {
                    axWindowsMediaPlayer1.URL = listSong[musiclist.SelectedIndex];
                    if (axWindowsMediaPlayer1.playState ==  WMPLib.WMPPlayState.wmppsPlaying)
                    {
                        playbtn.Image = Image.FromFile("img\\play0.png");
                        axWindowsMediaPlayer1.Ctlcontrols.pause();
                    }
                    else
                    {
                        playbtn.Image = Image.FromFile("img\\play1.png");
                        axWindowsMediaPlayer1.Ctlcontrols.play();
                        firstplay++;
                    }
                }
                else
                {
                    if (axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsPlaying)
                    {
                        playbtn.Image = Image.FromFile("img\\play0.png");
                        axWindowsMediaPlayer1.Ctlcontrols.pause();
                    }
                    else
                    {
                        playbtn.Image = Image.FromFile("img\\play1.png");
                        axWindowsMediaPlayer1.Ctlcontrols.play();
                    }
                }

            }
            catch(ArgumentException)
            {
                MessageBox.Show("請先匯入或選擇音樂");
            }
        }

        private void nextbtn_Click(object sender, EventArgs e)
        {
            if(random == 0)
            {
                firstplay = 1;
                int index = musiclist.SelectedIndex;
                index++;
                if (index == musiclist.Items.Count)
                {
                    index = 0;
                }
                musiclist.SelectedIndex = index;
                axWindowsMediaPlayer1.URL = listSong[index];
                axWindowsMediaPlayer1.Ctlcontrols.play();
            }
            else
            {
                firstplay = 1;
                checkrandom();
            }
        }

        private void prebtn_Click(object sender, EventArgs e)
        {
            if(random == 0)
            {
                firstplay = 1;
                int index = musiclist.SelectedIndex;
                index--;
                if (index < 0)
                {
                    index = musiclist.Items.Count - 1;
                }
                musiclist.SelectedIndex = index;
                axWindowsMediaPlayer1.URL = listSong[index];
                axWindowsMediaPlayer1.Ctlcontrols.play();
            }
            else
            {
                firstplay = 1;
                musiclist.SelectedIndex = preindex;
                axWindowsMediaPlayer1.URL = listSong[preindex];
                axWindowsMediaPlayer1.Ctlcontrols.play();
            }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            MessageBox.Show(axWindowsMediaPlayer1.currentMedia.name);
        }

        private void randombtn_Click(object sender, EventArgs e)
        {
            if (random == 0)
            {
                randombtn.Image = Image.FromFile("img\\random_g.png");
                random = 1;
            }
            else
            {
                randombtn.Image = Image.FromFile("img\\random.png");
                random = 0;
            }
        }

        private void checkrandom()
        {
            int index = musiclist.SelectedIndex;
            preindex = index;
            Random Rindex = new Random();
            int rindex = Rindex.Next(0, musiclist.Items.Count);
            if (rindex == index)
            {
                checkrandom();
            }
            else
            {
                musiclist.SelectedIndex = rindex;
                axWindowsMediaPlayer1.URL = listSong[rindex];
                axWindowsMediaPlayer1.Ctlcontrols.play();
            }
        }

        private void axWindowsMediaPlayer1_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            if (e.newState == 1)
            {
                this.BeginInvoke(new Action(() =>
                {
                    if(random == 0)
                    {
                        firstplay = 1;
                        int index = musiclist.SelectedIndex;
                        index++;
                        if (index == musiclist.Items.Count)
                        {
                            index = 0;
                        }
                        musiclist.SelectedIndex = index;
                        axWindowsMediaPlayer1.URL = listSong[index];
                        axWindowsMediaPlayer1.Ctlcontrols.play();
                    }
                    else
                    {
                        firstplay = 1;
                        checkrandom();
                    }
                }));
            }
        }
    }
}
