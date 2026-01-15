using SAP.Middleware.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Folio_Liquidacion_API.Data.Security
{
    public class SAPSystemConnect
    {
        private RfcConfigParameters Parameters;
        //internal RfcRepository repository;

        #region Propiedades
        public RfcRepository Repository { get; set; }
        public RfcDestination Destination { get; set; }
        #region Parametros de Conexion
        public string _Name { get; set; }
        public string _AppServerHost { get; set; }
        public string _SystemNumber { get; set; }
        public string _User { get; set; }
        public string _Password { get; set; }
        public string _Client { get; set; }
        public string _Language { get; set; }
        public string _SAPRouter { get; set; }
        public string _PoolSize { get; set; }
        public string _PeakConnectionsLimit { get; set; }
        public string _PoolIdleTimeout { get; set; }
        public Servers _Server { get; set; }
        #endregion
        #endregion

        public enum Servers
        {
            DEV = 100,
            QAS = 301,
            PRD = 300
        }

        public SAPSystemConnect(Servers server)
        {
            switch (server)
            {
                case Servers.DEV:
                    this._Name = "TRACUSA - ECC - DEV";
                    this._AppServerHost = "172.35.40.69";
                    this._SystemNumber = "00";
                    this._User = "LMHERNANDEZ";
                    this._Password = "6ESTRELLAS";
                    this._Client = ((Int16)server).ToString();
                    this._Language = "";
                    //this._SAPRouter = "/H/201.175.38.100/H/";
                    this._PoolSize = "5";
                    this._PeakConnectionsLimit = "10";
                    this._PoolIdleTimeout = "600";
                    break;
                case Servers.QAS:
                    this._Name = "TRACUSA - ECC - QAS";
                    this._AppServerHost = "172.35.40.88";
                    this._SystemNumber = "10";
                    //this._User = "DESARROLLOTI";
                    this._User = "LMHERNANDEZ";
                    this._Password = "6ESTRELLAS";
                    //this._Password = "TRSqas21";
                    this._Client = "300"; //IDServer.ToString();
                    this._Language = "";
                    //this._SAPRouter = "/H/201.175.38.100/H/";
                    this._PoolSize = "5";
                    this._PeakConnectionsLimit = "10";
                    this._PoolIdleTimeout = "600";
                    break;
                case Servers.PRD:
                    this._Name = "TRACUSA - ECC - PRD";
                    this._AppServerHost = "172.35.40.89";
                    this._SystemNumber = "20";
                    this._User = "LMHERNANDEZ";
                    this._Password = "6ESTRELLAS";
                    this._Client = "300";
                    this._Language = "";
                    //this._SAPRouter = "/H/201.175.38.100/H/";
                    this._PoolSize = "5";
                    this._PeakConnectionsLimit = "10";
                    this._PoolIdleTimeout = "600";
                    break;
            }
            InitializeConexion();

        }

        public SAPSystemConnect(string name, string appserverhost, string systemnumber, string user, string password, string client, string language, string saprouter, string poolsize, string peakconnectionslimit, string poolidletimeout)
        {
            this._Name = name;
            this._AppServerHost = appserverhost;
            this._SystemNumber = systemnumber;
            this._User = user;
            this._Password = password;
            this._Client = client;
            this._Language = language;
            this._SAPRouter = saprouter;
            this._PoolSize = poolsize;
            this._PeakConnectionsLimit = peakconnectionslimit;
            this._PoolIdleTimeout = poolidletimeout;

            InitializeConexion();
        }

        private void InitializeConexion()
        {
            this.Parameters = new RfcConfigParameters
            {
                { RfcConfigParameters.Name, this._Name },
                { RfcConfigParameters.AppServerHost, this._AppServerHost },
                { RfcConfigParameters.SystemNumber, this._SystemNumber },
                { RfcConfigParameters.User, this._User },
                { RfcConfigParameters.Password, this._Password },
                { RfcConfigParameters.Client, this._Client },
                { RfcConfigParameters.Language, this._Language },
                { RfcConfigParameters.SAPRouter, this._SAPRouter },
                { RfcConfigParameters.PoolSize, this._PoolSize },
                { RfcConfigParameters.PeakConnectionsLimit, this._PeakConnectionsLimit },
                { RfcConfigParameters.PoolIdleTimeout, this._PoolIdleTimeout }
            };

            this.Destination = RfcDestinationManager.GetDestination(Parameters);
            this.Repository = this.Destination.Repository;
        }

        public bool ChangeEventsSupported()
        {
            throw new NotImplementedException();
        }

        //public event RfcDestinationManager.ConfigurationChangeHandler ConfigurationChanged;

        public RfcConfigParameters GetParameters(string destinationName)
        {
            return this.Parameters;
        }
    }
}