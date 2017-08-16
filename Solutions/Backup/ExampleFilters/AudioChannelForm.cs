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

namespace ExampleFilters
{
    [ComVisible(true)]
    [Guid("8751B7B2-3EB0-45a4-A3EE-F0954088DEC8")]
    public partial class AudioChannelForm : BasePropertyPage
    {
        #region Classes

        protected class ChannelDecs
        {
            AudioChannel m_Channel;

            public ChannelDecs(AudioChannel _channel)
            {
                m_Channel = _channel;
            }

            public AudioChannel Channel
            {
                get { return m_Channel; }
            }

            public override string ToString()
            {
                switch (m_Channel)
                {
                    case AudioChannel.FRONT_LEFT:
                        return "Front Left";
                    case AudioChannel.FRONT_RIGHT:
                        return "Front Right";
                    case AudioChannel.BACK_LEFT:
                        return "Back Left";
                    case AudioChannel.BACK_RIGHT:
                        return "Back Right";
                    case AudioChannel.FRONT_CENTER:
                        return "Front Center";
                    case AudioChannel.LOW_FREQUENCY:
                        return "Low Frequency";
                    case AudioChannel.SIDE_LEFT:
                        return "Side Left";
                    case AudioChannel.SIDE_RIGHT:
                        return "Side Right";
                }
                return base.ToString();
            }

            public override bool Equals(object obj)
            {
                if (obj.GetType() == typeof(AudioChannel))
                {
                    return m_Channel == (AudioChannel)obj;
                }
                if (obj.GetType() == typeof(ChannelDecs))
                {
                    return m_Channel == ((ChannelDecs)obj).m_Channel;
                }
                return base.Equals(obj);
            }

            public override int GetHashCode()
            {
                return m_Channel.GetHashCode();
            }

            public static implicit operator AudioChannel(ChannelDecs _item)
            {
                return _item.Channel;
            }

            public static explicit operator ChannelDecs(AudioChannel _channel)
            {
                return new ChannelDecs(_channel);
            }

            public static bool operator !=(ChannelDecs _src, ChannelDecs _dest)
            {
                return !(_src == _dest);
            }

            public static bool operator ==(ChannelDecs _src, ChannelDecs _dest)
            {
                return _src.m_Channel == _dest.m_Channel;
            }
        }

        #endregion

        #region Variables

        private IAudioChannel m_pChannel = null;

        #endregion

        #region Constructor

        public AudioChannelForm()
        {
            InitializeComponent();
        }

        #endregion

        #region Form Handlers

        private void AudioChannelForm_Load(object sender, EventArgs e)
        {
            AudioChannel[] _channels = (AudioChannel[])Enum.GetValues(typeof(AudioChannel));
            foreach (AudioChannel _channel in _channels) cmboChannel.Items.Add(new ChannelDecs(_channel));
            if (m_pChannel != null)
            {
                AudioChannel _channel;
                m_pChannel.get_ActiveChannel(out _channel);
                cmboChannel.SelectedItem = _channel;
            }
        }

        private void cmboChannel_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Dirty = true;
        }

        #endregion

        #region Overridden Methods

        public override HRESULT OnConnect(IntPtr pUnknown)
        {
            m_pChannel = (IAudioChannel)Marshal.GetObjectForIUnknown(pUnknown);
            return HRESULT.NOERROR;
        }

        public override HRESULT OnDisconnect()
        {
            m_pChannel = null;
            return HRESULT.NOERROR;
        }

        public override HRESULT OnApplyChanges()
        {
            if (m_pChannel != null && cmboChannel.SelectedItem != null)
            {
                AudioChannel _channel = (cmboChannel.SelectedItem as ChannelDecs);
                return (HRESULT)m_pChannel.put_ActiveChannel(_channel);
            }
            return HRESULT.NOERROR;
        }

        #endregion
    }
}
