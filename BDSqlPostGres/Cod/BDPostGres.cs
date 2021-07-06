using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDSqlPostGres.Cod
{
    public class BDPostGres 
    {
        //IP Servidor
        private string _servidor;
        public string Servidor
        {
            get { return _servidor; }
            set
            {
                _servidor = value;
            }
        }

        private string _porta;
        public string Porta
        {
            get { return _porta; }
            set
            {
                _porta = value;
            }
        }
        //porta
        private string _banco;
        public string Banco
        {
            get { return _banco; }
            set
            {
                _banco = value;
            }
        }
        private string _usuario;
        public string Usuario
        {
            get { return _usuario; }
            set
            {
                _usuario = value;
            }
        }


        private string _senha;
        public string Senha
        {
            get { return _senha; }
            set
            {
                _senha = value;
            }
        }

        public string _strConn;
        public string StrConn
        {
            
            get { return StrConn = "Server=" + Servidor + ";Port=" + Porta + ";User Id=" + Usuario + ";Password=" + Senha + ";Database=" + Banco; ; }
            private set
            {
                _strConn = value;
            }
        }

        private string _pastaBkp;
        public string PastaBkp
        {
            get { return PastaBkp; }
            set
            {
                _pastaBkp = value;
            }
        }

        public BDPostGres()
        {
            this.Servidor = "";
            this.Porta = "";
            this.Banco = "";
            this.Usuario = "";
            this.Senha = "";
            this.StrConn = "";
            this.PastaBkp = "";
        }
    }
}
