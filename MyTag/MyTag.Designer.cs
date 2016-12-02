namespace MyTag
{
    partial class MyTag
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.bt_Save = new System.Windows.Forms.Button();
            this.cb_LinkToComment = new System.Windows.Forms.CheckBox();
            this.cb_AppendMode = new System.Windows.Forms.CheckBox();
            this.bt_Exit = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tagTreeView = new RikTheVeggie.TriStateTreeView();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bt_Save
            // 
            this.bt_Save.Location = new System.Drawing.Point(12, 352);
            this.bt_Save.Name = "bt_Save";
            this.bt_Save.Size = new System.Drawing.Size(75, 23);
            this.bt_Save.TabIndex = 1;
            this.bt_Save.Text = "Save";
            this.bt_Save.UseVisualStyleBackColor = true;
            this.bt_Save.Click += new System.EventHandler(this.bt_Save_Click);
            // 
            // cb_LinkToComment
            // 
            this.cb_LinkToComment.AutoSize = true;
            this.cb_LinkToComment.Location = new System.Drawing.Point(12, 285);
            this.cb_LinkToComment.Name = "cb_LinkToComment";
            this.cb_LinkToComment.Size = new System.Drawing.Size(149, 19);
            this.cb_LinkToComment.TabIndex = 2;
            this.cb_LinkToComment.Text = "link to comment";
            this.cb_LinkToComment.UseVisualStyleBackColor = true;
            // 
            // cb_AppendMode
            // 
            this.cb_AppendMode.AutoSize = true;
            this.cb_AppendMode.Location = new System.Drawing.Point(12, 310);
            this.cb_AppendMode.Name = "cb_AppendMode";
            this.cb_AppendMode.Size = new System.Drawing.Size(181, 19);
            this.cb_AppendMode.TabIndex = 3;
            this.cb_AppendMode.Text = "comment append mode";
            this.cb_AppendMode.UseVisualStyleBackColor = true;
            // 
            // bt_Exit
            // 
            this.bt_Exit.Location = new System.Drawing.Point(240, 352);
            this.bt_Exit.Name = "bt_Exit";
            this.bt_Exit.Size = new System.Drawing.Size(75, 23);
            this.bt_Exit.TabIndex = 4;
            this.bt_Exit.Text = "Exit";
            this.bt_Exit.UseVisualStyleBackColor = true;
            this.bt_Exit.Click += new System.EventHandler(this.bt_Exit_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 385);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(349, 22);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            this.toolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tagTreeView
            // 
            this.tagTreeView.Location = new System.Drawing.Point(0, 0);
            this.tagTreeView.Name = "tagTreeView";
            this.tagTreeView.Size = new System.Drawing.Size(349, 262);
            this.tagTreeView.TabIndex = 0;
            this.tagTreeView.TriStateStyleProperty = RikTheVeggie.TriStateTreeView.TriStateStyles.Standard;
            // 
            // MyTag
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(349, 407);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.bt_Exit);
            this.Controls.Add(this.cb_AppendMode);
            this.Controls.Add(this.cb_LinkToComment);
            this.Controls.Add(this.bt_Save);
            this.Controls.Add(this.tagTreeView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MyTag";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MyTag";
            this.Load += new System.EventHandler(this.MyTag_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MyTag_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MyTag_DragEnter);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private RikTheVeggie.TriStateTreeView tagTreeView;
        private System.Windows.Forms.Button bt_Save;
        private System.Windows.Forms.CheckBox cb_LinkToComment;
        private System.Windows.Forms.CheckBox cb_AppendMode;
        private System.Windows.Forms.Button bt_Exit;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
    }
}

