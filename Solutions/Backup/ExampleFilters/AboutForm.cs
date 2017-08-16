using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using DirectShow.BaseClasses;

namespace ExampleFilters
{
    [ComVisible(true)]
    [Guid("56F4E96B-D101-4de8-BC48-8C4311C9C8C4")]
    public partial class AboutForm : BasePropertyPage
    {
        public AboutForm()
        {
            InitializeComponent();
        }
    }
}
