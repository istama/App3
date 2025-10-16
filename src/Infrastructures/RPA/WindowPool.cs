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


        public WindowController GetOrCreateWindowController(string windowTitlePattern, int maxWindowWidth, int waittime_ms)
        {
            Assert.IsNullOrWhiteSpace(windowTitlePattern, nameof(windowTitlePattern));
            Assert.IsSmallerThan(maxWindowWidth, 1, nameof(maxWindowWidth));

            var key = new TitleAndWidth(windowTitlePattern, maxWindowWidth);

            var contains = _controllerBuffer.ContainsKey(key);
            if (contains)
            {
                var controller = _controllerBuffer[key];
                if (controller.Exists())
                    return controller;
            }

            var controllers = WindowController
                .FindAll(key.Title, waittime_ms)
                .Where(c => c.GetSize().Width <= key.Width)
                .ToList();

            if (controllers.Count == 0)
            {
                throw new NengaBoosterException($"{windowTitlePattern} のウィンドウが見つかりません。");
            }

            var newController = controllers.OrderByDescending(c => c.GetSize().Width).First();
            if (contains)
                _controllerBuffer[key] = newController;
            else
                _controllerBuffer.Add(key, newController);

            return newController;
        }
    }
}
