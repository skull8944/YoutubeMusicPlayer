using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using MediaToolkit;
using MediaToolkit.Model;
using VideoLibrary;

namespace PlayerUI
{
    public partial class Form2 : System.Windows.Forms.Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void download()
        {
            var source = "music\\";
                    
              var youtube = YouTube.Default;
               var vid = youtube.GetVideo(textBox1.Text);
               if(vid.FullName == "YouTube.mp4")
               {
                    download();
               }
            else
            {
                MessageBox.Show(vid.FullName);
                File.WriteAllBytes(source + vid.FullName, vid.GetBytes());

                 var inputFile = new MediaFile { Filename = source + vid.FullName };
                 var outputFile = new MediaFile { Filename = $"{source + vid.FullName}.mp3" };

                  using (var engine = new Engine())
                  {
                        engine.GetMetadata(inputFile);

                        engine.Convert(inputFile, outputFile);
                  }
                  MessageBox.Show("下載完成");
                  textBox1.Clear();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(textBox1.Text);
            if (textBox1.Text.Contains("youtube.com/watch?v=") )
            {
                try
                {
                    download();
                }
                catch (InvalidOperationException a)
                {
                    MessageBox.Show(a.Message);
                }
                catch(KeyNotFoundException b)
                {
                    MessageBox.Show(b.Message);
                } 
            }
            else
            {
                MessageBox.Show("請輸入正確的網址");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
