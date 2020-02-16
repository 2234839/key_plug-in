using System;
using System.Collections.Generic;
using System.Threading;

namespace 按键插件 {
    class 指令处理 {
        static public bool run (String str) {
            string[] info = str.Split (':');
            string fun_name = info[0];
            /** 键盘类 */
            if (fun_name == "key_press") {

                byte keyName = byte.Parse (info[1]);
                KeyBoard.keyPress (keyName);
            } else if (fun_name == "key_down") {
                byte keyName = byte.Parse (info[1]);
                KeyBoard.keyDown (keyName);
            } else if (fun_name == "key_up") {
                byte keyName = byte.Parse (info[1]);
                KeyBoard.keyUp (keyName);
            } else if (fun_name == "mouse_left_click") { /** 鼠标类 */
                int dx = int.Parse (info[1]);
                int dy = int.Parse (info[2]);
                MouseFlag.MouseLeftClickEvent (dx, dy, 0);
            } else if (fun_name == "mouse_right_click") {
                int dx = int.Parse (info[1]);
                int dy = int.Parse (info[2]);
                MouseFlag.MouseRightClickEvent (dx, dy, 0);
            } else if (fun_name == "sleep") { /** sleep 睡眠 一会 */
                int dx = int.Parse (info[1]);
                Thread.Sleep (dx);
            } else {
                /**未知的指令 */
                Console.WriteLine ("未知的指令: {0}", str);
                return false;
            }
            return true;
        }
        static public List<bool> run (List<string> list) {
            List<bool> execution_status = new List<bool> ();
            for (int i = 0; i < list.Count; i++) {
                bool res = run (list[i]);
                execution_status.Add (res);
            }
            return execution_status;
        }
    }
}