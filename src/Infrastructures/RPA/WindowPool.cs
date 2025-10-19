using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IsTama.NengaBooster.Error;
using IsTama.Utils;

namespace IsTama.NengaBooster.Infrastructures.RPA
{
    /// <summary>
    /// WindowControllerの生成と管理を行うプール。
    /// 同じ名前のWindowControllerがすでにある場合は再度生成しないようにする。
    /// </summary>
    sealed class WindowPool
    {
        private readonly Dictionary<TitleAndWidth, WindowController> _controllerBuffer = new Dictionary<TitleAndWidth, WindowController>();


        public WindowPool()
        {
        }

        public bool TryGetOrCreateWindowController(string windowTitlePattern, int maxWindowWidth, int waittime_ms, out WindowController _controller)
        {
            Assert.IsNullOrWhiteSpace(windowTitlePattern, nameof(windowTitlePattern));
            Assert.IsSmallerThan(maxWindowWidth, 1, nameof(maxWindowWidth));

            _controller = null;

            var key = new TitleAndWidth(windowTitlePattern, maxWindowWidth);

            var contains = _controllerBuffer.ContainsKey(key);
            if (contains)
            {
                var controller = _controllerBuffer[key];
                if (controller.Exists())
                {
                    _controller = controller;
                    return true;
                }
            }

            var controllers = WindowController
                .FindAll(key.Title, waittime_ms)
                .Where(c => c.GetSize().Width <= key.Width)
                .ToList();

            if (controllers.Count == 0)
            {
                return false;
            }

            var newController = controllers.OrderByDescending(c => c.GetSize().Width).First();
            if (contains)
                _controllerBuffer[key] = newController;
            else
                _controllerBuffer.Add(key, newController);

            _controller = newController;
            return true;
        }

        public WindowController GetOrCreateWindowController(string windowTitlePattern, int maxWindowWidth, int waittime_ms)
        {
            Assert.IsNullOrWhiteSpace(windowTitlePattern, nameof(windowTitlePattern));
            Assert.IsSmallerThan(maxWindowWidth, 1, nameof(maxWindowWidth));

            if (TryGetOrCreateWindowController(windowTitlePattern, maxWindowWidth, waittime_ms, out var controller))
                return controller;

            throw new NengaBoosterException($"{windowTitlePattern} のウィンドウが見つかりません。");
        }
    }
}
