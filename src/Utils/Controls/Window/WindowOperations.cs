using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IsTama.Utils
{
    /// <summary>
    /// 連続するウィンドウの操作を指示するクラス。
    /// </summary>
    sealed class WindowOperations
    {
        //private static readonly TaskScheduler _ui_task_scheduler = TaskScheduler.FromCurrentSynchronizationContext();
        private static readonly TaskScheduler _ui_task_scheduler = TaskScheduler.Default;

        /// <summary>
        /// ウィンドウタイトルと条件に一致するウィンドウをウェイト時間以内に取得する。
        /// 複数見つかった場合は一番最初に見つかったウィンドウを取得する。
        /// 見つからなかった場合は以降のウィンドウ操作は行われない。
        /// </summary>
        public static WindowOperations Of(string window_title_pattern, int waittime_ms, Func<WindowController, bool> condition=null)
        {
            return new WindowOperations(() =>
            {
                var controllers = WindowController.FindAll(window_title_pattern, waittime_ms, condition);

                if (controllers.Count == 0)
                    return (false, $"ウィンドウ（{window_title_pattern}）が見つかりません。", null);

                return (true, string.Empty, controllers[0]);
            });
        }

        /// <summary>
        /// ウィンドウタイトルと条件に一致するウィンドウをウェイト時間以内に取得する。
        /// 複数見つかった場合は例外を投げる。
        /// 見つからなかった場合は以降のウィンドウ操作は行われない。
        /// </summary>
        public static WindowOperations OfTheOnly(string window_title_pattern, int waittime_ms, Func<WindowController, bool> condition=null)
        {
            return new WindowOperations(() =>
            {
                var controllers = WindowController.FindAll(window_title_pattern, waittime_ms, condition);

                if (controllers.Count == 0)
                    return (false, "ウィンドウが見つかりません。", null);
                if (controllers.Count > 1)
                    return (false, "条件に当てはまるウィンドウが複数見つかりました。", null);

                return (true, string.Empty, controllers[0]);
            });
        }

        /// <summary>
        /// アクティブなウィンドウを取得する。
        /// </summary>
        public static WindowOperations OfActiveWindow()
        {
            return new WindowOperations(() =>
            {
                var controllers = WindowController.FindAll(".*", 0, w => w.IsActivated());

                if (controllers.Count == 0)
                    return (false, "ウィンドウが見つかりません。", null);
                if (controllers.Count > 1)
                    return (false, "条件に当てはまるウィンドウが複数見つかりました。", null);

                return (true, string.Empty, controllers[0]);
            });
        }

        /// <summary>
        /// ウィンドウを見つける処理やエラー処理は外部で行って、インスタンスを生成する。
        /// </summary>
        public static WindowOperations Of(WindowController controller)
        {
            return new WindowOperations(() =>
            {
                return (true, string.Empty, controller);
            });
        }

        /// <summary>
        /// ウィンドウの取得に失敗したWindowOperationsを返す。
        /// </summary>
        public readonly static WindowOperations Failed = new WindowOperations(() => (false, string.Empty, null));


        /// ウィンドウ操作を行うコールバック関数がfalseを返したとき、そのエラーメッセージがセットされる。
        public string ErrorMessage     { get; private set; }
        /// ウィンドウ操作を行うコールバック関数がfalseを返したとき、その命令の順番がセットされる。
        /// どの命令がfalseを返したかこの番号で分かる。
        public int  ErrorOrderNumber { get; private set; } = -1;

        private readonly Func<(bool, string, WindowController)> _f_find_window;

        private readonly List<(Func<WindowController, object, (bool, string)>, object)> _operation_and_arg_list =
            new List<(Func<WindowController, object, (bool, string)>, object)>();

        private readonly List<Func<WindowController, object, (bool, string)>> _func_to_throw_list =
            new List<Func<WindowController, object, (bool, string)>>();


        private WindowOperations(Func<(bool, string, WindowController)> f_find_window)
        {
            _f_find_window = f_find_window;
        }

        /// <summary>
        /// ウィンドウをコールバック関数に渡す。
        /// コールバック関数は正常に終了したときはtrueを返すようにする。
        /// コールバック関数がfalseを返すときはエラーメッセージも返すようにする。
        /// falseを返すと以降のウィンドウ操作は行われない。
        /// </summary>
        public WindowOperations Then(Func<WindowController, (bool, string)> f_operation)
            => Then((w, _) => f_operation(w), 0);
        public WindowOperations Then<TArg>(Func<WindowController, TArg, (bool, string)> f_operation, TArg arg)
        {
            (bool, string) op(WindowController w, object a) => f_operation(w, (TArg)a);
            _operation_and_arg_list.Add((op, arg));

            return this;
        }

        /// <summary>
        /// ウィンドウをコールバック関数に渡す。
        /// コールバック関数は正常に終了したときはtrueを返すようにする。
        /// falseを返すと以降のウィンドウ操作は行われない。
        /// </summary>
        public WindowOperations Then(Func<WindowController, bool> f_operation)
            => Then((w, _) => f_operation(w), 0);
        public WindowOperations Then<TArg>(Func<WindowController, TArg, bool> f_operation, TArg arg)
            => Then((w, a) =>
            {
                return (f_operation(w, a), string.Empty);
            }, arg);

        /// <summary>
        /// ウィンドウをコールバック関数に渡す。
        /// </summary>
        public WindowOperations Then(Action<WindowController> f_operation)
            => Then((w, _) => f_operation(w), 0);
        public WindowOperations Then<TArg>(Action<WindowController, TArg> f_operation, TArg arg)
            => Then((w, a) =>
            {
                f_operation(w, a);
                return (true, string.Empty);
            }, arg);

        /// <summary>
        /// 指定したインデックスのコントロールをコールバック関数に渡す。
        /// コールバック関数は正常に終了したときはtrueを返すようにする。
        /// コールバック関数がfalseを返すときはエラーメッセージも返すようにする。
        /// falseを返すと以降のウィンドウ操作は行われない。
        /// </summary>
        public WindowOperations Then(int control_index, Func<FormControl, (bool, string)> f_operation)
            => Then(control_index, (w, _) => f_operation(w), 0);
        public WindowOperations Then<TArg>(int control_index, Func<FormControl, TArg, (bool, string)> f_operation, TArg arg)
        {
            (bool, string) op(WindowController w, object a)
            {
                if (control_index < 0)
                    return (false, "コントロールインデックスが負の値です。");

                var control = w.GetControl(control_index);
                return f_operation(control, (TArg)a);
            }
            _operation_and_arg_list.Add((op, arg));

            return this;
        }

        /// <summary>
        /// 指定したインデックスのコントロールをコールバック関数に渡す。
        /// コールバック関数は正常に終了したときはtrueを返すようにする。
        /// falseを返すと以降のウィンドウ操作は行われない。
        /// </summary>
        public WindowOperations Then(int control_index, Func<FormControl, bool> f_operation)
            => Then(control_index, (w, _) => f_operation(w), 0);
        public WindowOperations Then<TArg>(int control_index, Func<FormControl, TArg, bool> f_operation, TArg arg)
            => Then(control_index, (w, a) =>
            {
                return (f_operation(w, a), string.Empty);
            }, arg);

        /// <summary>
        /// 指定したインデックスのコントロールをコールバック関数に渡す。
        /// </summary>
        public WindowOperations Then(int control_index, Action<FormControl> f_operation)
            => Then(control_index, (w, _) => f_operation(w), 0);
        public WindowOperations Then<TArg>(int control_index, Action<FormControl, TArg> f_operation, TArg arg)
            => Then(control_index, (w, a) =>
            {
                f_operation(w, a);
                return (true, string.Empty);
            }, arg);

        /// <summary>
        /// 指定したパターンに一致するテキストを持つコントロールをコールバック関数に渡す。
        /// コントロールが見つからない場合、もしくは複数見つかる場合は例外を投げる。
        /// コールバック関数は正常に終了したときはtrueを返すようにする。
        /// コールバック関数がfalseを返すときはエラーメッセージも返すようにする。
        /// falseを返すと以降のウィンドウ操作は行われない。
        /// </summary>
        public WindowOperations Then(string control_text_pattern, Func<FormControl, (bool, string)> f_operation, Func<FormControl, bool> condition = null)
            => Then(control_text_pattern, (w, _) => f_operation(w), 0, condition);
        public WindowOperations Then<TArg>(string control_text_pattern, Func<FormControl, TArg, (bool, string)> f_operation, TArg arg, Func<FormControl, bool> condition = null)
        {
            (bool, string) op(WindowController w, object a)
            {

                var controls = w.GetFormControls(control_text_pattern);
                if (condition != null)
                    controls = controls.Where(condition).ToList();
                if (controls.Count == 0)
                    return (false, $"{control_text_pattern} にマッチするコントロールが見つかりません。");
                if (controls.Count > 1)
                    return (false, $"{control_text_pattern} にマッチするコントロールが複数あります。");
                return f_operation(controls.First(), (TArg)a);
            }
            _operation_and_arg_list.Add((op, arg));

            return this;
        }

        /// <summary>
        /// 指定したパターンに一致するテキストを持つコントロールをコールバック関数に渡す。
        /// コントロールが見つからない場合、もしくは複数見つかる場合は例外を投げる。
        /// コールバック関数は正常に終了したときはtrueを返すようにする。
        /// falseを返すと以降のウィンドウ操作は行われない。
        /// </summary>
        public WindowOperations Order(string control_text_pattern, Func<FormControl, bool> f_operation, Func<FormControl, bool> condition = null)
            => Order(control_text_pattern, (w, _) => f_operation(w), 0, condition);
        public WindowOperations Order<TArg>(string control_text_pattern, Func<FormControl, TArg, bool> f_operation, TArg arg, Func<FormControl, bool> condition = null)
            => Then(control_text_pattern, (w, a) =>
            {
                return (f_operation(w, a), string.Empty);
            }, arg, condition);

        /// <summary>
        /// 指定したパターンに一致するテキストを持つコントロールをコールバック関数に渡す。
        /// コントロールが見つからない場合、もしくは複数見つかる場合は例外を投げる。
        /// </summary>
        public WindowOperations Then(string control_text_pattern, Action<FormControl> f_operation, Func<FormControl, bool> condition = null)
            => Then(control_text_pattern, (w, _) => f_operation(w), 0, condition);
        public WindowOperations Then<TArg>(string control_text_pattern, Action<FormControl, TArg> f_operation, TArg arg, Func<FormControl, bool> condition = null)
            => Then(control_text_pattern, (w, a) =>
            {
                f_operation(w, a);
                return (true, string.Empty);
            }, arg, condition);

        /// <summary>
        /// 指定した座標とサイズのコントロールをコールバック関数に渡す。
        /// 該当するコントロールが見つからないか複数見つかる場合は例外を投げる。
        /// コールバック関数は正常に終了したときはtrueを返すようにする。
        /// falseを返すと以降のウィンドウ操作は行われない。
        /// </summary>
        public WindowOperations Order(Point p, Func<FormControl, (bool, string)> f_operation)
            => Order(p, (sw, _) => f_operation(sw), 0);
        public WindowOperations Order<TArg>(Point p, Func<FormControl, TArg, (bool, string)> f_operation, TArg arg)
        {
            (bool, string) op(WindowController w, object a)
            {
                var controls = w.GetFormControls(p);
                if (controls.Count == 0)
                    return (false, $"x:{p.X} y:{p.Y} にマッチするコントロールが見つかりません。");
                if (controls.Count > 1)
                    return (false, $"x:{p.X} y:{p.Y} にマッチするコントロールが複数あります。");
                return f_operation(controls.First(), (TArg)arg);
            }
            _operation_and_arg_list.Add((op, arg));

            return this;
        }

        public WindowOperations Then(Point p, Func<FormControl, bool> f_operation)
            => Then(p, (sw, _) => f_operation(sw), 0);
        public WindowOperations Then<TArg>(Point p, Func<FormControl, TArg, bool> f_operation, TArg arg)
            => Order(p, (sw, a) =>
            {
                return (f_operation(sw, a), string.Empty);
            }, arg);


        public WindowOperations Then(Point p, Action<FormControl> f_operation)
            => Then(p, (sw, _) => f_operation(sw), 0);
        public WindowOperations Then<TArg>(Point p, Action<FormControl, TArg> f_operation, TArg arg)
            => Order(p, (sw, a) =>
            {
                f_operation(sw, a);
                return (true, string.Empty);
            }, arg);

        /// <summary>
        /// 指定した座標とサイズのコントロールをコールバック関数に渡す。
        /// 該当するコントロールが見つからないか複数見つかる場合は例外を投げる。
        /// コールバック関数は正常に終了したときはtrueを返すようにする。
        /// falseを返すと以降のウィンドウ操作は行われない。
        /// </summary>
        public WindowOperations Then(Rectangle r, Func<FormControl, (bool, string)> f_operation)
            => Then(r, (sw, _) => f_operation(sw), 0);
        public WindowOperations Then<TArg>(Rectangle r, Func<FormControl, TArg, (bool, string)> f_operation, TArg arg)
        {
            (bool, string) op(WindowController w, object a)
            {
                var controls = w.GetControls(r);
                if (controls.Count == 0)
                    return (false, $"x:{r.X} y:{r.Y} w:{r.Width} h:{r.Height} にマッチするコントロールが見つかりません。");
                if (controls.Count > 1)
                    return (false, $"x:{r.X} y:{r.Y} w:{r.Width} h:{r.Height} にマッチするコントロールが複数あります。");
                return f_operation(controls.First(), (TArg)arg);
            }
            _operation_and_arg_list.Add((op, arg));

            return this;
        }

        public WindowOperations Order(Rectangle r, Func<FormControl, bool> f_operation)
            => Order(r, (sw, _) => f_operation(sw), 0);
        public WindowOperations Order<TArg>(Rectangle r, Func<FormControl, TArg, bool> f_operation, TArg arg)
            => Then(r, (sw, a) =>
            {
                return (f_operation(sw, a), string.Empty);
            }, arg);


        public WindowOperations Then(Rectangle r, Action<FormControl> f_operation)
            => Then(r, (sw, _) => f_operation(sw), 0);
        public WindowOperations Then<TArg>(Rectangle r, Action<FormControl, TArg> f_operation, TArg arg)
            => Then(r, (sw, a) =>
            {
                f_operation(sw, a);
                return (true, string.Empty);
            }, arg);

        /// <summary>
        /// 指定時間待機する。
        /// </summary>
        public WindowOperations Wait(int waittime_ms)
        {
            (bool, string) op(WindowController w, object a)
            {
                var waittime = (int)a;
                Thread.Sleep(waittime);

                return (true, string.Empty);
            }
            _operation_and_arg_list.Add((op, waittime_ms));

            return this;
        }

        /// <summary>
        /// 途中で処理を失敗したなら例外として投げる。
        /// </summary>
        public WindowOperations ThrowIfFailed()
        {
            (bool, string) op(WindowController w, object a)
            {
                var error_msg = (string)a;
                if (!string.IsNullOrEmpty(error_msg))
                {
                    var nl = Environment.NewLine;
                    throw new MyException<WindowOperationsExceptionArgs>($"ウィンドウの操作に失敗しました。{nl}{nl}原因：{error_msg}");
                }
                return (true, string.Empty);
            }
            _operation_and_arg_list.Add((op, null));
            _func_to_throw_list.Add(op);

            return this;
        }

        /// <summary>
        /// ウィンドウ操作を非同期に実行する。
        /// </summary>
        public async Task<bool> DoAsync()
        {
            return await Task.Factory.StartNew<bool>((arg) =>
            {
                var myself = (WindowOperations)arg;
                return myself.Do();
            },
            this,
            CancellationToken.None,
            TaskCreationOptions.AttachedToParent,
            _ui_task_scheduler);
        }

        /// <summary>
        /// ウィンドウ操作を実行する。
        /// </summary>
        public bool Do()
        {
            ErrorMessage = string.Empty;
            ErrorOrderNumber = -1;

            var failed = false;
            var order_count = 0;


            // ウィンドウを見つける
            var (result, error_msg, window) = _f_find_window();
            if (!result)
            {
                ErrorOrderNumber = order_count;
                // エラーメッセージが空の場合は例外を投げないのでここで処理を終える
                if (string.IsNullOrEmpty(error_msg))
                    return false;
                ErrorMessage = error_msg;
                failed = true;
            }

            // 見つけたウィンドウに対して各種操作を行う
            foreach (var (op, arg) in _operation_and_arg_list)
            {
                // 例外を投げる関数かどうか
                var is_func_to_throw = _func_to_throw_list.Contains(op);

                // ウィンドウの操作に失敗した場合
                if (failed)
                {
                    if (is_func_to_throw)
                        op(null, ErrorMessage);
                    else
                        continue;
                }

                // 例外を投げる関数はスキップする
                if (is_func_to_throw)
                    continue;

                order_count += 1;

                (result, error_msg) = op(window, arg);
                if (!result)
                {
                    ErrorOrderNumber = order_count;
                    // エラーメッセージが空の場合は例外を投げないのでここで処理を終える
                    if (string.IsNullOrEmpty(error_msg))
                        break;
                    ErrorMessage = error_msg;
                    failed = true;
                }
            }

            return result;
        }
    }
}
