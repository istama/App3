using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IsTama.NengaBooster.Core.Configs;
using IsTama.Utils;

namespace IsTama.NengaBooster.Core.NengaApps
{
    sealed class NengaAppWindowFactory
    {
        private readonly INativeWindowFactory _nativeWindowFactory;

        private NengaMenuConfig _nengaMenuConfigBuffer;
        private NengaMenuWindow _nengaMenuWindowBuffer;
        private LoginFormConfig _loginFormConfigBuffer;
        private LoginFormWindow _loginFormWindowBuffer;

        private NaireApplicationConfig _naireApplicationConfigBuffer;
        private NaireWindow _naireWindowBuffer;
        private HensyuApplicationConfig _hensyuApplicationConfigBuffer;
        private HensyuWindow _hensyuWindowBuffer;
        private InformationApplicationConfig _infoApplicationConfigBuffer;
        private InformationWindow _infoWindowBuffer;
        private InformationDetailApplicationConfig _infoDetailApplicationConfigBuffer;
        private InformationDetailWindow _infoDetailWindowBuffer;
        private KouseishiApplicationConfig _kouseishiApplicationConfigBuffer;
        private KouseishiWindow _kouseishiWindowBuffer;

        private readonly Dictionary<string, DialogConfig> _dialogConfigBufferMap = new Dictionary<string, DialogConfig>();
        private readonly Dictionary<string, DialogWindow> _dialogWindowBufferMap = new Dictionary<string, DialogWindow>();


        public NengaAppWindowFactory(INativeWindowFactory nativeWindowFactory)
        {
            Assert.IsNull(nativeWindowFactory, nameof(nativeWindowFactory));

            _nativeWindowFactory = nativeWindowFactory;
        }


        public NengaMenuWindow GetOrCreateNengaMenuWindow(NengaMenuConfig config)
        {
            if (config != _nengaMenuConfigBuffer)
            {
                _nengaMenuConfigBuffer = config;
                _nengaMenuWindowBuffer = new NengaMenuWindow(_nativeWindowFactory, config);
            }
            return _nengaMenuWindowBuffer;
        }

        public LoginFormWindow GetOrCreateLoginFormWindow(LoginFormConfig config)
        {
            if (config != _loginFormConfigBuffer)
            {
                _loginFormConfigBuffer = config;
                _loginFormWindowBuffer = new LoginFormWindow(_nativeWindowFactory, config);
            }
            return _loginFormWindowBuffer;
        }

        public NaireWindow GetOrCreateNaireWindow(NaireApplicationConfig config)
        {
            if (config != _naireApplicationConfigBuffer)
            {
                _naireApplicationConfigBuffer = config;
                _naireWindowBuffer = new NaireWindow(_nativeWindowFactory, config);
            }
            return _naireWindowBuffer;
        }

        public HensyuWindow GetOrCreateHensyuWindow(HensyuApplicationConfig config)
        {
            if (config != _hensyuApplicationConfigBuffer)
            {
                _hensyuApplicationConfigBuffer = config;
                _hensyuWindowBuffer = new HensyuWindow(_nativeWindowFactory, config);
            }
            return _hensyuWindowBuffer;
        }

        public InformationWindow GetOrCreateInformationWindow(InformationApplicationConfig config)
        {
            if (config != _infoApplicationConfigBuffer)
            {
                _infoApplicationConfigBuffer = config;
                _infoWindowBuffer = new InformationWindow(_nativeWindowFactory, config);
            }
            return _infoWindowBuffer;
        }

        public InformationDetailWindow GetOrCreateInformationDetailWindow(InformationDetailApplicationConfig config)
        {
            if (config != _infoDetailApplicationConfigBuffer)
            {
                _infoDetailApplicationConfigBuffer = config;
                _infoDetailWindowBuffer = new InformationDetailWindow(_nativeWindowFactory, config);
            }
            return _infoDetailWindowBuffer;
        }

        public KouseishiWindow GetOrCreateKouseishiWindow(KouseishiApplicationConfig config)
        {
            if (config != _kouseishiApplicationConfigBuffer)
            {
                _kouseishiApplicationConfigBuffer = config;
                _kouseishiWindowBuffer = new KouseishiWindow(_nativeWindowFactory, config);
            }
            return _kouseishiWindowBuffer;
        }

        public DialogWindow GetOrCreateNaireDialogWindow(DialogConfig config)
        {
            return GetOrCreateDialogWindow("naire", config);
        }

        public DialogWindow GetOrCreateHensyuDialogWindow(DialogConfig config)
        {
            return GetOrCreateDialogWindow("hensyu", config);
        }

        public DialogWindow GetOrCreateInformationDialogWindow(DialogConfig config)
        {
            return GetOrCreateDialogWindow("information", config);
        }

        public DialogWindow GetOrCreateKouseishiDialogWindow(DialogConfig config)
        {
            return GetOrCreateDialogWindow("kouseishi", config);
        }

        private DialogWindow GetOrCreateDialogWindow(string key, DialogConfig config)
        {
            if (!_dialogConfigBufferMap.ContainsKey(key))
            {
                _dialogConfigBufferMap.Add(key, config);
                var window = new DialogWindow(_nativeWindowFactory, config);
                _dialogWindowBufferMap.Add(key, window);
                return window;
            }

            if (config != _dialogConfigBufferMap[key])
            {
                _dialogConfigBufferMap[key] = config;
                var window = new DialogWindow(_nativeWindowFactory, config);
                _dialogWindowBufferMap[key] = window;
                return window;
            }

            return _dialogWindowBufferMap[key];
        }
    }
}
