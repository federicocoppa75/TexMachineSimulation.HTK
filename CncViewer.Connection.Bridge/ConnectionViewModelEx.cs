using GZSoft.Tex.Controller.Interface;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using CVCIE = CncViewer.Connection.Interfaces.Enums;

namespace CncViewer.Connection.Bridge
{
    public class ConnectionViewModelEx : ConnectionViewModel
    {
        private bool _initialized;
        private VariablesSet _outSet;
        private VariablesSet _flagSet;
        private VariablesSet _fixPntSet;

        private IDigitalOutputService _oService;
        private IVolatileBitVariableService _fService;
        private IFixedPointVariableService _vService;


        protected override bool ReadVariables()
        {
            if (!CheckConnection()) return false;
            if (!_initialized) InitVarriablesSet();

            _outSet.Read(_oService);
            _flagSet.Read(_fService);
            _fixPntSet.Read(_vService);

            return true;
        }

        private void InitVarriablesSet()
        {
            _outSet = VariablesSet.Create(Variables, CVCIE.VariableType.Out);
            _flagSet = VariablesSet.Create(Variables, CVCIE.VariableType.Flag);
            _fixPntSet = VariablesSet.Create(Variables, CVCIE.VariableType.DWord);

            _oService = (IDigitalOutputService)_controller.GetControllerService(typeof(IDigitalOutputService));
            _fService = (IVolatileBitVariableService)_controller.GetControllerService(typeof(IVolatileBitVariableService));
            _vService = (IFixedPointVariableService)_controller.GetControllerService (typeof(IFixedPointVariableService));

            _initialized= true;
        }

        protected override void OnVariablesCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            base.OnVariablesCollectionChanged(sender, e);

            _initialized= false;
            _outSet = null;
            _flagSet = null;
            _fixPntSet= null;
        }
    }
}
