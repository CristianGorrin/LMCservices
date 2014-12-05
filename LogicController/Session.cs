using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Runtime.Serialization;

namespace LogicController
{
    partial class Session
    {
        private DatabaseConnection dbConnection;
        private Settings settings;

        public Session()
        {
            bool LoadDeff = false;
            if (File.Exists(@"data.bin"))
            {
                try
                {
                    var serialer = new Serialer();
                    var items = serialer.Load();

                    this.dbConnection = items.DatabaseConnection;
                    this.settings = items.Settings;
                }
                catch (Exception)
                {
                    LoadDeff = true;
                }
            }

            if (LoadDeff || this.dbConnection == null || this.settings == null)
            {
                // TODO deff
                this.dbConnection = new DatabaseConnection(@"(localdb)\ProjectsV12", "LMCdatabase", false, 30);
                this.settings = new Settings();
            }
        }

        ~Session()
        {
            var serialer = new Serialer();
            var items = new SerializeSession { DatabaseConnection = this.dbConnection, Settings = this.settings };

            serialer.Save(items);
        }

        #region DatabaseConnection
        public string ConnectionString { get { return this.dbConnection.ToString(); } }

        public void UpdateConnectionString(string dataSource, string initialCatalog, bool integratedSecurity, int? connectTimeout)
        {
            this.dbConnection.DataSource = dataSource;
            
            this.dbConnection.InitialCatalog = initialCatalog;
            
            this.dbConnection.IntegratedSecurity = integratedSecurity;
            
            if (connectTimeout != null)
                this.dbConnection.ConnectTimeout = (int)connectTimeout;
            else
                this.dbConnection.ConnectTimeout = 30;
        }
        #endregion


        #region Settings

        #endregion
    }

    // If data need to be save for next time look in SerializeSession.cs 
    [Serializable]
    public class Settings
    {

    }

    [Serializable]
    public class DatabaseConnection
    {
        private string dataSource;
        private string initialCatalog;
        private bool integratedSecurity;
        private int connectTimeout;

        public DatabaseConnection(string dataSource, string initialCatalog, bool integratedSecurity, int connectTimeout)
        {
            this.dataSource = dataSource;
            this.initialCatalog = initialCatalog;
            this.integratedSecurity = integratedSecurity;
            this.connectTimeout = connectTimeout;
        }

        public DatabaseConnection(string dataSource, string initialCatalog, bool integratedSecurity)
        {
            this.dataSource = dataSource;
            this.initialCatalog = initialCatalog;
            this.integratedSecurity = integratedSecurity;
            this.connectTimeout = 30;
        }

        public string DataSource { get { return this.dataSource; } set { this.dataSource = value; } }
        public string InitialCatalog { get { return this.initialCatalog; } set { this.initialCatalog = value; } }
        public bool IntegratedSecurity { get { return this.integratedSecurity; } set { this.integratedSecurity = value; } }
        public int ConnectTimeout { get { return this.connectTimeout; } set { this.connectTimeout = value; } }

        public override string ToString()
        {

            string connection = string.Empty;

            connection += "Data Source=" + this.dataSource + ";";
            connection += "Initial Catalog=" + this.initialCatalog + ";";
            connection += "Connect Timeout=" + this.connectTimeout.ToString() + ";";
            connection += "Integrated Security=";
            if (this.integratedSecurity) connection += "True"; else connection += "False";
            connection += ";";
            connection += "Encrypt=False;";
            connection += "TrustServerCertificate=False";

            return connection;
        }
    }
}
