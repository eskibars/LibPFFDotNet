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
using System.Net;
using LibPFFDotNet;
using System.Net.Mail;

namespace PFF_converter
{
    public partial class Form1 : Form
    {
        PFF o = null;
        PFFMessage ViewingMessage = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void cmdBrowseInput_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtInputODT.Text = openFileDialog1.FileName;
                scanODT();
            }
        }

        void scanODT()
        {
            o = new PFF();
            o.Open(txtInputODT.Text);
            lblPFFVersion.Text = o.ToString();
            PFFFolder p = o.GetRootFolder();

            List<PFFFolder> f = p.GetSubfolders();
            treeStructure.Nodes.Clear();
            for (int i = 0; i < f.Count; i++)
            {
                TreeNode tn = new TreeNode();
                string name = f[i].GetName();
                tn.Text = name;
                tn.Tag = f[i];
                tn.ToolTipText = name + ": " + f[i].GetSubmessages().Count + " messages";
                for (int j = 0; j < f[i].GetSubfolders().Count; j++ )
                {
                    tn.Nodes.Add("Loading...");
                }
                treeStructure.Nodes.Add(tn);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cmdBrowseInput_Click(sender, e);
        }

        private void treeStructure_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            TreeNode stn = e.Node;
            stn.Nodes.Clear();
            PFFFolder sf = (PFFFolder)stn.Tag;
            List<PFFFolder> f = sf.GetSubfolders();
            for (int i = 0; i < f.Count; i++)
            {
                TreeNode tn = new TreeNode();
                string name = f[i].GetName();
                PFFFolder.FolderType t = f[i].GetFolderType();
                tn.Text = name + ": " + f[i].GetSubmessages().Count + " messages";
                tn.Tag = f[i];
                for (int j = 0; j < f[i].GetSubfolders().Count; j++ )
                {
                    tn.Nodes.Add("Loading...");
                }
                stn.Nodes.Add(tn);
            }
        }

        private void treeFolder_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode stn = e.Node;
            ViewingMessage = null;
            if (stn.Level == 0)
            {
                PFFMessage sf = (PFFMessage)stn.Tag;
                PFFItem.ItemTypes t = sf.GetItemType();
                ViewingMessage = sf;
                if (t == PFFItem.ItemTypes.Appointment || t == PFFItem.ItemTypes.Meeting)
                {
                    PFFCalendarItem c = new PFFCalendarItem(sf);
                    string r = c.GetBody(PFFMessage.BodyType.HTML);
                    browser.DocumentText = r;
                }
                else
                {
                    string r = sf.GetBody(PFFMessage.BodyType.HTML);
                    browser.DocumentText = r;
                }
                sf.GetRecipients();
            }
        }

        private void browser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            foreach (HtmlElement image in browser.Document.Images)
            {
                string src = image.GetAttribute("src");
                if (src.StartsWith("cid:"))
                {
                    src = src.Substring(4);
                    foreach (PFFAttachment attach in ViewingMessage.GetAttachments())
                    {
                        if (src.Equals(attach.GetContentID()))
                        {
                            string data = Convert.ToBase64String(attach.GetContents());
                            string newsrc = "data:" + attach.GetMimeType() + ";base64," + data;
                            image.SetAttribute("src", newsrc);
                            //break;
                        }
                    }
                }
            }
        }

        private void treeStructure_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode stn = e.Node;
            PFFFolder sf = (PFFFolder)stn.Tag;
            List<PFFMessage> m = sf.GetSubmessages();
            treeFolder.Nodes.Clear();
            for (int i = 0; i < m.Count; i++)
            {
                TreeNode tn = new TreeNode();
                PFFItem.ItemTypes t = m[i].GetItemType();
                if (t == PFFItem.ItemTypes.Appointment || t == PFFItem.ItemTypes.Meeting)
                {
                    tn.ImageIndex = 8;
                    tn.SelectedImageIndex = 8;
                }
                tn.Tag = m[i];
                string name = m[i].GetName();
                tn.Text = name;
                List<PFFAttachment> a = m[i].GetAttachments();
                for (int j = 0; j < a.Count; j++)
                {
                    string an = a[j].GetName();
                    TreeNode tan = new TreeNode();
                    tan.Text = an;
                    tan.Tag = a[j];
                    tan.ImageIndex = 3;
                    tan.SelectedImageIndex = 3;
                    tn.Nodes.Add(tan);
                }
                treeFolder.Nodes.Add(tn);
            }
            tabControls.SelectedIndex = 1;
            browser.DocumentText = "<html></html>";
        }

        private void cmdExport_Click(object sender, EventArgs e)
        {
            string outputDir = txtOutputLocation.Text;
            if (Directory.Exists(outputDir))
            {
                int tabIndex = tabControls.SelectedIndex;
                if (tabIndex == 0)
                {
                    // in the folder browsing view -- export the full folder
                    TreeNode n = treeStructure.SelectedNode;
                    PFFFolder f = (PFFFolder)n.Tag;
                    string NewPath = Path.Combine(outputDir, "");
                    Directory.CreateDirectory(NewPath);
                    f.Export(NewPath);
                }
                else if (tabIndex == 1)
                {
                    // in the item browsing view -- export the single item
                    TreeNode n = treeFolder.SelectedNode;
                    if (n.Level == 0)
                    {
                        // this is a standard message
                        PFFMessage m = (PFFMessage)n.Tag;
                        switch (m.GetItemType())
                        {
                            case PFFItem.ItemTypes.Email:
                                SmtpClient Client = new SmtpClient("empty");
                                Client.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                                Client.PickupDirectoryLocation = outputDir;
                                Client.Send(m.GetAsMailMessage());
                                break;
                        }
                    }
                    else
                    {
                        // this is an attachment
                        PFFAttachment a = (PFFAttachment)n.Tag;
                        a.SaveContents(Path.Combine(outputDir, a.GetName()));
                    }
                }
            }
            else
            {
                MessageBox.Show("Output directory not set or doesn't exist", "Output Directory Error");
            }
        }

        private void cmdBrowseOutput_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtOutputLocation.Text = folderBrowserDialog1.SelectedPath;
            }
        }
    }
}
