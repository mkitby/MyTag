using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using TagLib;
using TagLib.Id3v2;
using System.Diagnostics;
using System.IO;

namespace MyTag
{
    public partial class MyTag : Form
    {
        private static TagLib.File audioFile;
        private static TagLib.Id3v2.Tag audioTag;
        private static string xmlPath;
        
        public MyTag(string audiopath)
        {
            UseWaitCursor = true;

            InitializeComponent();

            // to get the location the assembly is executing from
            //(not necessarily where the it normally resides on disk)
            // in the case of the using shadow copies, for instance in NUnit tests, 
            // this will be in a temp directory.
            string myTagPath = System.Reflection.Assembly.GetExecutingAssembly().Location;

            // Get the directory with
            string directory = System.IO.Path.GetDirectoryName(myTagPath);

            xmlPath = directory + "\\MyTag.xml";

            if (audiopath != null)
                createTreeViewFromXmlAndMedia(xmlPath, audiopath);
            else
            {
                // Disable save button if no file is opened
                bt_Save.Enabled = false;
            }

            UseWaitCursor = false;
        }

        void MyTag_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        void MyTag_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                {
                    string[] filePaths = (string[])(e.Data.GetData(DataFormats.FileDrop));
                    foreach (string fileLoc in filePaths)
                    {
                        // Code to read the contents of the text file
                        if (System.IO.File.Exists(fileLoc))
                        {
                            bt_Save.Enabled = true;
                            createTreeViewFromXmlAndMedia(xmlPath, fileLoc);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ExceptionInfo.ShowExceptionInfo(ex), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        // TODO: ape, mp4 support
        private void createTreeViewFromXmlAndMedia(string xmlpath, string path)
        {
            try
            {
                audioFile = TagLib.File.Create(path);

                // Display file name in status bar
                //statusStrip.Items[0].Text = Path.GetFileName(path);
                showInStatusBar(Path.GetFileName(path));

                audioTag = (TagLib.Id3v2.Tag)audioFile.GetTag(TagTypes.Id3v2);
                if (audioTag == null) {
                    MessageBox.Show("Only support ID3v2 tag", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                
                XmlDocument xmltag = new XmlDocument();

                xmltag.Load(xmlpath);

                XmlNode mp3tag = xmltag.SelectSingleNode("/mp3tag");

                XmlNodeList taglist = mp3tag.ChildNodes;

                // Clear all nodes first before add new nodes
                tagTreeView.Nodes.Clear();

                foreach (XmlNode tag in taglist)
                {
                    TreeNode nodetag = tagTreeView.Nodes.Add(tag.Attributes["field"].Value);
                    //Debug.WriteLine("\nField: " + tag.Attributes["field"].Value + "\n");

                    XmlNodeList valuelist = tag.ChildNodes;

                    foreach (XmlNode value in valuelist)
                    {
                        TreeNode nodevalue = new TreeNode(value.InnerText);

                        foreach (UserTextInformationFrame fm in audioTag.GetFrames("TXXX"))
                        {
                            if (fm.Description == tag.Attributes["field"].Value)
                            {
                                //Debug.WriteLine("\r\nString list length = " + fm.Text.Length + "\r\n");
                                if (fm.Text.Length == 0) continue;

                                if (fm.Text[0].Contains(nodevalue.Text))// TODO  use ; to separate
                                {
                                    nodevalue.Checked = true;
                                }
                            }
                        }

                        nodetag.Nodes.Add(nodevalue);

                        // Update node check status
                        tagTreeView.TriStateUpdateCheckStatus(nodevalue);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ExceptionInfo.ShowExceptionInfo(ex), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bt_Exit_Click(object sender, EventArgs e)
        {
            // TODO: show a message box to prompt save tag before exit

            System.Windows.Forms.Application.Exit();
        }

        // TODO: expection handling
        private void bt_Save_Click(object sender, EventArgs e)
        {
            TreeNodeCollection nodes = tagTreeView.Nodes;
            UserTextInformationFrame tagframe;
            List<UserTextInformationFrame> save_frame_list = new List<UserTextInformationFrame>();
            List<string> tag_desc_list = new List<string>();

            try
            {
                // RemoveFrame() is invalid, workaround: save other "TXXX" first, then remove all "TXXX", add selected tags and recovery other "TXXX" at last
                foreach (TreeNode n in nodes)
                {
                    tag_desc_list.Add(n.Text);
                }

                foreach (UserTextInformationFrame fm in audioTag.GetFrames("TXXX"))
                {
                    if (!tag_desc_list.Contains(fm.Description))
                    {
                        save_frame_list.Add(fm);
                    }
                }

                // Remove all "TXXX"
                audioTag.RemoveFrames("TXXX");

                // Pre-handle comment tag for link to comment
                if (cb_LinkToComment.Checked == true)
                {
                    if (cb_AppendMode.Checked == true)
                    {
                        if (string.IsNullOrWhiteSpace(audioTag.Comment))
                        {
                            audioTag.Comment = ""; // clear comment
                        }
                        else
                        {
                            audioTag.Comment += "||"; // add a link character "||"
                        }
                    }
                    else
                    {
                        audioTag.Comment = ""; // clear comment
                    }
                }

                // Add selected tags 
                foreach (TreeNode n in nodes)
                {
                    string valmixed = "";

                    foreach (TreeNode tn in n.Nodes)
                    {
                        if (tn.Checked == true)
                        {
                            valmixed += tn.Text + ";";

                            // Handle link to comment tag
                            if (cb_LinkToComment.Checked == true)
                            {
                                audioTag.Comment += tn.Text + ";";
                            }
                        }
                    }

                    tagframe = new UserTextInformationFrame(n.Text);
                    tagframe.Text = new string[] { valmixed };
                    audioTag.AddFrame(tagframe);
                }

                // Recovery other "TXXX"
                if (save_frame_list.Count != 0)
                {
                    foreach (UserTextInformationFrame fm in save_frame_list)
                    {
                        audioTag.AddFrame(fm);
                    }
                }

                // Save file
                audioFile.Save();

                showInStatusBar("Save OK");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ExceptionInfo.ShowExceptionInfo(ex), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            } 
        }

        private void MyTag_Load(object sender, EventArgs e)
        {
            if (!System.IO.File.Exists(xmlPath))
            {
                MessageBox.Show("No mp3tag.xml found", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Windows.Forms.Application.Exit();
            }
        }

        private void showInStatusBar(string s)
        {

            toolStripStatusLabel1.Text = s;
            //this.Refresh();
        }

    }
}
