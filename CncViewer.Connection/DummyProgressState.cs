using Machine.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CncViewer.Connection
{
    public class DummyProgressState : IProgressState
    {
        public ProgressDirection ProgressDirection => ProgressDirection.Farward;

        public int ProgressIndex => 0;

        public event EventHandler<int> ProgressIndexChanged;
    }
}
