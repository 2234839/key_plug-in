using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;

/** 按键的代码来源 https://blog.csdn.net/fjsd155/article/details/79495242 */
namespace 按键插件 {
    class Program {
        static void Main (string[] args) {
            int port = 9093;
            Console.WriteLine ("run!!  端口为 {0}", port);

            HttpServer httpServer = new MyHttpServer (port);
            httpServer.listen ();
        }
    }
    public class MyHttpServer : HttpServer {
        public MyHttpServer (int port) : base (port) { }
        public override void handleGETRequest (HttpProcessor p) {
            Console.WriteLine ("request: {0}", p.http_url);
            string url = HttpUtility.UrlDecode (p.http_url);
            List<string> 指令 = new List<string> (url.Split ("|"));

            // 丢弃第一条指令 因为他是路径
            指令.RemoveAt (0);
            bool[] res = 指令处理.run (指令).ToArray ();
            Console.WriteLine (res);

            p.writeSuccess ();
            for (int i = 0; i < res.Length; i++) {
                p.outputStream.WriteLine (指令[i] + "\t->\t" + res[i]);
            }
        }

        public override void handlePOSTRequest (HttpProcessor p, StreamReader inputData) {
            Console.WriteLine ("POST request: {0}", p.http_url);
            string data = inputData.ReadToEnd ();
            p.outputStream.WriteLine ("<html><body><h1>test server</h1>");
            p.outputStream.WriteLine ("<a href=/test>return</a><p>");
            p.outputStream.WriteLine ("postbody: <pre>{0}</pre>", data);
        }
    }
}