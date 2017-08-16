using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DirectShow.BaseClasses;
using System.Runtime.InteropServices;
using Sonic;
using System.Net;

namespace ExampleFilters
{
    [ComVisible(true)]
    [Guid("0273D255-AB7C-42c6-8E74-E0B1C5FEB4C2")]
    public partial class NetworkForm : BasePropertyPage
    {
        #region Variables

        private INetworkConfig m_pNetworkConfig = null;

        #endregion

        #region Constructor

        public NetworkForm()
        {
            InitializeComponent();
        }

        #endregion

        #region Form Handlers

        private void NetworkForm_Load(object sender, EventArgs e)
        {
            if (m_pNetworkConfig != null)
            {
                tbIP.Text = m_pNetworkConfig.IP;
                tbPort.Text = m_pNetworkConfig.Port.ToString();
            }
            Dirty = false;
        }

        private void EditTextChanged(object sender, EventArgs e)
        {
            Dirty = true;
        }

        #endregion

        #region Overridden Methods

        public override HRESULT OnConnect(IntPtr pUnknown)
        {
            m_pNetworkConfig = (INetworkConfig)Marshal.GetObjectForIUnknown(pUnknown);
            return HRESULT.NOERROR;
        }

        public override HRESULT OnDisconnect()
        {
            m_pNetworkConfig = null;
            return HRESULT.NOERROR;
        }

        public override HRESULT OnApplyChanges()
        {
            if (m_pNetworkConfig != null)
            {
                IPAddress _ip;
                if (IPAddress.TryParse(tbIP.Text,out _ip))
                {
                    m_pNetworkConfig.IP = tbIP.Text;
                }
                int _port;
                if (int.TryParse(tbPort.Text, out _port))
                {
                    m_pNetworkConfig.Port = _port;
                }
                tbIP.Text = m_pNetworkConfig.IP;
                tbPort.Text = m_pNetworkConfig.Port.ToString();
                Dirty = false;
            }
            return HRESULT.NOERROR;
        }

        #endregion
    }
}
