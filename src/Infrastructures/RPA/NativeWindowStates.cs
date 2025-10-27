using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using IsTama.NengaBooster.Core.NengaApps;
using IsTama.NengaBooster.Error;
using IsTama.Utils;

namespace IsTama.NengaBooster.Infrastructures.RPA
{
    /// <summary>
    /// ウィンドウの状態や情報を取得するクラス。
    /// </summary>
    sealed class NativeWindowStates : INativeWindowStates
    {
        private readonly string _windowTitlePattern;
        private readonly int _maxWindowWidth;
        private readonly WindowPool _windowPool;


        public NativeWindowStates(string windowTitlePattern, int maxWindowWidth, WindowPool windowPool)
        {
            Assert.IsNullOrEmpty(windowTitlePattern, nameof(windowTitlePattern));
            Assert.IsSmallerThan(maxWindowWidth, 1, nameof(maxWindowWidth));
            Assert.IsNull(windowPool, nameof(windowPool));

            _windowTitlePattern = windowTitlePattern;
            _maxWindowWidth = maxWindowWidth;
            _windowPool = windowPool;
        }


        private WindowController GetController(int waittime_ms)
        {
            return _windowPool.GetOrCreateWindowController(_windowTitlePattern, _maxWindowWidth, waittime_ms);
        }

        private bool TryGetController(int waittime_ms, out WindowController controller)
        {
            return _windowPool.TryGetOrCreateWindowController(_windowTitlePattern, _maxWindowWidth, waittime_ms, out controller);
        }
        
        public bool Exists(int waittime_ms)
        {
            return TryGetController(waittime_ms, out var controller) && controller.Exists();
        }

        public bool IsOpen(int waittime_ms)
        {
            return TryGetController(waittime_ms, out var controller) && controller.IsOpen();
        }

        public bool IsActivated(int waittime_ms)
        {
            return TryGetController(waittime_ms, out var controller) && controller.IsActivated();
        }


        public Point GetPoint()
        {
            return GetController(0).GetRelativePoint();
        }

        public Size GetSize()
        {
            return GetController(0).GetSize();
        }

        public string GetTitle()
        {
            return GetController(0).GetText();
        }
        

        public INativeFormControlStates GetFormControlStates(int index)
        {
            Assert.IsSmallerThan(index, 0, nameof(index));

            var controls = GetController(0).FindControls();
            if (index >= controls.Count)
                throw new NengaBoosterException($"インデックスの値がウィンドウ内のコントロールの総数を超えています。 / {index}");

            return new NativeFormControlStates(controls[index]);
        }

        public INativeFormControlStates[] GetFormControlStatesArray(string controlTitlePattern)
        {
            Assert.IsNull(controlTitlePattern, nameof(controlTitlePattern));

            var regex = new Regex(controlTitlePattern, RegexOptions.Compiled);
            var controls = GetController(0).FindControls();

            return controls
                .Where(c => regex.IsMatch(c.GetText()))
                .Select(c => new NativeFormControlStates(c))
                .ToArray();
        }

        public INativeFormControlStates[] GetFormControlStatesArray(Point point)
        {
            var controls = GetController(0).FindControls();

            return controls
                .Where(c => c.GetRelativePoint() == point)
                .Select(c => new NativeFormControlStates(c))
                .ToArray();
        }

        public INativeFormControlStates[] GetFormControlStatesArray(Point point, Size size)
        {
            var controls = GetController(0).FindControls();

            return controls
                .Where(c => c.GetRelativePoint() == point)
                .Where(c => c.GetSize() == size)
                .Select(c => new NativeFormControlStates(c))
                .ToArray();
        }
    }
}
