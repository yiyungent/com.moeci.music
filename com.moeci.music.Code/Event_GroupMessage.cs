using Meting4Net.Core;
using Native.Sdk.Cqp.EventArgs;
using Native.Sdk.Cqp.Interface;
using Native.Sdk.Cqp.Model;
using Native.Tool.IniConfig;
using System.Collections;
using System.Linq;
using System.Text;

namespace com.moeci.music.Code
{
    public class Event_GroupMessage : IGroupMessage
    {
        /// <summary>
        /// 收到群消息
        /// </summary>
        /// <param name="sender">事件来源</param>
        /// <param name="e">事件参数</param>
        public void GroupMessage(object sender, CQGroupMessageEventArgs e)
        {
            // 获取 At 某人对象
            CQCode cqat = e.FromQQ.CQCode_At();

            string message = e.Message.Text;
            bool isSong = false;
            StringBuilder sbSendMsg = new StringBuilder();

            if (message.Contains("音乐搜索"))
            {
                isSong = true;

                Meting api = null;
                string songKw = string.Empty;
                if (message.Contains("网易云音乐搜索"))
                {
                    api = new Meting(ServerProvider.Netease);
                    songKw = message.Replace("网易云音乐搜索", "").Trim();
                }
                else if (message.Contains("QQ音乐搜索"))
                {
                    api = new Meting(ServerProvider.Tencent);
                    songKw = message.Replace("QQ音乐搜索", "").Trim();
                }
                else if (message.Contains("酷狗音乐搜索"))
                {
                    api = new Meting(ServerProvider.Kugou);
                    songKw = message.Replace("酷狗音乐搜索", "").Trim();
                }
                else if (message.Contains("虾米音乐搜索"))
                {
                    api = new Meting(ServerProvider.Xiami);
                    songKw = message.Replace("虾米音乐搜索", "").Trim();
                }
                else if (message.Contains("百度音乐搜索"))
                {
                    api = new Meting(ServerProvider.Baidu);
                    songKw = message.Replace("百度音乐搜索", "").Trim();
                }


                try
                {
                    var result = api.SearchObj(songKw);
                    if (result.Count() > 5)
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            var item = result[i];
                            sbSendMsg.Append($"{item.name} 歌曲ID: {item.id} 专辑: {item.album}\n");
                        }
                    }
                    else
                    {
                        foreach (var item in result)
                        {
                            sbSendMsg.Append($"{item.name} 歌曲ID: {item.id} 专辑: {item.album}\n");
                        }
                    }
                }
                catch (System.Exception ex)
                {
                }
            }

            if (message.Contains("音乐下载"))
            {
                isSong = true;
                Meting api = null;
                string songId = string.Empty;

                if (message.Contains("网易云音乐下载"))
                {
                    api = new Meting(ServerProvider.Netease);
                    songId = message.Replace("网易云音乐下载", "").Trim();
                }
                else if (message.Contains("QQ音乐下载"))
                {
                    api = new Meting(ServerProvider.Tencent);
                    songId = message.Replace("QQ音乐下载", "").Trim();
                }
                else if (message.Contains("酷狗音乐下载"))
                {
                    api = new Meting(ServerProvider.Kugou);
                    songId = message.Replace("酷狗音乐下载", "").Trim();
                }
                else if (message.Contains("虾米音乐下载"))
                {
                    api = new Meting(ServerProvider.Xiami);
                    songId = message.Replace("虾米音乐下载", "").Trim();
                }
                else if (message.Contains("百度音乐下载"))
                {
                    api = new Meting(ServerProvider.Baidu);
                    songId = message.Replace("百度音乐下载", "").Trim();
                }

                try
                {
                    var result = api.SongObj(songId);
                    string url = api.UrlObj(result.url_id).url;
                    sbSendMsg.Append($"{url}");

                }
                catch (System.Exception ex)
                {
                }
            }

            if (isSong)
            {
                e.FromGroup.SendGroupMessage(cqat, sbSendMsg.ToString());
            }

            // 设置该属性, 表示阻塞本条消息, 该属性会在方法结束后传递给酷Q
            e.Handler = true;
        }
    }
}
