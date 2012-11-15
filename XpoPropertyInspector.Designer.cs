namespace CodeIssueSearcher
{
    partial class CodeIssueSearcher
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public CodeIssueSearcher()
        {
            /// <summary>
            /// Required for Windows.Forms Class Composition Designer support
            /// </summary>
            InitializeComponent();
        }

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
            this.my_issue_provider = new DevExpress.CodeRush.Core.IssueProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.Images16x16)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.my_issue_provider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // my_issue_provider
            // 
            this.my_issue_provider.DefaultIssueType = DevExpress.CodeRush.Core.CodeIssueType.Warning;
            this.my_issue_provider.Description = "Test auf falsche INotifyProperties";
            this.my_issue_provider.DisplayName = "INotify-Tester";
            this.my_issue_provider.ProviderName = "WrongProperty";
            this.my_issue_provider.Register = true;
            this.my_issue_provider.CheckCodeIssues += new DevExpress.CodeRush.Core.CheckCodeIssuesEventHandler(this.my_issue_provider_CheckCodeIssues);
            ((System.ComponentModel.ISupportInitialize)(this.Images16x16)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.my_issue_provider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.CodeRush.Core.IssueProvider my_issue_provider;
    }
}