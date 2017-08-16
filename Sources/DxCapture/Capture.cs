using System;
using Sonic;
using DirectShow;
using System.Runtime.InteropServices;

namespace DxCapture
{
    public class VideoCaptureGraph : DSFilterGraphBase, IFileDestSupport
    {
        #region Variables

        private string m_sOutputFileName = "";
        private DSFilter m_pCaptureDevice = null;

        #endregion

        #region Properties

        public DSFilter CaptureDevice
        {
            get { return m_pCaptureDevice; }
            set
            {
                if (!IsStopped) VFW_E_NOT_STOPPED.Throw();
                m_pCaptureDevice = value;
            }
        }

        #endregion

        #region IFileDestSupport Members

        public string OutputFileName
        {
            get
            {
                return m_sOutputFileName;
            }
            set
            {
                Save(value, false);
            }
        }

        public HRESULT Save()
        {
            return Save(true);
        }

        public HRESULT Save(bool bStart)
        {
            return Save(m_sOutputFileName, bStart);
        }

        public HRESULT Save(string sFileName)
        {
            return Save(sFileName, false);
        }

        public HRESULT Save(string sFileName, bool bStart)
        {
            m_sOutputFileName = sFileName;
            return NOERROR;
        }

        #endregion

        #region Overridden methods

        protected override HRESULT OnInitInterfaces()
        {
            HRESULT hr;
            // Create Capture Graph Builder
            ICaptureGraphBuilder2 _capture = (ICaptureGraphBuilder2)new CaptureGraphBuilder2();
            // Set Filter Graph
            _capture.SetFiltergraph(m_GraphBuilder);
            // Insert Capture Device into the Graph
            m_pCaptureDevice.FilterGraph = m_GraphBuilder;
            IBaseFilter _muxer = null;
            IFileSinkFilter _sync = null;
            // if we output into a file too
            if (!string.IsNullOrEmpty(m_sOutputFileName))
            {
                // Setup output for an avi file
                hr = (HRESULT)_capture.SetOutputFileName(MediaSubType.Avi, m_sOutputFileName, out _muxer, out _sync);
                hr.Assert();
                if (SUCCEEDED(hr))
                {
                    // Connect capture
                    hr = (HRESULT)_capture.RenderStream(PinCategory.Capture, MediaType.Video, m_pCaptureDevice.Value, null, _muxer);
                    hr.Assert();
                }
            }
            // Make preview
            hr = (HRESULT)_capture.RenderStream(PinCategory.Preview, MediaType.Video, m_pCaptureDevice.Value, null, null);
            Marshal.FinalReleaseComObject(_capture);
            return hr;
        }

        protected override HRESULT OnCloseInterfaces()
        {
            if (m_pCaptureDevice != null)
            {
                m_pCaptureDevice.FilterGraph = null;
            }
            return base.OnCloseInterfaces();
        }

        #endregion
    }
}