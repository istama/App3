using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IsTama.NengaBooster.Core.NengaApps;
using IsTama.Utils;

namespace IsTama.NengaBooster.Infrastructures.RPA
{
    /// ウィンドウを操作するクラスを取得・作成するファクトリクラス。
    sealed class NativeWindowFactory : INativeWindowFactory
    {
        private readonly WindowPool _windowPool;
        private readonly IKeyboard _keyboard;

        private readonly Dictionary<TitleAndWidth, INativeWindowStates> _windowStatesBuffer = new Dictionary<TitleAndWidth, INativeWindowStates>();
        private readonly Dictionary<TitleAndWidth, INativeWindowOperator> _windowOperatorBuffer = new Dictionary<TitleAndWidth, INativeWindowOperator>();


        public NativeWindowFactory(WindowPool windowPool, IKeyboard keyboard)
        {
            Assert.IsNull(windowPool, nameof(windowPool));
            Assert.IsNull(keyboard, nameof(keyboard));

            _windowPool = windowPool;
            _keyboard = keyboard;
        }


        /// ウィンドウの情報を取得するクラスを取得または作成する。
        public INativeWindowStates GetOrCreateWindowStates(string windowTitlePattern)
        {
            return GetOrCreateWindowStates(windowTitlePattern, 9999);
        }

        public INativeWindowStates GetOrCreateWindowStates(string windowTitlePattern, int maxWindowWidth)
        {
            Assert.IsNullOrWhiteSpace(windowTitlePattern, nameof(windowTitlePattern));
            Assert.IsSmallerThan(maxWindowWidth, 1, nameof(maxWindowWidth));

            var key = new TitleAndWidth(windowTitlePattern, maxWindowWidth);

            if (!_windowStatesBuffer.ContainsKey(key))
            {
                var states = new NativeWindowStates(key.Title, key.Width, _windowPool);
                _windowStatesBuffer.Add(key, states);
            }

            return _windowStatesBuffer[key];
        }

        /// ウィンドウを操作するクラスを取得または作成する。
        public INativeWindowOperator GetOrCreateWindowOperator(string windowTitlePattern)
        {
            return GetOrCreateWindowOperator(windowTitlePattern, 9999);
        }

        public INativeWindowOperator GetOrCreateWindowOperator(string windowTitlePattern, int maxWindowWidth)
        {
            Assert.IsNullOrWhiteSpace(windowTitlePattern, nameof(windowTitlePattern));
            Assert.IsSmallerThan(maxWindowWidth, 1, nameof(maxWindowWidth));

            var key = new TitleAndWidth(windowTitlePattern, maxWindowWidth);

            if (!_windowOperatorBuffer.ContainsKey(key))
            {
                var op = new NativeWindowOperator(key.Title, key.Width, _windowPool, _keyboard);
                _windowOperatorBuffer.Add(key, op);
            }

            return _windowOperatorBuffer[key];
        }
    }
}
