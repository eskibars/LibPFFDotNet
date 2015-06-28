namespace PFF_converter
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.txtInputODT = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmdBrowseInput = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.lblPFFVersion = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtOutputLocation = new System.Windows.Forms.TextBox();
            this.cmdBrowseOutput = new System.Windows.Forms.Button();
            this.treeStructure = new System.Windows.Forms.TreeView();
            this.icons = new System.Windows.Forms.ImageList(this.components);
            this.tabControls = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeFolder = new System.Windows.Forms.TreeView();
            this.browser = new System.Windows.Forms.WebBrowser();
            this.cmdExport = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.tabControls.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtInputODT
            // 
            this.txtInputODT.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtInputODT.Location = new System.Drawing.Point(235, 9);
            this.txtInputODT.Name = "txtInputODT";
            this.txtInputODT.Size = new System.Drawing.Size(511, 31);
            this.txtInputODT.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(209, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Input OST/PST/PAB:";
            // 
            // cmdBrowseInput
            // 
            this.cmdBrowseInput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdBrowseInput.Location = new System.Drawing.Point(752, 9);
            this.cmdBrowseInput.Name = "cmdBrowseInput";
            this.cmdBrowseInput.Size = new System.Drawing.Size(114, 40);
            this.cmdBrowseInput.TabIndex = 2;
            this.cmdBrowseInput.Text = "Browse";
            this.cmdBrowseInput.UseVisualStyleBackColor = true;
            this.cmdBrowseInput.Click += new System.EventHandler(this.cmdBrowseInput_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "OST Files (*.ost)|*.ost|PST Files (*.pst)|*.pst";
            // 
            // lblPFFVersion
            // 
            this.lblPFFVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblPFFVersion.AutoSize = true;
            this.lblPFFVersion.Location = new System.Drawing.Point(12, 615);
            this.lblPFFVersion.Name = "lblPFFVersion";
            this.lblPFFVersion.Size = new System.Drawing.Size(305, 25);
            this.lblPFFVersion.TabIndex = 3;
            this.lblPFFVersion.Text = "Load file to see file information";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(163, 25);
            this.label2.TabIndex = 5;
            this.label2.Text = "Output location:";
            // 
            // txtOutputLocation
            // 
            this.txtOutputLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOutputLocation.Location = new System.Drawing.Point(235, 66);
            this.txtOutputLocation.Name = "txtOutputLocation";
            this.txtOutputLocation.Size = new System.Drawing.Size(511, 31);
            this.txtOutputLocation.TabIndex = 6;
            // 
            // cmdBrowseOutput
            // 
            this.cmdBrowseOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdBrowseOutput.Location = new System.Drawing.Point(752, 66);
            this.cmdBrowseOutput.Name = "cmdBrowseOutput";
            this.cmdBrowseOutput.Size = new System.Drawing.Size(114, 40);
            this.cmdBrowseOutput.TabIndex = 7;
            this.cmdBrowseOutput.Text = "Browse";
            this.cmdBrowseOutput.UseVisualStyleBackColor = true;
            this.cmdBrowseOutput.Click += new System.EventHandler(this.cmdBrowseOutput_Click);
            // 
            // treeStructure
            // 
            this.treeStructure.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeStructure.ImageIndex = 0;
            this.treeStructure.ImageList = this.icons;
            this.treeStructure.Location = new System.Drawing.Point(6, 6);
            this.treeStructure.Name = "treeStructure";
            this.treeStructure.SelectedImageIndex = 0;
            this.treeStructure.Size = new System.Drawing.Size(833, 395);
            this.treeStructure.TabIndex = 8;
            this.treeStructure.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeStructure_BeforeExpand);
            this.treeStructure.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeStructure_NodeMouseDoubleClick);
            // 
            // icons
            // 
            this.icons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("icons.ImageStream")));
            this.icons.TransparentColor = System.Drawing.Color.Transparent;
            this.icons.Images.SetKeyName(0, "folder.png");
            this.icons.Images.SetKeyName(1, "folder-open.png");
            this.icons.Images.SetKeyName(2, "mail-unread.png");
            this.icons.Images.SetKeyName(3, "mail-attachment.png");
            this.icons.Images.SetKeyName(4, "mail-read.png");
            this.icons.Images.SetKeyName(5, "mail-replied.png");
            this.icons.Images.SetKeyName(6, "mail-sent.png");
            this.icons.Images.SetKeyName(7, "mail-task.png");
            this.icons.Images.SetKeyName(8, "text-calendar.png");
            // 
            // tabControls
            // 
            this.tabControls.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControls.Controls.Add(this.tabPage1);
            this.tabControls.Controls.Add(this.tabPage2);
            this.tabControls.ImageList = this.icons;
            this.tabControls.Location = new System.Drawing.Point(13, 112);
            this.tabControls.Name = "tabControls";
            this.tabControls.SelectedIndex = 0;
            this.tabControls.Size = new System.Drawing.Size(853, 445);
            this.tabControls.TabIndex = 9;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.treeStructure);
            this.tabPage1.Location = new System.Drawing.Point(4, 34);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(845, 407);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Folder Tree";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.splitContainer1);
            this.tabPage2.Location = new System.Drawing.Point(4, 34);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(845, 407);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Items in Folder";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.BackColor = System.Drawing.Color.LightGray;
            this.splitContainer1.Location = new System.Drawing.Point(7, 6);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeFolder);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.browser);
            this.splitContainer1.Size = new System.Drawing.Size(832, 395);
            this.splitContainer1.SplitterDistance = 277;
            this.splitContainer1.TabIndex = 2;
            // 
            // treeFolder
            // 
            this.treeFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeFolder.ImageIndex = 2;
            this.treeFolder.ImageList = this.icons;
            this.treeFolder.Location = new System.Drawing.Point(0, 0);
            this.treeFolder.Name = "treeFolder";
            this.treeFolder.SelectedImageIndex = 2;
            this.treeFolder.Size = new System.Drawing.Size(278, 392);
            this.treeFolder.TabIndex = 0;
            this.treeFolder.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeFolder_AfterSelect);
            // 
            // browser
            // 
            this.browser.AllowWebBrowserDrop = false;
            this.browser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.browser.Location = new System.Drawing.Point(3, 0);
            this.browser.MinimumSize = new System.Drawing.Size(20, 20);
            this.browser.Name = "browser";
            this.browser.Size = new System.Drawing.Size(545, 392);
            this.browser.TabIndex = 1;
            this.browser.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.browser_DocumentCompleted);
            // 
            // cmdExport
            // 
            this.cmdExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdExport.Location = new System.Drawing.Point(12, 563);
            this.cmdExport.Name = "cmdExport";
            this.cmdExport.Size = new System.Drawing.Size(178, 49);
            this.cmdExport.TabIndex = 10;
            this.cmdExport.Text = "Export";
            this.cmdExport.UseVisualStyleBackColor = true;
            this.cmdExport.Click += new System.EventHandler(this.cmdExport_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(878, 649);
            this.Controls.Add(this.cmdExport);
            this.Controls.Add(this.tabControls);
            this.Controls.Add(this.cmdBrowseOutput);
            this.Controls.Add(this.txtOutputLocation);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblPFFVersion);
            this.Controls.Add(this.cmdBrowseInput);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtInputODT);
            this.Name = "Form1";
            this.Text = "PFF Exporter";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControls.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtInputODT;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cmdBrowseInput;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label lblPFFVersion;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtOutputLocation;
        private System.Windows.Forms.Button cmdBrowseOutput;
        private System.Windows.Forms.TreeView treeStructure;
        private System.Windows.Forms.TabControl tabControls;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TreeView treeFolder;
        private System.Windows.Forms.WebBrowser browser;
        private System.Windows.Forms.ImageList icons;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button cmdExport;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
    }
}

