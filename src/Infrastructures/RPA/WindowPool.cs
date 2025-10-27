using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
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
        private readonly ConcurrentDictionary<TitleAndWidth, WindowController> _controllerBuffer = new ConcurrentDictionary<TitleAndWidth, WindowController>();


        public WindowPool()
        {
        }


        public bool TryGetOrCreateWindowController(string windowTitlePattern, int maxWindowWidth, int waittime_ms, out WindowController controller)
        {
            Assert.IsNullOrWhiteSpace(windowTitlePattern, nameof(windowTitlePattern));
            Assert.IsSmallerThan(maxWindowWidth, 1, nameof(maxWindowWidth));

            controller = null;

            var key = new TitleAndWidth(windowTitlePattern, maxWindowWidth);

            // バッファに同じ条件のWindowControllerが保存されており、今でも存在するウィンドウなら
            if (_controllerBuffer.ContainsKey(key))
            {
                var buffer = _controllerBuffer[key];
                if (buffer.Exists())
                {
                    controller = buffer;
                    return true;
                }
            }

            // バッファに同じ条件のウィンドウが存在しないなら新たに探す

            var controllers = WindowController
                .FindAll(key.Title, waittime_ms)
                .Where(c => c.GetSize().Width <= key.Width)
                .ToList();

            if (controllers.Count == 0)
            {
                return false;
            }

            var newController = controllers.OrderByDescending(c => c.GetSize().Width).First();
            // 値の追加または更新をスレッドセーフに行う
            controller = _controllerBuffer.AddOrUpdate(key, (_) => newController, (_, __) => newController);

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
