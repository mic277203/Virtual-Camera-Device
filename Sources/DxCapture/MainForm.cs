using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Sonic;
using DirectShow;

namespace DxCapture
{
    public partial class MainForm : Form
    {
        VideoCaptureGraph m_Capture = null;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            m_Capture = new VideoCaptureGraph();
            m_Capture.VideoControl = this.pbPreview;
            m_Capture.OnPlaybackStart += new EventHandler(Playback_OnPlaybackStart);
            m_Capture.OnPlaybackStop += new EventHandler(Playback_OnPlaybackStop);
            Playback_OnPlaybackStop(sender, e);
            btnCapture.Enabled = false;
            List<DSDevice> _devices = (new DSVideoCaptureCategory()).Objects;
            foreach (DSDevice _device in _devices) cmboCaptureDevice.Items.Add(_device);
            if (cmboCaptureDevice.Items.Count > 0) cmboCaptureDevice.SelectedIndex = 0;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            m_Capture.Dispose();
            m_Capture = null;
        }

        private void Playback_OnPlaybackStop(object sender, EventArgs e)
        {
            btnCapture.Text = "Capture";
            btnProperties.Enabled = (m_Capture.CaptureDevice != null && m_Capture.CaptureDevice.HaveProperties);
            btnBrowseDest.Enabled = true;
            cmboCaptureDevice.Enabled = true;
        }

        private void Playback_OnPlaybackStart(object sender, EventArgs e)
        {
            btnCapture.Text = "Stop";
            btnProperties.Enabled = false;
            btnBrowseDest.Enabled = false;
            cmboCaptureDevice.Enabled = false;
        }

        private void cmboCaptureDevice_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_Capture.CaptureDevice = null;
            if (cmboCaptureDevice.SelectedItem != null)
            {
                m_Capture.CaptureDevice = ((DSDevice)cmboCaptureDevice.SelectedItem).Filter;
                if (m_Capture.CaptureDevice != null && !m_Capture.CaptureDevice.IsValid)
                {
                    m_Capture.CaptureDevice = null;
                }
            }
            btnCapture.Enabled = (m_Capture.CaptureDevice != null && m_Capture.CaptureDevice.IsValid);
            btnProperties.Enabled = (m_Capture.CaptureDevice != null && m_Capture.CaptureDevice.HaveProperties);
        }

        private void btnProperties_Click(object sender, EventArgs e)
        {
            m_Capture.CaptureDevice.ShowProperties(Handle);
        }

        private void btnBrowseDest_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                tbDestFileName.Text = saveFileDialog.FileName;
                m_Capture.OutputFileName = saveFileDialog.FileName;
            }
        }

        private void btnCapture_Click(object sender, EventArgs e)
        {
            if (m_Capture.IsStopped)
            {
                m_Capture.Start();
            }
            else
            {
                m_Capture.Stop();
            }
        }
    }
}
