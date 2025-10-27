using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IsTama.NengaBooster.Core.NengaApps;
using IsTama.NengaBooster.Error;
using IsTama.Utils;

namespace IsTama.NengaBooster.Infrastructures.RPA
{
    sealed class NativeWindowOperator : INativeWindowOperator
    {
        private readonly string _windowTitlePattern;
        private readonly int _maxWindowWidth;
        private readonly WindowPool _windowPool;
        private readonly IKeyboard _keyboard;

        private readonly IEnumerable<(Func<WindowController, object[], bool>, object[])> _operationList;
        private readonly bool _shouldThrowException;


        public NativeWindowOperator(string windowTitlePattern, int maxWindowWidth, WindowPool windowPool, IKeyboard keyboard)
        {
            Assert.IsNullOrEmpty(windowTitlePattern, nameof(windowTitlePattern));
            Assert.IsSmallerThan(maxWindowWidth, 1, nameof(maxWindowWidth));
            Assert.IsNull(windowPool, nameof(windowPool));

            _windowTitlePattern = windowTitlePattern;
            _maxWindowWidth = maxWindowWidth;
            _windowPool = windowPool;
            _operationList = new List<(Func<WindowController, object[], bool>, object[])>();
            _keyboard = keyboard;
            _shouldThrowException = false;
        }

        private NativeWindowOperator(
            string windowTitlePattern,
            int maxWindowWidth,
            WindowPool windowPool,
            IKeyboard keyboard,
            IEnumerable<(Func<WindowController, object[], bool>, object[])> oplist,
            bool shouldThrowException)
        {
            _windowTitlePattern = windowTitlePattern;
            _maxWindowWidth = maxWindowWidth;
            _windowPool = windowPool;
            _keyboard = keyboard;
            _operationList = oplist;
            _shouldThrowException = shouldThrowException;
        }


        private WindowController GetWindowController(int waittime_ms)
        {
            return _windowPool.GetOrCreateWindowController(_windowTitlePattern, _maxWindowWidth, waittime_ms);
        }

        private FormControl GetFormControl(WindowController w, string controlName)
        {
            var controls = w.GetFormControls(controlName);
            if (controls.Count == 0)
                throw new NengaBoosterException($"{controlName} に該当するコントロールが見つかりません。");
            if (controls.Count > 1)
                throw new NengaBoosterException($"{controlName} に該当するコントロールが複数あります。");
            return controls[0];
        }

        private FormControl GetFormControl(WindowController w, Point controlPoint)
        {
            var controls = w.GetFormControls(controlPoint);
            if (controls.Count == 0)
                throw new NengaBoosterException($"座標[X:{controlPoint.X} Y:{controlPoint.Y}] にコントロールが見つかりません。");
            if (controls.Count > 1)
                throw new NengaBoosterException($"座標[X:{controlPoint.X} Y:{controlPoint.Y}] にコントロールが複数あります。");
            return controls[0];
        }

        private INativeWindowOperator AppendOperation(Func<WindowController, object[], bool> operation, params object[] args)
        {
            return new NativeWindowOperator(
                _windowTitlePattern,
                _maxWindowWidth,
                _windowPool,
                _keyboard,
                _operationList.Append((operation, args)), _shouldThrowException);
        }


        public INativeWindowOperator Activate()
        {
            bool Op(WindowController w, object[] args)
            {
                return w.Activate();
            };
            return AppendOperation(Op, null);
        }

        public INativeWindowOperator Focus(string controlName)
        {
            bool Op(WindowController w, object[] args)
            {
                var control = GetFormControl(w, (string)args[0]);
                return control.Focus();
            }
            return AppendOperation(Op, controlName);
        }

        public INativeWindowOperator Focus(Point controlPoint)
        {
            bool Op(WindowController w, object[] args)
            {
                var control = GetFormControl(w, (Point)args[0]);
                return control.Focus();
            }
            return AppendOperation(Op, controlPoint);
        }

        public INativeWindowOperator LeftClick(string controlName)
        {
            bool Op(WindowController w, object[] args)
            {
                var control = GetFormControl(w, (string)args[0]);
                control.LeftClick();
                return true;
            }
            return AppendOperation(Op, controlName);
        }

        public INativeWindowOperator LeftClick(Point controlPoint)
        {
            bool Op(WindowController w, object[] args)
            {
                var control = GetFormControl(w, (Point)args[0]);
                control.LeftClick();
                return true;
            }
            return AppendOperation(Op, controlPoint);
        }

        public INativeWindowOperator RightClick(string controlName)
        {
            bool Op(WindowController w, object[] args)
            {
                var control = GetFormControl(w, (string)args[0]);
                control.RightClick();
                return true;
            }
            return AppendOperation(Op, controlName);
        }

        public INativeWindowOperator RightClick(Point controlPoint)
        {
            bool Op(WindowController w, object[] args)
            {
                var control = GetFormControl(w, (Point)args[0]);
                control.RightClick();
                return true;
            }
            return AppendOperation(Op, controlPoint);
        }

        public INativeWindowOperator SendEnterTo(string controlName)
        {
            bool Op(WindowController w, object[] args)
            {
                var control = GetFormControl(w, (string)args[0]);
                if (!control.Focus())
                    return false;

                var keyboard = (IKeyboard)args[1];
                keyboard.PressKey(InputKey.ENTER);

                return true;
            }
            return AppendOperation(Op, controlName, _keyboard);
        }

        public INativeWindowOperator SendEnterTo(Point controlPoint)
        {
            bool Op(WindowController w, object[] args)
            {
                var control = GetFormControl(w, (Point)args[0]);
                if (!control.Focus())
                    return false;

                var keyboard = (IKeyboard)args[1];
                keyboard.PressKey(InputKey.ENTER);

                return true;
            }
            return AppendOperation(Op, controlPoint, _keyboard);
        }

        public INativeWindowOperator SetText(Point controlPoint, string text)
        {
            bool Op(WindowController w, object[] args)
            {
                var control = GetFormControl(w, (Point)args[0]);
                control.SetText((string)args[1]);
                return true;
            }
            return AppendOperation(Op, controlPoint, text);
        }

        public INativeWindowOperator ThrowIfFailed()
        {
            return new NativeWindowOperator(_windowTitlePattern, _maxWindowWidth, _windowPool, _keyboard, _operationList, true);
        }

        public INativeWindowOperator Wait(int waittime_ms)
        {
            bool Op(WindowController w, object[] args)
            {
                Thread.Sleep((int)args[0]);
                return true;
            }
            return AppendOperation(Op, waittime_ms);
        }


        public async Task<bool> DoAsync()
        {
            return await DoAsync(0);
        }

        public async Task<bool> DoAsync(int waittimeForProcessToExecute)
        {
            return await Task.Factory.StartNew(() =>
            {
                var w = GetWindowController(waittimeForProcessToExecute);

                foreach (var (operation, args) in _operationList)
                {
                    var (success, emsg) = Execute(w, operation, args);
                    if (!success)
                    {
                        if (_shouldThrowException)
                            throw new NengaBoosterException(emsg);
                        else
                            return false;
                    }
                }

                return true;
            }).ConfigureAwait(false);
        }

        public bool Do()
        {
            return Do(0);
        }

        public bool Do(int waittimeForProcessToExecute)
        {
            var w = GetWindowController(waittimeForProcessToExecute);

            foreach (var (operation, args) in _operationList)
            {
                var (success, emsg) = Execute(w, operation, args);
                if (!success)
                {
                    if (_shouldThrowException)
                        throw new NengaBoosterException(emsg);
                    else
                        return false;
                }
            }

            return true;
        }

        private (bool, string) Execute(WindowController w, Func<WindowController, object[], bool> op, object[] args_)
        {
            try
            {
                op(w, args_);
                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }
    }
}
