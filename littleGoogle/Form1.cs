using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace littleGoogle
{
    public partial class Form1 : Form
    {
        SortedSet<string> set = new SortedSet<string>();
        Dictionary<string, List<store_data>> data = new Dictionary<string, List<store_data>>();

        //store objstore = new store();
        string[] stopword;
        string[] word;
        string[] history;
        public List<string> l = new List<string>();
        string[] wordsofsearchbox;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] filepaths = Directory.GetFiles(@"C:\Users\aya14\Desktop\bla bla bla", "*.txt");
            int countersOfFiles = 0;
            stopwords();
            for (int a = 0; a < set.Count; a++)
                MessageBox.Show(set.ElementAt(a)+' ');
                foreach (string filepath in filepaths)
                {
                    string filename = Path.GetFileNameWithoutExtension(filepath);
                    string filecontent = File.ReadAllText(filepath);
                    word = filecontent.Split(new[] { ' ', ',', '.', '_', '"', '{', '}', '[', ']', '?', '!', '+', '=', '(', ')', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
                    for (int k = 0; k < word.Length; k++)
                    {
                        if (!set.Contains(word[k].ToLower()))
                        {
                            if (data.Keys.Contains(word[k].ToLower()))
                            {
                                if (data[word[k].ToLower()].ElementAt(data[word[k].ToLower()].Count - 1).fileName == filename)
                                {
                                    data[word[k].ToLower()].ElementAt(data[word[k].ToLower()].Count - 1).l.Add(k);
                                    data[word[k].ToLower()].ElementAt(data[word[k].ToLower()].Count - 1).fileName = filename;
                                    data[word[k].ToLower()].ElementAt(data[word[k].ToLower()].Count - 1).countlines++;
                                    // MessageBox.Show(Convert.ToString(data[word[k].ToLower()].ElementAt(data[word[k].ToLower()].Count - 1).countlines));

                                }
                                else
                                {  // objstore.countlines = 1;
                                    data[word[k].ToLower()].Add(new store_data());
                                }
                            }
                            else
                            {
                                data.Add(word[k].ToLower(), new List<store_data>());
                                /* data[word[k].ToLower()] = new List<store_data>();
                                  objstore.fileName = filename;
                                  objstore.l.Add(k);
                                  objstore.countlines = 1;*/
                                data[word[k].ToLower()].Add(new store_data());
                                //  MessageBox.Show(Convert.ToString(data[word[k].ToLower()].ElementAt(data[word[k].ToLower()].Count - 1).countlines));

                            }
                        }
                    }
                }
        }

            string fName;
        private void button1_Click(object sender, EventArgs e)
        {
            string returnofsearchbox = splitwordsofserchbox();
            int cnt = 0;
            if (textBox1.Text.ToLower() != " ")
            {
                if (data.ContainsKey(returnofsearchbox))
                {
                    for (int b = 0; b < data[returnofsearchbox].Count; b++)
                    {
                        cnt++;
                        fName = data[returnofsearchbox].ElementAt(b).fileName;
                        //MessageBox.Show(Convert.ToString(data["aya"].ElementAt(b).countlines));
                    }
                    textBox2.Text = Convert.ToString(cnt) + ' '+fName;
                }

            }
            else
            {
                MessageBox.Show("error!");
            }
        }


        public void stopwords()
        {
            StreamReader red = new StreamReader(@"C:\Users\aya14\Desktop\stop words.txt");
            string rdr;

            while ((rdr = red.ReadLine()) != null)
            {

                set.Add(Convert.ToString(rdr.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries)));

            }
        }

        public string splitwordsofserchbox()
        {
            wordsofsearchbox = textBox1.Text.Split(new[] { ' ', ',', '.', '_', '"', '{', '}', '[', ']', '?', '!', '+', '=', '(', ')', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            for (int l = 0; l < wordsofsearchbox.Length; l++)
            {
                if (!set.Contains(wordsofsearchbox[l]))
                    return wordsofsearchbox[l];

            }
            return "";
        }
    }
}