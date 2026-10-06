namespace ServerCloakService
{
    partial class ProjectInstaller
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ServerCloakServiceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.ServerCloakServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // ServerCloakServiceProcessInstaller
            // 
            this.ServerCloakServiceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.ServerCloakServiceProcessInstaller.Password = null;
            this.ServerCloakServiceProcessInstaller.Username = null;
            // 
            // ServerCloakServiceInstaller
            // 
            this.ServerCloakServiceInstaller.ServiceName = "ServerCloakService";
            this.ServerCloakServiceInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.ServerCloakServiceProcessInstaller,
            this.ServerCloakServiceInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller ServerCloakServiceProcessInstaller;
        private System.ServiceProcess.ServiceInstaller ServerCloakServiceInstaller;
    }
}